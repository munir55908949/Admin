using System.Collections.Generic;
using System.Linq;
using Core.AccessLayer;
using Core.ViewModels;
using System.Data.Entity;
using System;
using System.Globalization;

namespace Core.Services
{
    public class WarrantyServices
    {
        private DatabseEntities context = new DatabseEntities();
        public Warranty GetWarrantyById(int Id)
        {
            Warranty obj = context.Warranty
                .Where(p => p.Id == Id)
                .FirstOrDefault();

            return obj;
        }
        public WarrantyHistoryViewModel GetWarrantyHistoryById(int Id)
        {
            return context.Warranty
                .Where(p => p.Id == Id)
                .Select(x => new WarrantyHistoryViewModel
                {
                    WarrantyNumber = x.WarrantyNumber,
                    WarrantyValue = x.WarrantyValue,
                    GuaranteeStatusName = x.GuaranteeStatus.Name,
                    GuaranteeEndDate = x.GuaranteeEndDate,
                    Path = x.Path,
                }).FirstOrDefault();
        }
        public IQueryable<WarrantyViewModel> SearchWarrantyHistoryGrid(int? id)
        {
            var query = context.WarrantyHistory;
            var list = from f in query.AsQueryable()
                       where f.IsDeleted != true
                       && f.WarrantyId == id //to get data for the row selected 
                       select new WarrantyViewModel()
                       {
                           Id = f.Id,
                           ContractNumber = f.Warranty.ContractData.ContractNumber,
                           WarrantyNumber = f.WarrantyNumber,
                           WarrantyValue = f.WarrantyValue,
                           DecreaseOrIncreaseValue = f.DecreaseOrIncreaseValue,
                           GuaranteeSortId = f.GuaranteeSortId,
                           GuaranteeSortName = f.GuaranteeSort.Name,
                           GuaranteeStatusId = f.GuaranteeStatusId,
                           GuaranteeStatusName = f.GuaranteeStatus.Name,
                           WarrantyTypeId = f.WarrantyType.Id,
                           WarrantyTypeName = f.WarrantyType.Name,
                           DecreaseOrIncreaseId = f.DecreaseOrIncreaseId,
                           DecreaseOrIncreaseName = f.DecreaseOrIncrease.Name,
                           NoteWarranty = f.NoteWarrantyHistory,
                           CreatedByName = f.AspNetUsers.FullName,
                           CreatedAt = f.CreatedAt,
                           GuaranteeIssueDate = f.GuaranteeIssueDate,
                           GuaranteeStartDate = f.GuaranteeStartDate,
                           GuaranteeEndDate = f.GuaranteeEndDate,
                           ModifiedAt = f.ModifiedAt,
                           ModifiedByName = f.AspNetUsers.FullName,
                           Path = f.Path,
                       };
            return list;
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
        public List<DDLModel> GetGuaranteeSortName(string optionalLabel)
        {
            var list = new List<DDLModel>();
            if (!string.IsNullOrEmpty(optionalLabel))
                list.Add(new DDLModel { Id = 0, Name = optionalLabel });
            var listType = (from f in context.GuaranteeSort
                            select new DDLModel()
                            {
                                Id = f.Id,
                                Name = f.Name,
                            }).ToList();
            list.AddRange(listType);
            return list;
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
        public List<DDLModel> GetWarrantyTypeName(string optionalLabel)
        {
            var list = new List<DDLModel>();
            if (!string.IsNullOrEmpty(optionalLabel))
                list.Add(new DDLModel { Id = 0, Name = optionalLabel });
            var listType = (from f in context.WarrantyType
                            select new DDLModel()
                            {
                                Id = f.Id,
                                Name = f.Name,
                            }).ToList();
            list.AddRange(listType);
            return list;
        }
        public List<DDLModel> GetCurrencyName(string optionalLabel)
        {
            var list = new List<DDLModel>();
            if (!string.IsNullOrEmpty(optionalLabel))
                list.Add(new DDLModel { Id = 0, Name = optionalLabel });
            var listType = (from f in context.Currency
                            select new DDLModel()
                            {
                                Id = f.Id,
                                Name = f.Name,
                            }).ToList();
            list.AddRange(listType);
            return list;
        }
        public List<DDLModel> GetDecreaseOrIncreaseName(string optionalLabel)
        {
            var list = new List<DDLModel>();
            if (!string.IsNullOrEmpty(optionalLabel))
                list.Add(new DDLModel { Id = 0, Name = optionalLabel });
            var listType = (from f in context.DecreaseOrIncrease
                            select new DDLModel()
                            {
                                Id = f.Id,
                                Name = f.Name,
                            }).ToList();
            list.AddRange(listType);
            return list;
        }
        public Warranty CreateNewWarranty(int? contractDataId, string userId)
        {
            //if any previous invoice is not done
            var prevWarranty = context.Warranty.Where(x => x.IsDeleted != true && x.ContractDataId == contractDataId).FirstOrDefault();
            if (prevWarranty != null)
                return prevWarranty;
            else
            {
                var Warranty = context.Warranty.Add(new Warranty { ContractDataId = contractDataId, CreatedBy = userId });
                context.SaveChanges();
                return Warranty;
            }
        }
        public List<WarrantyViewModel> SearchGrid(string ContractNumber, string WarrantyNumber, decimal? WarrantyValue, decimal? DecreaseOrIncreaseValue, int? ContractDataId, int? GuaranteeSortId, int? GuaranteeStatusId, int? WarrantyTypeId, int? CurrencyId, int? DecreaseOrIncreaseId, DateTime? GuaranteeIssueDate, DateTime? GuaranteeStartDate, DateTime? GuaranteeEndDate,DateTime? From,DateTime? To, string NoteWarranty, string CreatedBy,
    out int AllListCount, int jtStartIndex = 0,
    int jtPageSize = 10, string jtSorting = null)

        {
            var query = context.Warranty.Where
                (p => (string.IsNullOrEmpty(WarrantyNumber) || p.WarrantyNumber.Contains(WarrantyNumber))
            && (string.IsNullOrEmpty(NoteWarranty) || p.NoteWarranty.Contains(NoteWarranty)
            && (DecreaseOrIncreaseValue.HasValue ? p.DecreaseOrIncreaseValue == DecreaseOrIncreaseValue : DecreaseOrIncreaseValue == null)
            && (WarrantyValue.HasValue ? p.WarrantyValue == WarrantyValue : WarrantyValue == null)
            && (p.ContractDataId == ContractDataId || ContractDataId == 0 || ContractDataId == null)
            && (p.GuaranteeSortId == GuaranteeSortId || GuaranteeSortId == 0 || GuaranteeSortId == null)
            && (p.GuaranteeStatusId == GuaranteeStatusId || GuaranteeStatusId == 0 || GuaranteeStatusId == null)
            && (p.WarrantyTypeId == WarrantyTypeId || WarrantyTypeId == 0 || WarrantyTypeId == null)
            && (p.CurrencyId == CurrencyId || CurrencyId == 0 || CurrencyId == null)
            && (GuaranteeIssueDate.HasValue ? p.GuaranteeIssueDate == GuaranteeIssueDate : GuaranteeIssueDate == null)
            && (GuaranteeStartDate.HasValue ? p.GuaranteeStartDate == GuaranteeStartDate : GuaranteeStartDate == null)
            && (GuaranteeEndDate.HasValue ? p.GuaranteeEndDate == GuaranteeEndDate : GuaranteeEndDate == null)
            && (From.HasValue ? p.GuaranteeEndDate >= From : true)
            && (To.HasValue ? p.GuaranteeEndDate >= To : true)
            && (p.DecreaseOrIncreaseId == DecreaseOrIncreaseId || DecreaseOrIncreaseId == 0 || DecreaseOrIncreaseId == null)
            && p.IsDeleted != true));

            AllListCount = query.Count();
            var list = from f in query.AsQueryable()
                       select new WarrantyViewModel()
                       {
                           Id = f.Id,
                           WarrantyNumber = f.WarrantyNumber,
                           WarrantyValue = f.WarrantyValue,
                           DecreaseOrIncreaseValue = f.DecreaseOrIncreaseValue,
                           GuaranteeSortName = f.GuaranteeSort.Name,
                           GuaranteeSortId = f.GuaranteeSortId,
                           GuaranteeStatusName = f.GuaranteeStatus.Name,
                           GuaranteeStatusId = f.GuaranteeStatusId,
                           WarrantyTypeName = f.WarrantyType.Name,
                           WarrantyTypeId = f.WarrantyTypeId,
                           CurrencyName = f.Currency.Name,
                           CurrencyId = f.CurrencyId,
                           DecreaseOrIncreaseName = f.DecreaseOrIncrease.Name,
                           DecreaseOrIncreaseId = f.DecreaseOrIncreaseId,
                           GuaranteeIssueDate = f.GuaranteeIssueDate,
                           GuaranteeStartDate = f.GuaranteeStartDate,
                           GuaranteeEndDate = f.GuaranteeEndDate,
                           NoteWarranty = f.NoteWarranty,
                           CreatedByName = f.CreatedBy,
                           CreatedAt = f.CreatedAt,
                       };
            return list.OrderByDescending(f => f.Id).Skip(jtStartIndex).Take(jtPageSize).ToList();
        }
        public IQueryable<WarrantyViewModel> SearchGrid(int? contractDataId = null)
        {
            var query = context.Warranty;
            var list = from f in query.AsQueryable()
                       where f.IsDeleted != true
                        && (contractDataId > 0 ? f.ContractDataId == contractDataId : true)///to get data for the row selected 
                       select new WarrantyViewModel()
                       {
                           Id = f.Id,
                           ContractNumber = f.ContractData.ContractNumber,
                           WarrantyNumber = f.WarrantyNumber,
                           WarrantyValue = f.WarrantyValue,
                           DecreaseOrIncreaseValue = f.DecreaseOrIncreaseValue,
                           GuaranteeSortName = f.GuaranteeSort.Name,
                           GuaranteeSortId = f.GuaranteeSortId,
                           GuaranteeStatusName = f.GuaranteeStatus.Name,
                           GuaranteeStatusId = f.GuaranteeStatusId,
                           WarrantyTypeName = f.WarrantyType.Name,
                           WarrantyTypeId = f.WarrantyTypeId,
                           CurrencyName = f.Currency.Name,
                           CurrencyId = f.CurrencyId,
                           DecreaseOrIncreaseName = f.DecreaseOrIncrease.Name,
                           DecreaseOrIncreaseId = f.DecreaseOrIncreaseId,
                           GuaranteeIssueDate = f.GuaranteeIssueDate,
                           GuaranteeStartDate = f.GuaranteeStartDate,
                           GuaranteeEndDate = f.GuaranteeEndDate,
                           NoteWarranty = f.NoteWarranty,
                           CreatedByName = f.AspNetUsers.FullName,
                           CreatedAt = f.CreatedAt,
                           Path = f.Path,
                       };
            return list;
        }
        public IQueryable<WarrantyViewModel> GetNotifications()
        {
            var notificationDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            var query = context.Warranty;
            var list = from f in query.AsQueryable()
                       where f.IsDeleted != true
                       where f.GuaranteeEndDate >= notificationDate
                       select new WarrantyViewModel()
                       {
                           Id = f.Id,
                           ContractNumber = f.ContractData.ContractNumber,
                           WarrantyNumber = f.WarrantyNumber,
                           WarrantyValue = f.WarrantyValue,
                           GuaranteeEndDate = f.GuaranteeEndDate,
                           GuaranteeStatusName = f.GuaranteeStatus.Name,

                       };
            return list;
        }
    }
}
