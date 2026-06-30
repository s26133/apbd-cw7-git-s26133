using MiniHelpdesk.Models;

namespace MiniHelpdesk.Repositories;

public interface ITicketRepository
{
    Task<List<Ticket>> GetAllAsync();
    Task<Ticket> GetByIdAsync(int id);
    Task AddTicketWithCommentAsync(Ticket ticket, TicketComment comment);
    Task UpdateAsync(Ticket ticket);
}