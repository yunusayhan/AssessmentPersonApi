#  Assessment.Phonebook.Api

Database olarak PostgreSQL kullanılmıştır.<br>
#### Assessment.Phonebook.Api üzerinden migration yapılmalıdır.<br>
Api request bilgileri aşağıdaki gibidir<br>
/api/persons/{personId} ilgili personelin bilgilerini döner.<br>
/api/persons yeni personel ekler.<br>
/api/persons personel siler<br>
/api/persons/{personId}/details personel detaylarını listeler.<br>
/api/persons/detail bir personele detay bilgi ekler<br>
/api/persons/detail [HttpDelete] Personel detayını siler<br>

# Assessment.Report.API

####  Assessment.Report.API üzerinden migration yapılmalıdır.

/api/Report bir rapor isteği oluşturur ve rabbitMQ publish edilir.<br>
/api/Report/getall var olan raporların listesini döner. Rapor oluşturuldu ise Status=1 oluşturulmadı ise Status=0 döner.<br>

#  RabbitMQ

Bu kısım için FileCreateWorkerService BackgroundService yazıldı.Servis arka planda çalışan bir servis. Oluşan rapor isteklerini rabbitMQ dan alıp Assessment.Report.API\wwwroot\files yoluna excel dosyası olarak kayıt eder.
