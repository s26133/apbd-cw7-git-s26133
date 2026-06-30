using Microsoft.AspNetCore.Mvc;
using MiniHelpdesk.Services;
using MiniHelpdesk.ViewModels;

namespace MiniHelpdesk.Controllers;

public class TicketsController : Controller
{
	private readonly ITicketService _ticketService;

	public TicketsController(ITicketService ticketService)
	{
		_ticketService = ticketService;
	}

	public async Task<IActionResult> Index()
	{
		var tickets = await _ticketService.GetAllTicketsAsync();
		return View(tickets);
	}

	public IActionResult Create()
	{
		return View();
	}

	[HttpPost]
	public async Task<IActionResult> Create(CreateTicketViewModel model)
	{
		if (!ModelState.IsValid)
		{
			return View(model);
		}

		await _ticketService.CreateTicketAsync(model);
		return RedirectToAction(nameof(Index));
	}

	public async Task<IActionResult> Details(int id)
	{
		var ticket = await _ticketService.GetTicketDetailsAsync(id);
		if (ticket == null) return NotFound();
		return View(ticket);
	}

	[HttpPost]
	public async Task<IActionResult> Close(int id)
	{
		await _ticketService.CloseTicketAsync(id);
		return RedirectToAction(nameof(Details), new { id });
	}
}