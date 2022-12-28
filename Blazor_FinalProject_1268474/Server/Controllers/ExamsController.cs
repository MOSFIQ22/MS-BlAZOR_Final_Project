using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Blazor_FinalProject_1268474.Shared.Models;
using Blazor_FinalProject_1268474.Shared.DTO;

namespace Blazor_FinalProject_1268474.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamsController : ControllerBase
    {
        private readonly CourseDbContext _context;

        public ExamsController(CourseDbContext context)
        {
            _context = context;
        }

        // GET: api/Exams
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Exam>>> GetExams()
        {
          if (_context.Exams == null)
          {
              return NotFound();
          }
            return await _context.Exams.ToListAsync();
        }
        [HttpGet("DTO")]
        public async Task<ActionResult<IEnumerable<ExamViewDTO>>> GetOrderDTOs()
        {
            if (_context.Exams == null)
            {
                return NotFound();
            }
            return await _context.Exams
                
                .Include(o => o.ExamResults).ThenInclude(oi => oi.Trainee)
                .Select(o =>
                    new ExamViewDTO
                    {
                        ExamID = o.ExamID,
                        ExamName = o.ExamName,
                        ExamFee = o.ExamFee,
                        
                        
                    })
                .ToListAsync();
        }
        // GET: api/Exams/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Exam>> GetOrder(int id)
        {
            if (_context.Exams == null)
            {
                return NotFound();
            }
            var exam = await _context.Exams.FindAsync(id);

            if (exam == null)
            {
                return NotFound();
            }

            return exam;
        }
        [HttpGet("DTO/{id}")]
        public async Task<ActionResult<ExamViewDTO>> GetOrderViewDTO(int id)
        {
            if (_context.Exams == null)
            {
                return NotFound();
            }
            var exam = await _context.Exams
                .Include(o => o.ExamResults).ThenInclude(oi => oi.Trainee).FirstOrDefaultAsync(o => o.ExamID == id);

            if (exam == null)
            {
                return NotFound();
            }

            return new ExamViewDTO
            {
                ExamID = exam.ExamID,
                
                ExamName = exam.ExamName,
                ExamFee = exam.ExamFee


            };
        }
        [HttpGet("Items/{id}")]
        public async Task<ActionResult<IEnumerable<ExamResultViewDTO>>> GetOrderItems(int id)
        {
            if (_context.ExamResults == null)
            {
                return NotFound();
            }
            var examresult = await _context.ExamResults.Include(x => x.Trainee).Where(oi => oi.ExamID == id).ToListAsync();

            if (examresult == null)
            {
                return NotFound();
            }

            return examresult.Select(oi => new ExamResultViewDTO { ExamID = oi.ExamID, TraineeName = oi.Trainee.TraineeName, Result=oi.Result }).ToList();
        }
        [HttpGet("OI/{id}")]
        public async Task<ActionResult<IEnumerable<ExamResult>>> GetOrderItemsOf(int id)
        {
            if (_context.ExamResults == null)
            {
                return NotFound();
            }
            var examresult = await _context.ExamResults.Where(oi => oi.ExamID == id).ToListAsync();

            if (examresult == null)
            {
                return NotFound();
            }

            return examresult;
        }
        // PUT: api/Orders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int id, Exam exam)
        {
            if (id != exam.ExamID)
            {
                return BadRequest();
            }

            _context.Entry(exam).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
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
        [HttpPut("DTO/{id}")]
        public async Task<IActionResult> PutOrderWithOrderItem(int id, ExamEditDTO exam)
        {
            if (id != exam.ExamID)
            {
                return BadRequest();
            }
            var existing = await _context.Exams.Include(x => x.ExamResults).FirstAsync(o => o.ExamID == id);
            _context.ExamResults.RemoveRange(existing.ExamResults);
            existing.ExamID = exam.ExamID;
            existing.ExamName = exam.ExamName;
            existing.ExamFee = exam.ExamFee;
            foreach (var item in exam.ExamResults)
            {
                _context.ExamResults.Add(new ExamResult { ExamID = exam.ExamID, TraineeID = item.TraineeID, Result = item.Result });
            }


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.InnerException?.Message);

            }

            return NoContent();
        }
        // POST: api/Orders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Exam>> PostOrder(Exam exam)
        {
            if (_context.Exams == null)
            {
                return Problem("Entity set 'CourseDbContext.Exams'  is null.");
            }
            _context.Exams.Add(exam);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrder", new { id = exam.ExamID }, exam);
        }

        [HttpPost("DTO")]
        public async Task<ActionResult<Exam>> PostOrderDTO(ExamDTO dto)
        {
            if (_context.Exams == null)
            {
                return Problem("Entity set 'CourseDbContext.Exams'  is null.");
            }
            var exam = new Exam { ExamName = dto.ExamName, ExamFee = dto.ExamFee};
            foreach (var oi in dto.ExamResults)
            {
                exam.ExamResults.Add(new ExamResult { TraineeID = oi.TraineeID, Result = oi.Result });
            }
            _context.Exams.Add(exam);
            await _context.SaveChangesAsync();

            return exam;
        }
        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            if (_context.Exams == null)
            {
                return NotFound();
            }
            var exam = await _context.Exams.FindAsync(id);
            if (exam == null)
            {
                return NotFound();
            }

            _context.Exams.Remove(exam);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderExists(int id)
        {
            return (_context.Exams?.Any(e => e.ExamID == id)).GetValueOrDefault();
        }
    }
}
