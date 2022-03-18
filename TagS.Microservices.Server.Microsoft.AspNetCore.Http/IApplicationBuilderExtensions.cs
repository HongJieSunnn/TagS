using EventBusCommon.Abstractions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using TagS.Microservices.Client.IntegrationEvents;
using TagS.Microservices.Client.Models;
using TagS.Microservices.Server.IntegrationEventHandler;
using TagS.Microservices.Server.Models;
using TagS.Microservices.Server.Repositories.TagRepository;

namespace TagS.Microservices.Server.Microsoft.AspNetCore.Http
{
    public static class IApplicationBuilderExtensions
    {
        public static IApplicationBuilder MapTagSMongoDBCollectionModels(this IApplicationBuilder builder)
        {
            

            
            return builder;
        }
        public static IApplicationBuilder AddReferrerDiscriminator<TReferrer>(this IApplicationBuilder builder)
            where TReferrer : IReferrer
        {
            var existedIdProperty = typeof(TReferrer).GetProperties().Any(p => p.Name.ToUpper() == "ID");
            if (!existedIdProperty)
                throw new InvalidOperationException("Referrer should have a ID match the ID of TagableEntity");
            BsonClassMap.RegisterClassMap<TReferrer>(t=>t.SetDiscriminator($"{typeof(TReferrer).FullName}, {typeof(TReferrer).Namespace}"));
            return builder;
        }

        public static IApplicationBuilder AddLocationIndexFroReferrer(this IApplicationBuilder builder, string geoFiledName)
        {
            var locIndex = new IndexKeysDefinitionBuilder<TagWithReferrer>().Geo2DSphere(geoFiledName);//TODO I do not know if it's useful.Maybe this is not necessary,we should use Geo in Meet but not here.
            var indexModel = new CreateIndexModel<TagWithReferrer>(locIndex);
            var context = builder.ApplicationServices.GetService(typeof(TagSMongoDBContext)) as TagSMongoDBContext??throw new NullReferenceException(nameof(TagSMongoDBContext));

            context.TagWithReferrers!.Indexes.CreateOne(indexModel);

            return builder;
        }

        public static IApplicationBuilder ConfigureTagServerEventBus(this IApplicationBuilder builder)
        {
            var eventBus = builder.ApplicationServices.GetRequiredService<IAsyncEventBus>();

            //Subscribe the integration events to handle.
            //To avoid unnecessary access of other services,the integration events should be create copy in this service.Because we can identify a integration event by name.
            //However,we have already reference the Client.So we need not create copies.
            eventBus.Subscribe<AddReferrerToTagServerIntegrationEvent, AddReferrerToTagServerIntegrationEventHandler>();
            eventBus.Subscribe<RemoveReferrerToTagServerIntegrationEvent, RemoveReferrerToTagServerIntegrationEventHandler>();

            return builder;
        }

