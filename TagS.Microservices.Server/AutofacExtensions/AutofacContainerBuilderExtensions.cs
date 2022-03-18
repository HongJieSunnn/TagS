﻿using Autofac;
using CommonService.Behaviors;

namespace TagS.Microservices.Server.AutofacExtensions
{
    public static class AutofacContainerBuilderExtensions
    {
        /// <summary>
        /// Call this method in MediatRModules.
        /// </summary>
        /// <param name="builder"></param>
        public static void RegisterTagSMicroservicesServerTypes(this ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(AddTagDomainEvent).GetTypeInfo().Assembly).AsClosedTypesOf(typeof(INotificationHandler<>));
            builder.RegisterAssemblyTypes(typeof(CreateReviewedTagCommand).GetTypeInfo().Assembly).AsClosedTypesOf(typeof(IRequestHandler<,>));
            builder.RegisterGeneric(typeof(MongoDBTransactionBehavior<,>)).As(typeof(IPipelineBehavior<,>));
        }
    }
}
