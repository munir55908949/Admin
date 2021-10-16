using System.Collections.Generic;
using System.Linq;
using Core.AccessLayer;
using Core.ViewModels;
using System.Data.Entity;
using System;
using System.Globalization;

namespace Core.Services
{
    public class AdministrationServices
    {
        private DatabseEntities context = new DatabseEntities();
        public Administration GetAdministrationById(int Id)
        {
            Administration obj = context.Administration
                .Where(p => p.Id == Id )
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

        public List<DDLModel> GetCompanyName(string optionalLabel)
        {
            var list = new List<DDLModel>();
            if (!string.IsNullOrEmpty(optionalLabel))
                list.Add(new DDLModel { Id = 0, Name = optionalLabel });
            var listType = (from f in context.Company
                            select new DDLModel()
                            {
                                Id = f.Id,
                                Name = f.Name,
                            }).ToList();
            list.AddRange(listType);
            return list;
        }
        public List<DDLModel> GetAdministrationName(string optionalLabel)
        {
            var list = new List<DDLModel>();
            if (!string.IsNullOrEmpty(optionalLabel))
                list.Add(new DDLModel { Id = 0, Name = optionalLabel });
            var listType = (from f in context.Administration
                            select new DDLModel()
                            {
                                Id = f.Id,
                                Name = f.Name,
                            }).ToList();
            list.AddRange(listType);
            return list;
        }
        public List<DDLModel> GetBankName(string optionalLabel)
        {
            var list = new List<DDLModel>();
            if (!string.IsNullOrEmpty(optionalLabel))
                list.Add(new DDLModel { Id = 0, Name = optionalLabel });
            var listType = (from f in context.Bank
                            select new DDLModel()
                            {
                                Id = f.Id,
                                Name = f.Name,
                            }).ToList();
            list.AddRange(listType);
            return list;
        }
 
        public List<AdministrationViewModel> SearchGrid(string Name, string Note, int? SectorId, string CreatedBy,
    out int AllListCount, int jtStartIndex = 0,
    int jtPageSize = 10, string jtSorting = null)

        {
            var query = context.Administration.Where(p => (string.IsNullOrEmpty(Name) || p.Name.Contains(Name))
            && (string.IsNullOrEmpty(Note) || p.Note.Contains(Note)
            && (p.SectorId == SectorId || SectorId == 0 || SectorId == null)
            && p.IsDeleted != true));

            AllListCount = query.Count();
            var list = from f in query.AsQueryable()
                       select new AdministrationViewModel()
                       {
                           Id = f.Id,
                           Name = f.Name,
                           Note = f.Note,
                           SectorName = f.Sector.Name,
                           SectorId = f.SectorId,
                           CreatedByName = f.CreatedBy,
                           CreatedAt = f.CreatedAt,
                       };
            return list.OrderByDescending(f => f.Id).Skip(jtStartIndex).Take(jtPageSize).ToList();
        }
        public IQueryable<AdministrationViewModel> SearchGrid()
        {
            var query = context.Administration;
            var list = from f in query.AsQueryable()
                       where f.IsDeleted != true
                       select new AdministrationViewModel()
                       {
                           Id = f.Id,
                           Name = f.Name,
                           Note = f.Note,
                           SectorName = f.Sector.Name,
                           SectorId = f.SectorId,
                           CreatedByName = f.AspNetUsers.FullName,
                           CreatedAt = f.CreatedAt,
                       };
            return list;
        }
    }
}
