using QuanLiThuVien.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLiThuVien.Controllers
{
    public class ChitietPTController : Controller
    {
        // GET: ChitietPT
        MyDataDataContext data = new MyDataDataContext();
        public ActionResult Index()
        {
            Debug.WriteLine(Session["UserLoaitk"]);
            if (Session["UserLoaitk"].ToString() == "2")
            {
                ViewData["Error"] = "Don't empty!";
                return RedirectToAction("Index", "Home");
            }
            var all_phieutra = from pt in data.PhieuTras select pt;
            return View(all_phieutra);
        }
        public ActionResult Detail(int id)
        {
            var D_phieutra = data.PhieuTras.Where(m => m.MaPhieuTra == id).First();
            return View(D_phieutra);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(FormCollection collection, PhieuTra pt)
        {
            var docgia = collection["TenNguoiTra"];
            
            var ngaytra = Convert.ToDateTime(collection["NgayTra"]);
            if (string.IsNullOrEmpty(docgia))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                pt.TenNguoiTra = docgia.ToString();
                pt.NgayTra = ngaytra;
                

                data.PhieuTras.InsertOnSubmit(pt);
                data.SubmitChanges();
                return RedirectToAction("Index");
            }
            return this.Create();
        }
        public ActionResult Edit(int id)
        {
            var E_category = data.PhieuTras.First(m => m.MaPhieuTra == id);
            return View(E_category);
        }
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var E_phieutra = data.PhieuTras.First(m => m.MaPhieuTra == id);
            var docgia = collection["TenNguoiTra"];
            var madocgia = collection["MaDocGia"];
            var masach = collection["MaSach"];
            var lido = collection["LiDoPhat"];
            var tienphat= Convert.ToDecimal(collection["TienPhat"]);
            
            var ngaytra = Convert.ToDateTime(collection["NgayHenTra"]);
            E_phieutra.MaPhieuTra = id;
            if (string.IsNullOrEmpty(docgia))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                E_phieutra.TenNguoiTra = docgia;
                E_phieutra.NgayTra = ngaytra;
                E_phieutra.LiDoPhat= lido;
                UpdateModel(E_phieutra);
                data.SubmitChanges();
                return RedirectToAction("Index");
            }
            return this.Edit(id);
        }
        public ActionResult Delete(int id)
        {
            var D_phieutra = data.PhieuTras.First(m => m.MaPhieuTra == id);
            return View(D_phieutra);
        }
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var D_phieutra = data.PhieuTras.Where(m => m.MaPhieuTra == id).First();
            data.PhieuTras.DeleteOnSubmit(D_phieutra);
            data.SubmitChanges();
            return RedirectToAction("Index");
        }
    }
}