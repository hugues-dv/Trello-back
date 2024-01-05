using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Trello_back.Models;

namespace Trello_back.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class ListController : Controller
    {
        private readonly TrelloContext _context;

        public ListController(TrelloContext context)
        {
            _context = context;
        }

        // GET: List
        [HttpGet]
        public IActionResult GetLists(int? projectId)
        {
            IQueryable<List> lists = _context.Lists;
            if (projectId != null)
            {
                lists = lists.Where(m => m.IdProject == projectId);
            }

            return Ok(lists);
        }

        // GET: List/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetListById(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var list = await _context.Lists
                .FirstOrDefaultAsync(m => m.Id == id);
            if (list == null)
            {
                return NotFound();
            }

            return Ok(list);
        }

        // GET: List/Create
        [HttpPost]
        // [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateList([Bind("id,name,idProject")] List list)
        {
            if (ModelState.IsValid)
            {
                _context.Add(list);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetListById), new { id = list.Id }, list);
            }
            return BadRequest(ModelState);
        }

        // POST: List/Update/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPut("{id}")]
        // [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateList(int id, [Bind("id,name,idProject")] List list)
        {
            if (id != list.Id)
            {
                return NotFound();
            }

            var existingList = await _context.Lists.FindAsync(id);
            if (existingList == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Entry(existingList).CurrentValues.SetValues(list);
                    await _context.SaveChangesAsync();
                    return NoContent();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ListExists(list.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return BadRequest(ModelState);
        }

        // POST: List/Delete/5
        [HttpDelete("{id}")]
        // [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteList(int id)
        {
            var list = await _context.Lists.FindAsync(id);
            if (list == null)
            {
                return NotFound();
            }

            _context.Lists.Remove(list);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool ListExists(int id)
        {
            return _context.Lists.Any(e => e.Id == id);
        }
    }
}
