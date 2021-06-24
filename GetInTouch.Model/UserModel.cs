using System;
using System.Collections.Generic;

namespace GetInTouch.Model
{
    public class UserModel
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        public string LivesIn { get; set; }
        public string Gender { get; set; }
        public string IsFrom { get; set; }
        public string WorksAt { get; set; }
        public string Email { get;  set; }
        public string Colleague { get; set; }
        public string HighSchool { get; set; }
        public string PhoneNo { get;  set; }
        public string ProfileImage { get; set; }
        public List<PostModel> Posts { get; set; }
    }
}
