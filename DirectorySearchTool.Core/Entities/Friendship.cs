namespace DirectorySearchTool.Core.Entities
{
    public class Friendship
    {
        public int FriendshipFromId { get; set; }
        public Member FriendshipFrom { get; set; }
        public int FriendshipToId { get; set; }
        public Member FriendshipTo { get; set; }

    }
}

