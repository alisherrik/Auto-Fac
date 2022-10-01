using System.ComponentModel.DataAnnotations;

namespace Auto_Fac.Models
{
    public class admin
    {
        public int id { get; set; }
        [MaxLength(256)]
        public string FirstName { get; set; }
        [MaxLength(256)]
        public string LastName { get; set; }
        [MaxLength(256)]
        public string login { get; set; }
        [MaxLength(256)]
        public string Password { get; set; }
        [MaxLength(256)]
        public string Email { get; set; }
        public Status status { get; set; }
        public int IdFaculty { get; set; }
    }
}