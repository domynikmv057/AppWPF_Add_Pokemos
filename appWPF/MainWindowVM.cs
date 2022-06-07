using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace appWPF
{
    public class MainWindowVM : Notifica, INotifyPropertyChanged
    {
        public ObservableCollection<Pokemon> pokeList { get; set; }
        public ICommand Add { get; private set; }
        public ICommand Remove { get; private set; }
        public ICommand Edite { get; private set; }
        public Pokemon selectPokemon { get; set; }

        public string nomeUsuario { get; set; } = "";

        public Treinador treinador { get; set; }

        private ConectaDb conectaDb { get; set; }
        public MainWindowVM()
        {
            conectaDb = new ConectaDb();
            pokeList = new ObservableCollection<Pokemon>();
            consultarPokemon();
            IniciaComando();
            treinador = new Treinador();
            if (nomeUsuario == "")
            {
                InsertUserName userName = new InsertUserName();
                userName.DataContext = treinador;
                userName.ShowDialog();
            }
        }

        public void inserirPokemon(string _nomePokemon, string _tipoPokemon, string _treinadorPokemon)
        {
            conectaDb.inserirPokemon(_nomePokemon, _tipoPokemon, _treinadorPokemon);
        }
        
        public void deletaPokemon(int _id)
        {
            conectaDb.deletarPokemon(_id);
        }
        public void editarPokemon(int _id, string _nomePokemon, string _tipoPokemon, string _treinadorPokemon)
        {
            conectaDb.editarPokemon(_id, _nomePokemon, _tipoPokemon, _treinadorPokemon);
        }

        public void consultarPokemon()
        {
            this.pokeList.Clear();
            this.pokeList = conectaDb.consultarPokemon();
        }


        public void IniciaComando()
        {

            Add = new RelayCommand((object _) =>
            {
                Pokemon newPokemon = new Pokemon();

                DadosPokemon tela = new DadosPokemon();
                tela.DataContext = newPokemon;
                tela.ShowDialog();
                inserirPokemon(newPokemon.Name, newPokemon.PokeType, newPokemon.Coach);
                consultarPokemon();
            });

            Remove = new RelayCommand((object _) =>
            {
                deletaPokemon(selectPokemon.Id);
                consultarPokemon();
            });

            Edite = new RelayCommand((object _) =>
            {
                  if(selectPokemon != null)
                {
                    Pokemon copy = (Pokemon)selectPokemon.Clone();

                    DadosPokemon tela = new DadosPokemon();
                    tela.DataContext = copy;

                    bool alterou = (bool)tela.ShowDialog();
                    if(alterou)
                    {
                        editarPokemon(copy.Id, copy.Name, copy.PokeType, copy.Coach);
                        consultarPokemon();
                    }

                }
                
            });
        }
       
    }
}
