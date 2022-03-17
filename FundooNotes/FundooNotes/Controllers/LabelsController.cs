using BusinessLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundooNotes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabelsController : ControllerBase
    {
        ILabelsBL labelsBL;
        public LabelsController(ILabelsBL labelsBL)
        {
            this.labelsBL = labelsBL;
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
    }
}
