namespace NineJoke.Data.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

    using NineJoke.Data.Common.Models;

    public class PrivateMessage : BaseModel<string>
    {
        public PrivateMessage()
        {
            this.Seen = false;
        }

        public string Caption { get; set; }

        public string Content { get; set; }

        public bool Seen { get; set; }

        public string SenderId { get; set; }
        [ForeignKey("SenderId")]
        public ApplicationUser Sender { get; set; }

        public string ReceiverId { get; set; }
        [ForeignKey("ReceiverId")]
        public ApplicationUser Receiver { get; set; }
    }
}
