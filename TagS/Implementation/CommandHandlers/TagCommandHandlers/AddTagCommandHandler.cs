using Innermost.IdempotentCommand;
using Innermost.IdempotentCommand.Infrastructure.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagS.Implementation.Commands;
using TagS.Implementation.Commands.TagCommands;
using TagS.Infrastructure.Repositories.Abstractions;

namespace TagS.Implementation.CommandHandlers.TagCommandHandlers
{
    internal class AddTagCommandHandler<TPersistence> : IRequestHandler<AddTagCommand<TPersistence>, bool>
    {
        private readonly ITagRepository<TPersistence> _tagRepository;
        public AddTagCommandHandler(ITagRepository<TPersistence> tagRepository)
        {
            _tagRepository = tagRepository;
        }
        public async Task<bool> Handle(AddTagCommand<TPersistence> request, CancellationToken cancellationToken)
        {
            //if existed or this tag can be as a synonym of a existed tag judege by AI.return false
            //Tip:如何做到对修改关闭对扩展开放呢？我一开始并没有人工智能的模块，后续若要加入又该如何实现呢？
            //我的想法是通过添加新的类实现 ITagRepository<TPersistence> 接口，这个实现接口为 TagRepositoryWithAI，那么我们不需要修改这里的 Existed 也不需要修改 TagRepository 里的 Existed。
            if (_tagRepository.Existed(request.Tag))
            {
                await Task.CompletedTask;
                return false;
            }

            await _tagRepository.AddAsync(request.Tag);

            return true;
        }
    }

    internal class IdempotentAddTagCommandHandler<TPersistence> : IdempotentCommandHandler<AddTagCommand<TPersistence>, bool>
    {
        public IdempotentAddTagCommandHandler(IMediator mediator, ICommandRequestRepository commandRequestRepository) : base(mediator, commandRequestRepository)
        {
        }
    }
}
