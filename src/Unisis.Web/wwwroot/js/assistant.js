(function () {
    const STORAGE_KEY = "unisis_assistant_history";
    const TOPIC_KEY = "unisis_assistant_topic";
    const API_URL = "/api/assistant/ask";

    const knowledgeBase = {
        ybs: {
            title: "Yonetim Bilisim Sistemleri",
            keywords: ["yonetim bilisim sistemleri", "ybs", "mis", "bilisim", "is analizi", "erp", "crm", "veri analizi"],
            summary: "Yonetim Bilisim Sistemleri, isletme ile bilgi teknolojilerini birlestiren bir alandir. Ogrenciler hem yonetsel surecleri hem de yazilim, veri, sistem analizi ve dijital donusum konularini ogrenir.",
            courses: "Tipik dersler arasinda programlama, veri tabani, sistem analizi ve tasarimi, proje yonetimi, is zekasi, ERP/CRM uygulamalari ve karar destek sistemleri bulunur.",
            careers: "Mezunlar is analisti, ERP danismani, veri analisti, urun uzmani, proje uzmani, dijital donusum uzmani veya BT surec yoneticisi gibi rollerde calisabilir.",
            comparison: "YBS, saf yazilim bolumlerinden farkli olarak teknik taraf ile yonetsel surecleri dengeler. Bu nedenle hem teknik ekiplerle hem de is birimleriyle calisabilecek profiller icin cok uygundur."
        },
        isletme: {
            title: "Isletme",
            keywords: ["isletme", "pazarlama", "finans", "muhasebe", "yonetim", "insan kaynaklari", "organizasyon"],
            summary: "Isletme bolumu; yonetim, pazarlama, finans, muhasebe, insan kaynaklari ve strateji gibi alanlarda kurumlarin nasil yonetilecegini ogreten genis kapsamli bir bolumdur.",
            courses: "Temel dersler genelde iktisat, muhasebe, finans yonetimi, pazarlama, yonetim organizasyon, istatistik, girisimcilik ve stratejik yonetim ekseninde ilerler.",
            careers: "Mezunlar finans, muhasebe, satis, insan kaynaklari, operasyon, denetim, banka, girisimcilik ve kurumsal yonetim gibi farkli alanlara yonelebilir.",
            comparison: "Isletme genis bir yonetsel temel sunar. Teknik sistem odakli bir alan isteyen ogrenciler YBS'ye, dis ticaret ve lojistik odakli bir alan isteyen ogrenciler ise Uluslararasi Ticaret'e daha yakin hissedebilir."
        },
        ticaret: {
            title: "Uluslararasi Ticaret",
            keywords: ["uluslararasi ticaret", "dis ticaret", "ithalat", "ihracat", "gumruk", "lojistik", "kuresel ticaret", "ticaret"],
            summary: "Uluslararasi Ticaret bolumu; ulkeler arasindaki mal ve hizmet hareketi, ihracat-ithalat surecleri, gumruk, lojistik, dis ticaret mevzuati ve kuresel pazar stratejilerini kapsar.",
            courses: "Sik gorulen dersler dis ticaret islemleri, gumruk mevzuati, lojistik yonetimi, uluslararasi pazarlama, kambiyo, dis ticaret finansmani ve tedarik zinciri yonetimidir.",
            careers: "Mezunlar ihracat uzmanligi, ithalat operasyonu, dis ticaret uzmanligi, lojistik planlama, satin alma, gumrukleme ve uluslararasi satis rollerinde calisabilir.",
            comparison: "Uluslararasi Ticaret, Isletme'ye gore daha dis pazar ve operasyon odaklidir. YBS'ye gore ise bilgi sistemlerinden cok ticaret akislarina, mevzuat ve lojistik tarafina yogunlasir."
        }
    };

    const followUpPatterns = [
        { keys: ["ders", "dersler", "hangi ders"], field: "courses" },
        { keys: ["is", "kariyer", "mezun", "nerede calis"], field: "careers" },
        { keys: ["fark", "karsilastir", "mi daha iyi", "avantaj"], field: "comparison" },
        { keys: ["nedir", "ne", "bolum", "alan"], field: "summary" }
    ];

    function normalize(text) {
        return (text || "")
            .toLowerCase()
            .replace(/ı/g, "i")
            .replace(/ğ/g, "g")
            .replace(/ü/g, "u")
            .replace(/ş/g, "s")
            .replace(/ö/g, "o")
            .replace(/ç/g, "c");
    }

    function getHistory() {
        return JSON.parse(sessionStorage.getItem(STORAGE_KEY) || "[]");
    }

    function setHistory(history) {
        sessionStorage.setItem(STORAGE_KEY, JSON.stringify(history));
    }

    function getCurrentTopic() {
        return sessionStorage.getItem(TOPIC_KEY) || "";
    }

    function setCurrentTopic(topic) {
        sessionStorage.setItem(TOPIC_KEY, topic || "");
    }

    function clearConversation() {
        sessionStorage.removeItem(STORAGE_KEY);
        sessionStorage.removeItem(TOPIC_KEY);
    }

    function shouldUseServerAnswer(answer) {
        const normalized = normalize(answer);
        if (!normalized) {
            return false;
        }

        const blockedPatterns = [
            "too many requests",
            "toomanyrequests",
            "yanit veremiyor",
            "baglanti hatasi",
            "sistem su an mesgul",
            "api anahtari",
            "yerel model",
            "servisi su an"
        ];

        return !blockedPatterns.some(pattern => normalized.includes(pattern));
    }

    function detectTopic(message) {
        const normalized = normalize(message);
        for (const [key, value] of Object.entries(knowledgeBase)) {
            if (value.keywords.some(keyword => normalized.includes(normalize(keyword)))) {
                return key;
            }
        }
        return "";
    }

    function detectIntent(message) {
        const normalized = normalize(message);
        for (const pattern of followUpPatterns) {
            if (pattern.keys.some(key => normalized.includes(normalize(key)))) {
                return pattern.field;
            }
        }
        return "summary";
    }

    function buildTopicAnswer(topicKey, intent) {
        const topic = knowledgeBase[topicKey];
        if (!topic) {
            return "";
        }

        if (intent === "courses") {
            return `${topic.title} icin ders tarafi soyle ozetlenebilir: ${topic.courses}`;
        }

        if (intent === "careers") {
            return `${topic.title} mezunlari icin kariyer tarafi genelde soyledir: ${topic.careers}`;
        }

        if (intent === "comparison") {
            return `${topic.title} hakkinda karsilastirma olarak sunu soyleyebilirim: ${topic.comparison}`;
        }

        return `${topic.title} hakkinda genel bir ozet: ${topic.summary} ${topic.courses}`;
    }

    function generalFallback(message) {
        const normalized = normalize(message);

        if (normalized.includes("mazeret")) {
            return "Mazeret islemleri Ogrenci Islemleri ekranindaki Mazeret Bildirimi alanindan baslatilabilir. Isterseniz mazeret onay surecini de adim adim anlatabilirim.";
        }

        if (normalized.includes("ariza") || normalized.includes("sorun")) {
            return "Ariza veya kampus sorunu bildirimi Ogrenci Islemleri ekranindaki Ariza Bildirimi bolumunden yapilabilir.";
        }

        if (normalized.includes("qr") || normalized.includes("giris")) {
            return "QR giris demo modda ogrenci panelinden uretiliyor. Gercek sistemde bu akis kisa omurlu ve tek kullanimlik token mantigiyla calismalidir.";
        }

        return "Bu konuda size yardimci olabilmem icin soruyu biraz daha netlestirir misiniz? Isterseniz Yonetim Bilisim Sistemleri, Isletme veya Uluslararasi Ticaret bolumleri hakkinda bilgi verebilirim.";
    }

    function generateResponse(message) {
        const topicFromMessage = detectTopic(message);
        const currentTopic = topicFromMessage || getCurrentTopic();
        const intent = detectIntent(message);

        if (topicFromMessage) {
            setCurrentTopic(topicFromMessage);
        }

        if (currentTopic) {
            return buildTopicAnswer(currentTopic, intent);
        }

        return generalFallback(message);
    }

    function appendMessage(container, role, text) {
        if (!container) {
            return;
        }

        const safeRole = role === "user" ? "user" : "assistant";
        container.insertAdjacentHTML("beforeend", `<div class="chat-bubble ${safeRole}">${text}</div>`);
        container.scrollTop = container.scrollHeight;
    }

    function renderHistory(container) {
        if (!container) {
            return;
        }

        const history = getHistory();
        container.innerHTML = "";

        if (!history.length) {
            appendMessage(container, "assistant", "Merhaba, Yonetim Bilisim Sistemleri, Isletme ve Uluslararasi Ticaret hakkinda soru sorabilirsiniz. Isterseniz bu bolumleri karsilastirabilirim.");
            return;
        }

        history.forEach(item => appendMessage(container, item.role, item.text));
    }

    async function askServer(message, history) {
        const response = await fetch(API_URL, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({
                userId: "demo-user",
                userRole: "Student",
                message,
                history
            })
        });

        if (!response.ok) {
            throw new Error("server_error");
        }

        return await response.json();
    }

    async function sendMessage(message, container) {
        const history = getHistory();
        history.push({ role: "user", text: message });

        let answer = "";

        try {
            const serverResult = await askServer(message, history);
            if (serverResult && shouldUseServerAnswer(serverResult.answer)) {
                answer = serverResult.answer;
            }
        } catch {
            answer = "";
        }

        if (!answer) {
            answer = generateResponse(message);
        }

        history.push({ role: "assistant", text: answer });
        setHistory(history);
        renderHistory(container);
        return answer;
    }

    function startNewChat(container) {
        clearConversation();
        renderHistory(container);
    }

    window.UnisisAssistant = {
        renderHistory,
        sendMessage,
        startNewChat,
        clearConversation
    };
})();
