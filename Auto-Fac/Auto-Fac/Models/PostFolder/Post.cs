using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Auto_Fac.Models
{
    public class Post
    {
        public int id { get; set; }
        [MaxLength(256)]
        public string Name { get; set; }
        public string Text { get; set; }
        [MaxLength(256)]
        public string photo { get; set; }
        [MaxLength(256)]
        public string title { get; set; }
        [DefaultValue(1)]
        public int Status { get; set; }

        public int idFaculty { get; set; }
        public DateTime DateTime { get; set; }
    }
}