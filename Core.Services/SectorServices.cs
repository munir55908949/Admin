using System.Collections.Generic;
using System.Linq;
using Core.AccessLayer;
using Core.ViewModels;
using System.Data.Entity;
using System;
using System.Globalization;

namespace Core.Services
{
    public class SectorServices
    {
        private DatabseEntities context = new DatabseEntities();
        public Sector GetSectorById(int Id)
        {
            Sector obj = context.Sector
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
        public List<DDLModel> GetSectorName(string optionalLabel)
        {
            var list = new List<DDLModel>();
            if (!string.IsNullOrEmpty(optionalLabel))
                list.Add(new DDLModel { Id = 0, Name = optionalLabel });
            var listType = (from f in context.Sector
                            select new DDLModel()
                            {
                                Id = f.Id,
                                Name = f.Name,
                                
                            }).ToList();
            list.AddRange(listType);
            return list;
        }
        
        public List<SectorViewModel> SearchGrid(int? Id, string Name,string Note, DateTime? CreatedAt, string CreatedBy,
            out int AllListCount, int jtStartIndex = 0,
            int jtPageSize = 10, string jtSorting = null)
        {
            var query = context.Sector.Where(p => (p.Id == Id || Id == 0 || Id == null)
            && (string.IsNullOrEmpty(Name) || p.Name.Contains(Name))
            && (string.IsNullOrEmpty(Note) || p.Name.Contains(Note))
            && p.IsDeleted != true);
            AllListCount = query.Count();
            var list = from f in query.AsQueryable()
                       select new SectorViewModel()
                       {
                           Id = f.Id,
                           Name = f.Name,
                           Note = f.Note,
                           CreatedAt = f.CreatedAt,
                           CreatedByName = f.AspNetUsers.FullName,
                       };
            return list.OrderByDescending(f => f.Id).Skip(jtStartIndex).Take(jtPageSize).ToList();
        }
        public IQueryable<SectorViewModel> SearchGrid()
        {
            var query = context.Sector;
            var list = from f in query.AsQueryable()
                       where f.IsDeleted != true
                       select new SectorViewModel()
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
