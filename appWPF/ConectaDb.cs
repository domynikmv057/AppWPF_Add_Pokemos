using Npgsql;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace appWPF
{
     public class ConectaDb
    {
        public ObservableCollection<Pokemon> pokemon;
        private NpgsqlConnection conn;
        private string sql;
        private NpgsqlCommand cmd;

        public ConectaDb()
        {
            pokemon = new ObservableCollection<Pokemon>();
        }

        public void inserirPokemon(string _nomePokemon, string _tipoPokemon, string _treinadorPokemon)
        {
            string query = $@"INSERT INTO pokemons(nome_pokemon, tipo_pokemon, treinador_pokemon) values('{_nomePokemon}', '{_tipoPokemon}', '{_treinadorPokemon}')";
            conectaDbd(query);
        }

        public void deletarPokemon(int _id)
        {
            string query = $@"DELETE FROM pokemons WHERE id_pokemon = {_id};";
            conectaDbd(query);
        }

        public void editarPokemon(int _id, string _nomePokemon, string _tipoPokemon, string _treinadorPokemon)
        {
            string query = $@"update pokemons set nome_pokemon='{_nomePokemon}', tipo_pokemon='{_tipoPokemon}', treinador_pokemon='{_treinadorPokemon}' where id_pokemon='{_id}'";
            conectaDbd(query);
        }

    


        public ObservableCollection<Pokemon> consultarPokemon()

        {
            using (NpgsqlConnection con = GetConnection())
            {
                string query = @"select * from pokemons";
                NpgsqlCommand cmd = new NpgsqlCommand(query, con);
                try
                {
                    con.Open();

                    int n = cmd.ExecuteNonQuery();

                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Pokemon pokemon = new Pokemon(Convert.ToInt32(reader[0]), Convert.ToString(reader[1]), Convert.ToString(reader[2]), Convert.ToString(reader[3]));
                            this.pokemon.Add(pokemon);

                        }
                    }

                    return this.pokemon;
                    cmd.Dispose();
                    con.Close();

                }
                catch (Exception ex)
                {
                    Console.WriteLine("ERRO AO LER");
                    return this.pokemon;
                }

            }
        }




        public void conectaDbd(string parametro)
        {
            using (NpgsqlConnection con = GetConnection())
            {
                NpgsqlCommand cmd = new NpgsqlCommand(parametro, con);
                try
                {
                    con.Open();
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
                    con.Close();
                    cmd.Dispose();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("ERROR: " + ex.Message);
                    con.Close();
                    cmd.Dispose();
                }
            }
        }



        public  void TestConnection()
        {
            using (NpgsqlConnection con=GetConnection())
            {
                con.Open();
                if(con.State == ConnectionState.Open)
                {
                    
                    Console.WriteLine("Conenected");
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
