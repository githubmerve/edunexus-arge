# Mimari ve Proje Plani

## 1. Hedef

Bu sistem, ogrenciler ve akademisyenler icin tek bir universite paneli sunar. Onerilen ana moduller:

- Kimlik ve yetkilendirme
- Ogrenci paneli
- Akademisyen paneli
- Ariza ve sorun yonetimi
- Mazeret yonetimi
- AI asistani
- Raporlama ve loglama

## 2. Frontend Yapisi

`ASP.NET Core MVC` veya istenirse ileride `Blazor Server/WebAssembly` ile genisletilebilir. Bu ornek `MVC` tabanlidir.

Ana ekranlar:

- Giris
- Ogrenci paneli
- Akademisyen paneli
- Ariza bildirim formu
- Mazeret bildirim formu
- Musaitlik takvimi
- AI asistan sohbet penceresi
- Yonetici paneli

Tasarim yaklasimi:

- Kurumsal renk degiskenleri tek dosyada toplanir
- Mobil uyumlu grid yapi kullanilir
- Navigasyon ogrenci, akademisyen ve yonetici rollerine gore filtrelenir
- Formlar sade ve erisilebilir yapida tutulur

## 3. Backend Yapisi

Katmanlar:

- `Controllers`: HTTP giris noktasi
- `Services`: is kurallari, entegrasyonlar, AI ve QR mantigi
- `Models`: domain modelleri ve DTO'lar
- `Firebase`: Authentication, Firestore ve Storage baglantisi

Akis:

1. Kullanici giris yapmak ister.
2. Sistem Firebase Authentication ile token dogrular.
3. Rol bilgisi Firestore veya custom claims uzerinden okunur.
4. Kullanici ilgili panele yonlendirilir.
5. Ariza, mazeret ve musaitlik verileri Firestore koleksiyonlarinda saklanir.

## 4. Firebase Entegrasyonu

Firebase bilesenleri:

- `Firebase Authentication`: ogrenci, akademisyen ve admin oturumlari
- `Cloud Firestore`: profil, ariza, mazeret, musaitlik ve sohbet metadata kayitlari
- `Firebase Storage`: mazeret eki, fotograf, belge gibi dosyalar
- `Cloud Functions` opsiyonel: bildirim, veri dogrulama, zamanlanmis gorevler

Onerilen koleksiyonlar:

- `users`
- `students`
- `academicians`
- `issueReports`
- `excuseRequests`
- `availabilitySlots`
- `aiConversations`
- `auditLogs`

## 5. QR Kod ile Giris Tasarimi

Onerilen akisin guvenli hali:

1. Ogrenci normal kullanici adi/sifre veya SSO ile bir kez oturum acar.
2. Sunucu ogrenci icin kisa omurlu bir `qrLoginToken` uretir.
3. Bu token bir QR kod olarak gosterilir.
4. Kampus giris cihazindaki uygulama QR kodu okur.
5. Cihaz tokeni API'ye yollar.
6. API tokenin tek kullanimlik, sureli ve imzali oldugunu dogrular.
7. Basarili ise giris olayi loglanir.

Guvenlik notlari:

- QR 30-60 saniye gecerlilikte olmali
- Token tek kullanimlik olmali
- Token icinde ham ogrenci numarasi tutulmamali
- Replay attack icin `nonce` ve `usedAt` kontrolu yapilmali
- Gecersiz denemeler `auditLogs` koleksiyonuna yazilmali

## 6. Yapay Zeka Asistani Entegrasyonu

Asistan su senaryolari destekleyebilir:

- Ders programi hakkinda soru
- Kampus hizmetleri
- Mazeret sureci
- Ariza bildirimi nasil yapilir
- Akademisyen musaitlik bilgisi yonlendirmesi

Onerilen mimari:

1. Ogrenci sohbet ekranindan mesaj gonderir.
2. ASP.NET Core backend mesaji alir.
3. Backend rol, oturum ve icerik filtrelerini uygular.
4. Mesaj AI servis saglayicisina iletilir.
5. Donen cevap kullaniciya aktarilir.
6. Sohbet ozeti ve gerekli metadata Firestore'a yazilir.

Dikkat edilmesi gerekenler:

- Ham gizli anahtarlar istemciye verilmez
- Prompt icine gereksiz kisisel veri konmaz
- Rate limit uygulanir
- Uygunsuz icerik filtrelenir

## 7. Ariza ve Mazeret Kayitlari Nasil Saklanir

### Ariza kayitlari

Her kayit icin su bilgiler tutulur:

- Bildirimi acan ogrenci ID'si
- Konum bilgisi
- Kategori
- Aciklama
- Fotograf URL'leri
- Durum: `Open`, `InReview`, `InProgress`, `Resolved`, `Rejected`
- Atanan birim
- Olusturma ve guncelleme tarihleri

Yonetim akisi:

1. Ogrenci formu doldurur.
2. Kural dogrulamasi yapilir.
3. Kayit Firestore `issueReports` koleksiyonuna yazilir.
4. Gerekirse teknik birime bildirim duser.
5. Yetkili personel durumu gunceller.
6. Tum durum degisiklikleri `auditLogs` icinde saklanir.

### Mazeret kayitlari

Her mazeret kaydinda:

- Ogrenci ID'si
- Baslik ve aciklama
- Baslangic ve bitis tarihi
- Mazeret tipi
- Ek belge URL'si
- Durum: `Pending`, `Approved`, `Rejected`, `NeedRevision`
- Inceleyen akademisyen veya idari personel

Yonetim akisi:

1. Ogrenci mazeret formunu doldurur.
2. Dosya varsa Firebase Storage'a yuklenir.
3. Metadata Firestore `excuseRequests` icine yazilir.
4. Yetkili kullanici mazereti inceler.
5. Karar verildiginde tarihce kaydi ve bildirim uretilir.

## 8. Guvenlik Onlemleri

- Firebase custom claims ile rol tabanli erisim
- Sunucu tarafli input validation
- CSRF korumasi
- Dosya yuklemelerinde tip ve boyut siniri
- Audit log
- Rate limiting
- Gizli anahtarlari `User Secrets` veya ortam degiskenlerinde tutma
- AI isteklerinde PII maskeleme

## 9. Gelistirme Asamalari

1. Kimlik ve rol yonetimi
2. Ogrenci ve akademisyen panelleri
3. Ariza ve mazeret modulu
4. QR giris modulu
5. AI asistani
6. Admin paneli
7. Test, loglama ve canliya alim
