using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientService.Core.Common
{
    public class Result<T>
    {
        public T? Value { get; }
        public string? Error { get; }
        public bool IsSuccess => Error is null;
        public bool IsFailure => !IsSuccess;

        private Result(T? value, string? error)
        {
            Value = value;
            Error = error;
        }

        public static Result<T> Success(T value)
        {
            ArgumentNullException.ThrowIfNull(value);
            return new Result<T>(value, null);
        }

        public static Result<T> Failure(string error)
        {
            ArgumentException.ThrowIfNullOrEmpty(error);
            return new Result<T>(default, error);
        }

        public T GetValueOrThrow()
        {
            if (IsFailure)
                throw new InvalidOperationException(Error);
            return Value!;
        }
    }

}
