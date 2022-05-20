using DirectorySearchTool.Application.Common.Exceptions;
using DirectorySearchTool.Core.Entities;
using DirectorySearchTool.Infrasctructure.Data;
using HtmlAgilityPack;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace DirectorySearchTool.Application.Features.Members.Commands
{
    public class CreateMemberCommand : IRequest<int>
    {
        public string Name { get; set; }
        public string PersonalWebsiteUrl { get; set; }
    }

    public class CreateMemberCommandHandler : IRequestHandler<CreateMemberCommand, int>
    {
        private readonly IApiContext _context;
        public CreateMemberCommandHandler(IApiContext context)
        {
            _context = context;
        }
        public async Task<int> Handle(CreateMemberCommand request, CancellationToken cancellationToken)
        {
            var url = (request.PersonalWebsiteUrl.IndexOf("http") == -1 ? $"https://{request.PersonalWebsiteUrl}" : request.PersonalWebsiteUrl).Trim();

            if (!Uri.TryCreate(url, UriKind.Absolute, out Uri result))
                throw new UnknowUrlException($"We could not recognize the url=> {url} as userfull url.");

            var newMember = new Member
            {
                Name = request.Name,
                ShortLink = new ShortLink { Url = url },
            };
            var hs = await ParseUrl(url);
            foreach (var item in hs)
            {
                newMember.Headings.Add(new MemberHeading { Heading = item });
            }
            _context.Members.Add(newMember);
            await _context.SaveChangesAsync();
            return newMember.Id;
        }
        private async Task<List<string>> ParseUrl(string url)
        {
            List<string> hs = new List<string> { "h1", "h2", "h3" };
            var html = await CallUrl(url);
            HtmlDocument htmldoc = new HtmlDocument();
            htmldoc.LoadHtml(html);
            var hText = new List<string>();
            foreach (var h in hs)
            {
                var hNodes = htmldoc.DocumentNode.SelectNodes($"//body//{h}");
                if (hNodes is null)
                    continue;
                foreach (var h1 in hNodes)
                    hText.Add(h1.InnerText);
            }
            return hText.Where(w => !string.IsNullOrEmpty(w)).ToList();
        }
        private static async Task<string> CallUrl(string fullUrl)
        {
            HttpClient client = new HttpClient();
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls13;
            client.DefaultRequestHeaders.Accept.Clear();

            var response = client.GetStringAsync(fullUrl);
            return await response;
        }
    }
}
