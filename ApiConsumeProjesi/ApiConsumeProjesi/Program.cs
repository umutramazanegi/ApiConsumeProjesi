#region Menü_Başlangıcı

using Newtonsoft.Json; // JSON.NET kütüphanesini içeri aktarır (JSON işleme için).
using Newtonsoft.Json.Linq; // JSON.NET'in LINQ uzantılarını içeri aktarır (JSON nesnelerinde gezinmek için).
using System; // Temel sistem sınıflarını içeri aktarır (Console, string, int vb.).
using System.Text; // Metin kodlama (encoding) işlemlerini içeri aktarır (HTTP isteği içeriği için).
// using System.Text.Json.Nodes; // System.Text.Json'dan JsonNode'u içeri aktarır (Bu projede aktif kullanılmıyor gibi görünüyor, Newtonsoft kullanılıyor).

Console.WriteLine("Api Consume İşlemine Hoş Geldiniz"); // Kullanıcıya hoş geldiniz mesajı gösterir.
Console.WriteLine(); // Boş bir satır yazdırır.
Console.WriteLine("### Yapmak İstediğiniz İşlemi Seçin ###"); // İşlem menüsünü gösterir.
Console.WriteLine(); // Boş bir satır yazdırır.
Console.WriteLine("1-Şehir Listesini Getirin"); // Seçenek 1'i gösterir.
Console.WriteLine("2-Şehir ve Hava Durumu Listesini Getirin"); // Seçenek 2'yi gösterir.
Console.WriteLine("3-Yeni Şehir Ekleme"); // Seçenek 3'ü gösterir.
Console.WriteLine("4-Şehir Silme İşlemi"); // Seçenek 4'ü gösterir.
Console.WriteLine("5-Şehir Güncelleme İşlemi"); // Seçenek 5'i gösterir.
Console.WriteLine("6-ID'ye Göre Şehir Getirme"); // Seçenek 6'yı gösterir.
Console.WriteLine(); // Boş bir satır yazdırır.

#endregion
string number; // Kullanıcının seçimini tutacak değişkeni tanımlar.
Console.Write("Tercihiniz: "); // Kullanıcıdan seçim yapmasını ister.
number = Console.ReadLine(); // Kullanıcının girdiği değeri okur ve 'number' değişkenine atar.
Console.WriteLine(); // Boş bir satır yazdırır.

