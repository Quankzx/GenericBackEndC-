# 🎯 Dự án Backend C# với Mediator & Clean Architecture

## 🚀 Giới thiệu

Đây là dự án tạo backend bằng **C#**, áp dụng **Mediator Pattern** và **Clean Architecture** để phát triển các nền tảng web ứng dụng hiện đại. Dự án hỗ trợ **đa loại database** và thiết kế theo kiến trúc microservices.

## 🏗 Công nghệ sử dụng
- **.NET Core / .NET 6+**
- **Mediator Pattern**
- **Clean Architecture**
- **FluentValidation**
- **AutoMapper**
- **Entity Framework Core** (hỗ trợ nhiều loại Database)
- **JWT Authentication**

## 📌 Cấu trúc thư mục
```bash
📂 Services.API          # API Gateway chính
📂 Services.Authen       # Xử lý xác thực, đăng nhập/đăng xuất
📂 Services.Library      # Chứa thư viện chung
📂 Services.Shared       # Các thành phần chia sẻ giữa các services
📂 .dockerignore         # Cấu hình Docker
📂 Services.sln          # Solution file chính
```

## 📚 Hướng dẫn cài đặt
1. **Clone repo:**
   ```sh
   git clone <repo-link>
   cd <repo-folder>
   ```
2. **Cấu hình database** trong `appsettings.json`
3. **Chạy ứng dụng:**
   ```sh
   dotnet run --project Services.API
   ```

## 🌟 Đóng góp & phát triển
Mọi đóng góp đều được hoan nghênh! Hãy tạo pull request hoặc issue để thảo luận.

🔥 Happy Coding! 🚀

