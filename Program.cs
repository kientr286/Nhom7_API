using System;
using System.Runtime.InteropServices;

class Program
{
    // Định nghĩa các hàm API

    // Hàm CreateFile dùng để mở hoặc tạo một tệp tin.
    // Nó trả về một con trỏ (IntPtr) đại diện cho tệp tin được mở hoặc tạo.
    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern IntPtr CreateFile(
        string lpFileName,
        uint dwDesiredAccess, 
        uint dwShareMode, 
        IntPtr lpSecurityAttributes, 
        uint dwCreationDisposition, 
        uint dwFlagsAndAttributes, 
        IntPtr hTemplateFile);

    // Hàm CloseHandle dùng để đóng tệp tin đã mở hoặc xử lý xong.
    [DllImport("kernel32.dll")]
    public static extern bool CloseHandle(IntPtr hObject);

    // Hàm ReadFile dùng để đọc dữ liệu từ tệp tin đã mở và lưu vào một buffer.
    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern int ReadFile(IntPtr hFile, byte[] lpBuffer, uint nNumberOfBytesToRead, out uint lpNumberOfBytesRead, IntPtr lpOverlapped);

    // Hàm WriteFile dùng để ghi dữ liệu từ một buffer vào tệp tin đã mở.
    [DllImport("kernel32.dll")]
    public static extern bool WriteFile(IntPtr hFile, byte[] lpBuffer, uint nNumberOfBytesToWrite, out uint lpNumberOfBytesWritten, IntPtr lpOverlapped);


   // Hàm DeleteFile dùng để xóa một tệp tin.
    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern bool DeleteFile(string lpFileName);

    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8; // Để hỗ trợ Unicode trong menu

