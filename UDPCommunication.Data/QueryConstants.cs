namespace UDPCommunication.Data
{
    public class QueryConstants
    {
        public static string INSERT_STATEMENT = @"insert into ""public"".""UDPLog""(""Id"", ""Message"", ""LogDate"", ""Ip"", ""LogDirection"")VALUES(:Id, :Message, :LogDate, :Ip, :LogDirection);";

        public static string DELETE_STATEMENT = @"delete from ""public"".""UDPLog"" where ""Id""=:Id";

        public static string GET_ALL_STATEMENT = @"select * from ""public"".""UDPLog""";

        public static string GET_BY_DATE_STATEMENT = @"select * from ""public"".""UDPLog"" where ""LogDate"">=:StartDate and ""LogDate""<=:EndDate";
    }
}
