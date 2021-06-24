using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace GetInTouch.Logic.ViewModels.Post
{
    public class AddPostViewModel
    {
        public string Description { get; set; }
        public IFormFile Image { get; set; }
    }
}
