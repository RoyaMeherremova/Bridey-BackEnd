﻿using System.ComponentModel.DataAnnotations;

namespace BrideyApp.Areas.Admin.ViewModels.Author
{
    public class AuthorCreateVM
    {
        [Required(ErrorMessage = "Don't be empty")]
        public string Name { get; set; }
    }
}
