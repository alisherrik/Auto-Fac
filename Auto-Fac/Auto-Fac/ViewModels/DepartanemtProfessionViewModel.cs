using System.Collections.Generic;
using Auto_Fac.Models.Faculty;
using Auto_Fac.Models.Faculty.Professions;

namespace Auto_Fac.ViewModels
{
    public class DepartanemtProfessionViewModel
    {
        public Departament Departament { get; set; }
        public IEnumerable<Profession> EnumerableProfession { get; set; }
        public Profession Profession { get; set; }
    }
}