using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Blazor_FinalProject_1268474.Shared.Models;

namespace Blazor_FinalProject_1268474.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamResultsController : ControllerBase
    {
        private readonly CourseDbContext _context;

        public ExamResultsController(CourseDbContext context)
        {
            _context = context;
        }

        // GET: api/ExamResults
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExamResult>>> GetExamResults()
        {
          if (_context.ExamResults == null)
          {
              return NotFound();
          }
            return await _context.ExamResults.ToListAsync();
        }

        // GET: api/ExamResults/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ExamResult>> GetExamResult(int id)
        {
          if (_context.ExamResults == null)
          {
              return NotFound();
          }
            var examResult = await _context.ExamResults.FindAsync(id);

            if (examResult == null)
            {
                return NotFound();
            }

            return examResult;
        }

        // PUT: api/ExamResults/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExamResult(int id, ExamResult examResult)
        {
            if (id != examResult.ExamID)
            {
                return BadRequest();
            }

            _context.Entry(examResult).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExamResultExists(id))
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

        // POST: api/ExamResults
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ExamResult>> PostExamResult(ExamResult examResult)
        {
          if (_context.ExamResults == null)
          {
              return Problem("Entity set 'CourseDbContext.ExamResults'  is null.");
          }
            _context.ExamResults.Add(examResult);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ExamResultExists(examResult.ExamID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetExamResult", new { id = examResult.ExamID }, examResult);
        }

        // DELETE: api/ExamResults/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExamResult(int id)
        {
            if (_context.ExamResults == null)
            {
                return NotFound();
            }
            var examResult = await _context.ExamResults.FindAsync(id);
            if (examResult == null)
            {
                return NotFound();
            }

            _context.ExamResults.Remove(examResult);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ExamResultExists(int id)
        {
            return (_context.ExamResults?.Any(e => e.ExamID == id)).GetValueOrDefault();
        }
    }
}