        public static IApplicationBuilder SeedDefaultEmotionTags(this IApplicationBuilder builder)
        {
            var context = builder.ApplicationServices.GetRequiredService<TagSMongoDBContext>();
            var emotionTag = context.Tags.Find(t => t.PreferredTagName == "心情").ToList();
            if (emotionTag.Any())
                return builder;

            var repository = builder.ApplicationServices.GetRequiredService<ITagRepository>();

            var baseEmotionTagObjectId=ObjectId.GenerateNewId().ToString();
            var baseEmotionTag = new Models.Tag(baseEmotionTagObjectId, "心情", "心情标签", null, new List<string> { "情绪","Emotion","Mood" }, null, null);
            baseEmotionTag.AddNextTag(new Models.Tag(null, "心情:积极", "心情可以被普遍地分为积极和消极两种，积极作为二元情绪中的一元，代表着人们正面的情绪。", baseEmotionTagObjectId, null, null, null));
            baseEmotionTag.AddNextTag(new Models.Tag(null, "心情:消极", "心情可以被普遍地分为积极和消极两种，消极作为二元情绪中的一元，代表着人们负面的情绪。", baseEmotionTagObjectId, null, null, null));
            baseEmotionTag.AddNextTag(new Models.Tag(null, "心情:生气", "六种基本情绪之一", baseEmotionTagObjectId, null, null, null));
            baseEmotionTag.AddNextTag(new Models.Tag(null, "心情:厌恶", "六种基本情绪之一", baseEmotionTagObjectId, null, null, null));
            baseEmotionTag.AddNextTag(new Models.Tag(null, "心情:害怕", "六种基本情绪之一", baseEmotionTagObjectId, null, null, null));
            baseEmotionTag.AddNextTag(new Models.Tag(null, "心情:悲伤", "六种基本情绪之一", baseEmotionTagObjectId, null, null, null));
            baseEmotionTag.AddNextTag(new Models.Tag(null, "心情:开心", "六种基本情绪之一", baseEmotionTagObjectId, null, null, null));
            baseEmotionTag.AddNextTag(new Models.Tag(null, "心情:惊讶", "六种基本情绪之一", baseEmotionTagObjectId, null, null, null));
            baseEmotionTag.AddNextTag(new Models.Tag(null, "心情:知足", "扩充情绪之一", baseEmotionTagObjectId, null, null, null));
            baseEmotionTag.AddNextTag(new Models.Tag(null, "心情:尴尬", "扩充情绪之一", baseEmotionTagObjectId, null, null, null));
            baseEmotionTag.AddNextTag(new Models.Tag(null, "心情:兴奋", "扩充情绪之一", baseEmotionTagObjectId, null, null, null));
            baseEmotionTag.AddNextTag(new Models.Tag(null, "心情:内疚", "扩充情绪之一", baseEmotionTagObjectId, null, null, null));
            baseEmotionTag.AddNextTag(new Models.Tag(null, "心情:自豪", "扩充情绪之一", baseEmotionTagObjectId, null, null, null));
            baseEmotionTag.AddNextTag(new Models.Tag(null, "心情:松口气", "扩充情绪之一", baseEmotionTagObjectId, null, null, null));
            baseEmotionTag.AddNextTag(new Models.Tag(null, "心情:满意", "扩充情绪之一", baseEmotionTagObjectId, null, null, null));
            baseEmotionTag.AddNextTag(new Models.Tag(null, "心情:羞愧", "扩充情绪之一", baseEmotionTagObjectId, null, null, null));
            baseEmotionTag.AddNextTag(new Models.Tag(null, "心情:崇拜", "扩充情绪之一", baseEmotionTagObjectId, null, null, null));
            baseEmotionTag.AddNextTag(new Models.Tag(null, "心情:挫败", "扩充情绪之一", baseEmotionTagObjectId, null, null, null));
            baseEmotionTag.AddNextTag(new Models.Tag(null, "心情:痛苦", "扩充情绪之一", baseEmotionTagObjectId, null, null, null));
            baseEmotionTag.AddNextTag(new Models.Tag(null, "心情:忧虑", "扩充情绪之一", baseEmotionTagObjectId, null, null, null));
            baseEmotionTag.AddNextTag(new Models.Tag(null, "心情:疏离", "扩充情绪之一", baseEmotionTagObjectId, null, null, null));
            baseEmotionTag.AddNextTag(new Models.Tag(null, "心情:烦恼", "扩充情绪之一", baseEmotionTagObjectId, null, null, null));
            baseEmotionTag.AddNextTag(new Models.Tag(null, "心情:顾虑", "扩充情绪之一", baseEmotionTagObjectId, null, null, null));
            baseEmotionTag.AddNextTag(new Models.Tag(null, "心情:担心", "扩充情绪之一", baseEmotionTagObjectId, null, null, null));
            baseEmotionTag.AddNextTag(new Models.Tag(null, "心情:开怀", "扩充情绪之一", baseEmotionTagObjectId, null, null, null));
            baseEmotionTag.AddNextTag(new Models.Tag(null, "心情:陶醉", "扩充情绪之一", baseEmotionTagObjectId, null, null, null));
            baseEmotionTag.AddNextTag(new Models.Tag(null, "心情:沮丧", "扩充情绪之一", baseEmotionTagObjectId, null, null, null));
            baseEmotionTag.AddNextTag(new Models.Tag(null, "心情:渴望", "扩充情绪之一", baseEmotionTagObjectId, null, null, null));
            baseEmotionTag.AddNextTag(new Models.Tag(null, "心情:反感", "扩充情绪之一", baseEmotionTagObjectId, null, null, null));
            baseEmotionTag.AddNextTag(new Models.Tag(null, "心情:热心", "扩充情绪之一", baseEmotionTagObjectId, null, null, null));
            baseEmotionTag.AddNextTag(new Models.Tag(null, "心情:狂喜", "扩充情绪之一", baseEmotionTagObjectId, null, null, null));
            baseEmotionTag.AddNextTag(new Models.Tag(null, "心情:享受", "扩充情绪之一", baseEmotionTagObjectId, null, null, null));
            baseEmotionTag.AddNextTag(new Models.Tag(null, "心情:敬畏", "扩充情绪之一", baseEmotionTagObjectId, null, null, null));
            baseEmotionTag.AddNextTag(new Models.Tag(null, "心情:绝望", "扩充情绪之一", baseEmotionTagObjectId, null, null, null));
            baseEmotionTag.AddNextTag(new Models.Tag(null, "心情:惊吓", "扩充情绪之一", baseEmotionTagObjectId, null, null, null));
            baseEmotionTag.AddNextTag(new Models.Tag(null, "心情:愁闷", "扩充情绪之一", baseEmotionTagObjectId, null, null, null));
            baseEmotionTag.AddNextTag(new Models.Tag(null, "心情:懊恼", "扩充情绪之一", baseEmotionTagObjectId, null, null, null));
            baseEmotionTag.AddNextTag(new Models.Tag(null, "心情:灰心", "扩充情绪之一", baseEmotionTagObjectId, null, null, null));
            baseEmotionTag.AddNextTag(new Models.Tag(null, "心情:孤独", "扩充情绪之一", baseEmotionTagObjectId, null, null, null));

            repository.AddAsync(baseEmotionTag).GetAwaiter().GetResult();
            repository.UnitOfWork.SaveEntitiesAsync(baseEmotionTag);
            return builder;
        }

