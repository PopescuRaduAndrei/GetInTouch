using GetInTouch.Logic.ViewModels.Appreciation;
using GetInTouch.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace GetInTouch.Logic.ViewModels.Post
{
    public class DetailsPostViewModel
    {
        public PostModel Post { get; set; }
        public bool ShouldDisplayActions { get; set; }
        public AppreciationViewModel AppreciationDetails { get; set; }
    }
}
