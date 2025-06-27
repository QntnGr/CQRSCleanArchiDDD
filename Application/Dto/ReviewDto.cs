
using Domain.Entities;
using System.Text.Json.Serialization;

namespace Application.Dto;

public class ReviewDto
{
    [JsonIgnore]
    public Guid Id { get; set; } = new Guid();
    public string AuthorName { get; set; } = null!;
    public string Language { get; set; } = null!;
    public string ProfilePhotoUrl { get; set; } = null!;
    public int Rating { get; set; }
    public string RelativeTimeDescription { get; set; } = null!;
    public string Text { get; set; } = null!;
    public DateTimeOffset Date { get; set; }
    public bool IsTranslated { get; set; }

    public Review ToEntity()
    {
        return new Review()
        {
            AuthorName = AuthorName,
            Language = Language,
            ProfilePhotoUrl = ProfilePhotoUrl,
            Rating = Rating,
            Date = Date,
            IsTranslated = IsTranslated,
            Id = Id,
            RelativeTimeDescription = RelativeTimeDescription,
            Text = Text
        };
    }
}

public class ReviewsModel
{
    public ResultReviews result {  get; set; }
}

public class ResultReviews
{
    public IEnumerable<Review> reviews { get; set; }
}
