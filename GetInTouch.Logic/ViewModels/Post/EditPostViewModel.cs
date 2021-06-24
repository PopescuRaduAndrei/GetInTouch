using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace GetInTouch.Logic.ViewModels.Post
{
    public class EditPostViewModel
    {
        public Guid PostId { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public IFormFile NewImage { get; set; }
    }
}
