﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TheEventsApp.Models;

namespace TheEventsApp.Controllers
{
    public class EventsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EventsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Events
        public async Task<IActionResult> Index()
        {
            return View(await _context.Events.Include("Organizer").ToListAsync());
        }

        // GET: Events/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Events
                .Include(e => e.Participants)
                .Include(e => e.Organizer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@event == null)
            {
                return NotFound();
            }





            return View(@event);
        }

        // GET: Events/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Events/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,StartDate,EndDate,MaxParticipants")] Event @event)
        {
            
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await _context.Users.FindAsync(userId);
            
            if (user == null)
            {
                return BadRequest();
            }

            @event.Organizer = user;
            ModelState.Remove("Participants");
            ModelState.Remove("Organizer");


            if (ModelState.IsValid)
            {
                _context.Add(@event);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(@event);
        }

        // GET: Events/Edit/5
        [Authorize()]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (User.Identity == null)
            {
                return BadRequest();
            }
            var loggedUser = await _context.Users.FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);

            var @event = await _context.Events.FindAsync(id);
            if (@event == null)
            {
                return NotFound();
            }

            if (@event.Organizer != loggedUser)
            {
                return BadRequest();
            }

            return View(@event);
        }

        // POST: Events/Edit/5
        [Authorize()]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,StartDate,EndDate,MaxParticipants")] Event @event)
        {
   
            ModelState.Remove("Participants");
            ModelState.Remove("Organizer");

      
            if (User.Identity == null)
            {
                return BadRequest();
            }
            var loggedUser = await _context.Users.FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);

            var selectedEvent = await _context.Events.Include(e => e.Organizer).FirstOrDefaultAsync(e => e.Id == id);

            if (selectedEvent == null || selectedEvent.Organizer != loggedUser)
            {
                return BadRequest();
            }
            _context.Entry(selectedEvent).CurrentValues.SetValues(@event);


            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(selectedEvent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventExists(@event.Id))
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
            return View(@event);
        }

        // POST: Events/JoinEvent/5
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> JoinEvent(int id)
        {
           
            var selectedEvent = _context.Events.
                Include(e => e.Participants).
                FirstOrDefault(e => e.Id == id);

            if (selectedEvent == null)
            {
                return NotFound();
            }
        
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (selectedEvent.Participants == null)
            {
                selectedEvent.Participants = new List<ApplicationUser>();
            }

            

            if (selectedEvent.Participants.Count >= selectedEvent.MaxParticipants)
            {
                TempData["Message"] = "The event is full!";
                return RedirectToAction("Details", new { id });
            }
            selectedEvent.Participants.Add(_context.Users.FirstOrDefault(u => u.Id == userId));

            await _context.SaveChangesAsync();

            TempData["Message"] = "You have successfully joined the event!";
            return RedirectToAction("Details", new { id });
        }


        // POST: Events/LeaveEvent/5
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> LeaveEvent(int id)
        {
           
            var selectedEvent = _context.Events.
                Include(e => e.Participants).
                FirstOrDefault(e => e.Id == id);

            if (selectedEvent == null)
            {
                return NotFound();
            }

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (selectedEvent.Participants == null)
            {
                return RedirectToAction("Details", new { id });
            }

            if (selectedEvent.Participants.Count == 0)
            {
                return RedirectToAction("Details", new { id });
            }

            var user = await _context.Users.FindAsync(userId);

            if (user != null && selectedEvent.Participants.Contains(user))
            {
                selectedEvent.Participants.Remove(_context.Users.First(u => u.Id == userId));
            }

     
            await _context.SaveChangesAsync();

            TempData["Message"] = "You have successfully left the event!";
            return RedirectToAction("Details", new { id });
        }



        // POST: Events/RemoveParticipant
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> RemoveParticipant(int eventId, string userId)
        {
            
            var selectedEvent = _context.Events
                .Include(e => e.Participants)
                .FirstOrDefault(e => e.Id == eventId);
            var participant = _context.Users.FirstOrDefault(u => u.Id == userId);

            if (User.Identity == null)
            {
                return BadRequest();
            }
            var loggedUser = await _context.Users.FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);

            if (selectedEvent != null && participant != null && selectedEvent.Organizer == loggedUser)
            {
                selectedEvent.Participants.Remove(participant);
                await _context.SaveChangesAsync();
            }


            return RedirectToAction("Details", new { id = eventId });
        }




        // GET: Events/Delete/5
        [Authorize()]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Events
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize()]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var @event = await _context.Events.FindAsync(id);
       
            if (User.Identity == null)
            {
                return BadRequest();
            }
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);

            // user has role admin
         

            if (@event != null && (@event.Organizer == user || User.IsInRole("Admin")))
            {
                _context.Events.Remove(@event);
            } else
            {
                return BadRequest();
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventExists(int id)
        {
            return _context.Events.Any(e => e.Id == id);
        }
    }



        

}
