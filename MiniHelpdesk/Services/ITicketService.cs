using MiniHelpdesk.Models;
using MiniHelpdesk.ViewModels;

namespace MiniHelpdesk.Services;

public interface ITicketService
{
    Task<List<Ticket>> GetAllTicketsAsync();
    Task<Ticket> GetTicketDetailsAsync(int id);
    Task CreateTicketAsync(CreateTicketViewModel model);
    Task CloseTicketAsync(int id);
}