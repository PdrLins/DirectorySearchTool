using DirectorySearchTool.Application.Features.Members.Queries;
using DirectorySearchTool.Infrasctructure.Data;
using FluentValidation;
using System.Linq;

namespace DirectorySearchTool.Application.Features.Members.Commands
{
    public class GetMemberByIdQueryValidator : AbstractValidator<GetMemberByIdQuery>
    {
        public GetMemberByIdQueryValidator(IApiContext context)
        {
            RuleFor(v => v.Id)
                .GreaterThan(0)
                .MustAsync(async (meId, token) => await context.Members.FindAsync(meId) != null)
                .WithMessage(m => $"Member with id {m.Id} does not exist.");
 
        }
    }
}
