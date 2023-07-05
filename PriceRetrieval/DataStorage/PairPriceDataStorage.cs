using System.Data.SQLite;
using PriceRetrieval.Models;

namespace PriceRetrieval.DataStorage
{
    public class PairPriceDataStorage : IPairPriceDataStorage
    {
        const string InMemoryConnectionString = "Data Source=InMemoryDB;Mode=Memory;Cache=Shared";
        const string MemorizedPairTableName = "MemorizedPairs";

        public void CreateTableCommand()
        {
            using var connection = new SQLiteConnection(InMemoryConnectionString);
            connection.Open();

            //check if the table present
            string query = "SELECT name FROM sqlite_master WHERE type='table'";
            using var command = new SQLiteCommand(query, connection);
            using var reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                reader.Read();

                var tableName = reader.GetString(0);

                if (tableName == MemorizedPairTableName)
                {
                    return;
                }
            }

            var createTableQuery = $"CREATE TABLE {MemorizedPairTableName} (DateTime DATETIME, Pair TEXT, ClosePrice REAL)";

            using var command2 = new SQLiteCommand(createTableQuery, connection);

            command2.ExecuteNonQuery();
        }

        public void InsertPairCommand(PriceRetrievalRequest request, double closePrice)
        {
            using var connection = new SQLiteConnection(InMemoryConnectionString);
            connection.Open();

            using var command = new SQLiteCommand($"INSERT INTO {MemorizedPairTableName} (DateTime, Pair, ClosePrice) VALUES (@DateTime, @Pair, @ClosePrice)", connection);

            command.Parameters.AddWithValue("@DateTime", request.DateTime);
            command.Parameters.AddWithValue("@Pair", request.Pair);
            command.Parameters.AddWithValue("@ClosePrice", closePrice);

            command.ExecuteNonQuery();
        }

        public string GetPair(PriceRetrievalRequest request)
        {
            using var connection = new SQLiteConnection(InMemoryConnectionString);
            connection.Open();

            using var command = new SQLiteCommand($"SELECT ClosePrice FROM {MemorizedPairTableName} WHERE DateTime = '{request.DateTime}' AND Pair = '{request.Pair}'", connection);
            using var reader = command.ExecuteReader();
            
            if (!reader.HasRows)
            {
                return null;
            }
            
            reader.Read();

            var closePrice = Convert.ToString(reader[0]);

            return closePrice;
        }
    }
}
