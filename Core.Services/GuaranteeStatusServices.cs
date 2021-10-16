using System.Collections.Generic;
using System.Linq;
using Core.AccessLayer;
using Core.ViewModels;
using System.Data.Entity;
using System;
using System.Globalization;

namespace Core.Services
{
    public class GuaranteeStatusServices
    {
        private DatabseEntities context = new DatabseEntities();
        public GuaranteeStatus GetGuaranteeStatusById(int Id)
        {
            GuaranteeStatus obj = context.GuaranteeStatus
                .Where(p => p.Id == Id)
                .FirstOrDefault();
            return obj;
        }
        public AspNetUsers GetICreatedById(string Id)
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
        public List<DDLModel> GetGuaranteeStatusName(string optionalLabel)
        {
            var list = new List<DDLModel>();
            if (!string.IsNullOrEmpty(optionalLabel))
                list.Add(new DDLModel { Id = 0, Name = optionalLabel });
            var listType = (from f in context.GuaranteeStatus
                            select new DDLModel()
                            {
                                Id = f.Id,
                                Name = f.Name,
                                
                            }).ToList();
            list.AddRange(listType);
            return list;
        }
        
        public List<GuaranteeStatusViewModel> SearchGrid(int? Id, string Name,string Note, DateTime? CreatedAt, string CreatedBy,
            out int AllListCount, int jtStartIndex = 0,
            int jtPageSize = 10, string jtSorting = null)
        {
            var query = context.GuaranteeStatus.Where(p => (p.Id == Id || Id == 0 || Id == null)
            && (string.IsNullOrEmpty(Name) || p.Name.Contains(Name))
            && (string.IsNullOrEmpty(Note) || p.Name.Contains(Note))
            && p.IsDeleted != true);
            AllListCount = query.Count();
            var list = from f in query.AsQueryable()
                       select new GuaranteeStatusViewModel()
                       {
                           Id = f.Id,
                           Name = f.Name,
                           Note = f.Note,
                           CreatedAt = f.CreatedAt,
                           CreatedByName = f.AspNetUsers.FullName,
                       };
            return list.OrderByDescending(f => f.Id).Skip(jtStartIndex).Take(jtPageSize).ToList();
        }
        public IQueryable<GuaranteeStatusViewModel> SearchGrid()
        {
            var query = context.GuaranteeStatus;
            var list = from f in query.AsQueryable()
                       where f.IsDeleted != true
                       select new GuaranteeStatusViewModel()
                       {
                           Id = f.Id,
                           Name = f.Name,
                           Note = f.Note,
                           CreatedAt = f.CreatedAt,
                           CreatedByName = f.AspNetUsers.FullName,
                           
                       };
            return list;
        }
    }
}
