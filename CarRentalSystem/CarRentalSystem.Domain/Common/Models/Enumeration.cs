namespace CarRentalSystem.Domain.Common.Models
{
    using System.Collections.Concurrent;
    using System.Reflection;

    public abstract class Enumeration : IComparable
    {
        private static readonly ConcurrentDictionary<Type, IEnumerable<object>> EnumCache;

        public string Name { get; set; }

        public int Value { get; set; }

        protected Enumeration(int value)
        {
            Value = value;
            Name = FromValue<Enumeration>(value).Name;
        }

        protected Enumeration(int value, string name)
        {
            Value = value;
            Name = name;
        }

        static Enumeration()
        {
            EnumCache = new();
        }

        public override string ToString() => Name;

        public static IEnumerable<T> GetAll<T>()
            where T : Enumeration
        {
            Type type = typeof(T);

            IEnumerable<object> values = EnumCache.GetOrAdd(type, _ => type
                .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)
                .Select(f => f.GetValue(null)!));

            return values.Cast<T>();
        }

        public override bool Equals(object? obj)
        {
            if (obj is not Enumeration otherValue)
            {
                return false;
            }

            bool typeMatches = GetType() == obj.GetType();
            bool valueMatches = Value.Equals(otherValue.Value);

            return typeMatches && valueMatches;
        }

        public override int GetHashCode() => (GetType().ToString() + Value).GetHashCode();

        public int CompareTo(object? other) => Value.CompareTo(((Enumeration)other!).Value);

        public static string NameFromValue<T>(int value) where T : Enumeration
            => FromValue<T>(value).Name;

        public static T FromValue<T>(int value) where T : Enumeration
            => Parse<T, int>(value, "value", item => item.Value == value);

        public static T FromName<T>(string name) where T : Enumeration
            => Parse<T, string>(name, "name", item => item.Name == name);

        public static bool HasValue<T>(int value) where T : Enumeration
        {
            try
            {
                FromValue<T>(value);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private static T Parse<T, TValue>(TValue value, string description, Func<T, bool> predicate)
            where T : Enumeration
        {
            T? matchingItem = GetAll<T>().FirstOrDefault(predicate);

            if (matchingItem == null)
            {
                throw new InvalidOperationException($"'{value}' is not valid {description} in {typeof(T)}");
            }

            return matchingItem;
        }
    }
}
