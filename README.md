# UniSis Entegre

Bu depo, bir universite icin ogrenci ve akademisyen yonetim sistemi gelistirmeye baslamak uzere hazirlanmis ornek bir `ASP.NET Core MVC + Firebase` iskeletidir.

Sistem su ihtiyaclari kapsar:

- QR kod ile ogrenci girisi
- Akademisyen musaitlik takvimi goruntuleme ve guncelleme
- Ogrenci ariza/sorun bildirimi
- Ogrenci mazeret bildirimi
- Yapay zeka destekli kampus asistani
- Firebase tabanli kimlik dogrulama ve veri saklama

## Klasor Yapisi

```text
src/
  Unisis.Web/
    Controllers/
    Models/
    Services/
    Views/
    wwwroot/
docs/
  architecture.md
```

## Hedef Teknoloji Yigini

- `ASP.NET Core MVC (.NET 8)`
- `Firebase Authentication`
- `Cloud Firestore`
- `Firebase Storage`
- `OpenAI uyumlu AI servis entegrasyonu`
- `QRCoder`

## Baslangic

1. `src/Unisis.Web/appsettings.json` icindeki ayarlari ihtiyaciniza gore guncelleyin.
2. Yerel yapay zeka kullanacaksaniz `Ollama` kurun.
3. Paketleri yuklemek icin:

```powershell
dotnet restore .\src\Unisis.Web\Unisis.Web.csproj
```

4. Calistirmak icin:

```powershell
dotnet run --project .\src\Unisis.Web\Unisis.Web.csproj
```

## Yerel Yapay Zeka: Ollama

Onerilen kurulum:

1. Ollama yukleyin: `https://ollama.com/download`
2. Modeli indirin:

```powershell
ollama pull llama3.1:8b
```

3. Gerekirse modeli test edin:

```powershell
ollama run llama3.1:8b
```

Uygulama varsayilan olarak su endpoint'e baglanir:

```text
http://localhost:11434/api/chat
```

`appsettings.json` icindeki `Ollama` bolumu ile model veya endpoint degistirilebilir.

## Asistan Calisma Sirasi

AI asistani su sirayla cevap uretir:

1. Yerel Ollama modeli
2. OpenAI API ve web aramasi
3. Tarayici tarafindaki yerel bilgi tabani

## Not

Bu iskelet dogrudan canli ortama alinacak son urun degildir. Amac, sizin projenize temel olacak temiz bir mimari, veri modeli ve entegrasyon akislari sunmaktir.
