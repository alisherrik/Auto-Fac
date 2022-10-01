using System.Collections.Generic;
using Auto_Fac.Models.Faculty.Professions;

namespace Auto_Fac.ViewModels
{
    public class WeekDayViewModel
    {
        public List<ProfessionLessonTablesViewModel> LessonTablesViewModel { get; set; }
        public Groups Groups { get; set; }
        public int IdCourse { get; set; }
    }
}