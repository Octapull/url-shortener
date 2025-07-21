# URL Shortener

Bu proje, kullanıcıların girdikleri uzun bağlantıları kolayca kısaltabilmesini sağlayan bir web uygulamasıdır.
İlk aşamada kullanıcılar anonim olarak kısaltılmış linkler üretebilir.
Sonraki fazlarda SSO (Single Sign-On) entegrasyonu ile kimlik doğrulamalı (loginli) bağlantı kısaltma, bağlantı istatistikleri gibi gelişmiş özellikler eklenecektir.

## Özellikler

* Uzun bağlantıların kolayca kısaltılması
* Anonim (giriş yapmadan) link kısaltma
* Kısaltılmış linklerin otomatik yönlendirmesi
* Üretilen linklerin Sosyal Medyalarda paylaşımı
* Temiz, modern ve hızlı kullanıcı arayüzü (Angular 20)
* Linkler için tıklanıldığında temel istatistik bilgilerinin tutulması
* Gelecek: SSO entegrasyonu ile kullanıcı bazlı link yönetimi ve istatistikler
* Gelecek: Gelişmiş yönetim paneli, analiz ve raporlama

## Gereksinimler

* **Frontend:** Angular 20
* **Backend:** .NET 9
* **Database:** PostgreSQL

## Kurulum

### Frontend

### Backend

## Kullanım

1. Tarayıcıda ana sayfayı açın.
2. Kısaltmak istediğiniz URL'i ilgili alana yapıştırın ve "Kısalt" butonuna tıklayın.
3. Oluşan kısa linki panonuza kopyalayabilir ve dilediğiniz yerde kullanabilirsiniz.

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

```md

```

## Contributing

1. Fork the repository
2. Create a feature branch
   `git checkout -b feature/amazing-feature`
3. Make your changes
4. Add tests for new functionality
5. Commit your changes
   `git commit -m 'Add amazing feature'`
6. Push to the branch
   `git push origin feature/amazing-feature`
7. Open a Pull Request
