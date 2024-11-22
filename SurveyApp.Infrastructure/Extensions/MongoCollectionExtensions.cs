using SurveyApp.Application.Common;
using SurveyApp.Domain.Attributes;
using SurveyApp.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Infrastructure.Extensions
{
    public static class MongoCollectionExtensions
    {
        public static string GetCollectionName<T>() where T : MongoEntity
        {
            var attribute = typeof(T).GetCustomAttribute<BsonCollectionAttribute>();

            if (attribute == null)
                throw new ArgumentException($"{typeof(T).Name} does not have BsonCollectionAttribute");

            return attribute.CollectionName;
        }
    }
}
