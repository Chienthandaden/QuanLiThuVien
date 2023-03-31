using QuanLiThuVien.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLiThuVien.Controllers
{
    public class PhieuMuonController : Controller
    {
        // GET: PhieuMuon
       MyDataDataContext data = new MyDataDataContext();
        public List<Phieumuon> Layphieumuon()
        {
            List<Phieumuon> lstPhieumuon = Session["Phieumuon"] as List<Phieumuon>;
            if (lstPhieumuon == null)
            {
                lstPhieumuon = new List<Phieumuon>();
                Session["Phieumuon"] = lstPhieumuon;
            }
            return lstPhieumuon;
        }
        public ActionResult ThemPhieuMuon (int id, string strURL)
        {
            List<Phieumuon> lstPhieumuon = Layphieumuon();
            Phieumuon sanpham = lstPhieumuon.Find(n => n.MaSach ==id);
            if (sanpham== null)
            {
                sanpham = new Phieumuon(id);
                lstPhieumuon.Add(sanpham);
                return Redirect(strURL);


            }
            else
            {
                sanpham.iSoluong++;
                return Redirect(strURL);
            }
        }
        private int TongSoLuong()
        {
            int tsl = 0;
            List<Phieumuon> lstPhieumuon = Session["PhieuMuon"] as List<Phieumuon>;
            if (lstPhieumuon != null)
            {
                tsl = lstPhieumuon.Sum(n => n.iSoluong);
            }
            return tsl;
        }
        private int TongSoLuongPhieuMuon()
        {
            int tsl = 0;
            List<Phieumuon> lstPhieumuon = Session["PhieuMuon"] as List<Phieumuon>;
            if (lstPhieumuon != null)
            {
                tsl = lstPhieumuon.Count();
            }
            return tsl;
        }
        public ActionResult PhieuMuon()
        {
            List<Phieumuon> lstPhieumuon = Layphieumuon();
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongsoluongsanpham= TongSoLuongPhieuMuon();
            return View(lstPhieumuon);
        }
        public ActionResult PhieuMuonPartial()
        {
            List<Phieumuon> lstPhieumuon = Layphieumuon();
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongsoluongsanpham = TongSoLuongPhieuMuon();
            return PartialView();
        }
        public ActionResult XoaPhieumuon(int id)
        {
            List<Phieumuon> lstPhieumuon = Layphieumuon();
            Phieumuon sanpham = lstPhieumuon.SingleOrDefault(n => n.MaSach == id);
            if (sanpham != null)
            {
                lstPhieumuon.RemoveAll (n =>n.MaSach == id);
                return RedirectToAction("PhieuMuon");
            }
            return RedirectToAction("PhieuMuon");
        }

        public ActionResult CapnhatPhieumuon(int id, FormCollection collection)
        {
            List<Phieumuon> lstPhieumuon = Layphieumuon();
            Phieumuon sanpham = lstPhieumuon.SingleOrDefault(n => n.MaSach == id);
            if (sanpham != null)
            {
                sanpham.iSoluong = int.Parse(collection["txtSoLg"].ToString());

            }
            return RedirectToAction("PhieuMuon");
        }
        public ActionResult XoaTatCaPhieuMuon()
        {
            List<Phieumuon> lstPhieumuon = Layphieumuon();
            lstPhieumuon.Clear();
            return RedirectToAction("PhieuMuon");
        }

        public ActionResult MuonSach()
        {
            if (Session["TaiKhoan"] == null || Session["TaiKhoan"].ToString() == "")
            {
                return RedirectToAction("DangNhap", "Dangnhap");
            }
            if (Session["PhieuMuon"] == null)
            {
                //return RedirectToAction("Home", "Home");
                return RedirectToAction("GioHangTrong", "PhieuMuon");
            }
            List<Phieumuon> lstPhieumuon = Layphieumuon();
            if (lstPhieumuon.Count == 0)
            {
                ViewData["ErrorMessage"] = "Có lỗi xảy ra";
            }
            ViewBag.TongsoLuong = TongSoLuong();
           
            ViewBag.TongsoLuongsanpham = TongSoLuongPhieuMuon();
            return View(lstPhieumuon);
        }
        public ActionResult MuonSach(FormCollection collection)
        {
            PhieuMuon dh = new PhieuMuon();
            DocGia kh = (DocGia)Session["TaiKhoan"];
            Sach s = new Sach();
            List<Phieumuon> gh = Layphieumuon();
            //var ngaygiao = String.Format("{0:MM/dd/yyyy}", collection["NgayGiao"]);

            dh.MaDocGia = kh.MaDocGia;
            dh.NgayMuon = DateTime.Now;
            //dh.ngaygiao = DateTime.Parse(ngaygiao);
            
            
            data.PhieuMuons.InsertOnSubmit(dh);
            data.SubmitChanges();
            
           
            Session["PhieuMuon"] = null;
            ViewData["ErrorMessage"] = "Có lỗi xảy ra";
            return RedirectToAction("XacnhanPhieumuon", "PhieuMuon");
        }
        public ActionResult XacnhanPhieumuon()
        {
            return View();
        }
        public ActionResult PhieuMuonTrong()
        {
            return View();
        }
    }
}