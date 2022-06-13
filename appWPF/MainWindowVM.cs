using appWPF.Conection;
using System;
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

        public string BancoEscolhido { get; set; } = "Mysql";

        private IConnection conectDbd;

        public MainWindowVM()
        {

            //conectDbd = new ConnectionMysql(); 
            conectDbd = new ConnectionPostgres(); 

            if (conectDbd.TestConnection())
            {
                pokeList = new ObservableCollection<Pokemon>(conectDbd.ConsultInDbd());
                IniciaComando();
            }
            else
            {
                string messageBoxText = "Não foi possível conectar ao banco de dados, tente novamente mais tarde!";
                MessageBox.Show(messageBoxText, "Erro De Conexão", MessageBoxButton.OK, MessageBoxImage.Error);
                System.Windows.Application.Current.Shutdown();
            }
        }

        public bool CheckEmptyString(string _CheckString)
        {
            if (string.IsNullOrEmpty(_CheckString))
            {
                throw new Exception("Um ou mais campos estao vazios");
            }
            else
            {
                return false;
            }
        }

        public void IniciaComando()
        {
            Add = new RelayCommand((object _) =>
            {
                Pokemon newPokemon = new Pokemon();


                FormsPokemon tela = new FormsPokemon();
                tela.DataContext = newPokemon;
                tela.ShowDialog();
                if ((bool)tela.DialogResult)
                {
                    try
                    {
                        CheckEmptyString(newPokemon.Name);
                        CheckEmptyString(newPokemon.PokeType);
                        CheckEmptyString(newPokemon.Coach);
                        try
                        {
                            conectDbd.InsertInDbd(newPokemon.Name, newPokemon.PokeType, newPokemon.Coach);//MOCK AQUI
                            newPokemon.Id = conectDbd.pegarId();
                            pokeList.Add(newPokemon);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Errro: " + ex);
                        }


                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);

                    }
                }
            });

            Remove = new RelayCommand((object _) =>
            {
                if (selectPokemon != null)
                {
                    if (MessageBox.Show($"Deseja remover o {selectPokemon.Name} do treinador {selectPokemon.Coach}?",
                    "Remover Pokemon",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        try
                        {
                            conectDbd.DeletInDbd(selectPokemon.Id);
                            pokeList.Remove(selectPokemon);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Errro: " + ex);
                        }
                    }
                }

            });

            Edite = new RelayCommand((object _) =>
            {
                if (selectPokemon != null)
                {

                    if (MessageBox.Show($"Deseja mesmo editar {selectPokemon.Name}?",
                    "Editar Pokemon",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        Pokemon copy = (Pokemon)selectPokemon.Clone();

                    
                        FormsPokemon tela = new FormsPokemon();
                        tela.DataContext = copy;
                        bool alterou = (bool)tela.ShowDialog();
                        if (alterou)
                        {
                            try
                            {
                                CheckEmptyString(copy.Name);
                                CheckEmptyString(copy.PokeType);
                                CheckEmptyString(copy.Coach);
                                try
                                {
                                    conectDbd.EditeInDbd(copy.Id, copy.Name, copy.PokeType, copy.Coach);
                                    selectPokemon.Name = copy.Name;
                                    selectPokemon.PokeType = copy.PokeType;
                                    selectPokemon.Coach = copy.Coach;
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("Errro: " + ex);

                                }
                            }
                            catch(Exception ex)
                            {
                                MessageBox.Show(ex.Message);

                            }
                        }
                    }
                }

            });
        }

    }
}
