
# UserRegisterApp – Kullanıcı Kayıt ve Yönetim Uygulaması

Bu proje, kullanıcıların isim, e-mail, parola ve profil resmi bilgileriyle kayıt olmasını sağlayan; aynı zamanda kullanıcıları listeleme, düzenleme ve silme özelliklerini içeren bir web uygulamasıdır.

Kullanılan Teknolojiler:

* C# / .NET 8 MVC
* MSSQL(SQL Server)
* Entity Framework Core
* Bootstrap 5
* BCrypt.Net(şifre hashing)
* xUnit + Moq (unit test altyapısı)
* Docker (container)**

---

##  Özellikler

* Kullanıcı Ekleme:

  * İsim, e-mail, parola alma
  * Profil resmi yükleme (jpg, jpeg, png)
  * Yüklenen resmin sunucuda saklanması

* Kullanıcı Listeleme

  * Profil resmi
  * Ad ve e-mail bilgileri
  * Düzenle & sil butonları

* Kullanıcı Düzenleme

  * Bilgilerin güncellenmesi
  * Profil fotoğrafının değiştirilmesi

* Kullanıcı Silme

  * Silme onay ekranı
  * Veritabanından kaldırma

* Şifre Güvenliği

  * Şifreler BCrypt ile hash'lenir, düz metin saklanmaz.

* Frontend

  * Bootstrap tabanlı sayfa tasarımları
  * Modern ve responsive görünüm

* Docker Container

  * Proje, Dockerfile ile tamamen containerize edilebilir.

* Unit Test Projesi

  * xUnit + EF Core InMemory + Moq altyapısı hazırdır.

---

## Kurulum

### Depoyu Klonla

```bash
git clone <repo-url>
cd UserRegisterApp
```

### Bağlantı Ayarları (appsettings.json)

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=UserDb;Trusted_Connection=True;TrustServerCertificate=True"
}
```

Gerekirse kendi SQL Server bağlantınızı yazabilirsiniz.

---

## Veritabanı Oluşturma

Proje EF Core ile çalışıyor.

### Migration oluşturmak için:

```bash
dotnet ef migrations add InitialCreate
```

### Veritabanını oluşturmak için:

```bash
dotnet ef database update
```

Veya SQL Server’da manuel olarak **UserDb** DB’sini oluşturabilirsiniz.

---
