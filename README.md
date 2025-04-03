# ApiConsumeProjesi 💻➡️☁️

Bu proje, .NET 6 üzerinde C# ile geliştirilmiş bir **Console uygulamasıdır**. Amacı, daha önce oluşturulan `ApiHavaDurumuProjesi` adlı ASP.NET Core Web API'sinin endpoint'lerini **tüketmeyi (consume)** göstermektir. API ile iletişim kurmak için `System.Net.Http.HttpClient` sınıfı ve JSON verilerini işlemek için `Newtonsoft.Json` kütüphanesi kullanılmıştır.

## 🚀 Genel Bakış

Bu Console uygulaması, çalışan `ApiHavaDurumuProjesi`'ne HTTP istekleri göndererek (GET, POST, PUT, DELETE) şehir hava durumu verileri üzerinde işlemler yapar. Kullanıcıya sunulan basit bir menü aracılığıyla API'nin farklı özelliklerini (listeleme, ekleme, silme, güncelleme, ID ile getirme, istatistik sorguları) tetikler ve sonuçları konsola yazdırır. Bu proje, bir .NET uygulamasından bir Web API ile nasıl etkileşim kurulacağını öğrenmek için pratik bir örnektir.

## ✨ Özellikler ve Gösterilen Teknikler

*   **API Tüketimi:** `HttpClient` kullanarak bir Web API'ye istek gönderme.
*   **Asenkron İşlemler:** Ağ isteklerini `async/await` ile asenkron olarak gerçekleştirme.
*   **HTTP Metotları:**
    *   `GET` istekleri (Tüm verileri listeleme, ID ile getirme, istatistikleri alma).
    *   `POST` isteği (Yeni veri ekleme).
    *   `PUT` isteği (Mevcut veriyi güncelleme).
    *   `DELETE` isteği (Veri silme).
*   **JSON İşleme:**
    *   Gönderilecek veriyi C# nesnesinden JSON formatına çevirme (`JsonConvert.SerializeObject`).
    *   API'den gelen JSON yanıtını C# tarafında işlemek için `Newtonsoft.Json` (`JArray`, `JObject`) kullanma.
*   **HTTP İçeriği:** `StringContent` kullanarak POST ve PUT istekleri için JSON içeriği oluşturma.
*   **Konsol Menüsü:** Kullanıcıya basit bir metin tabanlı menü sunarak farklı API işlemlerini tetikleme.

## 🛠️ Kullanılan Teknolojiler

*   **Programlama Dili:** C#
*   **Framework:** .NET 6
*   **Uygulama Tipi:** Console Application
*   **HTTP İstemcisi:** `System.Net.Http.HttpClient`
*   **JSON Kütüphanesi:** `Newtonsoft.Json`

## ⚙️ Çalıştırma Öncesi Gereksinimler

Bu projenin çalışabilmesi için **öncelikle `ApiHavaDurumuProjesi` Web API'sinin çalışıyor olması gerekmektedir.**

1.  **`ApiHavaDurumuProjesi` Kurulumu:**
    *   Eğer yapmadıysanız, `ApiHavaDurumuProjesi`'ni klonlayın ve onun README dosyasındaki kurulum adımlarını takip ederek veritabanını oluşturun ve API projesini çalıştırın.
    *   API'nin çalıştığı adresi (varsayılan olarak `https://localhost:7151` veya `http://localhost:5127` gibi) not alın.

## 💾 Bu Projenin (ApiConsumeProjesi) Kurulumu ve Çalıştırılması

1.  **Projeyi Klonlama:**
    ```bash
    git clone https://github.com/kullanici-adiniz/ApiConsumeProjesi.git
    ```
    *(kullanici-adiniz kısmını kendi GitHub kullanıcı adınızla değiştirin)*

2.  **API Adresini Kontrol Etme:**
    *   `ApiConsumeProjesi` projesindeki `Program.cs` dosyasını açın.
    *   Kod içerisindeki `url` veya `baseUrl` değişkenlerinde tanımlı olan API adresini (`https://localhost:7151`) kontrol edin. Bu adres, **çalışır durumda olan `ApiHavaDurumuProjesi`'nin adresiyle aynı olmalıdır.** Gerekirse bu URL'leri kendi API adresinizle güncelleyin.

3.  **Projeyi Derleme:**
    *   Visual Studio veya tercih ettiğiniz bir IDE ile `ApiConsumeProjesi.sln` dosyasını açın.
    *   Projeyi derleyin (Build -> Build Solution veya `dotnet build` komutu ile).

4.  **Uygulamayı Çalıştırma:**
    *   Projeyi Visual Studio üzerinden başlatın (F5 veya Debug -> Start Debugging) veya terminalde `dotnet run` komutunu kullanın.
    *   Konsol ekranında çıkan menüden yapmak istediğiniz işlemi seçin.

## 📝 Kullanım

Uygulama başlatıldığında aşağıdaki menü seçenekleri sunulur:

1.  **Şehir Listesini Getirin:** API'den tüm şehir adlarını alır ve listeler.
2.  **Şehir ve Hava Durumu Listesini Getirin:** API'den tüm şehirlerin detaylı bilgilerini (ad, ülke, sıcaklık) alır ve listeler.
3.  **Yeni Şehir Ekleme:** Kullanıcıdan aldığı bilgilerle API'ye yeni bir şehir kaydı gönderir.
4.  **Şehir Silme İşlemi:** Kullanıcıdan aldığı ID ile API üzerinden ilgili şehri siler.
5.  **Şehir Güncelleme İşlemi:** Kullanıcıdan aldığı ID ve yeni bilgilerle API üzerinden ilgili şehri günceller.
6.  **ID'ye Göre Şehir Getirme:** Kullanıcıdan aldığı ID ile API'den ilgili şehrin tüm bilgilerini getirir.

Seçiminizi yapıp Enter tuşuna basarak ilgili API işlemini gerçekleştirebilirsiniz.

---
Bu README, projenizin bir API tüketicisi olarak nasıl çalıştığını ve diğer API projesiyle olan bağımlılığını açıklar.
