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
            //var all_phim = from BanPhim in db.BanPhims select BanPhim;
            //return View(all_phim);
            ViewBag.Keyword = searchString;
            if (page == null) page = 1;
            var Book = (from s in data.Saches select s).OrderBy(m => m.MaSach);
            if (!string.IsNullOrEmpty(searchString)) Book = (IOrderedQueryable<Sach>)Book.Where(a => a.TenSach.Contains(searchString));
            int pageSize = 9;
            int pageNum = page ?? 1;
            ViewBag.SortOrder = String.IsNullOrEmpty(sortOrder) ? "desc" : "";

            //2.Tạo câu truy vấn kết 2 bảng Book, Author, Category

            // 4. Tạo thuộc tính sắp xếp mặc định là "Title"
            if (String.IsNullOrEmpty(sortProperty)) sortProperty = "MaSach";

            // 5. Sắp xếp tăng/giảm bằng phương thức OrderBy sử dụng trong thư viện Dynamic LINQ
            if (sortOrder == "desc")
                Book = (IOrderedQueryable<Sach>)Book.OrderBy(sortProperty + " desc");
            else
                Book = (IOrderedQueryable<Sach>)Book.OrderBy(sortProperty);

            return View(Book.ToPagedList(pageNum, pageSize));
        }
    }
}