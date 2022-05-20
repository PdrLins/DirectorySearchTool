using System.Collections.Generic;

namespace DirectorySearchTool.Core.Entities
{
    public class Member : Entity
    {
        public virtual string Name { get; set; }
        public virtual ShortLink ShortLink { get; set; }
        public virtual ICollection<Friendship> FriendshipFromFriend { get; set; }
        public virtual ICollection<Friendship> FriendshipFromMe { get; set; }
        public virtual ICollection<MemberHeading> Headings { get; set; }
        public Member()
        {
            Headings = new HashSet<MemberHeading>();
            FriendshipFromMe = new HashSet<Friendship>();
            FriendshipFromFriend = new HashSet<Friendship>();
            ShortLink = new ShortLink();
        }
    }
}

