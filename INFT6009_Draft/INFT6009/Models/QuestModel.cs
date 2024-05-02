using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INFT6009.Models
{
    public class QuestModel
    {
        public string UserId { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string When { get; set; }

        public DateTime Date
        {
            get
            {
                return DateTime.Parse(When);
            }
        }
    }
}
