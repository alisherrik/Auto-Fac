using System.Collections.Generic;
using Auto_Fac.Models.Faculty.Professions;

namespace Auto_Fac.ViewModels
{
    public class ProfessionLessonTablesViewModel
    {
        public IList<WeekDays> DaysList { get; set; }
        public string  NameWeekDays { get; set; }
        public int idNameWeekDays { get; set; }
    }
}