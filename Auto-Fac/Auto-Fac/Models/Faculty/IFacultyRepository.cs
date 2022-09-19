using System.Collections.Generic;
using Auto_Fac.Models.Faculty.Professions;

namespace Auto_Fac.Models.Faculty
{
    public interface IFacultyRepository
    {
        public Faculty CreateFaculty(Faculty faculty);
        public Departament CreateDepartament(Departament departament);
        public Profession CreateProfession(Profession profession);
        public LessonSchedule CreateLessonSchedule(int idLessonSchedule,LessonSchedule lessonSchedule);

        public IEnumerable<Departament> GetAllDepartaments(int idFaculty);
        public IEnumerable<Faculty> GetAllFaculties();
        public IEnumerable<Profession> GetAllProfessions(int idDepartament);
        public Faculty FacultyGetById(int id);
        public Departament DepartamentById(int id);

    }
}