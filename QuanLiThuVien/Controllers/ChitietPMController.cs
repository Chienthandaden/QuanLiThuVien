using QuanLiThuVien.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLiThuVien.Controllers
{
    public class ChitietPMController : Controller
    {
        // GET: ChitietPM
        MyDataDataContext data = new MyDataDataContext();
        public ActionResult chitiet()
        {
            Debug.WriteLine(Session["UserLoaitk"]);
            if (Session["UserLoaitk"].ToString() == "2")
            {
                ViewData["Error"] = "Don't empty!";
                return RedirectToAction("Index", "Home");
            }
            var all_phieumuon = from pm in data.PhieuMuons select pm;
            return View(all_phieumuon);
        }
        public ActionResult Detail(int id)
        {
            var D_phieumuon = data.PhieuMuons.Where(m => m.MaPhieuMuon == id).First();
            return View(D_phieumuon);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(FormCollection collection, PhieuMuon pm)
        {
            var docgia = collection["TenNguoiMuon"];
            var ngaymuon = Convert.ToDateTime(collection["NgayMuon"]);
            var ngaytra = Convert.ToDateTime(collection["NgayHenTra"]);
            if (string.IsNullOrEmpty(docgia))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                pm.TenNguoiMuon= docgia.ToString();
                pm.NgayMuon= ngaymuon;
                pm.NgayHenTra= ngaytra;

                data.PhieuMuons.InsertOnSubmit(pm);
                data.SubmitChanges();
                return RedirectToAction("Index");
            }
            return this.Create();
        }
        public ActionResult Edit(int id)
        {
            var E_category = data.PhieuMuons.First(m => m.MaPhieuMuon == id);
            return View(E_category);
        }
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var E_phieumuon = data.PhieuMuons.First(m => m.MaPhieuMuon == id);
            var docgia = collection["TenNguoiMuon"];
            var madocgia = collection["MaDocGia"];
            var masach = collection["MaSach"];

            var ngaymuon = Convert.ToDateTime(collection["NgayMuon"]);
            var ngaytra = Convert.ToDateTime(collection["NgayHenTra"]);
            E_phieumuon.MaPhieuMuon = id;
            if (string.IsNullOrEmpty(docgia))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                E_phieumuon.TenNguoiMuon = docgia;
                E_phieumuon.NgayMuon = ngaymuon;
                E_phieumuon.NgayHenTra = ngaytra;
                UpdateModel(E_phieumuon);
                data.SubmitChanges();
                return RedirectToAction("chitiet");
            }
            return this.Edit(id);
        }
        public ActionResult Delete(int id)
        {
            var D_phieumuon = data.PhieuMuons.First(m => m.MaPhieuMuon == id);
            return View(D_phieumuon);
        }
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var D_phieumuon = data.PhieuMuons.Where(m => m.MaPhieuMuon == id).First();
            data.PhieuMuons.DeleteOnSubmit(D_phieumuon);
            data.SubmitChanges();
            return RedirectToAction("Index");
        }
    }
}