        public static IApplicationBuilder SeedDefaultMusicTags(this IApplicationBuilder builder)
        {
            var context = builder.ApplicationServices.GetRequiredService<TagSMongoDBContext>();
            var emotionTag = context.Tags.Find(t => t.PreferredTagName == "音乐").ToList();
            if (emotionTag.Any())
                return builder;

            var repository = builder.ApplicationServices.GetRequiredService<ITagRepository>();

            var baseMusicTagName = "音乐";
            var baseMusicTagObjectId = ObjectId.GenerateNewId().ToString();
            var baseMusicTag = new Models.Tag(baseMusicTagObjectId, baseMusicTagName, "音乐标签", null, new List<string> { "Music" }, null, null);

            var baseMusicEmotionTagName = $"{baseMusicTagName}:心情";
            var baseMusicEmotionTagObjectId = ObjectId.GenerateNewId().ToString();
            var baseMusicEmotionTag = new Models.Tag(baseMusicEmotionTagObjectId, baseMusicEmotionTagName, "音乐是具有情绪的。", baseMusicTagObjectId, null, null, null);
            baseMusicTag.AddNextTag(baseMusicEmotionTag);
            baseMusicEmotionTag.AddNextTag(new Models.Tag(null, $"{baseMusicEmotionTagName}:伤感", "伤感音乐标签", baseMusicEmotionTagObjectId, null, null, null));
            baseMusicEmotionTag.AddNextTag(new Models.Tag(null, $"{baseMusicEmotionTagName}:安静", "安静音乐标签", baseMusicEmotionTagObjectId, null, null, null));
            baseMusicEmotionTag.AddNextTag(new Models.Tag(null, $"{baseMusicEmotionTagName}:快乐", "快乐音乐标签", baseMusicEmotionTagObjectId, null, null, null));
            baseMusicEmotionTag.AddNextTag(new Models.Tag(null, $"{baseMusicEmotionTagName}:治愈", "治愈音乐标签", baseMusicEmotionTagObjectId, null, null, null));
            baseMusicEmotionTag.AddNextTag(new Models.Tag(null, $"{baseMusicEmotionTagName}:励志", "励志音乐标签", baseMusicEmotionTagObjectId, null, null, null));
            baseMusicEmotionTag.AddNextTag(new Models.Tag(null, $"{baseMusicEmotionTagName}:甜蜜", "甜蜜音乐标签", baseMusicEmotionTagObjectId, null, null, null));
            baseMusicEmotionTag.AddNextTag(new Models.Tag(null, $"{baseMusicEmotionTagName}:寂寞", "寂寞音乐标签", baseMusicEmotionTagObjectId, null, null, null));
            baseMusicEmotionTag.AddNextTag(new Models.Tag(null, $"{baseMusicEmotionTagName}:宣泄", "宣泄音乐标签", baseMusicEmotionTagObjectId, null, null, null));
            baseMusicEmotionTag.AddNextTag(new Models.Tag(null, $"{baseMusicEmotionTagName}:思念", "思念音乐标签", baseMusicEmotionTagObjectId, null, null, null));

            var baseMusicSceneTagName = $"{baseMusicTagName}:场景";
            var baseMusicSceneTagObjectId = ObjectId.GenerateNewId().ToString();
            var baseMusicSceeTag = new Models.Tag(baseMusicSceneTagObjectId, baseMusicSceneTagName, "不同场景下的音乐。", baseMusicTagObjectId, null, null, null);
            baseMusicTag.AddNextTag(baseMusicEmotionTag);
            baseMusicSceeTag.AddNextTag(new Models.Tag(null, $"{baseMusicSceneTagName}:睡前", "睡前场景音乐标签", baseMusicSceneTagObjectId, null, null, null));
            baseMusicSceeTag.AddNextTag(new Models.Tag(null, $"{baseMusicSceneTagName}:夜店", "夜店场景音乐标签", baseMusicSceneTagObjectId, null, null, null));
            baseMusicSceeTag.AddNextTag(new Models.Tag(null, $"{baseMusicSceneTagName}:学习", "学习场景音乐标签", baseMusicSceneTagObjectId, null, null, null));
            baseMusicSceeTag.AddNextTag(new Models.Tag(null, $"{baseMusicSceneTagName}:运动", "运动场景音乐标签", baseMusicSceneTagObjectId, null, null, null));
            baseMusicSceeTag.AddNextTag(new Models.Tag(null, $"{baseMusicSceneTagName}:约会", "约会场景音乐标签", baseMusicSceneTagObjectId, null, null, null));
            baseMusicSceeTag.AddNextTag(new Models.Tag(null, $"{baseMusicSceneTagName}:工作", "工作场景音乐标签", baseMusicSceneTagObjectId, null, null, null));
            baseMusicSceeTag.AddNextTag(new Models.Tag(null, $"{baseMusicSceneTagName}:旅行", "旅行场景音乐标签", baseMusicSceneTagObjectId, null, null, null));
            baseMusicSceeTag.AddNextTag(new Models.Tag(null, $"{baseMusicSceneTagName}:派对", "派对场景音乐标签", baseMusicSceneTagObjectId, null, null, null));
            baseMusicSceeTag.AddNextTag(new Models.Tag(null, $"{baseMusicSceneTagName}:婚礼", "婚礼场景音乐标签", baseMusicSceneTagObjectId, null, null, null));
            baseMusicSceeTag.AddNextTag(new Models.Tag(null, $"{baseMusicSceneTagName}:咖啡馆", "咖啡馆场景音乐标签", baseMusicSceneTagObjectId, null, null, null));
            baseMusicSceeTag.AddNextTag(new Models.Tag(null, $"{baseMusicSceneTagName}:跳舞", "跳舞场景音乐标签", baseMusicSceneTagObjectId, null, null, null));
            baseMusicSceeTag.AddNextTag(new Models.Tag(null, $"{baseMusicSceneTagName}:校园", "校园场景音乐标签", baseMusicSceneTagObjectId, null, null, null));

            repository.AddAsync(baseMusicTag).GetAwaiter().GetResult();
            repository.UnitOfWork.SaveEntitiesAsync(baseMusicTag);
            return builder;
        }
    }
}