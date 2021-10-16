using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System.Security.Principal;
using System.Text;
using System.Data;
using System.ComponentModel;
using Core.Admin.Helpers;
using System.Data.Entity;
using Core.Admin.Models;
using System.Web.UI;
using System.Data.Entity.Core.Objects;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;
using System.IO;
using System.Net.Mail;
using Core.AccessLayer;
using Core.Services;
using System.Net;
using System.Reflection;
using System.Xml;
using System.Net.Http;

namespace Core.Admin.Controllers
{
    public class ParentController : Controller
    {

        #region public Feilds
        public DatabseEntities context = new DatabseEntities();
        public PagesServices _PagesServices = new PagesServices();
        public UsersServices _UsersServices = new UsersServices();
        public CompanyServices _CompanyServices = new CompanyServices();
        public BankServices _BankServices = new BankServices();
        public SectorServices _SectorServices = new SectorServices();
        public AdministrationServices _AdministrationServices = new AdministrationServices();
        public GuaranteeSortServices _GuaranteeSortServices = new GuaranteeSortServices();
        public GuaranteeStatusServices _GuaranteeStatusServices = new GuaranteeStatusServices();
        public ContractDataServices _ContractDataServices = new ContractDataServices();
        public WarrantyServices _WarrantyServices = new WarrantyServices();

        #endregion
        protected override JsonResult Json(object data, string contentType, Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            //TODO: Test this new custom json result to avoid recursion instead of default JsonResult
            return new JsonNoRecursionResult()
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = behavior,
                MaxJsonLength = Int32.MaxValue
            };
        }
        public static DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Defining type of data column gives proper data table 
                var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name, type);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }
       
    }
}
