using Npgsql;
using System.Collections.ObjectModel;
using System.Data.Common;

namespace appWPF.Conection
{
    public class ConnectionPostgres : ConnectionDb
    {
        public ConnectionPostgres()
        {
            host = "localhost";
            port = "5432";
            user = "postgres";
            password = "example";
            dbName = "postgres";
            pokemon = new ObservableCollection<Pokemon>();

        }



        public override DbConnection GetConnectionType()
        {
            return new NpgsqlConnection($"server={host};database={dbName};port={port};user id={user};password={password}");
        }
        public override DbCommand GetComandType(string _query, DbConnection _conn)
        {
            return new NpgsqlCommand(_query, (NpgsqlConnection)_conn);
        }
    }
}

