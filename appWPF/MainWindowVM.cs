using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
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



        private ConectaDb conectaDb { get; set; }
        public MainWindowVM()
        {
            conectaDb = new ConectaDb();
            pokeList = new ObservableCollection<Pokemon>();
            if(conectaDb.TestConnection() == 1)
            {
                consultarPokemon();
                IniciaComando();

            }else
            {
                string messageBoxText = "Não foi possível conectar ao banco de dados, tente novamente mais tarde!";
                MessageBox.Show(messageBoxText, "Erro De Conexão", MessageBoxButton.OK, MessageBoxImage.Error);
                System.Windows.Application.Current.Shutdown();
            }

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
                if ((bool)tela.DialogResult)
                {
                    if(string.IsNullOrEmpty(newPokemon.Name)|| string.IsNullOrEmpty(newPokemon.PokeType) || string.IsNullOrEmpty(newPokemon.Coach))
                    {
                        if (MessageBox.Show("Um ou mais campos estão sem valor, deseja inserir assim mesmo?",
                        "Campos vazios",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Question) == MessageBoxResult.Yes)
                        {
                            conectaDb.inserirPokemon(newPokemon.Name, newPokemon.PokeType, newPokemon.Coach);
                            consultarPokemon();
                        }

                    }
                    else
                    {
                        conectaDb.inserirPokemon(newPokemon.Name, newPokemon.PokeType, newPokemon.Coach);
                        consultarPokemon();
                    }

                }
            });

            Remove = new RelayCommand((object _) =>
            {
                if(selectPokemon != null)
                {
                    if (MessageBox.Show($"Deseja remover o {selectPokemon.Name} do treinador {selectPokemon.Coach}?",
                    "Remover Pokemon",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        conectaDb.deletarPokemon(selectPokemon.Id);
                        consultarPokemon();
                    }
                }

            });

            Edite = new RelayCommand((object _) =>
            {
                  if(selectPokemon != null)
                {

                    if (MessageBox.Show($"Deseja mesmo editar {selectPokemon.Name}?",
                    "Editar Pokemon",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        Pokemon copy = (Pokemon)selectPokemon.Clone();

                        DadosPokemon tela = new DadosPokemon();
                        tela.DataContext = copy;

                        bool alterou = (bool)tela.ShowDialog();
                        if (alterou)
                        {
                            conectaDb.editarPokemon(copy.Id, copy.Name, copy.PokeType, copy.Coach);
                            consultarPokemon();
                        }
                    }
                }
                
            });
        }
       
    }
}
