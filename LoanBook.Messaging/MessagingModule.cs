using Autofac;
using System;
using System.Linq;
using System.Reflection;

namespace LoanBook.Messaging
{
    public class MessagingModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assembly = Assembly.GetEntryAssembly();

            builder.RegisterAssemblyTypes(assembly)
                .Where(t => IsAssignableFromGenericType(t, typeof(IHandleCommand<>)))
                .AsClosedTypesOf(typeof(IHandleCommand<>));

            builder.RegisterAssemblyTypes(assembly)
                .Where(t => IsAssignableFromGenericType(t, typeof(ISubscribeToEvent<>)))
                .AsClosedTypesOf(typeof(ISubscribeToEvent<>));
        }

        private bool IsAssignableFromGenericType(Type type, Type genericType)
        {
            return type.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == genericType);
        }
    }
}