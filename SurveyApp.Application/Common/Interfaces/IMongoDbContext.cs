using Microsoft.VisualBasic.FileIO;
using MongoDB.Driver;
using SurveyApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Application.Common.Interfaces
{
    public interface IMongoDbContext
    {
        IMongoCollection<User> Users { get; }
        IMongoCollection<Survey> Surveys { get; }
        IMongoCollection<Option> Options { get; }
        IMongoCollection<Vote> Votes { get; }
    }
}
