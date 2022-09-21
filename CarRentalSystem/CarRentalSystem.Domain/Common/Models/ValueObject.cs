namespace CarRentalSystem.Domain.Common.Models
{
    using System.Reflection;

    public abstract class ValueObject
    {
        private readonly BindingFlags bindingFlags =
            BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;

        public override bool Equals(object? other)
        {
            if (other == null) return false;

            Type? type = GetType();
            Type? otherType = other.GetType();

            if (type != otherType) return false;

            FieldInfo[]? fields = type.GetFields(bindingFlags);

            foreach (var field in fields)
            {
                object? firstValue = field.GetValue(other);
                object? secondValue = field.GetValue(this);

                if (firstValue == null)
                {
                    if (secondValue != null)
                    {
                        return false;
                    }
                }
                else if (!firstValue.Equals(secondValue))
                {
                    return false;
                }
            }

            return true;
        }

        public override int GetHashCode()
        {
            var fields = GetFields();

            const int startValue = 17;
            const int multiplier = 59;

            return fields
                .Select(field => field.GetValue(this))
                .Where(value => value != null)
                .Aggregate(startValue, (current, value) => current * multiplier + value!.GetHashCode());
        }

        public static bool operator ==(ValueObject first, ValueObject second) => first.Equals(second);

        public static bool operator !=(ValueObject first, ValueObject second) => !(first == second);

        private IEnumerable<FieldInfo> GetFields()
        {
            Type type = GetType();

            List<FieldInfo>? fields = new List<FieldInfo>();

            while (type != typeof(object) && type != null)
            {
                fields.AddRange(type.GetFields(bindingFlags));

                type = type.BaseType!;
            }

            return fields;
        }
    }
}
