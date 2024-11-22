# SurveyApp
Anket Uygulaması
Proje Genel Bakış
Clean Architecture prensipleri kullanılarak .NET Core 7 (Backend) ve Angular 16 (Frontend) ile geliştirilmiş bir anket yönetim sistemi.
Kullanılan Teknolojiler
Backend

.NET Core 7
MongoDB
MediatR (CQRS Pattern)
Clean Architecture
JWT Kimlik Doğrulama

Frontend

Angular 16
Angular Material
Tailwind CSS
SweetAlert2
JWT Kimlik Doğrulama

Özellikler

Kullanıcı Kimlik Doğrulama (Giriş/Kayıt)
Anket Yönetimi:

Çoklu seçenekli anket oluşturma
Sayfalı anket listesi görüntüleme
Anketlere oy verme
Anket sonuçlarını görüntüleme



Proje Yapısı
Backend (.NET Core)
CopySurveyApp/
├── Core/
│   ├── Domain/
│   └── Application/
├── Infrastructure/
└── API/
Frontend (Angular)
Copysrc/
├── app/
│   ├── core/
│   │   ├── services/
│   │   ├── guards/
│   │   └── interceptors/
│   └── features/
│       ├── auth/
│       └── surveys/
└── main.ts
Kurulum Gereksinimleri
Kurulum Talimatları
Gereksinimler

Visual Studio 2022
MongoDB
Node.js (v16 veya üzeri)
.NET Core 7 SDK
Angular CLI

Backend Kurulum

MongoDB Kurulum

bashCopy# MongoDB'yi indirin ve yükleyin
# MongoDB Compass kurulumu önerilir (arayüz için)
# Varsayılan port: 27017

Proje Ayarları


appsettings.json düzenleme:

jsonCopy{
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

Projeyi Çalıştırma


Visual Studio ile:

Solution'ı açın
API projesini başlangıç projesi yapın
F5 ile çalıştırın


Terminal ile:

bashCopycd SurveyApp/API
dotnet run

Frontend Kurulum

Node.js ve Angular CLI Kurulum

bashCopy# Node.js'i yükleyin
# Angular CLI kurulum
npm install -g @angular/cli

Proje Bağımlılıklarını Yükleme

bashCopycd SurveyApp/ClientApp
npm install

Environment Ayarları

typescriptCopy// environment.ts
export const environment = {
  production: false,
  apiUrl: 'https://localhost:7027/api'  // Backend API URL
};

Projeyi Çalıştırma

bashCopyng serve

Güvenlik Özellikleri

JWT Token bazlı kimlik doğrulama
BCrypt ile şifre hashleme
Korumalı rotalar (Guards)
Token yönetimi için HTTP Interceptor'lar

Kullanılan Best Practice'ler

Clean Architecture
CQRS Pattern
Repository Pattern
Dependency Injection
Single Responsibility Principle
Interface Segregation
Hata Yönetimi
API Versiyonlama
Güvenli Kimlik Doğrulama
Responsive Tasarım