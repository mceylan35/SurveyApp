﻿using Microsoft.VisualBasic.FileIO;
using MongoDB.Driver;
using SurveyApp.Domain.Common;
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
        IMongoCollection<T> GetCollection<T>() where T : MongoEntity;
        //IMongoCollection<User> Users { get; }
        //IMongoCollection<Survey> Surveys { get; }
        //IMongoCollection<Option> Options { get; }
        //IMongoCollection<Vote> Votes { get; }
    }
}
