using System.Collections.Generic;
using System.Linq;
using Auto_Fac.Models.Faculty.Professions;
using Auto_Fac.ViewModels;

namespace Auto_Fac.Models.Faculty
{
    public class HelperClass
    {
        private IFacultyRepository _FacultyRepository;

        public HelperClass(IFacultyRepository repository)
        {
            _FacultyRepository = repository;
        }

        public WeekDayViewModel NewWeekDays(int idGroup,int idCourse)
        {
            WeekDayViewModel viewModel = new WeekDayViewModel();
            viewModel.Groups = _FacultyRepository.GetGroupById(idGroup);
            viewModel.LessonTablesViewModel = new List<ProfessionLessonTablesViewModel>();
            var WeekDaysName = _FacultyRepository.DaysEnumerable().ToList();
            foreach (var item in WeekDaysName)
            {
                ProfessionLessonTablesViewModel tablesViewModel = new ProfessionLessonTablesViewModel();
                tablesViewModel.idNameWeekDays = item.id;
                tablesViewModel.NameWeekDays = item.Name;
                tablesViewModel.DaysList = new List<WeekDays>();
                for (int i = 1; i < 7; i++)
                {
                    WeekDays days = new WeekDays();
                    days.idDay = item.id;
                    days.idCourse = idCourse;
                    days.idGroups = viewModel.Groups.id;
                    tablesViewModel.DaysList.Add(days);
                }
                viewModel.LessonTablesViewModel.Add(tablesViewModel);
                
            }
           
            viewModel.Groups = _FacultyRepository.GetGroupById(idGroup);
            viewModel.IdCourse = idCourse;
            return viewModel;
        }

        public WeekDayViewModel GetWeekDaysByProfssionCourse(int idGroup, int idCourse,int idSimester)
        {
            WeekDayViewModel viewModel = new WeekDayViewModel();
            viewModel.Groups = _FacultyRepository.GetGroupById(idGroup);
            viewModel.LessonTablesViewModel = new List<ProfessionLessonTablesViewModel>();
            var WeekDaysName = _FacultyRepository.DaysEnumerable().ToList();
            foreach (var item in WeekDaysName)
            {
                ProfessionLessonTablesViewModel tablesViewModel = new ProfessionLessonTablesViewModel();
                tablesViewModel.idNameWeekDays = item.id;
                tablesViewModel.NameWeekDays = item.Name;
                tablesViewModel.DaysList = new List<WeekDays>();
                tablesViewModel.DaysList = _FacultyRepository.GetAllWeekDays(idGroup, idCourse, idSimester,item.id).ToList();
                viewModel.LessonTablesViewModel.Add(tablesViewModel);
                
            }
            return viewModel;
            
        }
        public WeekDayViewModel EditWeekDays(int idGroup,int idCourse)
        {
            WeekDayViewModel viewModel = new WeekDayViewModel();
            viewModel.LessonTablesViewModel = new List<ProfessionLessonTablesViewModel>();
            var WeekDaysName = _FacultyRepository.DaysEnumerable().ToList();
            foreach (var item in WeekDaysName)
            {
                ProfessionLessonTablesViewModel tablesViewModel = new ProfessionLessonTablesViewModel();
                tablesViewModel.idNameWeekDays = item.id;
                tablesViewModel.NameWeekDays = item.Name;
                tablesViewModel.DaysList = new List<WeekDays>();
                tablesViewModel.DaysList = _FacultyRepository.GetAllWeekDays(idGroup,idCourse,1,item.id);
                viewModel.Groups = _FacultyRepository.GetGroupById(idGroup);
                viewModel.LessonTablesViewModel.Add(tablesViewModel);
                
            }
            return viewModel;
            
        }
    }
}