        while (true) // Vòng lặp vô hạn để thực hiện nhiều lần
        {
            Console.WriteLine("Chọn một chức năng để thực hiện:");
            Console.WriteLine("1. Kiểm tra tệp có tồn tại không");
            Console.WriteLine("2. Tạo tệp mới");
            Console.WriteLine("3. Ghi dữ liệu vào tệp");
            Console.WriteLine("4. Đọc nội dung của tệp");
            Console.WriteLine("5. Sao chép tệp");
            Console.WriteLine("6. Xóa tệp");
            Console.WriteLine("0. Thoát chương trình.");
            Console.Write("Nhập lựa chọn của bạn: ");

            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                switch (choice)
                {
                    case 1:
                        CheckFileExists();
                        break;
                    case 2:
                        CreateFile();
                        break;
                    case 3:
                        WriteToFile();
                        break;
                    case 4:
                        ReadFile();
                        break;
                    case 5:
                        CopyFile();
                        break;
                    case 6:
                        DeleteFile();
                        break;
                    case 0:
                        Console.WriteLine("Kết thúc chương trình.");
                        return; 
                    default:
                        Console.WriteLine("Lựa chọn không hợp lệ.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Lựa chọn không hợp lệ.");
            }

            Console.Write("Tiếp tục? (Nhập 'n' để thoát, bất kỳ ký tự khác để tiếp tục): ");
            char response = Console.ReadKey().KeyChar;
            Console.WriteLine();

            if (response == 'n' || response == 'N')
            {
                Console.WriteLine("Kết thúc chương trình.");
                break; // Thoát khỏi vòng lặp nếu người dùng chọn 'n'
            }
        }
    }
    static void CheckFileExists()
    {
        Console.Write("Nhập tên tệp cần kiểm tra: ");
        string? fileName = Console.ReadLine();

        if (!string.IsNullOrEmpty(fileName) && FileExists(fileName))
        {
            Console.WriteLine("Tệp tồn tại.");
        }
        else
        {
            Console.WriteLine("Tệp không tồn tại.");
        }
    }

    static void CreateFile()
    {
        Console.Write("Nhập tên tệp mới: ");
        string? fileName = Console.ReadLine();

        IntPtr hFile = IntPtr.Zero;

        if (fileName != null)
        {
            hFile = CreateFile(fileName, 0x40000000, 1, IntPtr.Zero, 2, 0, IntPtr.Zero);
        }
        else
        {
            // Xử lý khi 'fileName' là null, ví dụ, bạn có thể thông báo lỗi hoặc thực hiện hành động khác tùy thuộc vào yêu cầu của ứng dụng.
            Console.WriteLine("Lỗi: Tên tệp là null.");
        }

        if (hFile.ToInt32() != -1)
        {
            Console.WriteLine("Đã tạo tệp.");
            CloseHandle(hFile);
        }
        else
        {
            Console.WriteLine("Không thể tạo tệp.");
        }
    }
    static void WriteToFile()
    {
        Console.Write("Nhập tên tệp để ghi dữ liệu: ");
        string? fileName = Console.ReadLine();

        if (fileName != null)
        {
            IntPtr hFile = CreateFile(fileName, 0x40000000, 1, IntPtr.Zero, 3, 0, IntPtr.Zero);

            if (hFile.ToInt32() != -1)
            {
                Console.Write("Nhập nội dung cần ghi: ");
                string? content = Console.ReadLine();

                byte[] data;
                if (content != null)
                {
                    data = System.Text.Encoding.Default.GetBytes(content);
                }
                else
                {
                    data = new byte[0];
                }

                uint bytesWritten = 0;
                WriteFile(hFile, data, (uint)data.Length, out bytesWritten, IntPtr.Zero);
                CloseHandle(hFile);
                Console.WriteLine("Đã ghi dữ liệu vào tệp.");
            }
            else
            {
                Console.WriteLine("Không thể ghi dữ liệu vào tệp.");
            }
        }
       
    }

    static void ReadFile()
    {
        Console.Write("Nhập tên tệp để đọc: ");
        string? fileName = Console.ReadLine();

        if (fileName != null)
        {
            IntPtr hFile = CreateFile(fileName, 0x80000000, 1, IntPtr.Zero, 3, 0, IntPtr.Zero);

            if (hFile.ToInt32() != -1)
            {
                byte[] buffer = new byte[4096];
                uint bytesRead = 0;
                ReadFile(hFile, buffer, (uint)buffer.Length, out bytesRead, IntPtr.Zero);
                CloseHandle(hFile);
                string content = System.Text.Encoding.Default.GetString(buffer, 0, (int)bytesRead);
                Console.WriteLine("Nội dung tệp:");
                Console.WriteLine(content);
            }
            else
            {
                Console.WriteLine("Không thể đọc tệp.");
            }
        }
        
    }

    static void CopyFile()
    {
        Console.Write("Nhập tên tệp nguồn: ");
        string? sourceFileName = Console.ReadLine();
        Console.Write("Nhập tên tệp đích: ");
        string? destFileName = Console.ReadLine();

        if (sourceFileName != null && destFileName != null)
        {
            IntPtr hSource = CreateFile(sourceFileName, 0x80000000, 1, IntPtr.Zero, 3, 0, IntPtr.Zero);
            IntPtr hDest = CreateFile(destFileName, 0x40000000, 1, IntPtr.Zero, 2, 0, IntPtr.Zero);

            if (hSource.ToInt32() != -1 && hDest.ToInt32() != -1)
            {
                byte[] buffer = new byte[4096];
                uint bytesRead = 0;
                uint bytesWritten = 0;

                bool success;
                while ((success = ReadFile(hSource, buffer, (uint)buffer.Length, out bytesRead, IntPtr.Zero) != 0) && bytesRead > 0)
                {
                    WriteFile(hDest, buffer, bytesRead, out bytesWritten, IntPtr.Zero);
                }

                CloseHandle(hSource);
                CloseHandle(hDest);
                Console.WriteLine("Đã sao chép tệp.");
            }
            else
            {
                Console.WriteLine("Không thể sao chép tệp.");
            }
        }
        
    }

    static void DeleteFile()
    {
        Console.Write("Nhập tên tệp cần xóa: ");
        string? fileName = Console.ReadLine();

        if (fileName != null)
        {
            if (DeleteFile(fileName))
            {
                Console.WriteLine("Đã xóa tệp.");
            }
            else
            {
                Console.WriteLine("Không thể xóa tệp.");
            }
        }
        
    }

    static bool FileExists(string fileName)
    {
        IntPtr hFile = CreateFile(fileName, 0, 1, IntPtr.Zero, 3, 0, IntPtr.Zero);
        if (hFile.ToInt32() != -1)
        {
            CloseHandle(hFile);
            return true;
        }
        return false;
    }
}
