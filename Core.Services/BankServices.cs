using System.Collections.Generic;
using System.Linq;
using Core.AccessLayer;
using Core.ViewModels;
using System.Data.Entity;
using System;
using System.Globalization;

namespace Core.Services
{
    public class BankServices
    {
        private DatabseEntities context = new DatabseEntities();
        public Bank GetBankById(int Id)
        {
            Bank obj = context.Bank
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
        public List<BankViewModel> SearchGrid(int? Id, string Name,
            out int AllListCount, int jtStartIndex = 0,
            int jtPageSize = 10, string jtSorting = null)
        {
            var query = context.Bank.Where(p => (p.Id == Id || Id == 0 || Id == null)
            && (string.IsNullOrEmpty(Name) || p.Name.Contains(Name))
            && p.IsDeleted != true);
            AllListCount = query.Count();
            var list = from f in query.AsQueryable()
                       select new BankViewModel()
                       {
                           Id = f.Id,
                           Name = f.Name,
                       };
            return list.OrderByDescending(f => f.Id).Skip(jtStartIndex).Take(jtPageSize).ToList();
        }
        public IQueryable<BankViewModel> SearchGrid()
        {
            var query = context.Bank;
            var list = from f in query.AsQueryable()
                       where f.IsDeleted != true
                       select new BankViewModel()
                       {
                           Id = f.Id,
                           Name = f.Name,
                           //CreatedByName = f.AspNetUsers.FullName,
                           CreatedAt = f.CreatedAt,
                       };
            return list;
        }
    }
}
