using GetInTouch.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace GetInTouch.Logic.ViewModels.Post
{
    public class RestrictViewingViewModel
    {
        public Guid PostId { get; set; }
        public RestrictViewingType RestrictViewingType { get; set; }
    }
}
