using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundooNotes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollaboratorController : ControllerBase
    {
        private readonly ICollabBL collabBL;
        private readonly IMemoryCache memoryCache;
        private readonly IDistributedCache distributedCache;

        public CollaboratorController(ICollabBL collabBL, IMemoryCache memoryCache, IDistributedCache distributedCache)
        {
            this.collabBL = collabBL;
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
        }

        [Authorize]
        [HttpPost("Add")]
        public IActionResult AddCollab(string email, long noteId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                CollaboratorModel collaboratorModel = new CollaboratorModel();
                collaboratorModel.Id = userId;
                collaboratorModel.NoteID = noteId;
                collaboratorModel.CollabEmail = email;
                var result = collabBL.AddCollaborator(collaboratorModel);
                if (result != null)
                {
                    return this.Ok(new { Success = true, message = "Collaborator added successfully", data = result });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Failed to add collaborator" });
                }


            }
            catch (Exception)
            {
                throw;
            }
        }

        [Authorize]
        [HttpDelete("Remove")]
        public IActionResult RemoveCollab(long collabId)
        {
            try
            {
                // Take id of  Logged In User
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var result = this.collabBL.RemoveCollab(userId, collabId);
                if (result != null)
                {
                    return this.Ok(new { Success = true, message = " Collab Removed  successfully ", data = result });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Collab Remove Failed ! Try Again" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }

        [Authorize]
        [HttpGet("{noteId}/Get")]
        public IEnumerable<CollaboratorEntity> GetByNoteId(long noteId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var result = this.collabBL.GetByNoteId(noteId, userId);
                if (result != null)
                {
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("GetAll")]
        public IEnumerable<CollaboratorEntity> GetAllCollab()
        {
            try
            {
                var result = this.collabBL.GetAllCollab();
                if (result != null)
                {
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("redis")]
        public async Task<IActionResult> GetAllCollabUsingRedisCache()
        {
            var cacheKey = "collabList";
            string serializedcollabList;
            var collabList = new List<CollaboratorEntity>();
            var redisCollabList = await this.distributedCache.GetAsync(cacheKey);
            if (redisCollabList != null)
            {
                serializedcollabList = Encoding.UTF8.GetString(redisCollabList);
                collabList = JsonConvert.DeserializeObject<List<CollaboratorEntity>>(serializedcollabList);
            }
            else
            {
                collabList = (List<CollaboratorEntity>)this.collabBL.GetAllCollab();
                serializedcollabList = JsonConvert.SerializeObject(collabList);
                redisCollabList = Encoding.UTF8.GetBytes(serializedcollabList);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(15))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await this.distributedCache.SetAsync(cacheKey, redisCollabList, options);
            }

            return this.Ok(collabList);
        }

    }
}
