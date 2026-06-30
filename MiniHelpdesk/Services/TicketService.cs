using MiniHelpdesk.Models;
using MiniHelpdesk.Repositories;
using MiniHelpdesk.ViewModels;

namespace MiniHelpdesk.Services;

public class TicketService : ITicketService
{
    private readonly ITicketRepository _repository;

    public TicketService(ITicketRepository repository)
    {
        _repository = repository;
    }

    public Task<List<Ticket>> GetAllTicketsAsync() => _repository.GetAllAsync();

    public Task<Ticket> GetTicketDetailsAsync(int id) => _repository.GetByIdAsync(id);

    public async Task CreateTicketAsync(CreateTicketViewModel model)
    {
        if (string.IsNullOrWhiteSpace(model.Title) || string.IsNullOrWhiteSpace(model.CommentContent))
        {
            throw new ArgumentException("Title and comment content cannot be empty.");
        }

        var ticket = new Ticket
        {
            Title = model.Title,
            Description = model.Description
        };

        var comment = new TicketComment
        {
            Author = model.CommentAuthor,
            Content = model.CommentContent
        };

        await _repository.AddTicketWithCommentAsync(ticket, comment);
    }

    public async Task CloseTicketAsync(int id)
    {
        var ticket = await _repository.GetByIdAsync(id);
        if (ticket != null)
        {
            ticket.Status = "Closed";
            await _repository.UpdateAsync(ticket);
        }
    }
}