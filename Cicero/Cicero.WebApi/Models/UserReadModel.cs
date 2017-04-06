using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cicero.WebApi.Models
{
    public class UserReadModel
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}