using System;
using System.ComponentModel.DataAnnotations;
using Auto_Fac.Models;
using Microsoft.AspNetCore.Http;

namespace Auto_Fac.ViewModels
{
    public class CreatePostViewModel
    {
        public int id { get; set; }
        [MaxLength(256)]
        public string Name { get; set; }
        public string Text { get; set; }
        public IFormFile photo { get; set; }
        [MaxLength(256)]
        public string title { get; set; }

        public Status Status { get; set; }
        public DateTime DateTime { get; set; }
    }
}