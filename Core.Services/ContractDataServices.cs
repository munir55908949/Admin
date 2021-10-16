using System.Collections.Generic;
using System.Linq;
using Core.AccessLayer;
using Core.ViewModels;
using System.Data.Entity;
using System;
using System.Globalization;

namespace Core.Services
{
    public class ContractDataServices
    {
        private DatabseEntities context = new DatabseEntities();
        public ContractData GetContractDataById(int Id)
        {
            ContractData obj = context.ContractData
                .Where(p => p.Id == Id )
                .FirstOrDefault();

            return obj;
        }
        public ContractDataViewModel GetContractDataDetailsById(int Id)
        {
            return context.ContractData
                .Where(p => p.Id == Id)
                .Select(x => new ContractDataViewModel
                {
                    ContractNumber = x.ContractNumber,
                    CompanyName = x.Company.Name,
                    BankName = x.Bank.Name,
                    SectorName = x.Sector.Name,
                    AdministrationName = x.Administration.Name,
                    ContractStatusName = x.ContractStatus.Name,
                    Note = x.Note,
                }).FirstOrDefault();
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
        public List<DDLModel> GetContractStatusName(string optionalLabel)
        {
            var list = new List<DDLModel>();
            if (!string.IsNullOrEmpty(optionalLabel))
                list.Add(new DDLModel { Id = 0, Name = optionalLabel });
            var listType = (from f in context.ContractStatus
                            select new DDLModel()
                            {
                                Id = f.Id,
                                Name = f.Name,
                            }).ToList();
            list.AddRange(listType);
            return list;
        }
        //public ContractDataViewModel GetContractDataDetailsById2(int Id)
        //{
        //    return context.ContractData
        //        .Where(p => p.Id == Id && p.IsDeleted != true)
        //        .Select(f => new ContractDataViewModel
        //        {
        //            Id = f.Id,
        //            PoNo = f.PoNo,
        //            PracticeNo = f.PracticeNo,
        //            ContractDataNo = f.ContractDataNo,
        //            ContractDataValue = f.ContractDataValue,
        //            //TotalContractDataValue = f.TotalContractDataValue,
        //            ContractDataEndDate = f.ContractDataEndDate,
        //            ContractDataStatusName = f.ContractDataStatus.Name,
        //            ContractDataStatusId = f.ContractDataStatusId,
        //            BankName = f.Bank.Name,
        //            BankId = f.BankId,
        //            CompanyName = f.Company.Name,
        //            CompanyId = f.CompanyId,
        //            Notes = f.Notes,
        //            CreatedByName = f.CreatedBy,
        //            CreatedAt = f.CreatedAt,
        //        })
        //        .FirstOrDefault();

        //}
        public ContractData CreateNewContractData(int? Id)
        {
            //if any previous invoice is not done
            var prevContractData = context.ContractData.Where(x => x.IsDeleted != true ).FirstOrDefault();
            if (prevContractData != null)
                return prevContractData;
            else
            {
                var ContractData = context.ContractData.Add(new ContractData());
                context.SaveChanges();
                return ContractData;
            }
        }

        public List<ContractDataViewModel> SearchGrid(string ContractNumber, int? CompanyId,int? BankId,int? SectorId,int? AdministrationId, int? ContractStatusId, string Note, string CreatedBy,out int AllListCount, int jtStartIndex = 0, int jtPageSize = 10, string jtSorting = null)
        {
            var query = context.ContractData.Where
                (p => (string.IsNullOrEmpty(ContractNumber) || p.ContractNumber.Contains(ContractNumber))
            && (string.IsNullOrEmpty(Note) || p.Note.Contains(Note)
            && (p.BankId == BankId || BankId == 0 || BankId == null)
            && (p.CompanyId == CompanyId || CompanyId == 0 || CompanyId == null)
            && (p.SectorId == SectorId || SectorId == 0 || SectorId == null)
            && (p.AdministrationId == AdministrationId || AdministrationId == 0 || AdministrationId == null)
            && (p.ContractStatusId == ContractStatusId || ContractStatusId == 0 || ContractStatusId == null)
            && p.IsDeleted != true));

            AllListCount = query.Count();
            var list = from f in query.AsQueryable()
                       select new ContractDataViewModel()
                       {
                           Id = f.Id,
                           ContractNumber = f.ContractNumber,
                           CompanyName = f.Company.Name,
                           CompanyId = f.CompanyId,
                           BankName = f.Bank.Name,
                           BankId = f.BankId,
                           SectorName = f.Sector.Name,
                           SectorId = f.SectorId,
                           AdministrationName = f.Administration.Name,
                           AdministrationId = f.AdministrationId,
                           ContractStatusName = f.ContractStatus.Name,
                           ContractStatusId = f.ContractStatusId,
                           Note = f.Note,
                           CreatedByName = f.CreatedBy,
                           CreatedAt = f.CreatedAt,
                       };
            return list.OrderByDescending(f => f.Id).Skip(jtStartIndex).Take(jtPageSize).ToList();
        }
        public IQueryable<ContractDataViewModel> SearchGrid()
        {
            var query = context.ContractData;
            var list = from f in query.AsQueryable()
                       where f.IsDeleted != true
                       select new ContractDataViewModel()
                       {
                           Id = f.Id,
                           ContractNumber = f.ContractNumber,
                           CompanyName = f.Company.Name,
                           CompanyId = f.CompanyId,
                           BankName = f.Bank.Name,
                           BankId = f.BankId,
                           SectorName = f.Sector.Name,
                           SectorId = f.SectorId,
                           AdministrationName = f.Administration.Name,
                           AdministrationId = f.AdministrationId,
                           ContractStatusName = f.ContractStatus.Name,
                           ContractStatusId = f.ContractStatusId,
                           Note = f.Note,
                           CreatedByName = f.AspNetUsers.FullName,
                           CreatedAt = f.CreatedAt,
                           Path = f.Path,
                       };
            return list;
        }
        // When i generate Linkage I Uncomment this method
    //    public List<ContractDataViewModel> SearchContractDataDetailsGrid(string PoNo, string PracticeNo, string ContractDataNo, decimal? ContractDataValue,  DateTime? ContractDataEndDate, int? ContractDataStatusId, int? BankId, int? CompanyId, string Notes, string CreatedBy,
    //out int AllListCount, int jtStartIndex = 0,
    //int jtPageSize = 10, string jtSorting = null)

    //    {
    //        var query = context.ContractData.Where
    //            (p => (string.IsNullOrEmpty(PracticeNo) || p.PracticeNo.Contains(PracticeNo))
    //        && (string.IsNullOrEmpty(ContractDataNo) || p.ContractDataNo.Contains(ContractDataNo)
    //        && (p.CompanyId == CompanyId || CompanyId == 0 || CompanyId == null)
    //        && p.IsDeleted != true));

    //        AllListCount = query.Count();
    //        var list = from f in query.AsQueryable()
    //                   select new ContractDataViewModel()
    //                   {
    //                       Id = f.Id,
    //                       PoNo = f.PoNo,
    //                       PracticeNo = f.PracticeNo,
    //                       ContractDataNo = f.ContractDataNo,
    //                       ContractDataValue = f.ContractDataValue,
    //                       ContractDataEndDate = f.ContractDataEndDate,
    //                       ContractDataStatusName = f.ContractDataStatus.Name,
    //                       ContractDataStatusId = f.ContractDataStatusId,
    //                       BankName = f.Bank.Name,
    //                       BankId = f.BankId,
    //                       CompanyName = f.Company.Name,
    //                       CompanyId = f.CompanyId,
    //                       Notes = f.Notes,
    //                       CreatedByName = f.CreatedBy,
    //                       CreatedAt = f.CreatedAt,
    //                   };
    //        return list.OrderByDescending(f => f.Id).Skip(jtStartIndex).Take(jtPageSize).ToList();
    //    }
    //    public IQueryable<ContractDataViewModel> SearchContractDataDetailsGrid(int? id)
    //    {
    //        var query = context.ContractDataDetails;
    //        var list = from f in query.AsQueryable()
    //                   where f.IsDeleted != true 
    //                   && f.ContractDataId == id //to get data for the row selected 
    //                   select new ContractDataViewModel()
    //                   {
    //                       Id = f.Id,
    //                       Notes = f.Notes,
    //                       CreatedByName = f.AspNetUsers.FullName,
    //                       CreatedAt = f.CreatedAt,
    //                       NewContractDataEndDate = f.NewContractDataEndDate,
    //                       ModifiedAt = f.ModifiedAt,
    //                   };
    //        return list;
    //    }

        
    }
}
