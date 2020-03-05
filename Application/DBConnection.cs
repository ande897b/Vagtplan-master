using System.Data.SqlClient;

namespace Application
{
    public static class DBConnection
    {
        private static string databaseName = string.Empty;
        public static string DatabaseName
        {
            get { return databaseName; }
            set { databaseName = value; }
        }

        private static SqlConnection connection = null;
        public static SqlConnection Connection
        {
            get { return connection; }
        }

        public static bool IsConnected()
        {
            if (Connection == null)
            {
                if (string.IsNullOrEmpty(databaseName))
                    return false;
                string connstring = string.Format("Server=EALSQL1.eal.local; Database={0}; User Id=C_STUDENT02; Password=C_OPENDB02", databaseName);
                connection = new SqlConnection(connstring);
                connection.Open();
            }
            return true;
        }

        public static void Close()
        {
            connection.Close();
            connection = null;
        }
    }
}
