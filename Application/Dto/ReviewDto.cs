
using Domain.Entities;
using System.Text.Json.Serialization;

namespace Application.Dto;

public class ReviewDto
{
    [JsonIgnore]
    public Guid Id { get; set; }

    [JsonPropertyName("author_name")]
    public string AuthorName { get; set; } = null!;

    [JsonPropertyName("language")]
    public string Language { get; set; } = null!;

    [JsonPropertyName("profile_photo_url")]
    public string ProfilePhotoUrl { get; set; } = null!;

    [JsonPropertyName("rating")]
    public int Rating { get; set; }

    [JsonPropertyName("relative_time_description")]
    public string RelativeTimeDescription { get; set; } = null!;

    [JsonPropertyName("text")]
    public string Text { get; set; } = null!;

    [JsonPropertyName("time")]
    public DateTimeOffset Date { get; set; }

    [JsonPropertyName("translated")]
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
