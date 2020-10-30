using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tasker.Models;
using TaskManager.Models;

namespace Tasker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorksController : ControllerBase
    {
        private readonly TaskerDbContext _context;

        public WorksController(TaskerDbContext context)
        {
            _context = context;
        }

        // GET: api/Works
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Work>>> GetWorks()
        {
            return await _context.Works.ToListAsync();
        }


        //GET:api/Works/Page
        [HttpGet("Page/{page}")]
        public async Task<ActionResult<WorksModel>> GetPage(int page = 1)
        {
            int pageSize = 2;
            List<Work> works = await _context.Works.AsNoTracking()
                                     .AsQueryable()
                                     .Include(c => c.OrderWorks).ThenInclude(t => t.User)
                                     .Include(c => c.ExecWorks).ThenInclude(t => t.User)
                                     .ToListAsync();

            IEnumerable<Work> worksPerPages = works.Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = works.Count };
            WorksModel ivm = new WorksModel { PageInfo = pageInfo, Works = worksPerPages };

            return ivm;

        }


        //GET:api/Works/Order/1/Page/
        [HttpGet("Order/{id}/Page/")]
        public async Task<ActionResult<WorksModel>> GetOrderPage(int id, int page = 1)
        {
            int pageSize = 2;

            var works = await _context.Works.Where(c => c.Id == id).Include(p => p.OrderWorks).ThenInclude(t => t.Order).ToListAsync();
            //  return CreatedAtAction("GetPage", new { works = inputworks }, page);

            var worksPerPages = works.Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = works.Count };
            var ivm = new WorksModel { PageInfo = pageInfo, Works = worksPerPages };

            return ivm;

        }

        //GET:api/Works/Page
        [HttpGet("Execute/{id}/Page/")]
        public async Task<ActionResult<WorksModel>> GetExecPage(int id, int page = 1)
        {
            int pageSize = 2;

            var works = await _context.Works.Where(c => c.Id == id).Include(p => p.ExecWorks).ThenInclude(t => t.Execute).ToListAsync();
            //  return CreatedAtAction("GetPage", new { works = inputworks }, page);

            var worksPerPages = works.Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = works.Count };
            var ivm = new WorksModel { PageInfo = pageInfo, Works = worksPerPages };

            return ivm;

        }








        // GET: api/Works/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Work>> GetWork(int id)
        {
            var work = await _context.Works.AsNoTracking()
                                       .AsQueryable()
                                       .Include(c => c.OrderWorks).ThenInclude(t => t.User)
                                       .Include(c => c.ExecWorks).ThenInclude(t => t.User)
                                       .FirstAsync(p => p.Id == id);

            if (work == null)
            {
                return NotFound();
            }

            return work;
        }

        // PUT: api/Works/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWork(int id, Work work)
        {
            if (id != work.Id)
            {
                return BadRequest();
            }

            _context.Entry(work).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkExists(id))
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


        // PUT: api/Works/5/Status/4
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}/Status/{statusId}")]
        public async Task<IActionResult> PutWork(int id, int statusId)
        {
            Work work = _context.Works.FirstOrDefault(p => p.Id == id);
            if (id != work.Id)
            {
                return BadRequest();
            }


            work.Status = (StatusTask)statusId;

            _context.Entry(work).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkExists(id))
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



        // PUT: api/Works/5/Order/4
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}/Order/{idUser}")]
        public async Task<IActionResult> ChangeCutomer(int id, int idUser)
        {
            Work work = _context.Works.FirstOrDefault(p => p.Id == id);
            User user = _context.Users.FirstOrDefault(c => c.Id == idUser);

            if (id != work.Id || idUser != user.Id)
            {
                return BadRequest();
            }

            work.OrderWorks.Add(new UserOrder { UserId = user.Id, OrderId = work.Id });

            _context.Entry(work).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkExists(id))
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



        // POST: api/Works
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Work>> PostWork(Work work)
        {
            _context.Works.Add(work);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWork", new { id = work.Id }, work);
        }

        // DELETE: api/Works/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Work>> DeleteWork(int id)
        {
            var work = await _context.Works.FindAsync(id);
            if (work == null)
            {
                return NotFound();
            }

            _context.Works.Remove(work);
            await _context.SaveChangesAsync();

            return work;
        }

        private bool WorkExists(int id)
        {
            return _context.Works.Any(e => e.Id == id);
        }
    }
}