if (number == "1") // Eğer kullanıcı "1" girdiyse:
{
    string url = "https://localhost:7151/api/Weathers"; // Tüm şehirleri listeleyen API endpoint'inin URL'sini tanımlar.
    using (HttpClient client = new HttpClient()) // Yeni bir HttpClient nesnesi oluşturur (using bloğu iş bitince kaynağı serbest bırakır).
    {
        HttpResponseMessage response = await client.GetAsync(url); // Belirtilen URL'ye GET isteği gönderir ve yanıtı bekler.
        string responseBody = await response.Content.ReadAsStringAsync(); // Yanıtın içeriğini string olarak okur.
        JArray jArray = JArray.Parse(responseBody); // Gelen JSON string'ini bir JSON dizisine (JArray) dönüştürür.
        foreach (var item in jArray) // Dizideki her bir JSON nesnesi için döngü başlatır.
        {
            string cityName = item["cityName"].ToString(); // JSON nesnesinden 'cityName' alanının değerini alır.
            Console.WriteLine($"Şehir: {cityName}"); // Şehir adını konsola yazdırır.
        }
    }
}
if (number == "2") // Eğer kullanıcı "2" girdiyse:
{
    string url = "https://localhost:7151/api/Weathers"; // Tüm şehir ve detayları listeleyen API endpoint'inin URL'sini tanımlar.
    using (HttpClient client = new HttpClient()) // Yeni bir HttpClient nesnesi oluşturur.
    {
        HttpResponseMessage response = await client.GetAsync(url); // Belirtilen URL'ye GET isteği gönderir.
        string responseBody = await response.Content.ReadAsStringAsync(); // Yanıtın içeriğini string olarak okur.
        JArray jArray = JArray.Parse(responseBody); // Gelen JSON string'ini bir JArray'e dönüştürür.
        foreach (var item in jArray) // Dizideki her bir JSON nesnesi için döngü başlatır.
        {
            string cityName = item["cityName"].ToString(); // JSON'dan şehir adını alır.
            string temp = item["temp"].ToString(); // JSON'dan sıcaklık değerini alır.
            string country = item["country"].ToString(); // JSON'dan ülke adını alır.
            Console.WriteLine(cityName + "-" + country + "-->" + temp + " Derece"); // Şehir, ülke ve sıcaklık bilgisini yazdırır.
            Console.WriteLine("------------------------------------------------------"); // Ayırıcı çizgi yazdırır.
        }
    }
}
if (number == "3") // Eğer kullanıcı "3" girdiyse (Yeni şehir ekleme):
{
    Console.WriteLine("### Yeni Veri Girişi ###"); // Başlık yazdırır.
    Console.WriteLine(); // Boş satır.
    string cityName, country, detail; // Girilecek metin bilgiler için değişkenler tanımlar.
    decimal temp; // Girilecek sıcaklık için değişken tanımlar.

    Console.Write("Şehir Adı: "); // Kullanıcıdan şehir adı ister.
    cityName = Console.ReadLine(); // Girilen şehir adını okur.

    Console.Write("Ülke Adı: "); // Kullanıcıdan ülke adı ister.
    country = Console.ReadLine(); // Girilen ülke adını okur.

    Console.Write("Hava Durumu Detayı: "); // Kullanıcıdan detay ister.
    detail = Console.ReadLine(); // Girilen detayı okur.

    Console.Write("Sıcaklık: "); // Kullanıcıdan sıcaklık ister.
    temp = decimal.Parse(Console.ReadLine()); // Girilen sıcaklığı okur ve decimal tipine çevirir.


    string url = "https://localhost:7151/api/Weathers"; // Veri ekleme (POST) yapılacak API endpoint'inin URL'sini tanımlar.
    var newWeatherCity = new // Gönderilecek veriyi içeren anonim bir nesne oluşturur.
    {
        CityName = cityName, // Şehir adı alanını ayarlar.
        Country = country, // Ülke alanı ayarlar.
        Detail = detail, // Detay alanı ayarlar.
        Temp = temp // Sıcaklık alanı ayarlar.
    };

    using (HttpClient client = new HttpClient()) // Yeni bir HttpClient nesnesi oluşturur.
    {
        string json = JsonConvert.SerializeObject(newWeatherCity); // Oluşturulan nesneyi JSON formatına dönüştürür.
        StringContent content = new StringContent(json, Encoding.UTF8, "application/json"); // JSON string'ini HTTP isteği içeriği (body) haline getirir, UTF8 kodlaması ve JSON medya tipi ile.
        HttpResponseMessage response = await client.PostAsync(url, content); // Belirtilen URL'ye POST isteği gönderir (oluşturulan içerikle birlikte).
        response.EnsureSuccessStatusCode(); // Yanıtın başarılı olup olmadığını kontrol eder (hata varsa exception fırlatır).
        Console.WriteLine("Şehir başarıyla eklendi."); // Başarı mesajı yazdırır.
    }
}
if (number == "4") // Eğer kullanıcı "4" girdiyse (Şehir silme):
{
    // Dikkat: URL yapısı '...api/Weathers?id=' gibi olmalı, 'Weathersid=' değil. Düzeltildi varsayılıyor.
    string baseUrl = "https://localhost:7151/api/Weathers?id="; // Silme (DELETE) yapılacak API endpoint'inin temel URL'sini tanımlar.

    Console.Write("Silmek istediğiniz Id Değeri: "); // Kullanıcıdan silinecek ID'yi ister.
    int id = int.Parse(Console.ReadLine()); // Girilen ID'yi okur ve integer'a çevirir.

    using (HttpClient client = new HttpClient()) // Yeni bir HttpClient nesnesi oluşturur.
    {
        // ID'yi URL'ye ekleyerek tam silme URL'sini oluşturur.
        HttpResponseMessage response = await client.DeleteAsync(baseUrl + id); // Belirtilen URL'ye (ID ile birlikte) DELETE isteği gönderir.
        response.EnsureSuccessStatusCode(); // Yanıtın başarılı olup olmadığını kontrol eder.
        Console.WriteLine(id + " ID'li şehir başarıyla silindi."); // Başarı mesajı yazdırır.
    }
}
if (number == "5") // Eğer kullanıcı "5" girdiyse (Şehir güncelleme):
{
    string url = "https://localhost:7151/api/Weathers"; // Güncelleme (PUT) yapılacak API endpoint'inin URL'sini tanımlar.

    Console.WriteLine("### Veri Güncelleme İşlemi ###"); // Başlık yazdırır.
    Console.WriteLine(); // Boş satır.
    string cityName, country, detail; // Güncellenecek metin bilgiler için değişkenler tanımlar.
    decimal temp; // Güncellenecek sıcaklık için değişken tanımlar.
    int cityId; // Güncellenecek şehrin ID'si için değişken tanımlar.

    // Kullanıcıdan güncellenecek tüm bilgileri alır
    Console.Write("Güncellenecek Şehir Id: ");
    cityId = int.Parse(Console.ReadLine()); // ID'yi okur.

    Console.Write("Yeni Şehir Adı: ");
    cityName = Console.ReadLine(); // Yeni şehir adını okur.

    Console.Write("Yeni Ülke Adı: ");
    country = Console.ReadLine(); // Yeni ülke adını okur.

    Console.Write("Yeni Hava Durumu Detayı: ");
    detail = Console.ReadLine(); // Yeni detayı okur.

    Console.Write("Yeni Sıcaklık: ");
    temp = decimal.Parse(Console.ReadLine()); // Yeni sıcaklığı okur.


    var updatedWeatherValues = new // Gönderilecek güncel veriyi içeren anonim bir nesne oluşturur.
    {
        CityId = cityId, // Şehir ID'sini ayarlar.
        CityName = cityName, // Yeni şehir adını ayarlar.
        Country = country, // Yeni ülke adını ayarlar.
        Detail = detail, // Yeni detayı ayarlar.
        Temp = temp // Yeni sıcaklığı ayarlar.
    };

    using (HttpClient client = new HttpClient()) // Yeni bir HttpClient nesnesi oluşturur.
    {
        string json = JsonConvert.SerializeObject(updatedWeatherValues); // Güncel veriyi JSON formatına dönüştürür.
        StringContent content = new StringContent(json, Encoding.UTF8, "application/json"); // JSON string'ini HTTP içeriği haline getirir.
        HttpResponseMessage response = await client.PutAsync(url, content); // Belirtilen URL'ye PUT isteği gönderir (güncel içerikle birlikte).
        response.EnsureSuccessStatusCode(); // Yanıtın başarılı olup olmadığını kontrol eder.
        Console.WriteLine(cityId + " ID'li şehir başarıyla güncellendi."); // Başarı mesajı yazdırır.
    }
}
if (number == "6") // Eğer kullanıcı "6" girdiyse (ID'ye göre şehir getirme):
{
    string baseUrl = "https://localhost:7151/api/Weathers/GetByIdWeatherCity?id="; // Belirli bir ID'ye sahip şehri getiren API endpoint'inin temel URL'sini tanımlar.

    Console.Write("Bilgilerini Getirmek İstediğiniz Id Değeri: "); // Kullanıcıdan ID ister.
    int id = int.Parse(Console.ReadLine()); // Girilen ID'yi okur ve integer'a çevirir.
    Console.WriteLine(); // Boş satır yazdırır.

    using (HttpClient client = new HttpClient()) // Yeni bir HttpClient nesnesi oluşturur.
    {
        HttpResponseMessage response = await client.GetAsync(baseUrl + id); // Belirtilen URL'ye (ID ile birlikte) GET isteği gönderir.
        response.EnsureSuccessStatusCode(); // Yanıtın başarılı olup olmadığını kontrol eder.
        string responseBody = await response.Content.ReadAsStringAsync(); // Yanıtın içeriğini string olarak okur.
        JObject weatherCityObject = JObject.Parse(responseBody); // Gelen JSON string'ini tek bir JSON nesnesine (JObject) dönüştürür.

        string cityName = weatherCityObject["cityName"].ToString(); // JSON nesnesinden şehir adını alır.
        string detail = weatherCityObject["detail"].ToString(); // JSON nesnesinden detayı alır.
        string country = weatherCityObject["country"].ToString(); // JSON nesnesinden ülke adını alır.
        decimal temp = decimal.Parse(weatherCityObject["temp"].ToString()); // JSON nesnesinden sıcaklığı alır ve decimal'e çevirir.

        Console.WriteLine("Girmiş olduğunuz Id değerine ait bilgiler"); // Bilgilendirme mesajı yazdırır.
        Console.WriteLine(); // Boş satır yazdırır.
        Console.Write("Şehir: " + cityName + " Ülke: " + country + " Detay: " + detail + " Sıcaklık: " + temp); // Alınan bilgileri konsola yazdırır.


    }
}

Console.WriteLine("\n\nİşlem tamamlandı. Çıkmak için bir tuşa basın."); // İşlemler bittikten sonra mesaj gösterir.
Console.Read(); // Kullanıcı bir tuşa basana kadar konsol penceresini açık tutar.

// Örnek API URL'leri (muhtemelen test veya referans için bırakılmış).
// https://localhost:7151/api/Weathers?id=8  -> Muhtemelen silme (DELETE) veya ID ile getirme (GET) için query string parametresi.
// https://localhost:7151/api/Weathers/GetByIdWeatherCity?id=5 -> Özel bir action metoda ID ile getirme (GET) için query string parametresi.