using System;
using System.Collections.Generic;
using System.Text;

namespace FD.Entities
{
    public class Constituyente
    {
        //Nombre y Apellido
        public Constituyente(string dni, string nombreYApellido, int nroconsti)
        {
            this._dni = dni;
            this._nombreYApellido = nombreYApellido;
        }
        //Constituyente
        public Constituyente(string denominacion1, string denominacion2)
        {
            this._denominacion1 = denominacion1;
            this._denominacion2 = denominacion2;
        }
        //MIXTO
        public Constituyente(string dni, string nombreYApellido, string denominacion)
        {
            this._dni = dni;
            this._nombreYApellido = nombreYApellido;
            this._denominacion1 = denominacion;
        }

        private string _dni;

        public string DNI
        {
            get { return _dni; }
            set { _dni = value; }
        }

        private string _nombreYApellido;

        public string NombreYApellido
        {
            get { return _nombreYApellido; }
            set { _nombreYApellido = value; }
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
    }
}
