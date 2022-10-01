using Auto_Fac.Models;
using Auto_Fac.Models.Faculty;
using Auto_Fac.Models.Faculty.Professions;
using Auto_Fac.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Auto_Fac.Controllers
{
    public class ManageController : Controller
    {
        private IFacultyRepository _facultyRepository;
        private IUserIsLoged _isLoged;

        public ManageController(IFacultyRepository facultyRepository,IUserIsLoged isLoged)
        {
            _isLoged = isLoged;
            _facultyRepository = facultyRepository;
        }
        // GET
        public IActionResult Index()
        {
            return Redirect("~/Views/Home/Index.cshtml");
        }
        [HttpGet]
        public ActionResult GetAllDepartaments(int id)
        {
            var departamentViewModel = new FacultyDepartamentModelView();
            var departaments = _facultyRepository.GetAllDepartaments(id);
            departamentViewModel.IeEnumerableDepartaments = departaments;
            departamentViewModel.Faculty = _facultyRepository.FacultyGetById(id);
            return View(departamentViewModel);
        }
        [HttpGet]
        public ActionResult GetAllFaculty(int id)
        {
            var Faculty = _facultyRepository.FacultyGetById(id);
            return View(Faculty);
        }
        [HttpGet]
        public ActionResult GetAllProfessions(int id)
        {
            var Professions = _facultyRepository.GetAllProfessions(id);
            var Departament = _facultyRepository.DepartamentById(id);
            DepartanemtProfessionViewModel professionViewModel = new DepartanemtProfessionViewModel();
            professionViewModel.Departament = Departament;
            professionViewModel.EnumerableProfession = Professions;
            return View(professionViewModel);
        }
        [HttpGet]
        public ActionResult GetAllGroups(int idProfession)
        {
            var idFaculty = _isLoged.user.IdFaculty;
            var groups = _facultyRepository.GetAllGroups(idProfession, idFaculty);
            ProfessionGroupViewModel viewModel = new ProfessionGroupViewModel();
            viewModel.GroupsEnumerable = groups;
            var newProfession =_facultyRepository.ProfessionById(idProfession);
            viewModel.Profession = new Profession()
            {
                id=newProfession.id,
                IdDepartament = newProfession.IdDepartament,
                Name = newProfession.Name,
                Status = newProfession.Status
            };
            return View(viewModel);
        }
        public ActionResult GetLessonsWeekDay(int idGroup, int? idCourse,int? idSimester)
        {
            HelperClass helperClass = new HelperClass(_facultyRepository);
          var weekDays=  helperClass.GetWeekDaysByProfssionCourse(idGroup, idCourse??1, idSimester??1);
          
              return View(weekDays);
        }
        
        //---------------- New WeekDay Logic ------------------
        // public ActionResult GetAllCourses(int idFaculty)
        // {
        //     return View();
        // }
    }
}