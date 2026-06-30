using MiniHelpdesk.Services;
using MiniHelpdesk.ViewModels;

namespace MiniHelpdesk.Tests;

public class TicketServiceTests
{
	[Fact]
	public async Task CreateTicket_WithValidData_CreatesTicketAndComment()
	{
		var repo = new FakeTicketRepository();
		var service = new TicketService(repo);
		var model = new CreateTicketViewModel { Title = "T1", CommentAuthor = "A", CommentContent = "C" };

		await service.CreateTicketAsync(model);

		Assert.Single(repo.Tickets);
		Assert.Single(repo.Comments);
	}

	[Fact]
	public async Task CreateTicket_EmptyTitle_ThrowsArgumentException()
	{
		var repo = new FakeTicketRepository();
		var service = new TicketService(repo);
		var model = new CreateTicketViewModel { Title = "", CommentAuthor = "A", CommentContent = "C" };

		await Assert.ThrowsAsync<ArgumentException>(() => service.CreateTicketAsync(model));
		Assert.Empty(repo.Tickets);
	}

	[Fact]
	public async Task CloseTicket_ChangesStatusToClosed()
	{
		var repo = new FakeTicketRepository();
		var service = new TicketService(repo);
		await service.CreateTicketAsync(new CreateTicketViewModel { Title = "T", CommentContent = "C", CommentAuthor = "A" });
		var ticketId = repo.Tickets.First().Id;

		await service.CloseTicketAsync(ticketId);

		var ticket = await service.GetTicketDetailsAsync(ticketId);
		Assert.Equal("Closed", ticket.Status);
	}
}