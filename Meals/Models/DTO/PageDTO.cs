using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace Meals.Models.DTO
{
    public class PageOrderDTO
    {
        public IPagedList<SerchOrderDTO> list { get; set; }
        public int PageCount { get; set; }
    }
    public class PageProductlDTO
    {
        public IPagedList<ProductDTO> list { get; set; }
        public int PageCount { get; set; }
    }
}
