using System.Collections.Generic;

namespace DirectorySearchTool.Application.Features.Members.Queries
{
    public class GetMemberReponse
    {
        public string Name { get; set; }
        public string ShortUrl { get; set; }
        public int FriendQnt { get; set; }
        public GetMemberReponse()
        {
        }
    }
}
