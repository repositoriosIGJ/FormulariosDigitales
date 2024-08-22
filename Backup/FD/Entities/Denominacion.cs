using System;
using System.Collections.Generic;
using System.Text;

namespace FD.Entities
{
    public class Denominacion
    {
        public Denominacion(string denominacion1, string denominacion2, string denominacion3, List<Constituyente> constituyente, int tipo)
        {
            this._denominacion1 = denominacion1;
            this._denominacion2 = denominacion2;
            this._denominacion3 = denominacion3;
            this._constituyente = constituyente;
            this._tipo = tipo;
        }

        private string _denominacion1;

        public string Denominacion1
        {
            get { return _denominacion1; }
            set { _denominacion1 = value; }
        }

        private string _denominacion2;

        public string Denominacion2
        {
            get { return _denominacion2; }
            set { _denominacion2 = value; }
        }

        private string _denominacion3;

        public string Denominacion3
        {
            get { return _denominacion3; }
            set { _denominacion3 = value; }
        }

        private List<Constituyente> _constituyente;

        public List<Constituyente> Constituyente
        {
            get { return _constituyente; }
            set { _constituyente = value; }
        }

        private int _tipo;

        public int Tipo
        {
            get { return _tipo; }
            set { _tipo = value; }
        }
	
	
   } 	
}
