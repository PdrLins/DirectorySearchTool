using DirectorySearchTool.Application.Features.Members.Queries;
using DirectorySearchTool.Infrasctructure.Data;
using FluentValidation; 

namespace DirectorySearchTool.Application.Features.Members.Commands
{
    public class GetNewFriendsByTagQueryValidator : AbstractValidator<GetNewFriendsByTagQuery>
    {
        public GetNewFriendsByTagQueryValidator(IApiContext context)
        {
            //RuleFor(v => v.MeId)
            //    .GreaterThan(0)
            //    .MustAsync(async (meId, token) => await context.Members.FindAsync(meId) != null)
            //    .WithMessage(m => $"Member with id {m.MeId} does not exist.");
 
        }
    }
}
