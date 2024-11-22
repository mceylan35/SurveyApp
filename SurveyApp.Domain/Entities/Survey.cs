using Microsoft.VisualBasic.FileIO;
using MongoDB.Bson.Serialization.Attributes;
using SurveyApp.Domain.Attributes;
using SurveyApp.Domain.Common;
using SurveyApp.Domain.Exceptions;
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
        public string Title { get; set; }


        public static Survey Create(string title, Guid creatorId)
        {
            if (string.IsNullOrEmpty(title))
                throw new DomainException("Title cannot be empty");

            var survey = new Survey
            {
                Id = Guid.NewGuid(),
                Title = title,
                CreatorId = creatorId,
                CreatedAt = DateTime.UtcNow
            };

            return survey;
        }

       
    }
}
