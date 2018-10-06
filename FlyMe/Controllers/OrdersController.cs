﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FlyMe.Data;
using FlyMe.Models;

namespace FlyMe.Controllers
{
    public class OrdersController : Controller
    {
        private readonly FlyMeContext _context;

        public OrdersController(FlyMeContext context)
        {
            _context = context;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            return View(await _context.Ticket.Include(ticket => ticket.Flight)
                                                .ThenInclude(Flight => Flight.Airplane)
                                             .Include(ticket => ticket.Flight)
                                                .ThenInclude(Flight => Flight.SourceAirport)
                                             .Include(ticket => ticket.Flight)
                                                .ThenInclude(Flight => Flight.DestAirport)
                                                 .Where(ticket => ticket.Buyer == null)
                                                .OrderBy(ticket => ticket.Flight.Date)
                                                .ToListAsync());
        }

        // GET: Orders/Buy/*id*
        public async Task<IActionResult> Buy(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Ticket.FirstOrDefaultAsync(m => m.Id == id);

            if (ticket == null)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool TicketExists(int id)
        {
            return _context.Ticket.Any(e => e.Id == id);
        }

        [HttpGet]
        public async Task<IActionResult> Search(DateTime? date, string from, string to, int maxPrice)
        {
            if (date.HasValue)
                ViewBag.date = date.Value.ToString("yyyy-MM-dd");

            ViewBag.from = from;
            ViewBag.to = to;

            if (maxPrice > 0)
                ViewBag.maxPrice = maxPrice;

            var dayAfterDate = date?.AddDays(1);

            return View("Index",
                await _context.Ticket.Include(ticket => ticket.Flight)
                                                .ThenInclude(Flight => Flight.Airplane)
                                             .Include(ticket => ticket.Flight)
                                                .ThenInclude(Flight => Flight.SourceAirport)
                                             .Include(ticket => ticket.Flight)
                                                .ThenInclude(Flight => Flight.DestAirport)
                    .Where(ticket =>
                    (!date.HasValue || !dayAfterDate.HasValue || (date <= ticket.Flight.Date && ticket.Flight.Date < dayAfterDate)) &&
                    (string.IsNullOrEmpty(from) || ticket.Flight.SourceAirport.Country.ToLower().Contains(from.ToLower()) || ticket.Flight.SourceAirport.City.ToLower().Contains(from.ToLower())) &&
                    (string.IsNullOrEmpty(to) || ticket.Flight.DestAirport.Country.ToLower().Contains(to.ToLower()) || ticket.Flight.DestAirport.City.ToLower().Contains(to.ToLower())) &&
                    ((maxPrice <= 0) || (maxPrice >= ticket.Price)) &&
                    (ticket.Buyer == null))
                    .OrderBy(ticket => ticket.Flight.Date)
                    .ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> MostSoldFlightsView()
        {
            return View(await _context.Ticket.Include(ticket => ticket.Flight)
                                                .ThenInclude(Flight => Flight.Airplane)
                                             .Include(ticket => ticket.Flight)
                                                .ThenInclude(Flight => Flight.SourceAirport)
                                             .Include(ticket => ticket.Flight)
                                                .ThenInclude(Flight => Flight.DestAirport)
                    .Where(ticket => (ticket.Buyer != null))
                    .GroupBy(ticket => ticket.Flight)
                    .Select(o => new SoldFlightsView
                    {
                        Flight = o.Key,
                        TicketsSold = o.Count()
                    })
                    .OrderByDescending(o => o.TicketsSold)
                    .ToListAsync());
        }
    }
}
