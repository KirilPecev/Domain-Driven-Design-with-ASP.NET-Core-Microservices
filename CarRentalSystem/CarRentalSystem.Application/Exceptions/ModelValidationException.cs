namespace CarRentalSystem.Application.Exceptions
{
    using FluentValidation.Results;

    public class ModelValidationException : Exception
    {
        public IDictionary<string, string[]> Errors { get; }

        public ModelValidationException()
            : base("One or more validation errors have occurred.")
            => this.Errors = new Dictionary<string, string[]>();

        public ModelValidationException(IEnumerable<ValidationFailure> errors)
            : this()
        {
            IEnumerable<IGrouping<string, string>> failureGroups = errors
                .GroupBy(e => e.PropertyName, e => e.ErrorMessage);

            foreach (var failureGroup in failureGroups)
            {
                string propertyName = failureGroup.Key;
                string[] propertyFailures = failureGroup.ToArray();

                this.Errors.Add(propertyName, propertyFailures);
            }
        }
    }
}
