using System.Collections.Generic;
using System.Linq;
using Core.AccessLayer;
using Core.ViewModels;
using System.Data.Entity;

namespace Core.Services
{
    public class PagesServices
    {
        private DatabseEntities context = new DatabseEntities();
        public Pages GetItemById(int Id)
        {
            Pages obj = context.Pages
                .Where(p => p.Id == Id).FirstOrDefault();

            return obj;
        }
        public List<PagesViewModel> SearchGrid(string Name,
            out int AllListCount, int jtStartIndex = 0, 
            int jtPageSize = 10, string jtSorting = null)
        {
            var query = context.Pages.Where(p => (string.IsNullOrEmpty(Name)
                   || p.Name.ToLower().Contains(Name.ToLower())));
            AllListCount = query.Count();
            var list = from f in query.AsQueryable()
                       select new PagesViewModel()
                       {
                           Id = f.Id,
                           Name = f.Name,
                           Url=f.Url,
                           Order=f.Order,
                           Icon =f.Icon,
                       };
            return list.OrderBy(f => f.Order).Skip(jtStartIndex).Take(jtPageSize).ToList();
        }
    }
}
