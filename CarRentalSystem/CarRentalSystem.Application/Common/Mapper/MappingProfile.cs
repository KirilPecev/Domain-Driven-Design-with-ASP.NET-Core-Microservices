namespace CarRentalSystem.Application.Common.Mapper
{
    using System;
    using System.Reflection;

    using AutoMapper;

    public class MappingProfile : Profile
    {
        public MappingProfile()
            => ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());

        private void ApplyMappingsFromAssembly(Assembly assembly)
        {
            List<Type> types = assembly
                .GetExportedTypes()
                .Where(t => t.GetInterfaces()
                    .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>)))
                .ToList();

            foreach (var type in types)
            {
                object? instance = Activator.CreateInstance(type);

                const string mappingMethodName = "Mapping";

                MethodInfo? methodInfo = type.GetMethod(mappingMethodName)
                                        ?? type.GetInterface("IMapFrom`1")?.GetMethod(mappingMethodName);

                methodInfo?.Invoke(instance, new object[] { this });
            }
        }
    }
}
