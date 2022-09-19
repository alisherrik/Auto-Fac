using System;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using Auto_Fac.Models;
using Auto_Fac.Models.Faculty;
using Microsoft.AspNetCore.Mvc;
using Auto_Fac.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

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
    }
}