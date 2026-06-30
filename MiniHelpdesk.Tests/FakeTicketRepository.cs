using MiniHelpdesk.Models;
using MiniHelpdesk.Repositories;

namespace MiniHelpdesk.Tests;

public class FakeTicketRepository : ITicketRepository
{
    public List<Ticket> Tickets = new();
    public List<TicketComment> Comments = new();
    private int _ticketIdCounter = 1;

    public Task<List<Ticket>> GetAllAsync() => Task.FromResult(Tickets);

    public Task<Ticket> GetByIdAsync(int id)
    {
        var ticket = Tickets.FirstOrDefault(t => t.Id == id);
        if (ticket != null)
        {
            ticket.Comments = Comments.Where(c => c.TicketId == id).ToList();
        }
        return Task.FromResult(ticket);
    }

    public Task AddTicketWithCommentAsync(Ticket ticket, TicketComment comment)
    {
        ticket.Id = _ticketIdCounter++;
        comment.TicketId = ticket.Id;

        Tickets.Add(ticket);
        Comments.Add(comment);

        return Task.CompletedTask;
    }

    public Task UpdateAsync(Ticket ticket)
    {
        var index = Tickets.FindIndex(t => t.Id == ticket.Id);
        if (index >= 0) Tickets[index] = ticket;
        return Task.CompletedTask;
    }
}