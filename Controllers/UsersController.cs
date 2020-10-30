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
    public class UsersController : ControllerBase
    {
        private readonly TaskerDbContext _context;

        public UsersController(TaskerDbContext context)
        {
            _context = context;
        }



        //GET:api/Users/Page
        [HttpGet("Page/{page}")]
        public async Task<ActionResult<UsersModel>> GetPageUser(int page = 1)
        {
            int pageSize = 2;
            List<User> users = await _context.Users.AsNoTracking()
                                       .AsQueryable()
                                       .Include(c => c.UserOrders).ThenInclude(t => t.Order)
                                       .Include(c => c.UserExecutes).ThenInclude(t => t.Execute)
                                       .ToListAsync();

            IEnumerable<User> usersPerPages = users.Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = users.Count };
            UsersModel ivm = new UsersModel { PageInfo = pageInfo, Users = usersPerPages };

            return ivm;
        }




        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users
                                       .AsNoTracking()
                                       .AsQueryable()
                                       .Include(c => c.UserOrders).ThenInclude(t => t.Order)
                                       .Include(c => c.UserExecutes).ThenInclude(t => t.Execute)
                                       .ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.AsNoTracking()
                                       .AsQueryable()
                                       .Include(c => c.UserOrders).ThenInclude(t => t.Order)
                                       .Include(c => c.UserExecutes).ThenInclude(t => t.Execute)
                                       .FirstAsync(p => p.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // PUT: api/Users/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}/Task/{taskId}")]
        public async Task<IActionResult> PutUserTask(int id, int taskId = 1)
        {
            User user = _context.Users.FirstOrDefault(p => p.Id == id);
            Work work = _context.Works.FirstOrDefault(p => p.Id == taskId);
            if (user == null || work == null)
            {
                return BadRequest();
            }


            user.UserExecutes.Add(new UserExec { UserId = user.Id, Execute = work });

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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





        // POST: api/Users
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return user;
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
