using ApiApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace ApiApp.Controllers
{
    [Route("api/notes")]
    public class NotesController : Controller
    {
        private readonly ApplicationContext _context;
        public NotesController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Index()
        {
            try
            {
                ClaimsIdentity identity = User.Identity as ClaimsIdentity;
                Claim[] claims = identity.Claims.ToArray();
                List<Note> notes = _context.Notes.Where(x => x.Owner.Id == Guid.Parse(claims[1].Value)).ToList();
                List<object> result = new List<object>();
                foreach (Note note in notes)
                {
                    result.Add(new
                    {
                        note.Id,
                        note.Name,
                        note.Text,
                        owner = User.Identity.Name
                    });
                }
                return Ok(new
                {
                    result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [Route("create")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateNote(string name,string text)
        {
            ClaimsIdentity identity = User.Identity as ClaimsIdentity;
            Claim[] claims = identity.Claims.ToArray();
            Guid userId = Guid.Parse(claims[1].Value);
            User owner = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);
            Guid noteId = Guid.NewGuid();
            Note note = new Note() {
                Id = noteId,
                Name = name,
                Text = text,
                Owner = owner
            };

            try
            {
                await _context.Notes.AddAsync(note);
                await _context.SaveChangesAsync();
                return Ok(noteId);
            }
            catch(Exception ex)
            {
                return StatusCode(500);
            }
            
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ShowNote(Guid id)
        {
            Note note = await _context.Notes.FirstOrDefaultAsync(x => x.Id == id);
            if (note == null)
                return NotFound(id);
            return Ok(new
            {
                note.Id,
                note.Name,
                note.Text,
                owner = User.Identity.Name
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNote(Guid id)
        {
            Note note = await _context.Notes.FirstOrDefaultAsync(x => x.Id == id);
            if (note == null)
                return NotFound();
            _context.Notes.Remove(note);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNote(Guid id, string name, string text)
        {
            ClaimsIdentity identity = User.Identity as ClaimsIdentity;
            Claim[] claims = identity.Claims.ToArray();
            Guid userId = Guid.Parse(claims[1].Value);
            User owner = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);
            Note note = new Note()
            {
                Id = id,
                Name = name,
                Text = text,
                Owner = owner
            };
            if (!await _context.Notes.AnyAsync(x => x.Id == id))
                return NotFound();
            _context.Notes.Update(note);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
