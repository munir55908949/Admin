using System.Collections.Generic;
using System.Linq;
using Core.AccessLayer;
using Core.ViewModels;
using System.Data.Entity;
using System;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Runtime.InteropServices.WindowsRuntime;

namespace Core.Services
{
    public class UsersServices
    {
        private DatabseEntities context = new DatabseEntities();
        public AspNetUsers GetItemById(string Id)
        {
            AspNetUsers obj = context.AspNetUsers
                .Where(p => p.IsDelete != true && p.Id == Id).FirstOrDefault();

            return obj;
        }
        public AspNetUsers CheckUserName(string Id, string UserName)
        {
            AspNetUsers obj = context.AspNetUsers
                .Where(p => p.IsDelete != true && p.Id != Id && p.UserName == UserName).FirstOrDefault();

            return obj;
        }
        public AspNetUsers CheckPhoneNumber(string Id, string PhoneNumber)
        {
            AspNetUsers obj = context.AspNetUsers
                .Where(p => p.IsDelete != true && p.Id != Id && p.PhoneNumber == PhoneNumber).FirstOrDefault();

            return obj;
        }

        public List<UsersViewModel> SearchGrid(string UserName, string Fullname, string PhoneNumber,
              out int AllListCount, int jtStartIndex = 0,
          int jtPageSize = 10, string jtSorting = null)
        {
            var query = context.AspNetUsers.Where(p => (string.IsNullOrEmpty(PhoneNumber)
                   || p.PhoneNumber.ToLower().Contains(PhoneNumber.ToLower()))
                   && (string.IsNullOrEmpty(UserName)
                   || p.UserName.ToLower().Contains(UserName.ToLower()))
                   && (string.IsNullOrEmpty(Fullname)
                   || p.FullName.ToLower().Contains(Fullname.ToLower()))
                   && p.IsDelete != true &&
                   p.AspNetRoles.FirstOrDefault().Name != "Admin");
            AllListCount = query.Count();
            var list = from f in query.AsQueryable()
                       where f.IsDelete != true
                       select new UsersViewModel()
                       {
                           Id = f.Id,
                           FullName = f.FullName,
                           UserName = f.UserName,
                           PhoneNumber = f.PhoneNumber,
                           IsActive = f.IsActive.HasValue ? f.IsActive.Value : false,
                       };
            return list.OrderBy(f => f.Id).Skip(jtStartIndex).Take(jtPageSize).ToList();
        }
        public List<UsersViewModel> SearchGridCustomer(string UserName,
           string PhoneNumber, out int AllListCount, int jtStartIndex = 0,
         int jtPageSize = 10, string jtSorting = null)
        {
            var query = context.AspNetUsers.Where(p => (string.IsNullOrEmpty(PhoneNumber)
                   || p.PhoneNumber.ToLower().Contains(PhoneNumber.ToLower()))
                   && (string.IsNullOrEmpty(UserName)
                   || p.UserName.ToLower().Contains(UserName.ToLower()))
                   && p.IsDelete != true &&
                   p.AspNetRoles.FirstOrDefault().Name != "Admin");
            AllListCount = query.Count();
            var list = from f in query.AsQueryable()
                       where f.IsDelete != true
                       select new UsersViewModel()
                       {
                           Id = f.Id,
                           FullName = f.FullName,
                           UserName = f.UserName,
                           PhoneNumber = f.PhoneNumber,
                           IsActive = f.IsActive.HasValue ? f.IsActive.Value : false,
                       };
            return list.OrderByDescending(f => f.FullName).Skip(jtStartIndex).Take(jtPageSize).ToList();
        }
        public List<DDLModel> GetAllUsers(string optionalLabel)
        {
            List<DDLModel> result = new List<DDLModel>();
            result.Add(new DDLModel { Id = 0, Name = optionalLabel });
            var query = from q in context.AspNetUsers
                        where q.IsDelete != true
                        select new DDLModel()
                        {
                            IdString = q.Id,
                            Name = q.FullName,
                        };

            result.AddRange(query.ToList());
            return result;
        }

        public List<PagesViewModel> GetAllListPages(string userId)
        {
            var query = context.Pages
               .Include(f => f.Users_Pages);

            var list = from f in query.AsQueryable()
                       select new PagesViewModel()
                       {
                           Id = f.Id,
                           Name = f.Name,
                           Icon = f.Icon,
                           Url = f.Url,
                           Order = f.Order,
                           IsChecked = f.Users_Pages
                           .Any(s => s.UserId == userId),
                       };

            return list.OrderBy(f => f.Order).ToList();
        }
        public List<DDLModel> GetUserPages(string UserId)
        {
            var list = from f in context.Users_Pages
                       .Include(f => f.Pages)
                       where f.UserId == UserId
                       select new DDLModel()
                       {
                           Id = f.Id,
                           Name = f.Pages.Url,
                       };

            return list.ToList();
        }
        /////////////////////////////test//////////////
        ///











        //does not return any thing just do some action
        public void DeleteTempfiles()
        {
            ///
        }

        public void fun(int id)
        {
            //
        }

        public int Sum()
        {
            return 0;
        }

        public int Sum2()
        {
            int x =  0;
            return x;
        }

        public string name()
        {
            return "ay 7aga";
        }

        public string name2()
        {
            string x = "a7a";
            return x;
        }

        public UsersViewModel GetAllUserTest()
        {
            UsersViewModel obj = new UsersViewModel();

            return obj;

        }


        public UsersViewModel service(string id)
        {
            UsersViewModel obj = new UsersViewModel();


            var objFromDB = context.AspNetUsers.Where(x => x.Id == id).FirstOrDefault();

            obj.FullName = objFromDB.FullName;
            obj.IsActive = objFromDB.IsActive.Value;

            return obj;
        }

        public void CallFunction()
        {
            string id = "2";
            service(id);
        }
            //return type               //parmter input type
        public void searchController(string id)
        {
            service(id);
        }


        public void LastCall()
        {
            string id = "3";//view 
            searchController(id);
        }

















    }
}
