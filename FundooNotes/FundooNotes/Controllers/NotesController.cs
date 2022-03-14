using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundooNotes.Controllers
{
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
        [HttpPost("createNote")]
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
        [HttpPut("updateNote")]
        public IActionResult UpdateNote(NotesModel notesModel, long noteId)
        {
            try
            {
                var notes = this.notesBL.UpdateNote(notesModel, noteId);
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
    }
}
