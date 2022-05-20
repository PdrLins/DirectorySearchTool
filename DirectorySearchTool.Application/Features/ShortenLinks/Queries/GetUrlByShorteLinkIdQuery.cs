using DirectorySearchTool.Infrasctructure.Data;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DirectorySearchTool.Application.Features.ShortenLinks.Queries
{
    public class GetUrlByShorteLinkIdQuery : IRequest<string>
    {
        public string ShortUrl { get; set; }
    }

    public class GetUrlByShorteLinkIdQueryHandler : IRequestHandler<GetUrlByShorteLinkIdQuery, string>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly IApiContext _context;
        public GetUrlByShorteLinkIdQueryHandler(IApiContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<string> Handle(GetUrlByShorteLinkIdQuery request, CancellationToken cancellationToken)
        {
            var id = BitConverter.ToInt32(WebEncoders.Base64UrlDecode(request.ShortUrl));
            var shortLinkEntity = await _context.ShortLinks.Where(w => w.Id == id).SingleOrDefaultAsync();//.FirstOrDefault(f => f.Id == id);
            if (shortLinkEntity != null)
                return shortLinkEntity.Url;
            else
                return "";

        }
    }

}
