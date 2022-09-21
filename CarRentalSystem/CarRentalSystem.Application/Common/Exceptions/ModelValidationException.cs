namespace CarRentalSystem.Application.Common.Exceptions
{
    using FluentValidation.Results;

    public class ModelValidationException : Exception
    {
        public IDictionary<string, string[]> Errors { get; }

        public ModelValidationException()
            : base("One or more validation errors have occurred.")
            => Errors = new Dictionary<string, string[]>();

        public ModelValidationException(IEnumerable<ValidationFailure> errors)
            : this()
        {
            IEnumerable<IGrouping<string, string>> failureGroups = errors
                .GroupBy(e => e.PropertyName, e => e.ErrorMessage);

            foreach (var failureGroup in failureGroups)
            {
                string propertyName = failureGroup.Key;
                string[] propertyFailures = failureGroup.ToArray();

                Errors.Add(propertyName, propertyFailures);
            }
        }
    }
}
