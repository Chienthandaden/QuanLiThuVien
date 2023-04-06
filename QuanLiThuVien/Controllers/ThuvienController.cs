using QuanLiThuVien.Models;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Diagnostics;
using System.Drawing.Printing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;

namespace QuanLiThuVien.Controllers
{
    public class ThuvienController : Controller
    {
        MyDataDataContext data = new MyDataDataContext();
        public ActionResult Index(string searchString, string sortOrder, string sortProperty)
        {
            Debug.WriteLine(Session["UserLoaitk"]);
            if (Session["UserLoaitk"].ToString() == "2")
            {
                ViewData["Error"] = "Don't empty!";
                return RedirectToAction("Index", "Home");
            }
            var all_sach = from s in data.Saches select s;
            if (!String.IsNullOrEmpty(searchString)) // kiểm tra chuỗi tìm kiếm có rỗng/null hay không
            {
                all_sach = all_sach.Where(s => s.TenSach.Contains(searchString)); //lọc theo chuỗi tìm kiếm
            }
           



            return View(all_sach);
        }



        public ActionResult Detail(int id)
        {
            var D_sach = data.Saches.Where(m => m.MaSach == id).First();
            return View(D_sach);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(FormCollection collection, Sach s)

        {
            var ten = collection["TenSach"];
            if (string.IsNullOrEmpty(ten))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                s.TenSach = ten;
                data.Saches.InsertOnSubmit(s);
                data.SubmitChanges();
                return RedirectToAction("Index");
            }
            return this.Create();
        }

        public ActionResult Edit(int id)
        {
            var E_sach = data.Saches.First(m => m.MaSach == id);
            return View(E_sach);
        }
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var sach = data.Saches.First(m => m.MaSach == id);
            var E_sach = collection["TenSach"];
            sach.MaSach = id;
            if (string.IsNullOrEmpty(E_sach))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                sach.TenSach = E_sach;
                UpdateModel(sach);
                data.SubmitChanges();
                return RedirectToAction("Index");
            }
            return this.Edit(id);
        }
        //-----------------------------------------
        public ActionResult Delete(int id)
        {
            var D_sach = data.Saches.First(m => m.MaSach == id);
            return View(D_sach);
        }
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var D_sach = data.Saches.Where(m => m.MaSach == id).First();
            data.Saches.DeleteOnSubmit(D_sach);
            data.SubmitChanges();
            return RedirectToAction("Index");
        }
        public string ProcessUpload(HttpPostedFileBase file)
        {
            if (file == null)
            {
                return "";
            }
            file.SaveAs(Server.MapPath("~/Content/img/sach/" + file.FileName));
            return "/Content/img/sach/" + file.FileName;
        }
    }
}