using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using Auto_Fac.Models;
using Auto_Fac.Models.Faculty;
using Auto_Fac.Models.Faculty.Professions;
using Microsoft.AspNetCore.Mvc;
using Auto_Fac.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Auto_Fac.Controllers
{
    
    public class AdminController : Controller
    {
        private readonly IDataRepository _repository;
        private  readonly IUserIsLoged _isLoged;
        private readonly IWebHostEnvironment webHost;
        private IFacultyRepository _facultyRepository;


        public AdminController(IDataRepository repository,IUserIsLoged isLoged,IWebHostEnvironment webHostEnvironment,IFacultyRepository facultyRepository)
        {
            _repository = repository;
            _isLoged = isLoged;
            webHost = webHostEnvironment;
            _facultyRepository = facultyRepository;
        }
        [HttpGet]
        public IActionResult Index()
        {
            _isLoged.IsLogged = UserIsLogged.DisActive;
            ViewBag.ErrorLogin = "";

            return View();
        }

        [HttpGet]
        public IActionResult GetAllPost()
        {
            if (_isLoged.IsLogged == UserIsLogged.Active)
            {
                AdminIndexViewModel viewModel = new AdminIndexViewModel();
                var posts = _repository.GetAll(_isLoged.user.IdFaculty);
                viewModel.Post = posts;
                viewModel.UserIsLogged = UserIsLogged.Active;
                return View(viewModel);
            }
            else
            {
                return Redirect("Index");
            }
            
        }

        [HttpGet]
        public ViewResult CreatePostView()
        {
            if (_isLoged.IsLogged == UserIsLogged.Active)
            {
                return View();
            }
            else
            {
                return View("Index");
            }
           
        }
        [HttpPost]
        public ActionResult CreatePost(CreatePostViewModel createpost)
        {
            
           string uniqueFileName = string.Empty;
            if (createpost.photo !=null)
            {
                var uploadFile = Path.Combine(webHost.WebRootPath,"images");
                uniqueFileName = Guid.NewGuid().ToString() + createpost.photo.FileName;
                var imageFilePath = Path.Combine(uploadFile,uniqueFileName );
                createpost.photo.CopyTo(new FileStream(imageFilePath,FileMode.Create));
                
            }
            Post NewPost =new Post();
            NewPost.photo = uniqueFileName;
            NewPost.title = createpost.title;
            NewPost.Text = createpost.Text;
            NewPost.DateTime =DateTime.Now;
             NewPost.Status = 1;
             NewPost.idFaculty = _isLoged.user.IdFaculty;
            _repository.Create(NewPost);

           
            return Redirect("GetAllPost");
        }
        public ViewResult DetailPost(int id)
        {
           var detailPost = _repository.Detail(id);
           return View(detailPost);
        }

        [HttpGet]
        public  IActionResult SignOut()
        {
            _isLoged.IsLogged = UserIsLogged.DisActive;
            return Redirect("Index");
        }
        [HttpPost]
        public IActionResult login(admin Admin)
        {
            if (_repository.ChackAdmin(Admin.login,Admin.Password)==true)
            {
                ViewBag.ErrorLogin = "";
                ViewBag.AdminName = Admin.FirstName +" "+ Admin.LastName;
                _isLoged.IsLogged = UserIsLogged.Active;
                _isLoged.user = _repository.GetAdminByLoginPass(Admin.login, Admin.Password);
                return Redirect("GetAllPost");
                
            }
            else
            {
                ViewBag.ErrorLogin = "Логин ё пароли шумо мувофикат намекунад!";
                _isLoged.IsLogged = UserIsLogged.DisActive;
                return View("Index");
            }
           
        }

        public ViewResult EditPostView(int id)
        {
            if (_isLoged.IsLogged == UserIsLogged.Active)
            {
                var post = _repository.Detail(id);
                CreatePostViewModel postViewModel = new CreatePostViewModel();
                postViewModel.id = post.id;
                postViewModel.photo = new FormFile(Stream.Null, baseStreamOffset: Int64.MaxValue, Int64.MaxValue,
                    "False", post.photo);
                postViewModel.Text = post.Text;
                postViewModel.title = post.title;
                postViewModel.DateTime = post.DateTime;
                return View(postViewModel);
            }
            else
            {
                return View("Index");
            }
        }

        [HttpPost]
        public ActionResult EditPost(CreatePostViewModel EditPost)
        {
            string uniqueFileName = string.Empty;

            if (EditPost.photo !=null)
            {
                var uploadFile = Path.Combine(webHost.WebRootPath,"images");
                uniqueFileName = Guid.NewGuid().ToString() + EditPost.photo.FileName;
                var imageFilePath = Path.Combine(uploadFile,uniqueFileName );
                EditPost.photo.CopyTo(new FileStream(imageFilePath,FileMode.Create));
            }
                Post NewPost =new Post();
                NewPost.id = EditPost.id;
                NewPost.photo = uniqueFileName??"Example.png";
                NewPost.title = EditPost.title;
                NewPost.Text = EditPost.Text;
                NewPost.DateTime =DateTime.Now;
                NewPost.idFaculty = _isLoged.user.IdFaculty;
                _repository.EditPost(NewPost);
                return Redirect("GetAllPost");
        }
        public ActionResult DeletePost(int id)
        {
            _repository.DeletePost(id);
            return Redirect("~/Admin/GetAllPost");
        }
        [HttpGet]
        public ActionResult CheckFacultyIsCreated()
        {
            var facucty = _facultyRepository.FacultyGetById(_isLoged.user.IdFaculty);
            if (facucty !=null)
            {
                return Redirect($"~/Manage/GetAllFaculty/{_isLoged.user.IdFaculty}");
            }
            else
            {
                return RedirectToAction(nameof(CreateFacultyView));
            }
        }
        [HttpGet]
        public ActionResult CreateFacultyView()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateFaculty(Faculty faculty)
        {
            faculty.Status = 1;
           var  newfaculty= _facultyRepository.CreateFaculty(faculty);
           var DepartamentViewModel = new FacultyDepartamentModelView();
           DepartamentViewModel.Faculty = newfaculty;
           _isLoged.user.IdFaculty = newfaculty.id;
           _repository.EditAdmin(_isLoged.user);
           return RedirectToAction("CreateDepartamentView",new Faculty{id =newfaculty.id,Name = newfaculty.Name});
        }
        public ActionResult CreateDepartamentView( int id)
        {
            var Dp = new FacultyDepartamentModelView();
            Dp.Faculty = _facultyRepository.FacultyGetById(id);
            return View(Dp);
        }
        [HttpPost]
        public ActionResult CreateDepartament(FacultyDepartamentModelView departamentModelView)
        {
            departamentModelView.Departament.Status = 1;
            departamentModelView.Departament.idFaculty = departamentModelView.Faculty.id;
           var newdepartaments = _facultyRepository.CreateDepartament(departamentModelView.Departament);
           return Redirect($"~/Manage/GetAllDepartaments/{departamentModelView.Faculty.id}");
        }

        [HttpGet]
        public ViewResult CreateProfessionView(int id)
        {
            var departament = _facultyRepository.DepartamentById(id);
            DepartanemtProfessionViewModel professionViewModel = new DepartanemtProfessionViewModel();
            professionViewModel.Departament = departament;
            return View(professionViewModel);
        }
        [HttpPost]
        public ActionResult CreateProfession(DepartanemtProfessionViewModel professionViewModel)
        {
            professionViewModel.Profession.Status = 1;
            professionViewModel.Profession.IdDepartament = professionViewModel.Departament.id;
            _facultyRepository.CreateProfession(professionViewModel.Profession);
            return Redirect($"~/Manage/GetAllProfessions/{professionViewModel.Departament.id}");
        }
        [HttpGet]
        public ActionResult CreateGroupView(int idProfession)
        {
            ProfessionGroupViewModel groupViewModel = new ProfessionGroupViewModel();
            groupViewModel.Groups = new Groups {IdProfession = idProfession};
            Profession newProfession = _facultyRepository.ProfessionById(idProfession);
            groupViewModel.Profession = new Profession()
            {
                Name =  newProfession.Name,
                id =newProfession.id,
                Status = newProfession.Status,
                IdDepartament = newProfession.IdDepartament
            };
            return View(groupViewModel);
        }
        [HttpPost]
        public ActionResult CreateGroup(ProfessionGroupViewModel viewModel)
        {
            viewModel.Groups.Status = 1;
            viewModel.Groups.IdFaculty = _isLoged.user.IdFaculty;
            _facultyRepository.CreateGroup(viewModel.Groups);
            return Redirect($"~/Manage/GetAllGroups/?idProfession={viewModel.Groups.IdProfession}");
        }

        
        [HttpGet]
        public ActionResult CreatelessonScheduleView(int idGroup,int? idCourse)
        {
            HelperClass helperClass = new HelperClass(_facultyRepository);
           var weekday = helperClass.NewWeekDays(idGroup,idCourse??1);
            return View(weekday);
        }
        
        [HttpPost]
        public ActionResult CreateLessonSchedule(WeekDayViewModel tablesViewModel)
        {
            foreach (var item in tablesViewModel.LessonTablesViewModel)
            {
                foreach (var itemDaysList in item.DaysList)
                {
                    itemDaysList.idSimesters = 1;
                    _facultyRepository.CreateWeekDays(itemDaysList);
                }
            }
            return Redirect("Index");
        }
        [HttpPost]
        public ActionResult CheckIsNewWeekDay(int IdGroup,int? idCourse,int? idSimester)
        {
            HelperClass helperClass = new HelperClass(_facultyRepository);
            var weekDays=  helperClass.GetWeekDaysByProfssionCourse(IdGroup, idCourse??1, idSimester??1);
            if (weekDays.LessonTablesViewModel[0].DaysList.Count !=0)
            {
                return Redirect($"~/Manage/GetLessonsWeekDay/?idGroup={IdGroup}&&idCourse={idCourse}");
            }
            else
            {
                return Redirect($"~/Admin/CreatelessonScheduleView/?idGroup={IdGroup}&&idCourse={idCourse}");
            }
        }

        [HttpGet]
        public ActionResult EditWeekDayView(int idGroup,int idCourse)
        {
            HelperClass helperClass = new HelperClass(_facultyRepository);
            var weekDays = helperClass.EditWeekDays(idGroup,idCourse);
            return View(weekDays);
        }

        [HttpPost]
        public ActionResult EditWeekDay(WeekDayViewModel viewModel)
        {
            int IdGroup = 0;
            int idCourse =0;
            foreach (var item in viewModel.LessonTablesViewModel)
            {
                IdGroup = item.DaysList[0].idGroups;
                idCourse = item.DaysList[0].idCourse;
                _facultyRepository.EditWeekDay(item.DaysList.ToList());
            }
            return Redirect($"~/Manage/GetLessonsWeekDay/?idGroup={IdGroup}&&idCourse={idCourse}");
        }
        
    }
}