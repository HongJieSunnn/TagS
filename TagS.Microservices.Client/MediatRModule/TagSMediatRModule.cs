using Autofac;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TagS.Implementation.Commands.TagCommands;

namespace TagS.AutofacModules.MediatRModule
{
    internal class TagSMediatRModule:Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<Mediator>()
                .As<IMediator>()
                .InstancePerLifetimeScope();

            builder.Register<ServiceFactory>(context =>
            {
                var c = context.Resolve<IComponentContext>();
                return t => c.Resolve(t);
            });

            builder.RegisterAssemblyTypes(typeof(AddTagCommand<>).GetTypeInfo().Assembly).AsClosedTypesOf(typeof(IRequestHandler<,>));
        }
    }
}
