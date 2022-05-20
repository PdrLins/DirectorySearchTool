using DirectorySearchTool.Application.Features.Members.Commands;
using DirectorySearchTool.Application.Features.Members.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DirectorySearchTool.Controllers
{
    [Route("api/[Controller]")]
    public class MemberController : ApiController
    {
        [HttpGet]
        public async Task<IEnumerable<GetMemberReponse>> Get([FromQuery] GetMembersQuery query, CancellationToken cancellationToken)
             => await Mediator.Send(query, cancellationToken);


        [HttpGet("{id}")]
        public async Task<GetMemberByIdReponse> GetById([FromRoute] int id, CancellationToken cancellationToken)
             => await Mediator.Send(new GetMemberByIdQuery { Id = id }, cancellationToken);

        [HttpPost]
        public async Task<int> Create([FromBody] CreateMemberCommand command, CancellationToken cancellationToken)
            => await Mediator.Send(command, cancellationToken);


        [HttpPut("{id}/add-friend")]
        public async Task<Unit> Update([FromRoute] int id, [FromBody] AddFriendToMeberCommand command, CancellationToken cancellationToken)
        {
            command.MeId = id;
            return await Mediator.Send(command, cancellationToken);
        }

        [HttpPut("{id}/find-friend/{term}")]
        public async Task<IEnumerable<string>> Get([FromRoute] int id, [FromRoute] string term, CancellationToken cancellationToken)
            => await Mediator.Send(new GetNewFriendsByTagQuery { MeId = id, Tag = term }, cancellationToken);
    }
}
