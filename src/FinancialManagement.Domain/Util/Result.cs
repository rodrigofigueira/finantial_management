namespace FinancialManagement.Domain.Util
{
    public class Result<T>
    {
        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;
        public T Value { get; }
        public string? Error { get; }

        protected Result(bool isSuccess, T value, string? error)
        {
            if (isSuccess && value == null)
                throw new InvalidOperationException("Success result must have a value.");
            if (!isSuccess && string.IsNullOrEmpty(error))
                throw new InvalidOperationException("Failure result must have an error message.");

            IsSuccess = isSuccess;
            Value = value;
            Error = error;
        }

        public static Result<T> Success(T value)
        {
            return new Result<T>(true, value, null);
        }

        public static Result<T> Failure(string error)
        {
            return new Result<T>(false, default(T)!, error);
        }
    }

}
