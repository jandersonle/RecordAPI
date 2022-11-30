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
using RecordAPI.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RecordAPI.Controllers
{
    [Route("api")]
    [ApiController]
    public class RecordItemsController : ControllerBase
    {
        private readonly RecordContext _context;

        private readonly string iconnStr = "";

        public RecordItemsController(RecordContext context)
        {
            _context = context;
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
            ExternalDbRunner runner = new(iconnStr);
            var res = runner.getExposure(id);
            return res;

            // OLD APPROACH
            //DataTable dt = new DataTable();
            //var connStr = iconnStr;
            //var sqlQuery = @"select 
            //               * from Exposure 
            //                where EXPO_ID_NB = " + id + ";";
            //var rows_returned = 0;

            //using (SqlConnection connection = new SqlConnection(connStr))
            //using (SqlCommand cmd = connection.CreateCommand())
            //using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
            //{
            //    cmd.CommandText = sqlQuery;
            //    cmd.CommandType = CommandType.Text;
            //    connection.Open();
            //    rows_returned = sda.Fill(dt);
            //    connection.Close();
            //}

            //if (dt.Rows.Count == 1)
            //{
            //    return dt.ToJson();
            //}

            //return NotFound();


        }

        // GET api/calls/id
        [HttpGet("calls/{id}")]
        public async Task<ActionResult<object>> GetCall(long id)
        {
            // LATEST APPROACH
            ExternalDbRunner runner = new(iconnStr);
            var res = runner.getCall(id);
            return res;


            // ALTERNATIVE 1
            //var sqlQuery = @"select *
            //                from Call 
            //                where CALL_ID_NB = " + id + ";";
            //DataTable dt = new();
            //var connStr = iconnStr;
            //var rows_returned = 0;

            //using (SqlConnection connection = new(connStr))
            //using (SqlCommand cmd = connection.CreateCommand())
            //using (SqlDataAdapter sda = new(cmd))
            //{
            //    cmd.CommandText = sqlQuery;
            //    cmd.CommandType = CommandType.Text;
            //    connection.Open();
            //    rows_returned = sda.Fill(dt);
            //    connection.Close();
            //}

            //if (dt.Rows.Count == 1)
            //{
            //    return dt.ToJson();
            //}

            //return NotFound();


            // ALTERNATIVE 2
            //SqlConnection conn = new SqlConnection(iconnStr);
            //var dbCmd = new SqlCommand();
            //using(dbCmd)
            //{
            //    dbCmd.Connection = conn;
            //    dbCmd.CommandText = sqlQuery;
            //    conn.Open();

            //    using (var reader = dbCmd.ExecuteReader())
            //    {
            //        if(reader.HasRows)
            //        {
            //            return reader.ToJson();
            //        }
            //        else
            //        {
            //            return NotFound();
            //        }
            //    }
            //}
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
