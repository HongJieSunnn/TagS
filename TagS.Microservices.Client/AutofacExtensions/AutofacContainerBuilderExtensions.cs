namespace TagS.Microservices.Client.AutofacExtensions
{
    public static class AutofacContainerBuilderExtensions
    {
        /// <summary>
        /// Call this method in MediatRModules.
        /// </summary>
        /// <param name="builder"></param>
        public static void RegisterTagSMicroservicesClientTypes(this ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(AddTagDomainEvent).GetTypeInfo().Assembly).AsClosedTypesOf(typeof(INotificationHandler<>));
        }
    }
}
