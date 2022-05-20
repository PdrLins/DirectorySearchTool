using DirectorySearchTool.Infrasctructure.Data;
using FluentValidation;
using System.Linq;

namespace DirectorySearchTool.Application.Features.Members.Commands
{
    public class AddFriendToMeberCommandValidator : AbstractValidator<AddFriendToMeberCommand>
    {
        public AddFriendToMeberCommandValidator(IApiContext context)
        {
            RuleFor(v => v.MeId)
                .GreaterThan(0)
                .MustAsync(async (meId, token) => await context.Members.FindAsync(meId) != null)
                .WithMessage(m => $"Member with id {m.MeId} does not exist.")
                .Must((v, f) => !v.FriendToAddIds.Contains(v.MeId))
                .WithMessage(m => $"Member with id {m.MeId} can not be in the list of friends");

            RuleForEach(v => v.FriendToAddIds)
                .NotEmpty()
                .WithMessage(v => "Friends can not be empty.")
                .MustAsync(async (friendId, toke) => await context.Members.FindAsync(friendId) != null)
                .WithMessage($"Member does not exist.");
 
        }
    }
}
