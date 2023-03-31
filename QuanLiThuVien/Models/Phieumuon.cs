using QuanLiThuVien.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuanLiThuVien.Models
{
    public class Phieumuon
    {
        MyDataDataContext data = new MyDataDataContext();
        public int MaSach { get; set; }

        [Display(Name = "Ten sach")]
        public string TenSach { get; set; }

        [Display(Name = "Tac Gia")]
        public string TacGia { get; set; }

        [Display(Name = "Nha Xuat Ban")]
        public string NhaXuatBan { get; set; }

        [Display(Name = "Anh bia")]
        public string HinhAnh { get; set; }

        [Display(Name = "Gia Tien")]
        public double GiaTien { get; set; }

        [Display(Name = "So luong")]
        public int iSoluong { get; set; }

       
        public Phieumuon(int id)
        {
            MaSach = id;
            Sach sach = data.Saches.Single(n => n.MaSach == MaSach);
            TenSach = sach.TenSach;
            HinhAnh = sach.HinhAnh;
            NhaXuatBan = sach.NhaXuatBan;
            iSoluong = 1;
        }
    }
}