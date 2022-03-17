using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundooNotes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollaboratorController : ControllerBase
    {
        private readonly ICollabBL collabBL;

        public CollaboratorController(ICollabBL collabBL)
        {
            this.collabBL = collabBL;
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

     
    }
}
