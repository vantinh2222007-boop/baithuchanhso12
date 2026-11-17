
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Globalization;

class DateConverter : JsonConverter<DateTime>
{
    public override DateTime Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options)
    {
        return DateTime.ParseExact(
            reader.GetString(),
            "dd/MM/yyyy",
            CultureInfo.InvariantCulture);
    }

    public override void Write(
        Utf8JsonWriter writer,
        DateTime value,
        JsonSerializerOptions options)
    {
        writer.WriteStringValue(
            value.ToString("dd/MM/yyyy"));
    }
}

class NhanVien
{
    public string MaNV { get; set; }
    public string HoTen { get; set; }

    [JsonConverter(typeof(DateConverter))]
    public DateTime NgaySinh { get; set; }

    public double Luong { get; set; }

    public void HienThi()
    {
        Console.WriteLine($"Mã NV: {MaNV}");
        Console.WriteLine($"Họ tên: {HoTen}");
        Console.WriteLine($"Ngày sinh: {NgaySinh:dd/MM/yyyy}");
        Console.WriteLine($"Lương: {Luong}");
        Console.WriteLine("----------------------");
    }
}

class Program
{
    static void Main()
    {
    
        List<NhanVien> ds = new List<NhanVien>()
        {
            new NhanVien
            {
                MaNV = "NV01",
                HoTen = "Phan Văn Tỉnh",
                NgaySinh = new DateTime(2007, 5, 12),
                Luong = 10000000
            },

            new NhanVien
            {
                MaNV = "NV02",
                HoTen = "Nguyễn Văn Bảo",
                NgaySinh = new DateTime(2007, 8, 20),
                Luong = 12000000
            },

            new NhanVien
            {
                MaNV = "NV03",
                HoTen = "Lê Trung Duy",
                NgaySinh = new DateTime(2007, 3, 15),
                Luong = 15000000
            }
        };

   
        string json = JsonSerializer.Serialize(ds,
            new JsonSerializerOptions
            {
                WriteIndented = true
            });

        File.WriteAllText("nhanvien.json", json);

        Console.WriteLine("Da ghi danh sach vao file nhanvien.json");

        string data = File.ReadAllText("nhanvien.json");

        List<NhanVien> dsDoc =
            JsonSerializer.Deserialize<List<NhanVien>>(data);

        Console.WriteLine("\nDANH SACH NHAN VIEN:");

        foreach (NhanVien nv in dsDoc)
        {
            nv.HienThi();
        }
    }
}
