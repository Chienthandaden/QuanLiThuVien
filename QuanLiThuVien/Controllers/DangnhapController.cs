using QuanLiThuVien.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace QuanLiThuVien.Controllers
{
    public class DangnhapController : Controller
    {
        // GET: Dangnhap
        MyDataDataContext data = new MyDataDataContext();
        public ActionResult Index()
        {
            Debug.WriteLine(Session["UserLoaitk"]);
            if (Session["UserLoaitk"].ToString() == "2")
            {
                ViewData["Error"] = "Don't empty!";
                return RedirectToAction("Index", "Home");
            }
            
            return View();
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(FormCollection collection, TaiKhoan tk)

        {
            var ten = collection["TenDangNhap"];
            var mk = collection["Matkhau"];
            var xnmk = collection["xnmk"];
            var check = data.TaiKhoans.FirstOrDefault(u => u.TenDangNhap == ten || u.MatKhau == mk);
            if(check != null)
            {
                if (check.TenDangNhap == ten)
                {
                    ViewData["TenDangNhap"] = "Ten dang nhap da co";
                    return this.Create();
                }
               
            }
            
            if (string.IsNullOrEmpty(xnmk))
            {
                ViewData["Error"] = "Phai nhap mk xac nhan";
            }
            else
            {

                if (!mk.Equals(xnmk))
                {
                    ViewData["MatKhauGiongNhau"] = "Mat khau va MKXN giong nhau";

                }
                else
                {
                    tk.TenDangNhap = ten;
                    tk.MatKhau = mk;
                    data.TaiKhoans.InsertOnSubmit(tk);
                    data.SubmitChanges();
                    return RedirectToAction("DangNhap");
                }
                
            }
            return this.Create();
        }
        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DangNhap(TaiKhoan tk)
        {
            Debug.WriteLine(tk.TenDangNhap);
            if (ModelState.IsValid)
            {
                var sign = data.TaiKhoans.Where(s => s.TenDangNhap.Equals(tk.TenDangNhap)).FirstOrDefault();
                
                if (sign != null)
                {
                    Session["UserId"] = sign.TenDangNhap.ToString();
                    Session["UserMk"] = sign.MatKhau.ToString();
                    Session["UserLoaitk"] = sign.LoaiTaiKhoan ;
                    Debug.WriteLine(sign.LoaiTaiKhoan);
                    Debug.WriteLine(sign.TenDangNhap);
                    Debug.WriteLine(sign.MatKhau);
                    if (sign.LoaiTaiKhoan == 1)
                    {
                        return RedirectToAction("Index", "Thuvien");
                    }
                    else if (sign.LoaiTaiKhoan == 2)
                            {
                        return RedirectToAction("Index", "Home");
                    }
                }

                

            }
            return View(tk);
        }

        public ActionResult Dangxuat()
        {
            Session["use"] = null;
            return RedirectToAction("Index", "Home");
        }

    }
}