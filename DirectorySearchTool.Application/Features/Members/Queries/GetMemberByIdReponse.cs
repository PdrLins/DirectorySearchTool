using System.Collections.Generic;

namespace DirectorySearchTool.Application.Features.Members.Queries
{
    public class GetMemberByIdReponse
    {
        public string Name { get; set; }
        public string ShortUrl { get; set; }
        public string Url { get; set; }
        public IEnumerable<string> Headings { get; set; }
        public IEnumerable<FriendLinkReponse> FriendsWebSite { get; set; }
        public GetMemberByIdReponse()
        {
            Headings = new List<string>();
            FriendsWebSite = new List<FriendLinkReponse>();
        }
    }

    public class FriendLinkReponse
    {
        public string Name { get; set; }
        public string Url { get; set; }
    }
}
