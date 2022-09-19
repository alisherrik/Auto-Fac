using System.Collections.Generic;
using Auto_Fac.Models;

namespace Auto_Fac.ViewModels
{
    public class AdminIndexViewModel
    {
        public IEnumerable<Post> Post { get; set; }
        public admin NameAdmin { get; set; }
        public UserIsLogged UserIsLogged { get; set; }
    }
}