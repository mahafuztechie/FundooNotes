using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundooNotes.Controllers
{   [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly INotesBL notesBL;
        public NotesController(INotesBL notesBL)
        {
            this.notesBL = notesBL;

        }

        [Authorize]
        [HttpPost("create")]
        public IActionResult CreateNote(NotesModel notesModel)
        {
            try
            {
                //Id of login user
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "Id").Value);
                var result = notesBL.CreateNote(notesModel, userId);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "note created Successfully", data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "failed to create note" });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        [Authorize]
        [HttpPut("update")]
        public IActionResult UpdateNote(UpdatNoteModel notesModel, long noteId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var notes = this.notesBL.UpdateNote(notesModel, noteId, userId);
                if (notes != null)
                {
                    return this.Ok(new { Success = true, message = " Note Updated successfully ", data = notes });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "failed to update note" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [Authorize]
        [HttpDelete("Delete")]
        public IActionResult DeleteNote(long noteId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var notes = this.notesBL.DeleteNote(noteId, userId);
                if (!notes)
                {
                    return this.BadRequest(new { Success = false, message = "failed to Delete note" });  
                }
                else
                {
                    return this.Ok(new { Success = true, message = " Note is Deleted successfully ", data = notes });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        [Authorize]
        [HttpGet("Getnotes")]
        public IActionResult GetNotesByUserId()
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
               
                var notes = this.notesBL.GetNotesByUserId(userId);
                if (notes != null)
                {
                    return this.Ok(new { Success = true, message = "Notes are displayed", data = notes });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "failed to Display the notes" });
                }

            }
            catch (Exception)
            {
                throw;
            }
        }
        [Authorize]
        [HttpGet("{noteId}/GetNote")]
        public IActionResult GetNote(long noteId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var result = this.notesBL.getNote(noteId, userId);
                if (result != null)
                {
                    return this.Ok(new { Success = true, message = "Notes are displayed", data = result });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "failed! note maybe removed or deleted"});
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut("IsArchiveOrNot")]
        public IActionResult IsArchieveOrNot(long noteId)
        {
            try
            {
                // Take id of  Logged In User
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var result = this.notesBL.IsArchieveOrNot(noteId, userId);
                if (result != null)
                {
                    return this.Ok(new { Success = true, message = "  Is Archive Or Not Archive ", data = result });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Unsuccessful" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        [HttpPut("IsTrashOrNot")]
        public IActionResult IsTrashOrNot(long noteId)
        {
            try
            {
                // Take id of  Logged In User
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var result = this.notesBL.IsTrashOrNot(noteId, userId);
                if (result != null)
                {
                    return this.Ok(new { Success = true, message = "  Is Trash Or Not Trash ", data = result });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Unsuccessful" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut("IsPinnedOrNot")]
        public IActionResult IsPinnedOrNot(long noteId)
        {
            try
            {
                // Take id of  Logged In User
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var result = this.notesBL.IsPinnedOrNot(noteId, userId);
                if (result != null)
                {
                    return this.Ok(new { Success = true, message = "  Is Pinned Or Not Pinned ", data = result });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Unsuccessful" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [Authorize]
        [HttpPost("ImageUpload")]
        public IActionResult UploadImage(long noteId, IFormFile image)
        {
            try
            {
                // Take id of  Logged In User
                var userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var result = this.notesBL.UploadImage(noteId, userId, image);
                if (result != null)
                {
                    return this.Ok(new { Success = true, message = "Image Uploaded Successfully", data = result });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Image Upload Failed ! Try Again " });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut("colour")]
        public IActionResult UpdateColour(long noteId, string color)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var notes = this.notesBL.ChangeColour(noteId, userId, color);
                if (notes != null)
                {
                    return this.Ok(new { Success = true, message = " colour Added successfully ", data = notes });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "failed to add colour" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
