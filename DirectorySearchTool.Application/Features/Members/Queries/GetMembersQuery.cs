using DirectorySearchTool.Core.Helpers;
using DirectorySearchTool.Infrasctructure.Data;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DirectorySearchTool.Application.Features.Members.Queries
{
    public class GetMembersQuery : IRequest<IEnumerable<GetMemberReponse>>
    {
    }
    public class GetMembersQueryHandler : IRequestHandler<GetMembersQuery, IEnumerable<GetMemberReponse>>
    {
        private readonly IApiContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetMembersQueryHandler(IApiContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;

        }
        public async Task<IEnumerable<GetMemberReponse>> Handle(GetMembersQuery request, CancellationToken cancellationToken)
        {
            var membersReponseList = new List<GetMemberReponse>();
            var members = await _context.Members
                .Include(i=> i.ShortLink)
                .Include(i => i.FriendshipFromFriend)
                .Include(i => i.FriendshipFromMe).ToListAsync();

            foreach (var member in members)
            {
                var memberReponse = new GetMemberReponse
                {
                    Name   = member.Name,
                    ShortUrl = $"{_httpContextAccessor.HttpContext.Request.Scheme}//{_httpContextAccessor.HttpContext.Request.Host.Value}/{ShortLinkHelper.Encode(member.ShortLink.Id)}",
                    FriendQnt = member.FriendshipFromFriend.Count() + member.FriendshipFromMe.Count(),
                };
                membersReponseList.Add(memberReponse);
            }
            return membersReponseList;
        }
    }

}
