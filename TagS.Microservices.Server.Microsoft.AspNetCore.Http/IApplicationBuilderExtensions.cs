using EventBusCommon.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using TagS.Core.Models;
using TagS.Microservices.Server.IntegrationEventHandler;
using TagS.Microservices.Server.IntegrationEventHandler.IntegrationEvents;
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
            var existedIdProperty = typeof(TReferrer).GetProperties().Any(p => p.Name.ToUpper() == "REFERRERID");

            if (!existedIdProperty)
                throw new InvalidOperationException("Referrer should have a ReferrerID match the ID of TagableEntity");

            BsonClassMap.RegisterClassMap<TReferrer>(t =>
            {
                t.AutoMap();//If not contain this line,the referrer will only has a _t value.
                t.SetDiscriminator($"{typeof(TReferrer).FullName}, {typeof(TReferrer).Namespace}");
            });
            return builder;
        }

        /// <summary>
        /// Add ReferrerIndexes for the referrer in TagWithReferrer.
        /// </summary>
        /// <typeparam name="TReferrer">Referrer type.Just to identify which referrer add indexes.</typeparam>
        /// <param name="builder"></param>
        /// <param name="indexNames">will create indexKeysDefinitions like Builders<TagWithReferrer>.IndexKeys.Ascending($"Referrers.{indexName}")</param>
        /// <returns></returns>
        public static IApplicationBuilder AddReferrerIndexes<TReferrer>(this IApplicationBuilder builder,params string[] indexNames)
            where TReferrer : IReferrer
        {
            var context = builder.ApplicationServices.GetRequiredService<TagSMongoDBContext>();

            var indexes = indexNames.Select(i => Builders<TagWithReferrer>.IndexKeys.Ascending($"Referrers.{i}")).Select(i => new CreateIndexModel<TagWithReferrer>(i));

            context.TagWithReferrers!.Indexes.CreateMany(indexes);

            return builder;
        }

        /// <summary>
        /// Add ReferrerIndexes for the referrer in TagWithReferrer.
        /// </summary>
        /// <typeparam name="TReferrer">Referrer type.Just to identify which referrer add indexes.</typeparam>
        /// <param name="builder"></param>
        /// <param name="indexNames">create CreateIndexModel by IndexKeysDefinitions so that we can identify the index type.</param>
        /// <returns></returns>
        public static IApplicationBuilder AddReferrerIndexes<TReferrer>(this IApplicationBuilder builder, params IndexKeysDefinition<TagWithReferrer>[] indexKeysDefinitions)
            where TReferrer : IReferrer
        {
            var context = builder.ApplicationServices.GetRequiredService<TagSMongoDBContext>();

            var indexes = indexKeysDefinitions.Select(i => new CreateIndexModel<TagWithReferrer>(i));

            context.TagWithReferrers!.Indexes.CreateMany(indexes);

            return builder;
        }

        /// <summary>
        /// Add ReferrerIndexes for the referrer in TagWithReferrer.
        /// </summary>
        /// <typeparam name="TReferrer">Referrer type.Just to identify which referrer add indexes.</typeparam>
        /// <param name="builder"></param>
        /// <param name="indexNames">create CreateIndexModel by CreateIndexModels so that we can set the options of index like if is unique.</param>
        /// <returns></returns>
        public static IApplicationBuilder AddReferrerIndexes<TReferrer>(this IApplicationBuilder builder, params CreateIndexModel<TagWithReferrer>[] createIndexModels)
            where TReferrer : IReferrer
        {
            var context = builder.ApplicationServices.GetRequiredService<TagSMongoDBContext>();

            context.TagWithReferrers!.Indexes.CreateMany(createIndexModels);

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
            var session = builder.ApplicationServices.GetRequiredService<IClientSessionHandle>();

            var emotionTag = context.Tags.Find(t => t.PreferredTagName == "心情").ToList();
            if (emotionTag.Any())
                return builder;

            var mediator = builder.ApplicationServices.GetRequiredService<IMediator>();
            var logger = builder.ApplicationServices.GetRequiredService<ILogger<IApplicationBuilder>>();

            logger.LogInformation("--------------- Start add seed datas for emotion tags ------------");
            var repository = builder.ApplicationServices.GetRequiredService<ITagRepository>();

            var baseEmotionTagObjectId = ObjectId.GenerateNewId().ToString();
            var baseEmotionTag = new Models.Tag(baseEmotionTagObjectId, "心情", "心情标签", null, null, new List<string> { "情绪", "Emotion", "Mood" }, null);

            var secondLevelEmtionTagsAncestors = new List<string>() { baseEmotionTagObjectId };
            var secondLevelEmotionTags = new List<Models.Tag>()
            {
                new Models.Tag(null, "心情:积极", "心情可以被普遍地分为积极、中性和消极三种种，积极代表着人们正面的情绪。", baseEmotionTagObjectId, secondLevelEmtionTagsAncestors, null, null),
                new Models.Tag(null, "心情:中性", "心情可以被普遍地分为积极、中性和消极三种种，中性代表着人们中性面的情绪。", baseEmotionTagObjectId, secondLevelEmtionTagsAncestors, null, null),
                new Models.Tag(null, "心情:消极", "心情可以被普遍地分为积极、中性和消极三种种，消极代表着人们负面的情绪。", baseEmotionTagObjectId, secondLevelEmtionTagsAncestors, null, null),
                new Models.Tag(null, "心情:生气", "六种基本情绪之一", baseEmotionTagObjectId, secondLevelEmtionTagsAncestors, null, null),
                new Models.Tag(null, "心情:厌恶", "六种基本情绪之一", baseEmotionTagObjectId, secondLevelEmtionTagsAncestors, null, null),
                new Models.Tag(null, "心情:害怕", "六种基本情绪之一", baseEmotionTagObjectId, secondLevelEmtionTagsAncestors, null, null),
                new Models.Tag(null, "心情:悲伤", "六种基本情绪之一", baseEmotionTagObjectId, secondLevelEmtionTagsAncestors, null, null),
                new Models.Tag(null, "心情:开心", "六种基本情绪之一", baseEmotionTagObjectId, secondLevelEmtionTagsAncestors, null, null),
                new Models.Tag(null, "心情:惊讶", "六种基本情绪之一", baseEmotionTagObjectId, secondLevelEmtionTagsAncestors, null, null),
                new Models.Tag(null, "心情:知足", "扩充情绪之一", baseEmotionTagObjectId, secondLevelEmtionTagsAncestors, null, null),
                new Models.Tag(null, "心情:尴尬", "扩充情绪之一", baseEmotionTagObjectId, secondLevelEmtionTagsAncestors, null, null),
                new Models.Tag(null, "心情:兴奋", "扩充情绪之一", baseEmotionTagObjectId, secondLevelEmtionTagsAncestors, null, null),
                new Models.Tag(null, "心情:内疚", "扩充情绪之一", baseEmotionTagObjectId, secondLevelEmtionTagsAncestors, null, null),
                new Models.Tag(null, "心情:自豪", "扩充情绪之一", baseEmotionTagObjectId, secondLevelEmtionTagsAncestors, null, null),
                new Models.Tag(null, "心情:松口气", "扩充情绪之一", baseEmotionTagObjectId, secondLevelEmtionTagsAncestors, null, null),
                new Models.Tag(null, "心情:满意", "扩充情绪之一", baseEmotionTagObjectId, secondLevelEmtionTagsAncestors, null, null),
                new Models.Tag(null, "心情:羞愧", "扩充情绪之一", baseEmotionTagObjectId, secondLevelEmtionTagsAncestors, null, null),
                new Models.Tag(null, "心情:崇拜", "扩充情绪之一", baseEmotionTagObjectId, secondLevelEmtionTagsAncestors, null, null),
                new Models.Tag(null, "心情:挫败", "扩充情绪之一", baseEmotionTagObjectId, secondLevelEmtionTagsAncestors, null, null),
                new Models.Tag(null, "心情:痛苦", "扩充情绪之一", baseEmotionTagObjectId, secondLevelEmtionTagsAncestors, null, null),
                new Models.Tag(null, "心情:忧虑", "扩充情绪之一", baseEmotionTagObjectId, secondLevelEmtionTagsAncestors, null, null),
                new Models.Tag(null, "心情:疏离", "扩充情绪之一", baseEmotionTagObjectId, secondLevelEmtionTagsAncestors, null, null),
                new Models.Tag(null, "心情:烦恼", "扩充情绪之一", baseEmotionTagObjectId, secondLevelEmtionTagsAncestors, null, null),
                new Models.Tag(null, "心情:顾虑", "扩充情绪之一", baseEmotionTagObjectId, secondLevelEmtionTagsAncestors, null, null),
                new Models.Tag(null, "心情:担心", "扩充情绪之一", baseEmotionTagObjectId, secondLevelEmtionTagsAncestors, null, null),
                new Models.Tag(null, "心情:开怀", "扩充情绪之一", baseEmotionTagObjectId, secondLevelEmtionTagsAncestors, null, null),
                new Models.Tag(null, "心情:陶醉", "扩充情绪之一", baseEmotionTagObjectId, secondLevelEmtionTagsAncestors, null, null),
                new Models.Tag(null, "心情:沮丧", "扩充情绪之一", baseEmotionTagObjectId, secondLevelEmtionTagsAncestors, null, null),
                new Models.Tag(null, "心情:渴望", "扩充情绪之一", baseEmotionTagObjectId, secondLevelEmtionTagsAncestors, null, null),
                new Models.Tag(null, "心情:反感", "扩充情绪之一", baseEmotionTagObjectId, secondLevelEmtionTagsAncestors, null, null),
                new Models.Tag(null, "心情:热心", "扩充情绪之一", baseEmotionTagObjectId, secondLevelEmtionTagsAncestors, null, null),
                new Models.Tag(null, "心情:狂喜", "扩充情绪之一", baseEmotionTagObjectId, secondLevelEmtionTagsAncestors, null, null),
                new Models.Tag(null, "心情:享受", "扩充情绪之一", baseEmotionTagObjectId, secondLevelEmtionTagsAncestors, null, null),
                new Models.Tag(null, "心情:敬畏", "扩充情绪之一", baseEmotionTagObjectId, secondLevelEmtionTagsAncestors, null, null),
                new Models.Tag(null, "心情:绝望", "扩充情绪之一", baseEmotionTagObjectId, secondLevelEmtionTagsAncestors, null, null),
                new Models.Tag(null, "心情:惊吓", "扩充情绪之一", baseEmotionTagObjectId, secondLevelEmtionTagsAncestors, null, null),
                new Models.Tag(null, "心情:愁闷", "扩充情绪之一", baseEmotionTagObjectId, secondLevelEmtionTagsAncestors, null, null),
                new Models.Tag(null, "心情:懊恼", "扩充情绪之一", baseEmotionTagObjectId, secondLevelEmtionTagsAncestors, null, null),
                new Models.Tag(null, "心情:灰心", "扩充情绪之一", baseEmotionTagObjectId, secondLevelEmtionTagsAncestors, null, null),
                new Models.Tag(null, "心情:孤独", "扩充情绪之一", baseEmotionTagObjectId, secondLevelEmtionTagsAncestors, null, null),


            };
            session.StartTransaction();
            repository.AddAsync(baseEmotionTag).GetAwaiter().GetResult();
            foreach (var tag in secondLevelEmotionTags)
            {
                repository.AddAsync(tag).GetAwaiter().GetResult();
            }
            session.CommitTransaction();
            logger.LogInformation("--------------- Finish add seed datas for emotion tags ------------");
            return builder;
        }

        public static IApplicationBuilder SeedDefaultMusicTags(this IApplicationBuilder builder)
        {
            var context = builder.ApplicationServices.GetRequiredService<TagSMongoDBContext>();
            var session = builder.ApplicationServices.GetRequiredService<IClientSessionHandle>();

            var emotionTag = context.Tags.Find(t => t.PreferredTagName == "音乐").ToList();
            if (emotionTag.Any())
                return builder;

            var mediator = builder.ApplicationServices.GetRequiredService<IMediator>();
            var logger = builder.ApplicationServices.GetRequiredService<ILogger<IApplicationBuilder>>();

            logger.LogInformation("--------------- Start add seed datas for music tags ------------");
            var repository = builder.ApplicationServices.GetRequiredService<ITagRepository>();

            var baseMusicTagName = "音乐";
            var baseMusicTagObjectId = ObjectId.GenerateNewId().ToString();
            var baseMusicTag = new Models.Tag(baseMusicTagObjectId, baseMusicTagName, "音乐标签", null, null, new List<string> { "Music" }, null);
            var secondMusicTagAncestors = new List<string>() { baseMusicTagObjectId };

            var baseMusicEmotionTagName = $"{baseMusicTagName}:心情";
            var baseMusicEmotionTagObjectId = ObjectId.GenerateNewId().ToString();
            var baseMusicEmotionTag = new Models.Tag(baseMusicEmotionTagObjectId, baseMusicEmotionTagName, "音乐是具有情绪的。", baseMusicTagObjectId, secondMusicTagAncestors, null, null);
            var thirdMusicEmotionTagAncestors = new List<string>() { baseMusicTagObjectId, baseMusicEmotionTagObjectId };

            var thirdMusicEmotionTags = new List<Models.Tag>
            {
                new Models.Tag(null, $"{baseMusicEmotionTagName}:伤感", "伤感音乐标签", baseMusicEmotionTagObjectId, thirdMusicEmotionTagAncestors, null, null),
                new Models.Tag(null, $"{baseMusicEmotionTagName}:安静", "安静音乐标签", baseMusicEmotionTagObjectId, thirdMusicEmotionTagAncestors, null, null),
                new Models.Tag(null, $"{baseMusicEmotionTagName}:快乐", "快乐音乐标签", baseMusicEmotionTagObjectId, thirdMusicEmotionTagAncestors, null, null),
                new Models.Tag(null, $"{baseMusicEmotionTagName}:治愈", "治愈音乐标签", baseMusicEmotionTagObjectId, thirdMusicEmotionTagAncestors, null, null),
                new Models.Tag(null, $"{baseMusicEmotionTagName}:励志", "励志音乐标签", baseMusicEmotionTagObjectId, thirdMusicEmotionTagAncestors, null, null),
                new Models.Tag(null, $"{baseMusicEmotionTagName}:甜蜜", "甜蜜音乐标签", baseMusicEmotionTagObjectId, thirdMusicEmotionTagAncestors, null, null),
                new Models.Tag(null, $"{baseMusicEmotionTagName}:寂寞", "寂寞音乐标签", baseMusicEmotionTagObjectId, thirdMusicEmotionTagAncestors, null, null),
                new Models.Tag(null, $"{baseMusicEmotionTagName}:宣泄", "宣泄音乐标签", baseMusicEmotionTagObjectId, thirdMusicEmotionTagAncestors, null, null),
                new Models.Tag(null, $"{baseMusicEmotionTagName}:思念", "思念音乐标签", baseMusicEmotionTagObjectId, thirdMusicEmotionTagAncestors, null, null),

            };

            var baseMusicSceneTagName = $"{baseMusicTagName}:场景";
            var baseMusicSceneTagObjectId = ObjectId.GenerateNewId().ToString();
            var baseMusicSceeTag = new Models.Tag(baseMusicSceneTagObjectId, baseMusicSceneTagName, "不同场景下的音乐。", baseMusicTagObjectId, secondMusicTagAncestors, null, null);
            var thirdMusicSceneTagAncestors = new List<string>() { baseMusicTagObjectId, baseMusicSceneTagObjectId };

            var thirdMusicSceneTags = new List<Models.Tag>
            {
                new Models.Tag(null, $"{baseMusicSceneTagName}:睡前", "睡前场景音乐标签", baseMusicSceneTagObjectId, thirdMusicSceneTagAncestors, null, null),
                new Models.Tag(null, $"{baseMusicSceneTagName}:夜店", "夜店场景音乐标签", baseMusicSceneTagObjectId, thirdMusicSceneTagAncestors, null, null),
                new Models.Tag(null, $"{baseMusicSceneTagName}:学习", "学习场景音乐标签", baseMusicSceneTagObjectId, thirdMusicSceneTagAncestors, null, null),
                new Models.Tag(null, $"{baseMusicSceneTagName}:运动", "运动场景音乐标签", baseMusicSceneTagObjectId, thirdMusicSceneTagAncestors, null, null),
                new Models.Tag(null, $"{baseMusicSceneTagName}:开车", "开车场景音乐标签", baseMusicSceneTagObjectId, thirdMusicSceneTagAncestors, null, null),
                new Models.Tag(null, $"{baseMusicSceneTagName}:约会", "约会场景音乐标签", baseMusicSceneTagObjectId, thirdMusicSceneTagAncestors, null, null),
                new Models.Tag(null, $"{baseMusicSceneTagName}:工作", "工作场景音乐标签", baseMusicSceneTagObjectId, thirdMusicSceneTagAncestors, null, null),
                new Models.Tag(null, $"{baseMusicSceneTagName}:旅行", "旅行场景音乐标签", baseMusicSceneTagObjectId, thirdMusicSceneTagAncestors, null, null),
                new Models.Tag(null, $"{baseMusicSceneTagName}:派对", "派对场景音乐标签", baseMusicSceneTagObjectId, thirdMusicSceneTagAncestors, null, null),
                new Models.Tag(null, $"{baseMusicSceneTagName}:婚礼", "婚礼场景音乐标签", baseMusicSceneTagObjectId, thirdMusicSceneTagAncestors, null, null),
                new Models.Tag(null, $"{baseMusicSceneTagName}:咖啡馆", "咖啡馆场景音乐标签", baseMusicSceneTagObjectId, thirdMusicSceneTagAncestors, null, null),
                new Models.Tag(null, $"{baseMusicSceneTagName}:跳舞", "跳舞场景音乐标签", baseMusicSceneTagObjectId, thirdMusicSceneTagAncestors, null, null),
                new Models.Tag(null, $"{baseMusicSceneTagName}:校园", "校园场景音乐标签", baseMusicSceneTagObjectId, thirdMusicSceneTagAncestors, null, null),

            };

            var baseMusicGenreTagName = $"{baseMusicTagName}:流派";
            var baseMusicGenreTagObjectId = ObjectId.GenerateNewId().ToString();
            var baseMusicGenreTag = new Models.Tag(baseMusicGenreTagObjectId, baseMusicGenreTagName, "不同流派的音乐。", baseMusicTagObjectId, secondMusicTagAncestors, null, null);
            var thirdMusicGenreTagAncestors = new[] { baseMusicTagObjectId, baseMusicGenreTagObjectId };

            var tagNamesGenre = new[]
            {
                "流行","轻音乐","摇滚","民谣","R&B","嘻哈","电子","古典","乡村","蓝调","爵士","新世纪","拉丁","后摇","中国传统","世界音乐"
            };
            var tagDetailsGenre = tagNamesGenre.Select(tn => $"{tn}流派音乐标签").ToArray();
            var thirdMusicGenreTags = GetMultiLevelTags(null, baseMusicGenreTagObjectId, baseMusicGenreTagName, tagNamesGenre, tagDetailsGenre, thirdMusicGenreTagAncestors);

            var baseMusicThemeTagName = $"{baseMusicTagName}:主题";
            var baseMusicThemeTagObjectId = ObjectId.GenerateNewId().ToString();
            var baseMusicThemeTag = new Models.Tag(baseMusicThemeTagObjectId, baseMusicThemeTagName, "不同主题的音乐。", baseMusicTagObjectId, secondMusicTagAncestors, null, null);
            var thirdMusicThemeTagAncestors = new[] { baseMusicTagObjectId, baseMusicThemeTagObjectId };

            var tagNamesTheme = new[]
            {
                "ACG","经典","网络歌曲","影视","KTV热歌","儿歌","中国风","古风","情歌","城市","现场音乐","背景音乐","佛教音乐","UP主","乐器","DJ"
            };
            var tagDetailsTheme = tagNamesTheme.Select(tn => $"{tn}主题音乐标签").ToArray();
            var thirdMusicThemeTags = GetMultiLevelTags(null, baseMusicThemeTagObjectId, baseMusicThemeTagName, tagNamesTheme, tagDetailsTheme, thirdMusicThemeTagAncestors);

            session.StartTransaction();
            repository.AddAsync(baseMusicTag).GetAwaiter().GetResult();
            repository.AddAsync(baseMusicSceeTag).GetAwaiter().GetResult();
            repository.AddAsync(baseMusicEmotionTag).GetAwaiter().GetResult();
            repository.AddAsync(baseMusicGenreTag).GetAwaiter().GetResult();
            repository.AddAsync(baseMusicThemeTag).GetAwaiter().GetResult();
            foreach (var tag in thirdMusicEmotionTags)
            {
                repository.AddAsync(tag).GetAwaiter().GetResult();
            }
            foreach (var tag in thirdMusicSceneTags)
            {
                repository.AddAsync(tag).GetAwaiter().GetResult();
            }
            foreach (var tag in thirdMusicGenreTags)
            {
                repository.AddAsync(tag).GetAwaiter().GetResult();
            }
            foreach (var tag in thirdMusicThemeTags)
            {
                repository.AddAsync(tag).GetAwaiter().GetResult();
            }
            session.CommitTransaction();
            logger.LogInformation("--------------- Finish add seed datas for music tags ------------");
            return builder;
        }

        private static IEnumerable<Models.Tag> GetMultiLevelTags(string? id,string preTagId,string preTagName,string[] tagNames,string[] tagDetails,string[] tagAncestors)
        {
            var tags= tagNames.Select((tn, i) =>
            {
                return new Models.Tag(id, $"{preTagName}:{tn}", tagDetails[i], preTagId, tagAncestors.ToList(), null, null);
            });

            return tags;
        }
    }
}