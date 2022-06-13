using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;

namespace appWPF.Conection
{
    public abstract class ConnectionDb : IConnection //exemplo de abstração, possue caracteristicas que uma conexao deveria ter
    {
        public static string host { get; set; }
        public static string port { get; set; }
        public static string user { get; set; }
        public static string password { get; set; }
        public static string dbName { get; set; }
        public static string query { get; set; }

        public static DbDataReader reader;
        public static DbConnection conn;
        public static DbCommand cmd;

        public List<Pokemon> pokemon;
        //public ObservableCollection<Pokemon> pokemon;
        // Essas funções sao exemplos de Herança, pois sao herdadas da <IConnection> a interface pai Diretamente
        // e as classes <connectioMysql> e <ConnectionPostgres> tambem irao herdar indiretamente da <IConnection>
        public abstract DbConnection GetConnectionType();

        public abstract DbCommand GetComandType(string _query, DbConnection _conn);

        public List<Pokemon> ConsultInDbd()
        {
            pokemon = new List<Pokemon>();
            query = @"select * from pokemons order by id_pokemon;";
            try
            {
                conn = GetConnectionType();
                cmd = GetComandType(query, conn);
                conn.Open();

                cmd.ExecuteNonQuery();

                using (reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Pokemon pokemon = new Pokemon(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3));
                        this.pokemon.Add(pokemon);

                    }
                }

                return this.pokemon;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.Message);
                return this.pokemon;
            }
            finally
            {
                cmd.Dispose();
                conn.Close();
            }
        }

        public bool TestConnection()
        {
            try
            {
                conn = GetConnectionType();
                conn.Open();
                return true;
            }
            catch 
            {
                throw new Exception("Não Conseguimos Conectar Ao Banco De Dados");
            }
            finally
            {
                conn.Close();
            }
        }

        public void ConnectionInDbd(string _query)
        {

            try
            {
                conn = GetConnectionType();
                cmd = GetComandType(query, conn);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERRO: " + ex.Message);
            }
            finally
            {
                cmd.Dispose();
                conn.Close();
            }
        }

        public void InsertInDbd(string _pokeNme, string _pokeType, string pokeCoach)
        {
            try
            {
                query = $"INSERT INTO pokemons (nome_pokemon, tipo_pokemon, treinador_pokemon)VALUES('{_pokeNme}', '{_pokeType}', '{pokeCoach}')";
                ConnectionInDbd(query);

            }
            catch (Exception ex)
            {
                Console.WriteLine("ERRO: " + ex.Message);

            }
        }

        public void DeletInDbd(int _id)
        {
            query = $@"DELETE FROM pokemons WHERE id_pokemon = {_id};";
            try
            {
                ConnectionInDbd(query);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void EditeInDbd(int _id, string _nomePokemon, string _tipoPokemon, string _treinadorPokemon)
        {
            query = $@"update pokemons set nome_pokemon='{_nomePokemon}', tipo_pokemon='{_tipoPokemon}', treinador_pokemon='{_treinadorPokemon}' where id_pokemon={_id}";
            try
            {
                ConnectionInDbd(query);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int pegarId()
        {
            query = "SELECT MAX(id_pokemon) FROM pokemons;";

            try
            {
                conn = GetConnectionType();
                cmd = GetComandType(query, conn);

                conn.Open();
                cmd.ExecuteNonQuery();

                cmd.ExecuteScalar();
                int _id = (int)cmd.ExecuteScalar();
                return _id;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conn.Close();
                cmd.Dispose();
            }
        }
    }
  
}
