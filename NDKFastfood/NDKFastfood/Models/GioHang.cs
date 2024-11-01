using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NDKFastfood.Models
{
    public class GioHang
    {
        dbKiwiFastfoodDataContext data = new dbKiwiFastfoodDataContext();
        public int iMaMon { set; get; }
        public string sTenMon { set; get; }
        public double dGiaBan { set; get; }
        public string sAnhDD { set; get; }
        public int iSoLuong { set; get; }
        public double dThanhTien
        {
            get { return iSoLuong * dGiaBan; }
        }
        public GioHang(int MaMon)
        {
            iMaMon = MaMon;
            MonAn monan = data.MonAns.Single(n => n.MaMon == iMaMon);
            sTenMon = monan.TenMon;
            sAnhDD = monan.AnhDD;
            dGiaBan = double.Parse(monan.GiaBan.ToString());
            iSoLuong = 1;
        }
    }
}