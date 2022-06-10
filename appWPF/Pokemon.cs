using System;
using System.ComponentModel;

namespace appWPF
{
    public class Pokemon : Notifica, INotifyPropertyChanged, ICloneable
    {
        private int id;//exempleo de encapsulamento, usamos para proporcionar maior segurança, dando ao usuario somente dados relevantes para ele
        private string name;
        private string type;
        private string coach;
        public Pokemon()
        {
        }

        public Pokemon(int id, string _name, string _type, string _coach)
        {
            this.id = id;
            this.name = _name;
            this.type = _type;
            this.coach = _coach;
        }

        public string Name
        {
            get { return name; }
            set
            {
                if (name != value)
                {
                    name = value;
                    RaisePropertyChanged("Name");
                }
            }
        }

        public string PokeType
        {
            get { return type; }
            set
            {
                if (type != value)
                {
                    type = value;
                    RaisePropertyChanged("PokeType");
                }
            }
        }

        public string Coach
        {
            get { return coach; }
            set
            {
                if (coach != value)
                {
                    coach = value;
                    RaisePropertyChanged("Coach");
                }
            }
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
