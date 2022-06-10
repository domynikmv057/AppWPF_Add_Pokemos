using System.ComponentModel;

namespace appWPF
{

    //Exemplo do S do solid, pois antes de passar para ca, esse mesmo treixo de codigo
    //era usado em varias partes do codigo, na <Pokemon.cs> para avisar quando algum campo mudava.
    // fazendo com que a classe <pokemon.cs> nao fosse apenas responsavel pelos metodos e valores
    // que constitue um pokemon, mas tambem por um metodo para notificar essa mudança
    public abstract class Notifica : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged(string prop)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
