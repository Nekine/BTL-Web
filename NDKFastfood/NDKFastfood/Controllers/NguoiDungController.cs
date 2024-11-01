using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NDKFastfood.Models;
namespace NDKFastfood.Controllers
{
    public class NguoiDungController : Controller
    {
        // GET: NguoiDung
        dbKiwiFastfoodDataContext data = new dbKiwiFastfoodDataContext();
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult DangKy()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangKy(FormCollection collection, KhachHang kh)
        {
            var hoten = collection["HoTenKH"];
            var tendn = collection["TenDN"];
            var matkhau = collection["MatKhau"];
            var matkhaunhaplai = collection["MatKhauNhapLai"];
            var diachi = collection["DiaChi"];
            var email = collection["Email"];
            var dienthoai = collection["DienThoai"];
            var ngaysinh = String.Format("{0:MM/dd/yyyy}", collection["NgaySinh"]);

            // Kiểm tra các trường rỗng
            if (String.IsNullOrEmpty(hoten))
            {
                ViewData["loi1"] = "Họ tên khách hàng không được để trống";
            }
            else if (string.IsNullOrEmpty(tendn))
            {
                ViewData["loi2"] = "Phải nhập tên đăng nhập";
            }
            else if (string.IsNullOrEmpty(matkhau))
            {
                ViewData["loi3"] = "Phải nhập mật khẩu";
            }
            else if (matkhau.Length < 6)
            {
                ViewData["loi3"] = "Mật khẩu phải có ít nhất 6 ký tự";
            }
            else if (string.IsNullOrEmpty(matkhaunhaplai))
            {
                ViewData["loi4"] = "Phải nhập lại mật khẩu";
            }
            else if (!matkhau.Equals(matkhaunhaplai))
            {
                ViewData["loi4"] = "Mật khẩu nhập lại không khớp";
            }

            if (string.IsNullOrEmpty(diachi))
            {
                ViewData["loi5"] = "Địa chỉ không được bỏ trống";
            }

            if (string.IsNullOrEmpty(email))
            {
                ViewData["loi6"] = "Email không được bỏ trống";
            }
            else if (!System.Text.RegularExpressions.Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                ViewData["loi6"] = "Email không đúng định dạng";
            }

            if (string.IsNullOrEmpty(dienthoai))
            {
                ViewData["loi7"] = "Phải nhập điện thoại";
            }
            else if (!System.Text.RegularExpressions.Regex.IsMatch(dienthoai, @"^\d{10,11}$"))
            {
                ViewData["loi7"] = "Số điện thoại phải có từ 10 đến 11 chữ số";
            }

            // Nếu không có lỗi, thực hiện thêm khách hàng
            if (ViewData["loi1"] == null && ViewData["loi2"] == null && ViewData["loi3"] == null &&
                ViewData["loi4"] == null && ViewData["loi5"] == null && ViewData["loi6"] == null &&
                ViewData["loi7"] == null)
            {
                kh.HoTen = hoten;
                kh.TaiKhoan = tendn;
                kh.MatKhau = matkhau;
                kh.Email = email;
                kh.DiaChiKH = diachi;
                kh.DienThoaiKH = dienthoai;
                kh.NgaySinh = DateTime.Parse(ngaysinh);
                data.KhachHangs.InsertOnSubmit(kh);
                data.SubmitChanges();
                return RedirectToAction("DangNhap");
            }

            return this.DangKy();
        }

        public ActionResult DangNhap(FormCollection collection)
        {
            var tendn = collection["TenDN"];
            var matkhau = collection["MatKhau"];
            if (String.IsNullOrEmpty(tendn))
            {
                ViewData["loi1"] = "Phải nhập tên đăng nhập";
            }
            else if (String.IsNullOrEmpty(matkhau))
            {
                ViewData["loi2"] = "Phải nhập mật khẩu";
            }
            else
            {
                KhachHang kh = data.KhachHangs.SingleOrDefault(n => n.TaiKhoan == tendn && n.MatKhau == matkhau);
                if (kh != null)
                {
                    Session["TaiKhoan"] = kh;
                    return RedirectToAction("Index", "Home");
                }
                else
                    ViewBag.Thongbao = "TÊN ĐĂNG NHẬP HOẶC MẬT KHẨU KHÔNG ĐÚNG";
            }
            return View();
        }
        public String HienThi()
        {
            var user = Session["TaiKhoan"] as KhachHang;
            if (user != null)
            {
                return user.TaiKhoan;
            }
            else
            {
                return "Đăng nhập";
            }
        }
        public ActionResult DangXuat()
        {
            Session["TaiKhoan"] = null;
            return RedirectToAction("Index", "Home");
        }
    }
}