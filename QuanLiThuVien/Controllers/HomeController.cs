using PagedList;
using QuanLiThuVien.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Linq.Dynamic;
using System.Linq.Expressions;
using System.Data.SqlClient;

namespace QuanLiThuVien.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        MyDataDataContext data = new MyDataDataContext();
        public ActionResult Index(int? page, string searchString, string sortProperty, string sortOrder)
        {
           
            ViewBag.Keyword = searchString;
            if (page == null) page = 1;
            var Book = (from s in data.Saches select s).OrderBy(m => m.MaSach);
            if (!string.IsNullOrEmpty(searchString)) Book = (IOrderedQueryable<Sach>)Book.Where(a => a.TenSach.Contains(searchString));
            int pageSize = 6;
            int pageNum = page ?? 1;
            ViewBag.SortOrder = String.IsNullOrEmpty(sortOrder) ? "desc" : "";
           
            if (String.IsNullOrEmpty(sortProperty)) sortProperty = "MaSach";
            
            if (sortOrder == "desc")
                Book = (IOrderedQueryable<Sach>)Book.OrderBy(sortProperty + " desc");
            else
                Book = (IOrderedQueryable<Sach>)Book.OrderBy(sortProperty);

            return View(Book.ToPagedList(pageNum, pageSize));
        }
    }
}