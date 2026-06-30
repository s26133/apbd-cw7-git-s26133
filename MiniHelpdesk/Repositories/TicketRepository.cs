using Microsoft.EntityFrameworkCore;
using MiniHelpdesk.Data;
using MiniHelpdesk.Models;

namespace MiniHelpdesk.Repositories;

public class TicketRepository : ITicketRepository
{
    private readonly AppDbContext _context;

    public TicketRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<List<Ticket>> GetAllAsync() => _context.Tickets.ToListAsync();

    public Task<Ticket> GetByIdAsync(int id) =>
        _context.Tickets.Include(t => t.Comments).FirstOrDefaultAsync(t => t.Id == id);

    public async Task AddTicketWithCommentAsync(Ticket ticket, TicketComment comment)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync();

            comment.TicketId = ticket.Id;
            _context.TicketComments.Add(comment);
            await _context.SaveChangesAsync();

            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task UpdateAsync(Ticket ticket)
    {
        _context.Tickets.Update(ticket);
        await _context.SaveChangesAsync();
    }
}