using DirectorySearchTool.Core.Entities;
using DirectorySearchTool.Infrasctructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace DirectorySearchTool.Application.Features.Members.Queries
{
    public class GetNewFriendsByTagQuery : IRequest<IEnumerable<string>>
    {
        public int MeId { get; set; }
        public string Tag { get; set; }
    }
    public class GetNewFriendsByTagQueryHandler : IRequestHandler<GetNewFriendsByTagQuery, IEnumerable<string>>
    {
        private readonly IApiContext _context;
        public GetNewFriendsByTagQueryHandler(IApiContext apiContext)
        {
            _context = apiContext;
        }
        public async Task<IEnumerable<string>> Handle(GetNewFriendsByTagQuery request, CancellationToken cancellationToken)
        {

            var m1 = new Member { Name = "M1", Headings = new List<MemberHeading> { new MemberHeading { Heading = "Dog" }, new MemberHeading { Heading = "Cat" } } };
            var m2 = new Member { Name = "M2", Headings = new List<MemberHeading> { new MemberHeading { Heading = "House" }, new MemberHeading { Heading = "Condor" } } };
            var m3 = new Member { Name = "M3", Headings = new List<MemberHeading> { new MemberHeading { Heading = "Tesla" }, new MemberHeading { Heading = "Hyundai" } } };
            var m4 = new Member { Name = "M3", Headings = new List<MemberHeading> { new MemberHeading { Heading = "Pasta" }, new MemberHeading { Heading = "Sushi" } } };

            _context.Members.Add(m1);
            _context.Members.Add(m2);
            _context.Members.Add(m3);
            _context.Members.Add(m4);

            _context.Friendships.Add(new Friendship { FriendshipFrom = m1, FriendshipTo = m2 });
            _context.Friendships.Add(new Friendship { FriendshipFrom = m2, FriendshipTo = m3 });
            _context.Friendships.Add(new Friendship { FriendshipFrom = m3, FriendshipTo = m4 });

            await _context.SaveChangesAsync();

            List<string> breadcrumb = new List<string>();
            var member = await _context.Members
                         .Include(x => x.Headings)
                         .Include(x => x.ShortLink)
                         .Include(x => x.FriendshipFromFriend).ThenInclude(f => f.FriendshipFrom)
                         .Include(x => x.FriendshipFromFriend).ThenInclude(f => f.FriendshipTo) 
                         .Include(x => x.FriendshipFromMe).ThenInclude(f => f.FriendshipFrom) 
                         .Include(x => x.FriendshipFromMe).ThenInclude(f => f.FriendshipTo) 
                         .SingleOrDefaultAsync(w => w.Id == request.MeId);

            DeepSearch(m1.Id, member.FriendshipFromMe, "");

            return breadcrumb;
        }


        private string DeepSearch(int memberid,string term, IEnumerable<Friendship> leaf, string path)
        {
            //if (leaf.Count() == 0)
            //    return path;
            //foreach (var item in leaf.SelectMany(s=>s.FriendshipTo))
            //{
            //    if(item.FriendshipTo.Headings.Any(a=>a.Heading.ToLower() == term.ToLower()))
            //    {

            //    }
            //}
            return path;
        }
    }

}
