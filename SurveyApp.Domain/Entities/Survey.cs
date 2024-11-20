using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SurveyApp.Domain.Entities
{
    [BsonCollection("Surveys")]
    public class Survey : MongoEntity
    {
        [BsonElement("title")]
        public string Title { get; private set; }

       

        [BsonElement("options")]
        private readonly List<Option> _options = new();

        [BsonIgnore]
        public IReadOnlyCollection<Option> Options => _options.AsReadOnly();

        [BsonElement("votes")]
        private readonly List<Vote> _votes = new();

        [BsonIgnore]
        public IReadOnlyCollection<Vote> Votes => _votes.AsReadOnly();

        public static Survey Create(string title, Guid creatorId, List<string> options)
        {
            if (string.IsNullOrEmpty(title))
                throw new DomainException("Title cannot be empty");

            if (options.Count < 2)
                throw new DomainException("Survey must have at least 2 options");

            var survey = new Survey
            {
                Id = Guid.NewGuid(),
                Title = title,
                CreatorId = creatorId,
                CreatedAt = DateTime.UtcNow
            };

            foreach (var optionText in options)
            {
                survey._options.Add(Option.Create(optionText));
            }

            return survey;
        }

        public void Vote(Guid userId, Guid optionId)
        {
            var option = _options.FirstOrDefault(o => o.Id == optionId)
                ?? throw new DomainException("Option not found");

            option.IncrementVoteCount();
        }
    }
}
