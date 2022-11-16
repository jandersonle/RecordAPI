using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecordAPI.Models;

namespace RecordAPI.Controllers
{
    [Route("api/RecordItemsController")]
    [ApiController]
    public class RecordItemsController : ControllerBase
    {
        private readonly RecordContext _context;

        public RecordItemsController(RecordContext context)
        {
            _context = context;
        }

        // GET: api/RecordItemsController/
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RecordItem>>> GetRecordItems()
        {
          if (_context.RecordItems == null)
          {
              return NotFound();
          }
            return await _context.RecordItems.ToListAsync();
        }

        // GET: api/RecordItemsController/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RecordItem>> GetRecordItem(long id)
        {
          if (_context.RecordItems == null)
          {
              return NotFound();
          }
            var recordItem = await _context.RecordItems.FindAsync(id);

            if (recordItem == null)
            {
                return NotFound();
            }

            return recordItem;
        }
        // GET api/RecordItemsController/example.json
        [HttpGet("example.json")]
        public RecordItem GetExample()
        {
            var rec = new RecordItem();
            rec.id = 1;
            rec.recordDate = DateTime.Now;
            rec.recordReviewed = false;
            rec.recordPublished = false;

            return rec;
        }

        // PUT: api/RecordItemsController/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRecordItem(long id, RecordItem recordItem)
        {
            if (id != recordItem.id)
            {
                return BadRequest();
            }

            _context.Entry(recordItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecordItemExists(id))
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

        // POST: api/RecordItemsController/
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RecordItem>> PostRecordItem(RecordItem recordItem)
        {
          if (_context.RecordItems == null)
          {
              return Problem("Entity set 'RecordContext.RecordItems'  is null.");
          }
            _context.RecordItems.Add(recordItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRecordItem), new { id = recordItem.id }, recordItem);
        }

        // DELETE: api/RecordItemsController/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecordItem(long id)
        {
            if (_context.RecordItems == null)
            {
                return NotFound();
            }
            var recordItem = await _context.RecordItems.FindAsync(id);
            if (recordItem == null)
            {
                return NotFound();
            }

            _context.RecordItems.Remove(recordItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RecordItemExists(long id)
        {
            return (_context.RecordItems?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
