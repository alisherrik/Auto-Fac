using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Auto_Fac.Models
{
    public class DataRepository:IDataRepository
    {
        private AppDbContext _DbContext;
        public UserIsLogged IsLogged { get; set; }
        public DataRepository(AppDbContext context)
        {
            _DbContext = context;
            IsLogged = UserIsLogged.DisActive;
        }
        public IEnumerable<Post> GetAll(int idFaculty)
        {
            var posts = _DbContext.Posts.Where(s => s.Status == 1 && s.idFaculty == idFaculty);
            return posts;
        }

        public Post Create(Post post)
        {
            _DbContext.Posts.Add(post);
            _DbContext.SaveChanges();
            return post;
        }

       
        public bool ChackAdmin(string login, string pass)
        {
            var useradmin = _DbContext.Admins.FirstOrDefault(s=>s.Password.Equals(pass)&& s.login.Equals(login));
            if (useradmin !=null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Post Detail(int id)
        {
            var PostDetail = _DbContext.Posts.FirstOrDefault(s => s.id.Equals(id));
            return PostDetail;
        }

        public Post EditPost(Post UpdatePost)
        {
            var NewPost = new Post {id = UpdatePost.id};
           var post = _DbContext.Posts.Attach(NewPost);
           if(UpdatePost.photo !=String.Empty) NewPost.photo = UpdatePost.photo;
            NewPost.title = UpdatePost.title;
            NewPost.Text = UpdatePost.Text;
            NewPost.Status = 1;
            post.State = EntityState.Modified;
            _DbContext.SaveChanges();
          return UpdatePost;
        }
        
        public void DeletePost(int id)
        {
            var NewPost = _DbContext.Posts.FirstOrDefault(s => s.id.Equals(id));
            NewPost.Status = 0;
           var post = _DbContext.Posts.Attach(NewPost);
           post.State = EntityState.Modified;
            _DbContext.SaveChanges();
           
        }

        public admin GetAdminByLoginPass(string login, string pass)
        {
           var log_admin = _DbContext.Admins.FirstOrDefault(s => s.login.Equals(login) && s.Password.Equals(pass));
           return log_admin;
        }

        public admin EditAdmin(admin Uadmin)
        {
            var updateAdmin = _DbContext.Admins.Attach(Uadmin);
            updateAdmin.State = EntityState.Modified;
            _DbContext.SaveChanges();
            return Uadmin;
        }
    }
}