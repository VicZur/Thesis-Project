using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeardHospitality.Models
{
    public class DBReplyModel
    {
        public int ReplyID { get; }

        public string ReplyContent { get; set; }

        public DateTime DatePosted { get; set; }

        public bool IsDisplayed { get; set; }

        public int RatingID { get; set; }

        public int BusinessRegID { get; set; }

    }
}
