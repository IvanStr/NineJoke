﻿using NineJoke.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NineJoke.Data.Models
{
    public class ReportPostServiceModel : BaseModel<string>
    {
        public string Reason { get; set; }

        public string UploaderId { get; set; }
        public ApplicationUser Uploader { get; set; }

        public string ReporterId { get; set; }
        public ApplicationUser Reporter { get; set; }

        public string PostId { get; set; }
        public Post Post { get; set; }
    }
}
