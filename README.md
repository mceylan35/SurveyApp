# 📊 SurveyApp (Anket Uygulaması)

[![.NET Core](https://img.shields.io/badge/.NET%20Core-8.0-blue.svg)](https://dotnet.microsoft.com/download)
[![Angular](https://img.shields.io/badge/Angular-19-red.svg)](https://angular.io/)
[![MongoDB](https://img.shields.io/badge/MongoDB-4.4+-green.svg)](https://www.mongodb.com/)

## 📌 Proje Genel Bakış

Clean Architecture prensipleri kullanılarak .NET Core 7 (Backend) ve Angular 16 (Frontend) ile geliştirilmiş modern bir anket yönetim sistemi.

## 🚀 Özellikler

- 👤 Kullanıcı Kimlik Doğrulama (Giriş/Kayıt)
- 📝 Anket Yönetimi:
  - Çoklu seçenekli anket oluşturma
  - Sayfalı anket listesi görüntüleme
  - Anketlere oy verme
  - Anket sonuçlarını görüntüleme

## 🛠️ Kullanılan Teknolojiler

### Backend
- **.NET Core 8**
- **MongoDB** - NoSQL Veritabanı
- **MediatR** - CQRS Pattern implementasyonu
- **Clean Architecture**
- **JWT** - Kimlik Doğrulama

### Frontend
- **Angular 19**
- **Angular Material** - UI Bileşenleri
- **Tailwind CSS** - Stil
- **SweetAlert2** - Bildirimler
- **JWT** - Kimlik Doğrulama

## 📁 Proje Yapısı

### Backend (.NET Core)
```
SurveyApp/
├── Core/
│   ├── Domain/
│   └── Application/
├── Infrastructure/
└── API/
```

### Frontend (Angular)
```
src/
├── app/
│   ├── core/
│   │   ├── services/
│   │   ├── guards/
│   │   └── interceptors/
│   └── features/
│       ├── auth/
│       └── surveys/
└── main.ts
```

## ⚙️ Kurulum

### Gereksinimler
- Visual Studio 2022
- MongoDB
- Node.js (v16 veya üzeri)
- .NET Core 8 SDK
- Angular CLI

### Backend Kurulum

1. **MongoDB Kurulum**
```bash
# MongoDB'yi indirin ve yükleyin
# MongoDB Compass kurulumu önerilir (arayüz için)
# Varsayılan port: 27017
```

2. **Proje Ayarları**

`appsettings.json` dosyasını düzenleyin:
```json
{
  "MongoDbSettings": {
    "ConnectionString": "mongodb://localhost:27017",
    "DatabaseName": "SurveyAppDb"
  },
  "JwtSettings": {
    "Secret": "gizli-anahtariniz",
    "Issuer": "SurveyApp",
    "Audience": "SurveyAppClient",
    "ExpiryMinutes": 60
  }
}
```

3. **Projeyi Çalıştırma**

Visual Studio ile:
- Solution'ı açın
- API projesini başlangıç projesi yapın
- F5 ile çalıştırın

Terminal ile:
```bash
cd SurveyApp/API
dotnet run
```

### Frontend Kurulum

1. **Node.js ve Angular CLI Kurulum**
```bash
# Node.js'i yükleyin
npm install -g @angular/cli
```

2. **Proje Bağımlılıklarını Yükleme**
```bash
cd SurveyApp/ClientApp
npm install
```

3. **Environment Ayarları**
```typescript
// environment.ts
export const environment = {
  production: false,
  apiUrl: 'https://localhost:7027/api'  // Backend API URL
};
```

4. **Projeyi Çalıştırma**
```bash
ng serve
```

## 🔒 Güvenlik Özellikleri

- JWT Token bazlı kimlik doğrulama
- BCrypt ile şifre hashleme
- Korumalı rotalar (Guards)
- Token yönetimi için HTTP Interceptor'lar

## 💡 Kullanılan Best Practice'ler

- Clean Architecture
- CQRS Pattern
- Repository Pattern
- Dependency Injection
- Single Responsibility Principle
- Interface Segregation
- Hata Yönetimi
- API Versiyonlama
- Güvenli Kimlik Doğrulama
- Responsive Tasarım

 