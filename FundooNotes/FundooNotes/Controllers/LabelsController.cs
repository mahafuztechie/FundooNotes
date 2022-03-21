using BusinessLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using RepositoryLayer.Entity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundooNotes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabelsController : ControllerBase
    {
        ILabelsBL labelsBL;
        private readonly IMemoryCache memoryCache;
        private readonly IDistributedCache distributedCache;
        public LabelsController(ILabelsBL labelsBL, IMemoryCache memoryCache, IDistributedCache distributedCache)
        {
            this.labelsBL = labelsBL;
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
        }
        [Authorize]
        [HttpPost("create")]
        public IActionResult CreateLabel(string labelName, long noteID)
        {
            try
            {
                long userID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var result = labelsBL.CreateLabel(userID, noteID, labelName);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Label added successfully", Response = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Label already created" });
                }
            }
            catch (Exception)
            {

                throw;
            }

        }
        [Authorize]
        [HttpPut("rename")]
        public IActionResult RenameLabel(string lableName, string newLabelName)
        {
            try
            {
                long userID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var result = labelsBL.RenameLabel(userID, lableName, newLabelName);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Label renamed successfully", Response = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "User access denied" });
                }
            }
            catch (Exception)
            {

                throw;
            }

        }
        [Authorize]
        [HttpDelete("remove")]
        public IActionResult RemoveLabel(string lableName)
        {
            try
            {
                long userID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                if (labelsBL.RemoveLabel(userID, lableName))
                {
                    return this.Ok(new { success = true, message = "Label removed successfully" });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "User access denied" });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        [Authorize]
        [HttpDelete("removeBynoteId")]
        public IActionResult RemoveLabelByNoteID(long noteID, string lableName)
        {
            try
            {
                long userID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                if (labelsBL.RemoveLabelByNoteID(userID, noteID, lableName))
                {
                    return this.Ok(new { success = true, message = "Label removed successfully" });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "User access denied" });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        [Authorize]
        [HttpGet("get")]
        public IEnumerable GetLabelsByNoteID(long noteID)
        {
            try
            {
                long userID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                return labelsBL.GetLabelsByNoteID(userID, noteID);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet("GetAll")]
        public IEnumerable<LabelsEntity> GetAllLabels()
        {
            try
            {
                var result = this.labelsBL.GetAllLabels();
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

        /// <summary>
        /// Gets all labels using cache.
        /// </summary>
        /// <returns> Label details from cache </returns>
        [HttpGet("redis")]
        public async Task<IActionResult> GetAllLabesUsingRedisCache()
        {
            var cacheKey = "labelList";
            string serializedLabelList;
            var labelsList = new List<LabelsEntity>();
            var redisLabelList = await distributedCache.GetAsync(cacheKey);
            if (redisLabelList != null)
            {
                serializedLabelList = Encoding.UTF8.GetString(redisLabelList);
                labelsList = JsonConvert.DeserializeObject<List<LabelsEntity>>(serializedLabelList);
            }
            else
            {
                labelsList = (List<LabelsEntity>)this.labelsBL.GetAllLabels();
                serializedLabelList = JsonConvert.SerializeObject(labelsList);
                redisLabelList = Encoding.UTF8.GetBytes(serializedLabelList);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(15))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await distributedCache.SetAsync(cacheKey, redisLabelList, options);
            }

            return this.Ok(labelsList);
        }
    }
}
