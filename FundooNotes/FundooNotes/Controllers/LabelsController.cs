// <copyright file="LabelsController.cs" company="mahafuz">
//     Company copyright tag.
// </copyright>
namespace FundooNotes.Controllers
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using BusinessLayer.Interface;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Distributed;
    using Microsoft.Extensions.Caching.Memory;
    using Newtonsoft.Json;
    using RepositoryLayer.Entity;

    /// <summary>
    /// labels controller
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    public class LabelsController : ControllerBase
    {
        /// <summary>
        /// The labels business layer object reference
        /// </summary>
        private readonly ILabelsBL labelsBL;

        /// <summary>
        /// The memory cache
        /// </summary>
        private readonly IMemoryCache memoryCache;

        /// <summary>
        /// The distributed cache
        /// </summary>
        private readonly IDistributedCache distributedCache;

        /// <summary>
        /// Initializes a new instance of the <see cref="LabelsController"/> class.
        /// </summary>
        /// <param name="labelsBL">The labels business layer.</param>
        /// <param name="memoryCache">The memory cache.</param>
        /// <param name="distributedCache">The distributed cache.</param>
        public LabelsController(ILabelsBL labelsBL, IMemoryCache memoryCache, IDistributedCache distributedCache)
        {
            this.labelsBL = labelsBL;
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
        }

        /// <summary>
        /// Creates the label.
        /// </summary>
        /// <param name="labelName">Name of the label.</param>
        /// <param name="noteID">The note identifier.</param>
        /// <returns>a created label</returns>
        [Authorize]
        [HttpPost("create")]
        public IActionResult CreateLabel(string labelName, long noteID)
        {
            try
            {
                long userID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var result = this.labelsBL.CreateLabel(userID, noteID, labelName);
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

        /// <summary>
        /// Renames the label.
        /// </summary>
        /// <param name="lableName">Name of the label.</param>
        /// <param name="newLabelName">New name of the label.</param>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>an updated label</returns>
        [Authorize]
        [HttpPut("rename")]
        public IActionResult RenameLabel(string lableName, string newLabelName, long noteId)
        {
            try
            {
                long userID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var result = this.labelsBL.RenameLabel(userID, lableName, newLabelName, noteId);
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

        /// <summary>
        /// Removes the label.
        /// </summary>
        /// <param name="lableName">Name of the label.</param>
        /// <returns>removed label</returns>
        [Authorize]
        [HttpDelete("remove")]
        public IActionResult RemoveLabel(string lableName)
        {
            try
            {
                long userID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                if (this.labelsBL.RemoveLabel(userID, lableName))
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

        /// <summary>
        /// Removes the label by note identifier.
        /// </summary>
        /// <param name="noteID">The note identifier.</param>
        /// <param name="lableName">Name of the label.</param>
        /// <returns>removed label by note id</returns>
        [Authorize]
        [HttpDelete("removeBynoteId")]
        public IActionResult RemoveLabelByNoteID(long noteID, string lableName)
        {
            try
            {
                long userID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                if (this.labelsBL.RemoveLabelByNoteID(userID, noteID, lableName))
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

        /// <summary>
        /// Gets the labels by note identifier.
        /// </summary>
        /// <param name="noteID">The note identifier.</param>
        /// <returns>a list of labels by note id</returns>
        [Authorize]
        [HttpGet("get")]
        public IActionResult GetLabelsByNoteID(long noteID)
        {
            try
            {
                long userID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var result = this.labelsBL.GetLabelsByNoteID(userID, noteID);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Label removed successfully", Response = result });
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

        /// <summary>
        /// Gets all labels.
        /// </summary>
        /// <returns>list of labels from database</returns>
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
            var redisLabelList = await this.distributedCache.GetAsync(cacheKey);
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
                await this.distributedCache.SetAsync(cacheKey, redisLabelList, options);
            }

            return this.Ok(labelsList);
        }
    }
}
