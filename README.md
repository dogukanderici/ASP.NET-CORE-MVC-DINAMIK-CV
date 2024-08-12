# DinamikCV

**DinamikCV**, 6 katmandan oluþan bir .NET Core projesidir. Bu proje, katmanlý mimari kullanarak ölçeklenebilir ve modüler bir yapý sunmaktadýr.Projede, Entity Framework ile CRUD iþlemleri, Autofac ile Dependency Injection iþlemleri ve Fluent ile validasyon iþlemleri yapýlmýþtýr.

**Kullanýcý Ýþlemleri**

- Microsoft.AspNetCore.Identity kütüphanesi kullanýlarak yeni kayýt, kullanýcý giriþi, þifre deðiþtirme, kullanýcý rolleri iþlemleri,

**Admin Sayfasý**

- CV sayfasýndaki bilgilerin admin kullanýcýlarý tarafýndan güncellenebilmesi,
- Kullanýcýlara ait yeni rol tanýmlarýnýn yapýlmasý,
- Rol tanýmlarýnýn istenilen sayfalara tanýmlanmasý,
- Rollerin kullanýcýlara tanýmlanarak anlýk olarak sayfalarý görüntüleme izninin verilmesi

**Yazar Sayfasý**

- Ansayfa üzerinde gelen ve giden maillerin görüntülenebilmesi, yanýtlanmasý ve yeni mail gönderim iþlemlerinin yapýlmasý,
- Kullanýcýsýna ait profil bilgilerinin güncellenebilmesi (Ad/Soyad, Þifre ve Profil Fotoðrafý)

## Kullaným Rehberi

- Admin Sayfasý linkine týklandýðýnda sistemde kayýtlý kullanýcýnýz varsa giriþ yapabilir eðer yoksa yeni bir kullanýcý oluþturabilirsiniz.
- Eðer kullanýcýnýza Admin Rolü verilmiþse giriþ yapýldýktan sonra Admin Anasdayfasýna yönlendireleceksiniz. Burada Cv sayfasýndaki bilgileri güncelleme ve rol ile ilgili iþlemeleri yapabilirsiniz.
- Admin rolünüz yoksa Yazar Anasayfasýna yönlendireleceksiniz. Burada Gelen ve Giden mailleri görüntüleyebilir, yanýtlayabilir ve yen ileti oluþturabilirsiniz.
- Admin rolüne tanýmlanmadýðýnýz sürece Admin Anasayasýna eriþiminiz olmayacaktýr.
- Kullanýcý Rolü için sisteme ilk olarak Admin Rolünün AspNetRoles tablosuna Admin rolünün tanýmý ve sonrasýnda da AspNetUserRoles tablosuna Kullanýcý-Rol eþleþtirilmesinin yapýlmasý gerekmektedir. Ýlk kayýt iþleminden sonra tüm rol ve kullanýcý iþlemlerini Admin sayfasýndan yapabilirsiniz.

## Detaylý Açýklamalar

**Veritabaný**

- DataAccess katmanýndaki Concrete/Context klasörü içerisindeki PortfolyoContext içerisinde veritabaný baðlantý ayarlarýný yapýnýz.
- Veritabaný tablolarýnýn oluþmasý için Package Manager Console'dan Output Context'in bulunduðu DataAccess seçilip add-migration mig1 ile migrasyon dosyasý oluþturunuz.
- Oluþturulan migrasyon dosyasýndan sonra update-database ile veritabanýný güncelleyiniz ve tablolar oluþacaktýr.

**Admin Sayfasý Rol iþlemleri**

- Sisteme ilk giriþ yapýldýðýnda Admin tanýmý yapýldýktan sonra herhangi bir rol tanýmý bulunmayacaktýr. Rol tanýmý gerçekleþtirmek için Admin Sayfasýndaki Panel Rol Yönetimine týklayarak var olan rolleri görüntüleyebilir ve yeni rol tanýmý yapabilirsiniz.
- Kullanýcý Rol Yönetimi sayfasýna týkladýðýnýzda kullanýcý adýndan kullanýcý aramasý yaparak kullanýcýya ait rolleri görüntüleyebilir, yeni rol ekleyebilir veya rol çýkarabilirsiniz.

** Yazar Sayfasý**
- Yazar Anasayfasýnda bulunan hava durumu için https://api.openweathermap.org adresinden hesap oluþturup kendizine ait api key'inizi aldýktan sonra Portfolyo/Areas/User/Controllers/WriterDashbaord.cs dosyasýnda "your_api_key" alanýna yapýþtýrýnýz. (Sitenin api key'i geçerli hale getirmesi birkaç dakika sürmektedir.)
## Katmanlar

- **Business**: Ýþ mantýðýnýn bulunduðu katmandýr. Kullanýcý Giriþ-Çýkýþ, Login ve Register iþlemleri, CV sayfasýndaki verilerin veritabaný iþlemleri (listeleme, ekleme, güncelleme, silme) buradan yönetilir.
- **Core**: Genel altyapý ve yardýmcý sýnýflarýn yer aldýðý katmandýr. 
	- `DataAccess` klasörü içerisinde Entity Framework tanýmlamalarý (Abstarct ve Concrete klasörlerinde), 
	- `Entities` klasöründeki Entities katmanýndaki sýnýflara interafce implemenentasyonu için kullanýlan IEntity interface'i, 
	- `Utilities` klasörü altýnda bulunan Fileoperation ile klasöre veri kaydetme,
- Result ile Ýþlem Baþarý ve Hata Yönetimi,
- Security klasörüyle JWT token oluþturma iþlemleri bulunmaktadýr.
- **DataAccess**: Veritabaný eriþim katmanýdýr. Concrete klasöründe yer alan `PortfolyoContext.cs` sýnýfý ile veritabaný baðlantý ayarlarý yapýlýr.
- **Entities**: Veri tabanýna karþýlýk gelen nesnelerin tanýmlandýðý katman.
- **Presentation**: Uygulamanýn kullanýcý arayüzünün yer aldýðý katman (MVC).
- **API**: Web API katmaný, RESTful servislerin sunulduðu katman.

## Gereksinimler

- .NET Core SDK 6.0+
- MSSQL

## Kurulum

1. Bu projeyi yerel makinenize klonlayýn:
   ```bash
   git clone https://github.com/dogukanderici/AspNetCore-MVC-Dinamik-CV.git
   cd MyDotNetProject/src/MyDotNetProject
2. dotnet restore kütüphaneleri
3. dotnet ef database update --project DataAccess veritabaný tablolarýný güncelleyebilirsiniz.
