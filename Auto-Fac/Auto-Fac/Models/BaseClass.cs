using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Auto_Fac.Models
{
    public class BaseClass
    {
        public int id { get; set; }
        [MaxLength(256)]
        public string? Name { get; set; }
       [DefaultValue(1)] 
        public int? Status { get; set; }
    }
}