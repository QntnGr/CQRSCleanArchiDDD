
namespace Domain.Entities;

public class Review
{
    public Guid Id { get; set; }
    public string AuthorName { get; set; } = null!;
    public string Language { get; set; } = null!;
    public string ProfilePhotoUrl { get; set; } = null!;
    public int Rating { get; set; }
    public string RelativeTimeDescription { get; set; } = null!;
    public string Text { get; set; } = null!;
    public DateTimeOffset Date { get; set; }
    public bool IsTranslated {  get; set; }

}
