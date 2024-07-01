using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INFT6009.Models
{
    public class UserModel
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AccountType { get; set; }

        public bool IsFetcher
        {
            get
            {
                return AccountType?.Equals("fetcher") ?? false;
            }
        }
    }
}
