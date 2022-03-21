# AssessmentPersonApi

Assessment.Phonebook.Api


1-Database olarak PostgreSQL kullanılmıştır.
2-Assessment.Phonebook.Api  üzerinden migration yapılmalıdır. 
3- Api istek bilgileri aşağıdaki gibidir
   /api/persons/{personId} ilgili personelin bilgilerini döner.
   /api/persons yeni personel ekler.
   /api/persons personel siler
   /api/persons/{personId}/details personel detaylarını listeler.
   /api/persons/detail  bir personele detay bilgi ekler
   /api/persons/detail  [HttpDelete] Personel detayını siler
   
   
  Assessment.Report.API
  
   Assessment.Report.API üzerinden migration yapılmalıdır.
   /api/Report bir rapor isteği oluşturur ve rabbitMQ publish edilir.
   /api/Report/getall  var olan raporların listesini döner. Rapor oluşturuldu ise Status=1 oluşturulmadı ise Status=0 döner.
   
   RabbitMQ
   
   Bu kısım için FileCreateWorkerService BackgroundService yazıldı.Servis arka planda çalışan bir servis. Oluşan rapor isteklerini rabbitMQ dan alıp
   Assessment.Report.API\wwwroot\files yoluna excel dosyası olarak kayıt eder.
   
