using System.Collections.Generic;

namespace Auto_Fac.Models
{
    public interface IDataRepository
    {
        public IEnumerable<Post> GetAll(int idFaculty);
        public Post Create(Post post);
        public bool ChackAdmin(string login, string pass);
        public Post Detail(int id);
        public Post EditPost(Post UpdatePost);
        public void DeletePost(int id);
        public admin GetAdminByLoginPass(string login, string pass);
        public admin EditAdmin(admin admin);
    }
}