using System.Collections.ObjectModel;
using System.Data.Common;

namespace appWPF.Conection
{
    public interface IConnection
    {
        ObservableCollection<Pokemon> ConsultInDbd();
        bool TestConnection();
        void ConnectionInDbd(string _query);
        void InsertInDbd(string _pokeNme, string _pokeType, string pokeCoach);
        void DeletInDbd(int _id);
        void EditeInDbd(int _id, string _nomePokemon, string _tipoPokemon, string _treinadorPokemon);
        int pegarId();
    }
}
