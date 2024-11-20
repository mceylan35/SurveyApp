﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Application.Common.Results
{
    public class Result<T>
    {
        public bool IsSuccess { get; }
        public T Value { get; }
        public string Error { get; }
        public bool IsFailure => !IsSuccess;

        private Result(bool isSuccess, T value, string error)
        {
            IsSuccess = isSuccess;
            Value = value;
            Error = error;
        }

        public static Result<T> Success(T value) => new(true, value, string.Empty);
        public static Result<T> Failure(string error) => new(false, default, error);
    }
    public static class ResultExtensions
    {
        public static Result<bool> ToResult(this bool success, string error = null)
            => success ? Result<bool>.Success(true) : Result<bool>.Failure(error ?? "Operation failed");
    }
}
