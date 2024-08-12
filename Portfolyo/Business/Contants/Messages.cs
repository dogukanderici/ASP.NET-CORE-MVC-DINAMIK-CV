using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Contants
{
    public class Messages
    {
        public static string QuerySuccess = "DB Sorgusu Başarılı";
        public static string AddedData = "DB Veri Kaydı Başarılı";
        public static string DeletedData = "DB Veri Silme Başarılı";
        public static string UpdatedData = "DB Veri Güncelleme Başarılı";
        public static string AccessTokenCreated = "Access Token Oluşturuldu";
        public static string UserNotFound = "Kullanıcı Bulunamadı";
        public static string InvalidPassword = "Hatalı Şifre";
        public static string LoginSuccess = "Giriş Başarılı";
        public static string UserAlreadyExists = "Kullanıcı Zaten Kayıtlı";
        public static string UserRegistered = "Kullanıcı Kaydedildi.";
        public static string UserRegisteredError = "Kullanıcı Kaydı Yapılamadı";
    }
}
