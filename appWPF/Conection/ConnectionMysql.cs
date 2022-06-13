
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;


namespace appWPF.Conection
{
    public class ConnectionMysql : ConnectionDb
    {
        private ConnectionMysql object1;
        private IConnection object2;

        public ConnectionMysql()
        {
            host = "localhost";
            port = "3306";
            user = "root";
            password = "mysql_luz";
            dbName = "PokeDb";
            pokemon = new List<Pokemon>();
        }

        public ConnectionMysql(ConnectionMysql object1, IConnection object2)//SimsMock
        {
            this.object1 = object1;
            this.object2 = object2;
        }


        //Exemplo de polimorfismo, pois os metodos GetConnectionType(), e GetComandType(...)
        //Sao herdados da classe ConnectionDb, porem precisam tomar comportamentos diferentes,
        //entre o <ConnectioMySql>, <ConnectionPostgres>
        public override DbConnection GetConnectionType()
        {
            return new MySqlConnection($"server={host};database={dbName};port={port};user id={user};password={password}");
        }
        public override DbCommand GetComandType(string _query, DbConnection _conn)
        {
            return new MySqlCommand(_query, (MySqlConnection)_conn);
        }

    }
}
