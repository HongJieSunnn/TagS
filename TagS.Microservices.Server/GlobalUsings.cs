﻿global using System;
global using System.Collections.Generic;
global using System.Linq;
global using System.Text;
global using System.Threading.Tasks;
global using MongoDB.Driver;
global using MongoDB.Bson;
global using MongoDB.Bson.Serialization.Attributes;
global using EventBusCommon.Abstractions;
global using Innermost.MongoDBContext;
global using DomainSeedWork;
global using Tag = TagS.Microservices.Server.Models.Tag;
global using TagS.Microservices.Server.Models;
global using TagS.Microservices.Server.Repositories.TagWithReferrerRepository;
global using System.Reflection;
global using DomainSeedWork.Abstractions;
global using TagS.Microservices.Server.DomainEvents;
global using MediatR;
global using TagS.Microservices.Server.Commands;
global using TagS.Microservices.Server.Repositories.TagReviewedRepository;
global using TagS.Microservices.Server.Repositories.TagRepository;
global using TagS.Microservices.Server.DomainEvents.TagReviewed;
global using Innermost.IdempotentCommand;
global using TagS.Microservices.Server.Queries.Models;
global using EventBusCommon;
global using TagS.Microservices.Server.IntegrationEventHandler.IntegrationEvents;
global using Microsoft.Extensions.Logging;
global using TagS.Core.Models;