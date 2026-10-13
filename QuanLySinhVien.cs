using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab03_QuanLySinhVienOOP
{
    public class QuanLySinhVien
    {
        private readonly List<SinhVien> _danhSach = new List<SinhVien>();

        public bool Them(SinhVien sinhVien)
        {
            if (_danhSach.Any(sv => sv.MaSinhVien.Equals(sinhVien.MaSinhVien, StringComparison.OrdinalIgnoreCase)))
                return false;

            _danhSach.Add(sinhVien);
            return true;
        }

        public bool Sua(string maSinhVien, double diemMoi)
        {
            SinhVien? sv = TimTheoMa(maSinhVien);
            if (sv == null) return false;

            sv.DiemTrungBinh = diemMoi;
            return true;
        }

        public bool Xoa(string maSinhVien)
        {
            SinhVien? sv = TimTheoMa(maSinhVien);
            if (sv == null) return false;

            _danhSach.Remove(sv);
            return true;
        }

        public SinhVien? TimTheoMa(string maSinhVien)
        {
            return _danhSach.FirstOrDefault(sv =>
                sv.MaSinhVien.Equals(maSinhVien, StringComparison.OrdinalIgnoreCase));
        }

        public List<SinhVien> TimTheoTen(string tuKhoa)
        {
            return _danhSach
                .Where(sv => sv.HoTen.Contains(tuKhoa, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public List<SinhVien> SapXepTheoDiem()
        {
            return _danhSach.OrderByDescending(sv => sv.DiemTrungBinh).ToList();
        }

        public List<SinhVien> LayDanhSach()
        {
            return _danhSach.ToList();
        }

        public List<SinhVien> LocSinhVienDat()
        {
            return _danhSach.Where(sv => sv.DiemTrungBinh >= 5).ToList();
        }
    }
}
