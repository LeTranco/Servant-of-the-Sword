# Servant-of-the-Sword
Fantastik canavarlarla savaşılan, Unity ile geliştirilmiş fizik tabanlı bir oyun.
## Oyun
Oyunu tarayıcı üzerinden oynamak için tıklayın:

https://letranco.itch.io/servant-of-the-sword?secret=x2bMuwltUKYmbKsqo7oBaBkAr0
## Proje Hakkında
Bu proje, Unity Oyun Motoru ile geliştirilmiştir. Ana karakter olan şövalye, kılıcıyla farklı saldırılar kullanarak ve fizik tabanlı interaksiyonlarda bulunarak hayatta kalıp bölümü geçmeye çalışır.
## Mekanikler
### 1- Hareket Etme
Karakterin x ekseninde sağa ve sola hareket etmesini sağlar.
### 2- Zıplama
Karakter, fizik kurallarını uygun bir şekilde y ekseninde pozitif yönde atılma gerçekleştirir.
### 3- Normal Saldırı
Karakter, kılıcını savurarak rakiplerine hasar verir.
### 4- Kritik Saldırı
Karakter, bu özelliği hareket etmiyorken kullanabilir, normal saldırıya kıyasla daha yüksek hasar verir.
## Kontroller
| Tuş | Eylem | Açıklama |
|------|------|--------|
| A/D | Hareket | Karakter x ekseninde hareket eder. |
| SPACE | Zıplama | Karakter yukarı yönde atılır. |
| M1  | Normal Saldırı | Karekter rakibe hasar verir. |
| M2 | Kritik Saldırı | Karakter rakibe fazladan hasar verir. |
## Atmosfer
Çeşitli varlıkler (Asset) kullanılarak karakterin oynayabileceği bir harita oluşturulmuştur.
## Ses
**Arka Plan Sesi:** Oyun boyunca çalar.

**Normal Saldırı Sesi:** Kılıç savrulduğunda ses çıkar.

**Kritik Saldırı Sesi:** Kritik saldırı kullanıldığında daha tok bir ses çıkar.

## Yapay Zeka
Oyunun bu sürümünde rakipler, geleneksel kodlanmış davranışlar yerine Q-Learning algoritmasıyla geliştirilmiş birer yapay zeka ajanıdır.

**Öğrenme Süreci:** Düşman, çevreyle rastgele bir şekilde etkileşime girerek hangi eylemlerin (saldırı, yaklaşma) kendilerine en yüksek "ödülü" getireceğini deneyimleyerek öğrenir.

**Dinamik Tepki:** Ajan, oyuncunun hareketlerine göre kendi Q-Tablolarını günceller ve her savaşta daha optimize stratejiler geliştirir.

**Fizik Temelli Karar Alma:** Ajan, Unity'nin fizik motoruyla entegre çalışarak sadece komut çalıştırmaz, aynı zamanda çevredeki fiziksel engelleri de analiz eder.
