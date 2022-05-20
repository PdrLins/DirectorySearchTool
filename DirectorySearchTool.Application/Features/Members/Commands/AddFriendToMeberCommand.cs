using DirectorySearchTool.Core.Entities;
using DirectorySearchTool.Infrasctructure.Data;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace DirectorySearchTool.Application.Features.Members.Commands
{
    public class AddFriendToMeberCommand : IRequest<Unit>
    {
        public IEnumerable<int> FriendToAddIds { get; set; }
        public int MeId { get; set; }
    }
    public class AddFriendToMeberCommandHandler : IRequestHandler<AddFriendToMeberCommand, Unit>
    {
        private readonly IApiContext _context;
        public AddFriendToMeberCommandHandler(IApiContext context)
        {
            _context = context;
        }
        public async Task<Unit> Handle(AddFriendToMeberCommand request, CancellationToken cancellationToken)
        {
            foreach (var friendId in request.FriendToAddIds.Distinct())
                await _context.Friendships.AddAsync(new Friendship { FriendshipFromId = request.MeId, FriendshipToId = friendId });

            await _context.SaveChangesAsync();
            return Unit.Value;
        }
    }
}
