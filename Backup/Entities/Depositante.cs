using System;
using System.Collections.Generic;
using System.Text;

namespace FD.Entities
{
    public class Depositante
    {
        public Depositante()
        { 
        }

        //Constructor
        public Depositante(Int64 dni, string banco, string bancodesc)
        {
            this._dni = dni;
            this._banco = banco;
            this._bancodesc = bancodesc;
        }

        private Int64 _dni;

        public Int64 dni
        {
            get { return _dni; }
            set { _dni = value; }
        }

        private string _banco;

        public string banco
        {
            get { return _banco; }
            set { _banco = value; }
        }

        private string _bancodesc;

        public string bancodesc
        {
            get { return _bancodesc; }
            set { _bancodesc = value; }
        }
    }
}
