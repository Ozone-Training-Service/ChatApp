using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ChatApp.Models;
using ChatApp2.Models;
using ChatApp.Hubs;

namespace ChatApp2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly MessageHub messageHub;
        public MessagesController(AppDbContext context, MessageHub messageHub)
        {
            _context = context;
            this.messageHub = messageHub;
        }

        // GET: api/Messages
        [HttpGet]
       public async Task<ActionResult<IEnumerable<Message>>> GetMessage(Message msg)
        {
            var x=this.messageHub.NewMessage(msg);
            return await _context.Message.ToListAsync();
        }

        // GET: api/Messages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Message>> GetMessage(string id)
        {
            var message = await _context.Message.FindAsync(id);

            if (message == null)
            {
                return NotFound();
            }

            return message;
        }

        // PUT: api/Messages/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMessage(string id, Message message)
        {
            if (id != message.clientuniqueid)
            {
                return BadRequest();
            }

            _context.Entry(message).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MessageExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Messages
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Message>> PostMessage(Message message)
        {
            _context.Message.Add(message);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (MessageExists(message.clientuniqueid))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetMessage", new { id = message.clientuniqueid }, message);
        }

        // DELETE: api/Messages/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMessage(string id)
        {
            var message = await _context.Message.FindAsync(id);
            if (message == null)
            {
                return NotFound();
            }

            _context.Message.Remove(message);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MessageExists(string id)
        {
            return _context.Message.Any(e => e.clientuniqueid == id);
        }
    }
}
