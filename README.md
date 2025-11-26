# URL Shortener

Bu proje, kullanıcıların girdikleri uzun bağlantıları kolayca kısaltabilmesini sağlayan bir web uygulamasıdır.
İlk aşamada kullanıcılar anonim olarak kısaltılmış linkler üretebilir.
Sonraki fazlarda SSO (Single Sign-On) entegrasyonu ile kimlik doğrulamalı (loginli) bağlantı kısaltma, bağlantı istatistikleri gibi gelişmiş özellikler eklenecektir.

## Özellikler 

### MVP (Aktif Gelişim)

* Uzun bağlantıların kolayca kısaltılması
* Anonim (giriş yapmadan) link kısaltma
* Kısaltılmış linklerin otomatik yönlendirmesi
* Üretilen linklerin Sosyal Medyalarda paylaşımı
* Temiz, modern ve hızlı kullanıcı arayüzü (Angular 20)
* Linkler için tıklanıldığında temel istatistik bilgilerinin tutulması

### Sonraki Fazlar (Planlanan)

* SSO entegrasyonu (Google, Microsoft vb.)
* Loginli kullanıcılar için:
  -Kişisel link listesi,
  -Link düzenleme silme,
  -Detaylı istatistikler (tıklama zamanı, cihaz, lokasyon vb.),
* Gelişmiş admin paneli:
  -Spam/kötüye kullanım tespiti,
  -Link kısıtlama/silme,
  -Raporlama ve analiz ekranları,
* Docker compose ile tek komutla ayağa kaldırma,
* Detaylı test altyapısı (unit / integration / e2e)

## Teknoloji Yığını

**Frontend:** Angular 20
**Backend:** .NET 9
**Database:** PostgreSQL

**Diğer:**
  -RESTful API mimarisi,
  -Entity Framework Core (varsayım),
  -Docker (planlı)

## Mimari Genel Bakış
Proje, frontend ve backend bileşenlerinden oluşan katmanlı bir yapıda tasarlanmıştır.
  -'src/'
  -'backend/' -.NET 9 Web API
  -URL oluşturma ve çözme (redirect) işlemleri
  -URL doğrulama ve kısa kod üretimi
  -İstatistiklerin tutulması
  -`frontend/` – Angular 20 uygulaması
  -URL kısaltma formu
  -Oluşan kısa linkin gösterimi ve kopyalanması
  -(Gelecek) Kullanıcı login / dashboard ekranları

## Gereksinimler
  -**Node.js** (Angular için, en az v18 önerilir)
  -**.NET SDK 9**
  -**PostgreSQL** (lokalde veya bir servis üzerinden)
  -**npm / pnpm / yarn** (Angular CLI için en yaygını `npm`)

## Kurulum

### 1. Repoyu klonlayın
'''bash
git clone https://github.com/Octapull/url-shortener.git
cd url-shortener

cd src/backend

# Gerekli paketleri restore et
dotnet restore

# Development veritabanı migration (örnek)
dotnet ef database update

# Uygulamayı çalıştır
dotnet run

cd src/frontend

# Bağımlılıkları yükle
npm install

# Development sunucusunu çalıştır
npm start
# veya
ng serve

## Kullanım

1. Tarayıcıda frontend URL’ini açın (http://localhost:4200).
2. Kısaltmak istediğiniz URL'i ilgili alana yapıştırın.
3. "Kısalt" (örnek) butonuna tıklayın.
4. Oluşan kısa linki panonuza kopyalayabilir ve dilediğiniz yerde kullanabilirsiniz.
5. Kısa linke gidildiğinde backend, sizi orijinal URL'e yönlendirir.

## Yol Haritası / Yapılacaklar

* [ ] Anonim link kısaltma özelliğinin eklenmesi
* [ ] Kısaltılmış linklerin otomatik yönlendirmesi (FE Tarafından)
* [ ] Link istatistiklerinin tutulması (görülme sayısı, son kullanım vs.)
* [ ] SSO entegrasyonu (Google, Microsoft vb.)
* [ ] Loginli kullanıcılar için link geçmişi ve yönetim paneli
* [ ] İleri düzey admin paneli (kısıtlama, silme, raporlama)
* [ ] Docker Compose desteği
* [ ] Detaylı test altyapısı (unit/integration)

## Proje Hiyerarşisi

url-shortener/
  ├─ src/
  │  ├─ backend/
  │  │  ├─ UrlShortener.Api/
  │  │  ├─ UrlShortener.Application/
  │  │  ├─ UrlShortener.Domain/
  │  │  └─ UrlShortener.Infrastructure/
  │  └─ frontend/
  │     ├─ src/
  │     └─ angular.json
  ├─ docs/
  │  ├─ REQUIREMENTS.md
  │  ├─ API_SPEC.md
  │  └─ ARCHITECTURE.md
  ├─ .gitignore
  ├─ README.md
  └─ LICENSE (opsiyonel)

## Contributing

1. Repoyu forklayın
2. Branch açın: git checkout -b feature/amazing-feature
3. Geliştirmelerinizi yapın
4. Yeni fonksiyonellik için test ekleyin
5. Commit: git commit -m 'Add amazing feature'
6. Branch’i push’layın: git push origin feature/amazing-feature
7. Pull request açın

