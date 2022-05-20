using DirectorySearchTool.Application.Common.Exceptions;
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
    public class GetMemberByIdQuery : IRequest<GetMemberByIdReponse>
    {
        public int Id { get; set; }
    }
    
    /* 
         Viewing an actual member should display the name, website URL, shortening, website headings, and links to their friends' pages.
     */
    public class GetClientByIdQueryHandler : IRequestHandler<GetMemberByIdQuery, GetMemberByIdReponse>
    {
        private readonly IApiContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetClientByIdQueryHandler(IApiContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<GetMemberByIdReponse> Handle(GetMemberByIdQuery request, CancellationToken cancellationToken)
        {
            var member = await _context.Members
                .Include(x => x.Headings)
                .Include(x => x.ShortLink)
                .Include(x => x.FriendshipFromFriend).ThenInclude(f => f.FriendshipFrom).ThenInclude(x => x.ShortLink)
                .Include(x => x.FriendshipFromFriend).ThenInclude(f => f.FriendshipTo).ThenInclude(x => x.ShortLink)
                .Include(x => x.FriendshipFromMe).ThenInclude(f => f.FriendshipFrom).ThenInclude(x => x.ShortLink)
                .Include(x => x.FriendshipFromMe).ThenInclude(f => f.FriendshipTo).ThenInclude(x => x.ShortLink)
                .SingleOrDefaultAsync(w => w.Id == request.Id);

            if (member == null)
                throw new FindMemberException($"Could find a member with id {request.Id}");


            var fromFriend = member.FriendshipFromFriend.Select(s => new FriendLinkReponse { Name = s.FriendshipFrom.Name, Url = s.FriendshipFrom.ShortLink.Url });
            var FromMe = member.FriendshipFromMe.Select(s => new FriendLinkReponse { Name = s.FriendshipTo.Name, Url = s.FriendshipTo.ShortLink.Url });

            var response = new GetMemberByIdReponse
            {
                Name = member.Name,
                ShortUrl = $"{_httpContextAccessor.HttpContext.Request.Scheme}//{_httpContextAccessor.HttpContext.Request.Host.Value}/{ShortLinkHelper.Encode(member.ShortLink.Id)}",
                Url = member.ShortLink.Url,
                Headings = member.Headings.Select(s => s.Heading),
                FriendsWebSite = new List<FriendLinkReponse>(fromFriend.Concat(FromMe))

            };

            return response;

        }


    }

}