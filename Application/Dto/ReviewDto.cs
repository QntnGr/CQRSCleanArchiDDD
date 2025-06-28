
using Domain.Entities;
using System.Text.Json.Serialization;

namespace Application.Dto;

public class ReviewDto
{
    [JsonIgnore]
    public Guid Id { get; set; } = Guid.NewGuid();
    public string author_name { get; set; } = null!;
    public string language { get; set; } = null!;
    public string profile_photo_url { get; set; } = null!;
    public int rating { get; set; }
    public string relative_time_description { get; set; } = null!;
    public string text { get; set; } = null!;
    public long time { get; set; }
    public DateTimeOffset Date
    {
        get
        {
            if (!date.HasValue)
            {
                date = DateTimeOffset.FromUnixTimeSeconds(time);
            }
            return date.Value;
        }
    }
    public bool translated { get; set; }

    private DateTimeOffset? date;

    public Review ToEntity()
    {
        return new Review()
        {
            AuthorName = author_name,
            Language = language,
            ProfilePhotoUrl = profile_photo_url,
            Rating = rating,
            Date = Date,
            IsTranslated = translated,
            Id = Id,
            RelativeTimeDescription = relative_time_description,
            Text = text
        };
    }

    public static ReviewDto FromEntity(Review review)
    {
        return new ReviewDto()
        {
            Id = review.Id,
            author_name = review.AuthorName,
            language = review.Language,
            date = review.Date,
            translated = review.IsTranslated,
            profile_photo_url = review.ProfilePhotoUrl,
            rating = review.Rating,
            relative_time_description = review.RelativeTimeDescription,
            text = review.Text
        };
    }
}

public class ReviewsModel
{
    public ResultReviews result { get; set; }
}

public class ResultReviews
{
    public IEnumerable<ReviewDto> reviews { get; set; }
}
