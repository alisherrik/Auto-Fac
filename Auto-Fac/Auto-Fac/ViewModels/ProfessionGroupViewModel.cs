using System.Collections.Generic;
using Auto_Fac.Models.Faculty.Professions;

namespace Auto_Fac.ViewModels
{
    public class ProfessionGroupViewModel
    {
        public Profession Profession { get; set; }
        public Groups Groups { get; set; }
        public List<Groups> GroupsEnumerable { get; set; }
    }
}