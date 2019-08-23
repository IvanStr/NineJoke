// ReSharper disable VirtualMemberCallInConstructor
namespace NineJoke.Data.Models
{
    using System;
    using System.Collections.Generic;

    using NineJoke.Data.Common.Models;

    using Microsoft.AspNetCore.Identity;

    using System.ComponentModel.DataAnnotations.Schema;

    public class ApplicationUser : IdentityUser, IAuditInfo, IDeletableEntity
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Roles = new HashSet<IdentityUserRole<string>>();
            this.Claims = new HashSet<IdentityUserClaim<string>>();
            this.Logins = new HashSet<IdentityUserLogin<string>>();
            this.Posts = new HashSet<Post>();
            this.Comments = new HashSet<Comment>();
            this.VotedUpPosts = new HashSet<VotePost>();
            this.VotedUpComments = new HashSet<VoteComment>();
            this.SentPMs = new HashSet<PrivateMessage>();
            this.ReceivedPMs = new HashSet<PrivateMessage>();
        }

        // Audit info
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }

        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }

        public int PostCount { get; set; }

        public int CommentCount { get; set; }

        public int AllPostsVoteCount { get; set; }

        public int RemovedPosts { get; set; }

        public ICollection<Post> Posts { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public ICollection<VotePost> VotedUpPosts { get; set; }

        public ICollection<VoteComment> VotedUpComments { get; set; }

        [InverseProperty("Sender")]
        public ICollection<PrivateMessage> SentPMs { get; set; }

        [InverseProperty("Receiver")]
        public ICollection<PrivateMessage> ReceivedPMs { get; set; }
    }
}
