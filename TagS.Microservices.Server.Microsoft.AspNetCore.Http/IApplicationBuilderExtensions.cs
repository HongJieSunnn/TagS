using Microsoft.AspNetCore.Builder;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using TagS.Microservices.Client.Models;
using TagS.Microservices.Server.Models;

namespace TagS.Microservices.Server.Microsoft.AspNetCore.Http
{
    public static class IApplicationBuilderExtensions
    {
        public static IApplicationBuilder AddReferrerDiscriminator<TReferrer>(this IApplicationBuilder builder)
            where TReferrer : IReferrer
        {
            var existedIdProperty = typeof(TReferrer).GetProperties().Any(p => p.Name.ToUpper() == "ID");
            if (!existedIdProperty)
                throw new InvalidOperationException("Referrer should have a ID match the ID of TagableEntity");
            BsonClassMap.RegisterClassMap<TReferrer>();
            return builder;
        }

        public static IApplicationBuilder AddLocationIndexFroReferrer(this IApplicationBuilder builder, string geoFiledName)
        {
            var locIndex = new IndexKeysDefinitionBuilder<TagWithReferrer>().Geo2DSphere(geoFiledName);//TODO I do not know if it's useful.
            var indexModel = new CreateIndexModel<TagWithReferrer>(locIndex);
            var context = builder.ApplicationServices.GetService(typeof(TagSMongoDBContext)) as TagSMongoDBContext??throw new NullReferenceException(nameof(TagSMongoDBContext));

            context.TagWithReferrers!.Indexes.CreateOne(indexModel);

            return builder;
        }
    }
}