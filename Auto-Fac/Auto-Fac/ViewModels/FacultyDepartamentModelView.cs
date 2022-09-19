using System.Collections.Generic;
using Auto_Fac.Models.Faculty;

namespace Auto_Fac.ViewModels
{
    public class FacultyDepartamentModelView
    {
        public Faculty Faculty { get; set; }
        public Departament Departament { get; set; }
        public IEnumerable<Departament> IeEnumerableDepartaments { get; set; }
    }
}