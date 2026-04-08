<h1>Edunexus</h1>

<p>
Edunexus, üniversite içi dijital süreçleri tek panelde toplayan modern bir kampüs yönetim platformudur.
Öğrenci işlemleri, akademisyen müsaitlik takibi, kampüs bildirimleri ve yapay zeka destekli yönlendirme gibi modülleri tek bir sistemde sunar.
</p>

<p>
Bu proje;
<ul>
  <li><a href="https://github.com/">Merve Subaşı</a></li>
  <li><a href="https://github.com/132530025">Recep Berkay Ünsal</a></li>
  <li><a href="https://github.com/mmustafa-kara">Muhammed Mustafa Kara</a></li>
  <li><a href="https://github.com/ssenadogann">Sena Doğan</a></li>
</ul>
tarafından geliştirilmiştir.
</p>

<hr>

<h2>Proje Amacı</h2>
<p>
Bu proje, üniversite öğrencileri ile akademik ve idari personelin günlük işlemlerini dijitalleştirmek amacıyla geliştirilmiştir.
Amaç; başvuru, bildirim, takvim ve yönlendirme süreçlerini merkezi ve kullanıcı dostu bir yapı altında toplamaktır.
</p>

<hr>

<h2>Özellikler</h2>
<ul>
    <li>Kullanıcı giriş sistemi (öğrenci numarası ile)</li>
    <li>Öğrenci paneli</li>
    <li>Akademisyen müsaitlik takibi</li>
    <li>Mazeret bildirimi oluşturma</li>
    <li>Arıza bildirimi oluşturma</li>
    <li>QR ile giriş sistemi</li>
    <li>Yapay zeka kampüs asistanı</li>
    <li>Firebase tabanlı gerçek zamanlı sohbet</li>
    <li>Kullanıcıya özel profil ve akademik bilgiler</li>
    <li>Hardcoded demo kullanıcı desteği</li>
</ul>

<hr>

<h2>Modüller</h2>

<h3>Giriş Sistemi</h3>
<p>
Kullanıcılar öğrenci numarası ve şifre ile sisteme giriş yapar.
Demo kullanıcılar sistem içerisinde tanımlıdır.
</p>

<h3>Öğrenci İşlemleri</h3>
<ul>
    <li>Arıza bildirimi oluşturma</li>
    <li>Mazeret bildirimi gönderme</li>
    <li>Belge yükleme</li>
    <li>QR giriş oluşturma</li>
    <li>Akademik bilgileri görüntüleme (GANO, AKTS)</li>
</ul>

<h3>Akademisyen Takvimi</h3>
<p>
Akademisyenlerin haftalık müsaitlik saatleri görüntülenir ve öğrencilerin daha doğru yönlendirilmesi sağlanır.
</p>

<h3>Kampüs Sohbeti</h3>
<ul>
    <li>Firebase Firestore tabanlıdır</li>
    <li>Gerçek zamanlı çalışır</li>
    <li>Kullanıcılar arası iletişim sağlar</li>
</ul>

<h3>Yapay Zeka Asistanı</h3>
<p>
Sistem içerisinde yer alan yapay zeka asistanı öğrenci sorularını yanıtlar ve kampüs süreçleri hakkında yönlendirme yapar.
</p>

<ul>
    <li>Gemini</li>
    <li>OpenAI</li>
    <li>Ollama</li>
</ul>

<h2>Görüntüler</h2>
<img width="900" height="400" alt="Giriş Ekranı" src="https://github.com/user-attachments/assets/759206e0-8306-4110-81cf-477716600471" />
<img width="900" height="400" alt="Anasayfa" src="https://github.com/user-attachments/assets/7c8370fe-905d-4aa9-a922-3ff1c8a98bb7" />
<img width="900" height="400" alt="Yapay Zeka Asistanı" src="https://github.com/user-attachments/assets/b1392080-37a0-437b-b1cd-74e4ab9873dd" />
<img width="900" height="400" alt="Genel Sohbet" src="https://github.com/user-attachments/assets/29665353-1efc-4fa3-b1d3-8730b4d37d7d" />
<img width="900" height="400" alt="Akademisyen Takvimi" src="https://github.com/user-attachments/assets/09e0d774-57d1-422a-969f-807ce263327d" />
<img width="900" height="400" alt="Arıza Kaydı" src="https://github.com/user-attachments/assets/60274385-d5cf-4159-999f-de31bc3a28e5" />
<img width="900" height="400" alt="Mazeret Takibi" src="https://github.com/user-attachments/assets/a9997272-6c3e-4471-b0bc-e8fe94070815" />
<img width="900" height="400" alt="Qr kodu" src="https://github.com/user-attachments/assets/f41ebf32-acd5-49fb-9126-9be33439ec4c" />



<hr>

<h2>Sistem Mimarisi</h2>
<ul>
    <li><b>Controllers:</b> İş akışları</li>
    <li><b>Models:</b> Veri yapıları</li>
    <li><b>Services:</b> İş mantığı ve API entegrasyonları</li>
    <li><b>Views:</b> Arayüz</li>
    <li><b>wwwroot:</b> Statik dosyalar</li>
</ul>

<p>Detaylı mimari: <code>docs/architecture.md</code></p>

<hr>

<h2>Kullanılan Teknolojiler</h2>
<ul>
    <li>ASP.NET Core MVC (.NET 8)</li>
    <li>C#</li>
    <li>Razor Views</li>
    <li>JavaScript</li>
    <li>CSS</li>
    <li>Firebase / Firestore</li>
    <li>Gemini / OpenAI / Ollama</li>
    <li>Bootstrap Icons</li>
</ul>

<hr>

<h2>Kurulum</h2>

<h3>Gereksinimler</h3>
<ul>
    <li>.NET 8 SDK</li>
    <li>Firebase projesi</li>
    <li>Gemini API Key (opsiyonel)</li>
    <li>OpenAI API Key (opsiyonel)</li>
    <li>Ollama (opsiyonel)</li>
</ul>

<h3>Adımlar</h3>
<pre>
git clone &lt;repo-url&gt;
cd &lt;repo-folder&gt;

dotnet restore
dotnet run --project src/Unisis.Web/Unisis.Web.csproj
</pre>
Tarayıcıda http://localhost:5000/

<hr>

<h2>Yapılandırma</h2>
<p><code>appsettings.json</code> dosyasında yapılandırmalar bulunur:</p>

<pre>
"Gemini": {
  "ApiKey": "YOUR_API_KEY",
  "ModelId": "gemini-2.0-flash"
}
</pre>

<hr>

<h2>Gelecek Geliştirmeler</h2>
<ul>
    <li>Gerçek dosya yükleme sistemi</li>
    <li>Rol bazlı yetkilendirme</li>
    <li>Canlı bildirimler</li>
    <li>Yapay zeka yanıt kalitesinin geliştirilmesi</li>
    <li>Mobil uyumluluğun artırılması</li>
    <li>Admin paneli</li>
</ul>

<hr>

<h2>Not</h2>
<p>
Bu proje, üniversite ortamında kullanılabilecek bir sistemin prototipi olarak geliştirilmiştir ve eğitim amaçlıdır.
</p>

