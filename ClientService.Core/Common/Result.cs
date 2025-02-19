using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientService.Core.Common
{
    // a class for 
    public class Result<T>
    {
        public T? Value { get; }
        public string? Error { get; }
        public bool IsSuccess => Error is null;
        public bool IsFailure => !IsSuccess;

        // Private constructor to enforce usage of Success or Failure methods.
        private Result(T? value, string? error)
        {
            Value = value;
            Error = error;
        }

        // Creates a successful result with a given value.
        public static Result<T> Success(T value)
        {
            ArgumentNullException.ThrowIfNull(value);
            return new Result<T>(value, null);
        }

        // Creates a failed result with a given error message.
        public static Result<T> Failure(string error)
        {
            ArgumentException.ThrowIfNullOrEmpty(error);
            return new Result<T>(default, error);
        }

        // Returns the value or throws an exception if the operation failed.
        public T GetValueOrThrow()
        {
            if (IsFailure)
                throw new InvalidOperationException(Error);
            return Value!;
        }
    }

}
