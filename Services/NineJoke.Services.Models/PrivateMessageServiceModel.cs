﻿using NineJoke.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NineJoke.Data.Models
{
    public class PrivateMessageServiceModel : BaseModel<string>
    {
        public PrivateMessageServiceModel()
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
