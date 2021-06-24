using System;
using System.Collections.Generic;
using System.Text;

namespace GetInTouch.Logic.ViewModels.User
{
    public class UserDetailsViewModel
    {
        public Guid UserId { get; set; }
        public DateTime Birthday { get; set; }
        public string LivesIn { get; set; }
        public string IsFrom { get; set; }
        public string WorksAt { get; set; }
        public string Email { get; set; }
        public string Colleague { get; set; }
        public string HighSchool { get; set; }
        public string PhoneNo { get; set; }
        public string Gender { get; set; }
        public string FullName { get; set; }
        public string ProfileImage { get; set; }
    }
}
