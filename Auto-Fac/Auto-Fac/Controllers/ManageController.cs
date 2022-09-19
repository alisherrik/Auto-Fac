using Auto_Fac.Models.Faculty;
using Auto_Fac.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Auto_Fac.Controllers
{
    public class ManageController : Controller
    {
        private IFacultyRepository _facultyRepository;

        public ManageController(IFacultyRepository facultyRepository)
        {
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
    }
}