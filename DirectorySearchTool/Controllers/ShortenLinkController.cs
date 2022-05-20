using DirectorySearchTool.Application.Features.ShortenLinks.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace DirectorySearchTool.Controllers
{
    public class ShortenLinkController : ApiController
    {
        [HttpGet("{id}")]
        public async Task<string> GetById([FromRoute] string id, CancellationToken cancellationToken)
             => await Mediator.Send(new GetUrlByShorteLinkIdQuery { ShortUrl = id }, cancellationToken);

    }
}
