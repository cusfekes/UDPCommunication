namespace UDPCommunication.Data
{
    /// <summary>
    /// Constants variables for database queries and exception messages
    /// </summary>
    public class Constants
    {
        // Query constants
        public const string INSERT_STATEMENT_QUERY = @"insert into ""public"".""UDPLog""(""Id"", ""Message"", ""LogDate"", ""IpAddress"", ""PortNumber"", ""LogDirection"")VALUES(:Id, :Message, :LogDate, :IpAddress, :PortNumber, :LogDirection);";

        public const string DELETE_STATEMENT_QUERY = @"delete from ""public"".""UDPLog"" where ""Id""=:Id";

        public const string GET_ALL_STATEMENT_QUERY = @"select * from ""public"".""UDPLog""";

        public const string GET_BY_DATE_STATEMENT_QUERY = @"select * from ""public"".""UDPLog"" where ""LogDate"">=:StartDate and ""LogDate""<=:EndDate";


        // Message constants
        public const string DB_ERROR = "Veritabanı seviyesi hatası: ";

        public const string DB_SAVE_ERROR = "Kayıt işlemi başarısız. ";

        public const string DB_DELETE_ERROR = "Silme işlemi başarısız. ";

        public const string DB_GET_ALL_ERROR = "Tüm öğeleri çekme işlemi başarısız. ";

        public const string DB_GET_BY_DATE_RANGE_ERROR = "Tarihe göre öğe filtreleme işlemi başarısız. ";

        public const string CRYPTO_ERROR = "Şifreleme mekanizması hatası: ";
    }
}
