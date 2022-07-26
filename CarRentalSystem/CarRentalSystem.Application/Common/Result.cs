﻿namespace CarRentalSystem.Application.Common
{
    public class Result
    {
        private readonly List<string> errors;

        public bool Succeeded { get; }

        internal Result(bool succeeded, List<string> errors)
        {
            Succeeded = succeeded;
            this.errors = errors;
        }

        public List<string> Errors => Succeeded ? new List<string>() : errors;

        public static Result Success
            => new Result(true, new List<string>());

        public static Result Failure(IEnumerable<string> errors)
            => new Result(false, errors.ToList());

        public static implicit operator Result(string error)
            => Failure(new List<string>() { error });

        public static implicit operator Result(List<string> errors)
           => Failure(errors);

        public static implicit operator Result(bool success)
           => success ? Success : Failure(new[] { "Unsuccesful operation." });

        public static implicit operator bool(Result result)
            => result.Succeeded;
    }

    public class Result<TData> : Result
    {
        private readonly TData data;

        private Result(bool succeeded, TData data, List<string> errors)
            : base(succeeded, errors)
            => this.data = data;

        public TData Data
            => Succeeded
                ? data
                : throw new InvalidOperationException(
                    $"{nameof(Data)} is not available with a failed result. Use {Errors} instead.");

        public static Result<TData> SuccessWith(TData data)
            => new Result<TData>(true, data, new List<string>());

        public new static Result<TData> Failure(IEnumerable<string> errors)
            => new Result<TData>(false, default!, errors.ToList());

        public static implicit operator Result<TData>(string error)
            => Failure(new List<string> { error });

        public static implicit operator Result<TData>(List<string> errors)
            => Failure(errors);

        public static implicit operator Result<TData>(TData data)
            => SuccessWith(data);

        public static implicit operator bool(Result<TData> result)
            => result.Succeeded;
    }
}
