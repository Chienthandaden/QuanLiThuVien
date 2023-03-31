using QuanLiThuVien.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLiThuVien.Controllers
{
    public class DocgiaController : Controller
    {
        // GET: Docgia
        MyDataDataContext data = new MyDataDataContext();
        public ActionResult Index()
        {
            Debug.WriteLine(Session["UserLoaitk"]);
            if (Session["UserLoaitk"].ToString() == "2")
            {
                ViewData["Error"] = "Don't empty!";
                return RedirectToAction("Index", "Home");
            }
            var all_docgia = from dg in data.DocGias select dg;
            return View(all_docgia);
        }
        public ActionResult Detail(int id)
        {
            var D_docgia = data.DocGias.Where(m => m.MaDocGia == id).First();
            return View(D_docgia);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(FormCollection collection, DocGia dg)
        {
            var ten = collection["HoTen"];
            var ngaysinh = Convert.ToDateTime(collection["NgaySinh"]);
            var gt = collection["GioiTinh"];
            var dc = collection["DiaChi"];
            var email = collection["Email"];
            var ngaylt= Convert.ToDateTime(collection["NgayLapThe"]);
            var ngayhh = Convert.ToDateTime(collection["NgayHetHan"]);
            var no = collection["TienNo"];
            if (string.IsNullOrEmpty(ten))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                dg.HoTen = ten.ToString();
                dg.NgaySinh = ngaysinh;
                dg.GioiTinh= gt.ToString();
                dg.DiaChi = dc.ToString();
                dg.Email = email.ToString();
                dg.NgayLapThe = ngaylt;
                dg.NgayHetHan = ngayhh;
                



                data.DocGias.InsertOnSubmit(dg);
                data.SubmitChanges();
                return RedirectToAction("Index");
            }
            return this.Create();
        }
        public ActionResult Edit(int id)
        {
            var E_category = data.DocGias.First(m => m.MaDocGia == id);
            return View(E_category);
        }
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var E_docgia = data.DocGias.First(m => m.MaDocGia == id);
            var ten = collection["HoTen"];
            var ngaysinh = Convert.ToDateTime(collection["NgaySinh"]);
            var gt = collection["GioiTinh"];
            var dc = collection["DiaChi"];
            var email = collection["Email"];
            var ngaylt = Convert.ToDateTime(collection["NgayLapThe"]);
            var ngayhh = Convert.ToDateTime(collection["NgayHetHan"]);
            var no = collection["TienNo"];

            var ngaytra = Convert.ToDateTime(collection["NgayHenTra"]);
            E_docgia.MaDocGia = id;
            if (string.IsNullOrEmpty(ten))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                E_docgia.HoTen= ten;
                E_docgia.NgaySinh = ngaysinh;
                E_docgia.GioiTinh = gt;
                E_docgia.DiaChi= dc;
                E_docgia.Email = email;
                E_docgia.NgayLapThe= ngaylt;
                E_docgia.NgayHetHan= ngayhh;
                
                UpdateModel(E_docgia);
                data.SubmitChanges();
                return RedirectToAction("Index");
            }
            return this.Edit(id);
        }
        public ActionResult Delete(int id)
        {
            var D_docgia = data.DocGias.First(m => m.MaDocGia == id);
            return View(D_docgia);
        }
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var D_docgia = data.DocGias.Where(m => m.MaDocGia == id).First();
            data.DocGias.DeleteOnSubmit(D_docgia);
            data.SubmitChanges();
            return RedirectToAction("Index");
        }
    }
}