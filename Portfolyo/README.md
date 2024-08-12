# DinamikCV

**DinamikCV**, 6 katmandan olu�an bir .NET Core projesidir. Bu proje, katmanl� mimari kullanarak �l�eklenebilir ve mod�ler bir yap� sunmaktad�r.Projede, Entity Framework ile CRUD i�lemleri, Autofac ile Dependency Injection i�lemleri ve Fluent ile validasyon i�lemleri yap�lm��t�r.

**Kullan�c� ��lemleri**

- Microsoft.AspNetCore.Identity k�t�phanesi kullan�larak yeni kay�t, kullan�c� giri�i, �ifre de�i�tirme, kullan�c� rolleri i�lemleri,

**Admin Sayfas�**

- CV sayfas�ndaki bilgilerin admin kullan�c�lar� taraf�ndan g�ncellenebilmesi,
- Kullan�c�lara ait yeni rol tan�mlar�n�n yap�lmas�,
- Rol tan�mlar�n�n istenilen sayfalara tan�mlanmas�,
- Rollerin kullan�c�lara tan�mlanarak anl�k olarak sayfalar� g�r�nt�leme izninin verilmesi

**Yazar Sayfas�**

- Ansayfa �zerinde gelen ve giden maillerin g�r�nt�lenebilmesi, yan�tlanmas� ve yeni mail g�nderim i�lemlerinin yap�lmas�,
- Kullan�c�s�na ait profil bilgilerinin g�ncellenebilmesi (Ad/Soyad, �ifre ve Profil Foto�raf�)

## Kullan�m Rehberi

- Admin Sayfas� linkine t�kland���nda sistemde kay�tl� kullan�c�n�z varsa giri� yapabilir e�er yoksa yeni bir kullan�c� olu�turabilirsiniz.
- E�er kullan�c�n�za Admin Rol� verilmi�se giri� yap�ld�ktan sonra Admin Anasdayfas�na y�nlendireleceksiniz. Burada Cv sayfas�ndaki bilgileri g�ncelleme ve rol ile ilgili i�lemeleri yapabilirsiniz.
- Admin rol�n�z yoksa Yazar Anasayfas�na y�nlendireleceksiniz. Burada Gelen ve Giden mailleri g�r�nt�leyebilir, yan�tlayabilir ve yen ileti olu�turabilirsiniz.
- Admin rol�ne tan�mlanmad���n�z s�rece Admin Anasayas�na eri�iminiz olmayacakt�r.
- Kullan�c� Rol� i�in sisteme ilk olarak Admin Rol�n�n AspNetRoles tablosuna Admin rol�n�n tan�m� ve sonras�nda da AspNetUserRoles tablosuna Kullan�c�-Rol e�le�tirilmesinin yap�lmas� gerekmektedir. �lk kay�t i�leminden sonra t�m rol ve kullan�c� i�lemlerini Admin sayfas�ndan yapabilirsiniz.

## Detayl� A��klamalar

**Veritaban�**

- DataAccess katman�ndaki Concrete/Context klas�r� i�erisindeki PortfolyoContext i�erisinde veritaban� ba�lant� ayarlar�n� yap�n�z.
- Veritaban� tablolar�n�n olu�mas� i�in Package Manager Console'dan Output Context'in bulundu�u DataAccess se�ilip add-migration mig1 ile migrasyon dosyas� olu�turunuz.
- Olu�turulan migrasyon dosyas�ndan sonra update-database ile veritaban�n� g�ncelleyiniz ve tablolar olu�acakt�r.

**Admin Sayfas� Rol i�lemleri**

- Sisteme ilk giri� yap�ld���nda Admin tan�m� yap�ld�ktan sonra herhangi bir rol tan�m� bulunmayacakt�r. Rol tan�m� ger�ekle�tirmek i�in Admin Sayfas�ndaki Panel Rol Y�netimine t�klayarak var olan rolleri g�r�nt�leyebilir ve yeni rol tan�m� yapabilirsiniz.
- Kullan�c� Rol Y�netimi sayfas�na t�klad���n�zda kullan�c� ad�ndan kullan�c� aramas� yaparak kullan�c�ya ait rolleri g�r�nt�leyebilir, yeni rol ekleyebilir veya rol ��karabilirsiniz.

** Yazar Sayfas�**
- Yazar Anasayfas�nda bulunan hava durumu i�in https://api.openweathermap.org adresinden hesap olu�turup kendizine ait api key'inizi ald�ktan sonra Portfolyo/Areas/User/Controllers/WriterDashbaord.cs dosyas�nda "your_api_key" alan�na yap��t�r�n�z. (Sitenin api key'i ge�erli hale getirmesi birka� dakika s�rmektedir.)
## Katmanlar

- **Business**: �� mant���n�n bulundu�u katmand�r. Kullan�c� Giri�-��k��, Login ve Register i�lemleri, CV sayfas�ndaki verilerin veritaban� i�lemleri (listeleme, ekleme, g�ncelleme, silme) buradan y�netilir.
- **Core**: Genel altyap� ve yard�mc� s�n�flar�n yer ald��� katmand�r. 
	- `DataAccess` klas�r� i�erisinde Entity Framework tan�mlamalar� (Abstarct ve Concrete klas�rlerinde), 
	- `Entities` klas�r�ndeki Entities katman�ndaki s�n�flara interafce implemenentasyonu i�in kullan�lan IEntity interface'i, 
	- `Utilities` klas�r� alt�nda bulunan Fileoperation ile klas�re veri kaydetme,
- Result ile ��lem Ba�ar� ve Hata Y�netimi,
- Security klas�r�yle JWT token olu�turma i�lemleri bulunmaktad�r.
- **DataAccess**: Veritaban� eri�im katman�d�r. Concrete klas�r�nde yer alan `PortfolyoContext.cs` s�n�f� ile veritaban� ba�lant� ayarlar� yap�l�r.
- **Entities**: Veri taban�na kar��l�k gelen nesnelerin tan�mland��� katman.
- **Presentation**: Uygulaman�n kullan�c� aray�z�n�n yer ald��� katman (MVC).
- **API**: Web API katman�, RESTful servislerin sunuldu�u katman.

## Gereksinimler

- .NET Core SDK 6.0+
- MSSQL

## Kurulum

1. Bu projeyi yerel makinenize klonlay�n:
   ```bash
   git clone https://github.com/dogukanderici/AspNetCore-MVC-Dinamik-CV.git
   cd MyDotNetProject/src/MyDotNetProject
2. dotnet restore k�t�phaneleri
3. dotnet ef database update --project DataAccess veritaban� tablolar�n� g�ncelleyebilirsiniz.
