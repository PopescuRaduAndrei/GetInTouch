using System;
using System.Collections.Generic;
using System.Text;

namespace GetInTouch.Logic.ViewModels.User
{
    public class ChangeProfilePictureViewModel
    {
        public IEnumerable<string> UserImages { get; set; }
        public string StringImages { get; set; }
        public string SelectedImage { get; set; }
    }
}
