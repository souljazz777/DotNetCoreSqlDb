using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DotNetCoreSqlDb.Models;
using DotNetCoreSqlDb.Data;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DotNetCoreSqlDb.Controllers
{
    public class TodosController : Controller
    {
        private readonly MyDatabaseContext _context;

        public TodosController(MyDatabaseContext context)
        {
            _context = context;
        }
        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            var todos = new List<Todo>();
            try
            {
                todos = await _context.Todo.ToListAsync();
            }
            catch (Exception e)
            {
                return View(todos);
            }
            return View(todos);
        }
        // Get : Todos/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var todo = await _context.Todo
                .FirstOrDefaultAsync(m => m.Id == id);
            if (todo == null)
            {
                return NotFound();
            }

            return View(todo);
        }
        // Get: Todos/Create
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Description,CreateDate")] Todo todo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(todo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        // Get: Todos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var todo = await _context.Todo.FindAsync(id);
            if (todo == null)
            {
                return NotFound();
            }
            return View(todo);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Description,CreateDate")] Todo todo)
        {
            if (id != todo.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(todo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TodoExists(todo.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(todo);
        }
        //Get: Todos/Delete/{id}
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var todo = await _context.Todo.FirstOrDefaultAsync(m => m.Id == id);
            if (todo == null)
            {
                return NotFound();
            }
            return View(todo);
        }
        // Post : Todos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var todo = await _context.Todo.FindAsync(id);
            _context.Todo.Remove(todo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool TodoExists(int id)
        {
            return _context.Todo.Any(e => e.Id == id);
        }
    }
}

