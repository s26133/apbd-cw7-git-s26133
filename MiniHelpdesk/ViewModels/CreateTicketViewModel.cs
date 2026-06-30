using System.ComponentModel.DataAnnotations;

namespace MiniHelpdesk.ViewModels;

public class CreateTicketViewModel
{
    [Required]
    public string Title { get; set; }
    public string Description { get; set; }
    [Required]
    public string CommentAuthor { get; set; }
    [Required]
    public string CommentContent { get; set; }
}