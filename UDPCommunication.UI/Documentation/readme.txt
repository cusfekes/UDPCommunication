#####################################
#                                   #
#                                   #
#    UDP Mesaj Alıcı / Gönderici    #
#          Çağdaş Üsfekes           #
#                                   #
#                                   #
#####################################



Uygulamanın veritabanına bağlanabilmesi için yapılması gerekenler:

1- "DBScripts" klasörü altındaki "create_database_script.sql" dosyası pgAdmin veya farklı bir veritabanı yönetim uygulaması üzerinden çalıştırılır.
	Bu adım ile sahibi(owner) "postgres" olan "UDP" isimli veritabanı oluşturulur.
2- "DBScripts" klasörü altındaki "create_table_script.sql" dosyası çalıştırılır.
	Bu adım ile sahibi(owner) "postgres" olan, "public" şemasında "UDPLog" isimli tablo oluşturulur.
3-  Uygulamanın çalıştırılabilir halinin olduğu dizinde yer alan "hibernate.cfg.xml" isimli hibernate konfigurasyon dosyası açılır.
4-  Bu dosyadaki "connection.connection_string" isimli alanda bulunan bağlantı bilgileri veritabanının bulunduğu sunucu veya
	 local bilgisayara göre güncellenir.

Uygulamanın çalıştırılabilir hali "Executable" klasörü altındaki "UDPCommunicator.zip" arşivindedir. Bu arşiv dosyası içindeki "UDPCommunicator.exe"
isimli exe üzerinden uygulama çalıştırılabilir.

Not: Uygulama, veritabanının hiç olmadığı senaryoda da çalışabilecek şekilde tasarlanmıştır. Bu senaryoda veritabanı işlemleri sırasında 
oluşan hatalar kullanıcıya uyarı olarak verilir ve akış başarılı şekilde devam eder.

