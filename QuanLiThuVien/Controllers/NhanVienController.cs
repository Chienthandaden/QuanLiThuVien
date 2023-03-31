using QuanLiThuVien.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLiThuVien.Controllers
{
    public class NhanVienController : Controller
    {
        // GET: NhanVien
        MyDataDataContext data = new MyDataDataContext();
        public ActionResult Index()
        {
            Debug.WriteLine(Session["UserLoaitk"]);
            if (Session["UserLoaitk"].ToString() == "2")
            {
                ViewData["Error"] = "Don't empty!";
                return RedirectToAction("Index", "Home");
            }
            var all_nhanvien = from nv in data.NhanViens select nv;
            return View(all_nhanvien);
        }
        public ActionResult Detail(int id)
        {
            var D_nhanvien = data.NhanViens.Where(m => m.MaNhanVien == id).First();
            return View(D_nhanvien);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(FormCollection collection, NhanVien nv)
        {
            var ten = collection["HoTen"];
            var ngaysinh = Convert.ToDateTime(collection["NgaySinh"]);
            var gt = collection["GioiTinh"];
            var dc = collection["DiaChi"];
            var email = collection["Email"];
            
            if (string.IsNullOrEmpty(ten))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                nv.HoTen = ten.ToString();
                nv.NgaySinh = ngaysinh;
                nv.GioiTinh = gt.ToString();
                nv.DiaChi = dc.ToString();
                nv.Email = email.ToString();
               
                data.NhanViens.InsertOnSubmit(nv);
                data.SubmitChanges();
                return RedirectToAction("Index");
            }
            return this.Create();
        }
        public ActionResult Edit(int id)
        {
            var E_category = data.NhanViens.First(m => m.MaNhanVien == id);
            return View(E_category);
        }
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var E_nhanvien = data.NhanViens.First(m => m.MaNhanVien == id);
            var ten = collection["HoTen"];
            var ngaysinh = Convert.ToDateTime(collection["NgaySinh"]);
            var gt = collection["GioiTinh"];
            var dc = collection["DiaChi"];
            var email = collection["Email"];

            E_nhanvien.MaNhanVien = id;
            if (string.IsNullOrEmpty(ten))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                E_nhanvien.HoTen = ten;
                E_nhanvien.NgaySinh = ngaysinh;
                E_nhanvien.GioiTinh = gt;
                E_nhanvien.DiaChi = dc;
                E_nhanvien.Email = email;
                

                UpdateModel(E_nhanvien);
                data.SubmitChanges();
                return RedirectToAction("Index");
            }
            return this.Edit(id);
        }
        public ActionResult Delete(int id)
        {
            var D_nhanvien = data.NhanViens.First(m => m.MaNhanVien == id);
            return View(D_nhanvien);
        }
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var D_nhanvien = data.NhanViens.Where(m => m.MaNhanVien == id).First();
            data.NhanViens.DeleteOnSubmit(D_nhanvien);
            data.SubmitChanges();
            return RedirectToAction("Index");
        }
    }
}