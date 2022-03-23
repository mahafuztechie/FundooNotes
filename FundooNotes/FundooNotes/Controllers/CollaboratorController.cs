// <copyright file="CollaboratorController.cs" company="mahafuz">
//     Company copyright tag.
// </copyright>
namespace FundooNotes.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using BusinessLayer.Interface;
    using CommonLayer.Model;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Distributed;
    using Microsoft.Extensions.Caching.Memory;
    using Newtonsoft.Json;
    using RepositoryLayer.Entity;

    /// <summary>
    ///   Collaborator controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CollaboratorController : ControllerBase
    {
        /// <summary>
        /// The collaborator object reference
        /// </summary>
        private readonly ICollabBL collabBL;

        /// <summary>
        /// The memory cache object reference
        /// </summary>
        private readonly IMemoryCache memoryCache;

        /// <summary>
        /// The distributed cache object reference
        /// </summary>
        private readonly IDistributedCache distributedCache;

        /// <summary>Initializes a new instance of the <see cref="CollaboratorController" /> class.</summary>
        /// <param name="collabBL">The collaborator business layer.</param>
        /// <param name="memoryCache">The memory cache.</param>
        /// <param name="distributedCache">The distributed cache.</param>
        public CollaboratorController(ICollabBL collabBL, IMemoryCache memoryCache, IDistributedCache distributedCache)
        {
            this.collabBL = collabBL;
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
        }

        /// <summary>Adds the collaborator.</summary>
        /// <param name="email">The email.</param>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>
        ///   a collaborator entity
        /// </returns>
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
                var result = this.collabBL.AddCollaborator(collaboratorModel);
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

        /// <summary>Removes the collaborator.</summary>
        /// <param name="collabId">The collaborator identifier.</param>
        /// <returns>
        ///   removed collaborator entity
        /// </returns>
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

        /// <summary>
        /// Gets the by note identifier.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>collaborator entity by note id</returns>
        [Authorize]
        [HttpGet("{noteId}/Get")]
        public IActionResult GetByNoteId(long noteId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var result = this.collabBL.GetByNoteId(noteId, userId);
                if (result != null)
                {
                    return this.Ok(new { Success = true, message = " get collab is successfull ", data = result });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "couldn't  get collab, Failed ! Try Again" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Gets all collaborator.
        /// </summary>
        /// <returns>all collaborators</returns>
        [HttpGet("GetAll")]
        public IActionResult GetAllCollab()
        {
            try
            {
                var result = this.collabBL.GetAllCollab();
                if (result != null)
                {
                    return this.Ok(new { Success = true, message = "Collaborators fetched successfully ", data = result });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "couldn't get collabs, Failed ! Try Again" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Gets all collaborator using (redis-cache).
        /// </summary>
        /// <returns>all collaborators by (redis-cache)</returns>
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
