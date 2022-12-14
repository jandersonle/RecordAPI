using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NuGet.Protocol;
using RecordAPI.ExternalDBService;
using Microsoft.Extensions.Configuration;
using RecordAPI.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RecordAPI.Controllers
{
    [Route("api")]
    [ApiController]
    public class RecordItemsController : ControllerBase
    {
        private readonly RecordContext _context;

        private readonly ExternalDbRunner _runner;

        private readonly string iconnStr = "data Source = 10.1.0.5; Encrypt = yes; TrustServerCertificate = True; Initial Catalog = UFPoison; Max Pool Size = 200; App = ToxSentry NPDS AU-Dev; MultipleActiveResultSets = true;User ID = ToxSentryApp; Password=4gKAhcEJbsZ5CN&x";

        public RecordItemsController(RecordContext context)
        {
            _context = context;
            _runner = new(iconnStr);
        }


        // GET: api/
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RecordItem>>> GetRecordItems()
        {
          if (_context.RecordItems == null)
          {
              return NotFound();
          }
            return await _context.RecordItems.ToListAsync();
        }


        // GET: api/5
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


        // GET api/exposures/id
        [HttpGet("exposures/{id}")]
        public async Task<ActionResult<object>> GetExposure(long id)
        {
            // LATEST APPROACH
            var res = _runner.GetExposure(id);
            return res;
        }


        // GET api/calls/id
        [HttpGet("calls/{id}")]
        public async Task<ActionResult<object>> GetCall(long id)
        {
            // LATEST APPROACH
            var res = _runner.GetCall(id);
            return res;
        }


        // PUT: api/5
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

        // POST: api/
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

        // DELETE: api/5
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
