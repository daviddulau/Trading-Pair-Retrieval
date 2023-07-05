using System.Data.SQLite;

namespace PriceRetrieval.UnitTests.DataStorage
{
    public class PairPriceDataStorageTests : IDisposable
    {
        private SQLiteConnection _connection = new();

        const string TestTableName = "TestPairs";
        const string ConnectionString = "Data Source=:memory:;Version=3;New=True;";

        private void StartDataBaseAndCreateTable()
        {
            _connection = new SQLiteConnection(ConnectionString);
            _connection.Open();

            var createTableQuery = $"CREATE TABLE {TestTableName} (DateTime DATETIME, Pair TEXT, ClosePrice REAL)";

            using var command = new SQLiteCommand(createTableQuery, _connection);
            command.ExecuteNonQuery();
        }

        public void Dispose()
        {
            _connection.Close();
        }

        [Theory]
        [InlineData("2023-01-01T12:00:00", "BTCUSD", 20000)]
        [InlineData("2023-01-01T12:00:00", "btcusd", 20000)]
        [InlineData("2023-01-01T13:00:00", "audusd", 2.543)]
        [InlineData("2023-01-01T14:00:00", "gbpusd", 1.123)]
        [InlineData("2023-01-01T14:00:00", "GBPUSD", 1.123)]
        [InlineData("2023-01-01T15:00:00", "AUDCHF", 2.56)]
        [InlineData("2023-01-01T15:00:00", "AUDCHF", 2.56)]
        public void Insert_RecordInsertedSuccessfully(string datetime, string pair, double price)
        {
            StartDataBaseAndCreateTable();

            var insertQuery = $"INSERT INTO {TestTableName} (DateTime, Pair, ClosePrice) VALUES (@DateTime, @Pair, @ClosePrice)";

            var expectedRowAffected = 1;

            using var command = new SQLiteCommand(insertQuery, _connection);

            command.Parameters.AddWithValue("@DateTime", datetime);
            command.Parameters.AddWithValue("@Pair", pair);
            command.Parameters.AddWithValue("@ClosePrice", price);
            
            var actualRowsAffected = command.ExecuteNonQuery();

            actualRowsAffected.Should().Be(expectedRowAffected);
        }

        [Theory]
        [InlineData("2023-01-01T12:00:00", "btcusd", 20000)]
        [InlineData("2023-01-01T13:00:00", "audusd", 2.543)]
        [InlineData("2023-01-01T14:00:00", "gbpusd", 1.123)]
        [InlineData("2023-01-01T15:00:00", "audchf", 2.56)]
        public void Select_RecordRetrievedSuccessfully(string datetime, string pair, double price)
        {
            StartDataBaseAndCreateTable();

            var insertQuery = $"INSERT INTO {TestTableName} (DateTime, Pair, ClosePrice) VALUES (@DateTime, @Pair, @ClosePrice)";

            using (var insertCommand = new SQLiteCommand(insertQuery, _connection))
            {
                insertCommand.Parameters.AddWithValue("@DateTime", datetime);
                insertCommand.Parameters.AddWithValue("@Pair", pair);
                insertCommand.Parameters.AddWithValue("@ClosePrice", price);
                insertCommand.ExecuteNonQuery();
            }

            using var command = new SQLiteCommand($"SELECT ClosePrice FROM {TestTableName} WHERE DateTime = '{datetime}' AND Pair = '{pair}'", _connection);
            using var reader = command.ExecuteReader();

            reader.HasRows.Should().BeTrue();

            reader.Read();

            reader[0].Should().Be(price);  
        }
    }
}
