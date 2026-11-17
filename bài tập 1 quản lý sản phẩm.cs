
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

class SanPham
{
    public string MaSP { get; set; }
    public string TenSP { get; set; }
    public int SoLuong { get; set; }
    public double DonGia { get; set; }

    public void HienThi()
    {
        Console.WriteLine($"Mã SP: {MaSP}");
        Console.WriteLine($"Tên SP: {TenSP}");
        Console.WriteLine($"Số lượng: {SoLuong}");
        Console.WriteLine($"Đơn giá: {DonGia}");
        Console.WriteLine($"Giá trị tồn kho: {SoLuong * DonGia}");
        Console.WriteLine("----------------------");
    }
}

class Program
{
    static void Main()
    {
  
        List<SanPham> ds = new List<SanPham>()
        {
            new SanPham { MaSP = "SP01", TenSP = "Laptop", SoLuong = 10, DonGia = 15000000 },
            new SanPham { MaSP = "SP02", TenSP = "Chuột", SoLuong = 50, DonGia = 200000 },
            new SanPham { MaSP = "SP03", TenSP = "Bàn phím", SoLuong = 30, DonGia = 500000 },
            new SanPham { MaSP = "SP04", TenSP = "Màn hình", SoLuong = 15, DonGia = 3000000 },
            new SanPham { MaSP = "SP05", TenSP = "Tai nghe", SoLuong = 40, DonGia = 400000 }
        };

        string json = JsonSerializer.Serialize(ds,
            new JsonSerializerOptions { WriteIndented = true });

        File.WriteAllText("sanpham.json", json);

        Console.WriteLine("Đã lưu danh sách vào file sanpham.json");

        // 3. Đọc dữ liệu từ file
        string data = File.ReadAllText("sanpham.json");

        List<SanPham> dsDoc = JsonSerializer.Deserialize<List<SanPham>>(data);

    
        Console.WriteLine("\nDANH SÁCH SẢN PHẨM:");

        foreach (SanPham sp in dsDoc)
        {
            sp.HienThi();
        }

        SanPham max = dsDoc
            .OrderByDescending(sp => sp.SoLuong * sp.DonGia)
            .First();

        Console.WriteLine("SẢN PHẨM CÓ GIÁ TRỊ TỒN KHO LỚN NHẤT:");
        max.HienThi();
    }
}
