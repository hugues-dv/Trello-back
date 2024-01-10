using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Trello_back.Models;


namespace Trello_back.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class CardController : Controller
    {
        private readonly TrelloContext _context;

        public CardController(TrelloContext context)
        {
            _context = context;
        }

        // GET: Card
        [HttpGet]
        public IActionResult GetCards(int? IdList)
        {
            IQueryable<Card> cards = _context.Cards;
            if (IdList != null)
            {
                cards = cards.Where(m => m.IdList == IdList);
            }
            return Ok(cards);
        }

        // GET: Card/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCardById(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var card = await _context.Cards
                .FirstOrDefaultAsync(m => m.Id == id);
            if (card == null)
            {
                return NotFound();
            }

            return Ok(card);
        }
        // GET Card?listId=5
        // [HttpGet]
        // public async Task<ActionResult<IEnumerable<Card>>> GetCardsByListId(int? listId)
        // {
        //     if (listId == null)
        //     {
        //         return BadRequest();
        //     }

        //     var cards = await _context.Cards.Where(c => c.IdList == listId).ToListAsync();

        //     if (cards == null)
        //     {
        //         return NotFound();
        //     }

        //     return cards;
        // }

        // POST: Card/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        // [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCard([Bind("id,title,description,createdAt,idList")] Card card)
        {
            if (ModelState.IsValid)
            {
                _context.Add(card);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetCardById), new { id = card.Id }, card);
            }
            return BadRequest(ModelState);
        }

        // POST: Card/Update/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPut("{id}")]
        // [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateCard(int id, [Bind("id,title,description,createdAt,idList")] Card card)
        {
            if (id != card.Id)
            {
                return NotFound();
            }

            var existingCard = await _context.Cards.FindAsync(id);
            if (existingCard == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Entry(existingCard).CurrentValues.SetValues(card);
                    await _context.SaveChangesAsync();
                    return NoContent();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CardExists(card.Id))
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

        // POST: Card/Delete/5
        [HttpDelete("{id}")]
        // [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCard(int id)
        {
            var card = await _context.Cards.FindAsync(id);
            if (card == null)
            {
                return NotFound();
            }

            _context.Cards.Remove(card);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        private bool CardExists(int id)
        {
            return _context.Cards.Any(e => e.Id == id);
        }
    }
}
