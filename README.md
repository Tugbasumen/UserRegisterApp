
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

<img width="1919" height="912" alt="Ekran görüntüsü 2025-11-22 141840" src="https://github.com/user-attachments/assets/adc40172-7596-40df-a8d5-f6f2595e56d3" />
<img width="1879" height="866" alt="Ekran görüntüsü 2025-11-22 142136" src="https://github.com/user-attachments/assets/7aa8cf7e-7d71-4db7-9b24-a6c06b511782" />
<img width="658" height="580" alt="Ekran görüntüsü 2025-11-22 141934" src="https://github.com/user-attachments/assets/729703ff-b999-46ed-be14-ac086c61d192" />
<img width="698" height="740" alt="Ekran görüntüsü 2025-11-22 142009" src="https://github.com/user-attachments/assets/bec26d24-593d-49ef-85a3-452bd5fdb604" /><img width="1918" height="885" alt="Ekran görüntüsü 2025-11-22 142035" src="https://github.com/user-attachments/assets/75327bc6-5a97-4568-8e8a-a5bcdff98d28" />


---
