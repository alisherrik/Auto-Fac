using System.Collections.Generic;
using System.Linq;
using Auto_Fac.Models.Faculty.Professions;

namespace Auto_Fac.Models.Faculty
{
    public class FacultyRepository:IFacultyRepository
    {
        
        private AppDbContext _dbContext;

        public FacultyRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Faculty CreateFaculty(Faculty faculty)
        {
            _dbContext.Faculties.Add(faculty);
            _dbContext.SaveChanges();
            var idFaculty  = _dbContext.Faculties.Max(s => s.id);
            var lastFaculty = _dbContext.Faculties.FirstOrDefault(s => s.id.Equals(idFaculty));
            return lastFaculty;
        }

        public Departament CreateDepartament( Departament departament)
        {
            
            _dbContext.Departaments.Add(departament);
            _dbContext.SaveChanges();
            var idDepartament  = _dbContext.Departaments.Max(s => s.id);
            var lastDepartament = _dbContext.Departaments.FirstOrDefault(s => s.id.Equals(idDepartament));
            return lastDepartament;
        }

        public Profession CreateProfession(Profession profession)
        {
            _dbContext.Professions.Add(profession);
            _dbContext.SaveChanges();
            return profession;
        }

        public LessonSchedule CreateLessonSchedule(int idLessonSchedule, LessonSchedule lessonSchedule)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Departament> GetAllDepartaments(int idFaculty)
        {
            var departaments = _dbContext.Departaments.Where(s => s.idFaculty.Equals(idFaculty) && s.Status.Equals(1));
            return departaments;
        }

        public IEnumerable<Faculty> GetAllFaculties()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Profession> GetAllProfessions(int idDepartament)
        {
            var AllProfessions =
                _dbContext.Professions
                    .Where(s => s.IdDepartament
                        .Equals(idDepartament) && s.Status.Equals(1));
            return AllProfessions;
        }

        public Faculty FacultyGetById(int id)
        {
            var faculty = _dbContext.Faculties.FirstOrDefault(s => s.id.Equals(id));
            return faculty;
        }

        public Departament DepartamentById(int id)
        {
            var result = _dbContext.Departaments.FirstOrDefault(s => s.id.Equals(id));
            return result;
        }
    }
}