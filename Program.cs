using System;
using System.Globalization;
using System.Collections.Generic;

namespace Lab03_QuanLySinhVienOOP
{
    internal class Program
    {
        private static readonly QuanLySinhVien QuanLy = new QuanLySinhVien();

        private static void Main()
        {
            int luaChon;
            do
            {
                HienThiMenu();
                luaChon = NhapSoNguyen("Chọn chức năng: ");

                switch (luaChon)
                {
                    case 1: ThemSinhVien(); break;
                    case 2: XuatDanhSach(QuanLy.LayDanhSach()); break;
                    case 3: TimSinhVienTheoMa(); break;
                    case 4: TimSinhVienTheoTen(); break;
                    case 5: SuaDiem(); break;
                    case 6: XoaSinhVien(); break;
                    case 7: XuatDanhSach(QuanLy.SapXepTheoDiem()); break;
                    case 8: XuatDanhSach(QuanLy.LocSinhVienDat()); break;
                    case 0: Console.WriteLine("Đã thoát chương trình."); break;
                    default: Console.WriteLine("Lựa chọn không hợp lệ. Vui lòng chọn lại."); break;
                }

                if (luaChon != 0)
                {
                    Console.WriteLine();
                    Console.Write("Nhấn Enter để tiếp tục...");
                    Console.ReadLine();
                }
            } while (luaChon != 0);
        }

        private static void HienThiMenu()
        {
            Console.Clear();
            Console.WriteLine("========== QUẢN LÝ SINH VIÊN ==========");
            Console.WriteLine("1. Thêm sinh viên");
            Console.WriteLine("2. Xuất danh sách");
            Console.WriteLine("3. Tìm sinh viên theo mã");
            Console.WriteLine("4. Tìm sinh viên theo tên");
            Console.WriteLine("5. Sửa điểm trung bình");
            Console.WriteLine("6. Xóa sinh viên");
            Console.WriteLine("7. Sắp xếp theo điểm giảm dần");
            Console.WriteLine("8. Lọc sinh viên đạt (điểm >= 5)");
            Console.WriteLine("0. Thoát");
            Console.WriteLine("=======================================");
        }

        private static void ThemSinhVien()
        {
            Console.WriteLine("--- THÊM SINH VIÊN ---");
            string ma = NhapChuoi("Mã sinh viên: ");
            if (QuanLy.TimTheoMa(ma) != null)
            {
                Console.WriteLine("Mã sinh viên đã tồn tại.");
                return;
            }

            string hoTen = NhapChuoi("Họ tên: ");
            DateTime ngaySinh = NhapNgay("Ngày sinh (dd/MM/yyyy): ");
            string maLop = NhapChuoi("Mã lớp: ");
            double diem = NhapDiem("Điểm trung bình (0-10): ");

            var sv = new SinhVien(ma, hoTen, ngaySinh, maLop, diem);
            Console.WriteLine(QuanLy.Them(sv) ? "Thêm sinh viên thành công." : "Mã sinh viên đã tồn tại.");
        }

        private static void TimSinhVienTheoMa()
        {
            string ma = NhapChuoi("Nhập mã sinh viên cần tìm: ");
            SinhVien? sv = QuanLy.TimTheoMa(ma);
            if (sv == null) Console.WriteLine("Không tìm thấy sinh viên.");
            else Console.WriteLine(sv.LayThongTin());
        }

        private static void TimSinhVienTheoTen()
        {
            string tuKhoa = NhapChuoi("Nhập từ khóa họ tên: ");
            List<SinhVien> ketQua = QuanLy.TimTheoTen(tuKhoa);
            XuatDanhSach(ketQua);
        }

        private static void SuaDiem()
        {
            string ma = NhapChuoi("Nhập mã sinh viên cần sửa điểm: ");
            if (QuanLy.TimTheoMa(ma) == null)
            {
                Console.WriteLine("Không tìm thấy sinh viên.");
                return;
            }

            double diem = NhapDiem("Nhập điểm trung bình mới (0-10): ");
            QuanLy.Sua(ma, diem);
            Console.WriteLine("Cập nhật điểm thành công.");
        }

        private static void XoaSinhVien()
        {
            string ma = NhapChuoi("Nhập mã sinh viên cần xóa: ");
            Console.WriteLine(QuanLy.Xoa(ma) ? "Xóa sinh viên thành công." : "Không tìm thấy sinh viên.");
        }

        private static void XuatDanhSach(List<SinhVien> danhSach)
        {
            Console.WriteLine();
            if (danhSach.Count == 0)
            {
                Console.WriteLine("Danh sách trống hoặc không có kết quả phù hợp.");
                return;
            }

            Console.WriteLine("DANH SÁCH SINH VIÊN");
            Console.WriteLine(new string('-', 115));
            Console.WriteLine("{0,-12} {1,-25} {2,-13} {3,-12} {4,8} {5,-15}",
                "Mã SV", "Họ tên", "Ngày sinh", "Mã lớp", "Điểm TB", "Xếp loại");
            Console.WriteLine(new string('-', 115));

            foreach (SinhVien sv in danhSach)
            {
                Console.WriteLine("{0,-12} {1,-25} {2,-13} {3,-12} {4,8:F1} {5,-15}",
                    sv.MaSinhVien, RutGon(sv.HoTen, 24), sv.NgaySinh.ToString("dd/MM/yyyy"),
                    sv.MaLop, sv.DiemTrungBinh, sv.XepLoai());
            }
            Console.WriteLine(new string('-', 115));
            Console.WriteLine($"Tổng số sinh viên: {danhSach.Count}");
        }

        private static string RutGon(string chuoi, int doDai)
        {
            return chuoi.Length <= doDai ? chuoi : chuoi.Substring(0, doDai - 3) + "...";
        }

        private static string NhapChuoi(string thongBao)
        {
            while (true)
            {
                Console.Write(thongBao);
                string? input = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(input)) return input.Trim();
                Console.WriteLine("Dữ liệu không được để trống. Vui lòng nhập lại.");
            }
        }

        private static int NhapSoNguyen(string thongBao)
        {
            while (true)
            {
                Console.Write(thongBao);
                string? input = Console.ReadLine();
                if (int.TryParse(input, out int ketQua)) return ketQua;
                Console.WriteLine("Vui lòng nhập một số nguyên hợp lệ.");
            }
        }

        private static DateTime NhapNgay(string thongBao)
        {
            while (true)
            {
                Console.Write(thongBao);
                string? input = Console.ReadLine();
                if (DateTime.TryParseExact(input, "dd/MM/yyyy", CultureInfo.InvariantCulture,
                    DateTimeStyles.None, out DateTime ngay))
                {
                    if (ngay <= DateTime.Today) return ngay;
                    Console.WriteLine("Ngày sinh không được ở tương lai.");
                }
                else Console.WriteLine("Ngày không hợp lệ. Hãy nhập theo định dạng dd/MM/yyyy.");
            }
        }

        private static double NhapDiem(string thongBao)
        {
            while (true)
            {
                Console.Write(thongBao);
                string? input = Console.ReadLine();
                if (double.TryParse(input, NumberStyles.Float, CultureInfo.CurrentCulture, out double diem) ||
                    double.TryParse(input, NumberStyles.Float, CultureInfo.InvariantCulture, out diem))
                {
                    if (diem >= 0 && diem <= 10) return diem;
                    Console.WriteLine("Điểm không hợp lệ. Điểm phải từ 0 đến 10.");
                }
                else Console.WriteLine("Vui lòng nhập một số hợp lệ, ví dụ 8,5 hoặc 8.5.");
            }
        }
    }
}
