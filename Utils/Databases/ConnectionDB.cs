namespace Utils.Databases
{
    public class ConnectionDB
    {
        private static readonly string _connectionString =
            "Data Source=localhost;Initial Catalog=LocadoraBD;User ID=sa;Password=SqlServer@2022;TrustServerCertificate=True;Connect Timeout=120";

        public static string GetConnectionString()
        {
            return _connectionString;
        }
    }
}
