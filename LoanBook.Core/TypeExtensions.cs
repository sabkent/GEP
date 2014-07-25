using System;
using System.Linq;
namespace LoanBook.Core
{
    public static class TypeExtensions
    {
        public static bool IsAssiableFromGenericType(this Type type, Type genericType)
        {
            return type.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == genericType);
        }
    }
}