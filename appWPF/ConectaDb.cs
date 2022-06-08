using Npgsql;
using System;
using System.Collections.ObjectModel;

namespace appWPF
{
    public class ConectaDb
    {
        public ObservableCollection<Pokemon> pokemon;
        private NpgsqlConnection conn;
        private string query;
        private NpgsqlCommand cmd;

        public ConectaDb()
        {
            pokemon = new ObservableCollection<Pokemon>();
        }

        public void inserirPokemon(string _nomePokemon, string _tipoPokemon, string _treinadorPokemon)
        {
            query = $@"INSERT INTO pokemons(nome_pokemon, tipo_pokemon, treinador_pokemon) values('{_nomePokemon}', '{_tipoPokemon}', '{_treinadorPokemon}')";
            conectaDbd(query);
        }

        public void deletarPokemon(int _id)
        {
            query = $@"DELETE FROM pokemons WHERE id_pokemon = {_id};";
            conectaDbd(query);
        }

        public void editarPokemon(int _id, string _nomePokemon, string _tipoPokemon, string _treinadorPokemon)
        {
            query = $@"update pokemons set nome_pokemon='{_nomePokemon}', tipo_pokemon='{_tipoPokemon}', treinador_pokemon='{_treinadorPokemon}' where id_pokemon='{_id}'";
            conectaDbd(query);
        }

    


        public ObservableCollection<Pokemon> consultarPokemon()

        {
            using (conn = GetConnection())
            {
                query = @"select * from pokemons";
                cmd = new NpgsqlCommand(query, conn);
                try
                {
                    conn.Open();

                    int n = cmd.ExecuteNonQuery();

                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Pokemon pokemon = new Pokemon(Convert.ToInt32(reader[0]), Convert.ToString(reader[1]), Convert.ToString(reader[2]), Convert.ToString(reader[3]));
                            this.pokemon.Add(pokemon);

                        }
                    }
                    cmd.Dispose();
                    conn.Close();
                    return this.pokemon;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("ERROR: " + ex.Message);
                    return this.pokemon;
                }

            }
        }




        public void conectaDbd(string parametro)
        {
            using (conn = GetConnection())
            {
                cmd = new NpgsqlCommand(parametro, conn);
                try
                {
                    conn.Open();
                    int n = cmd.ExecuteNonQuery();
                    if (n == 1)
                    {
                        Console.WriteLine("Comando executado");
                    }
                    else
                    {
                        Console.WriteLine(n);
                        Console.WriteLine("algo Deu errado");
                    }
                    conn.Close();
                    cmd.Dispose();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("ERROR: " + ex.Message);
                    conn.Close();
                    cmd.Dispose();
                }
            }
        }



        public  int TestConnection()
        {
            using (conn=GetConnection())
            {
                try
                {
                    conn.Open();
                    conn.Close();
                    return 1;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("ERROR: " + ex.Message);
                    conn.Close();
                    return 0;
                }
            }
        }


        //public void selecionar
        private static NpgsqlConnection GetConnection()
        {
            return new NpgsqlConnection(@"Server=localhost;Port=5432;User Id=postgres;Password=example;Database=postgres;"); ;
        }

    }
}
