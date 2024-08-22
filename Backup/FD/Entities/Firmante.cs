using System;
using System.Collections.Generic;
using System.Text;

namespace FD.Entities
{
    public class Firmante
    {
        public Firmante()
        { }

        public Firmante(Int64 dni)
        {
            this._dni = dni;
        }

        public Firmante(Int64 dni, string nombre, string apellido, string mail)
        {
            this._dni = dni;
            this._nombre = nombre;
            this._apellido = apellido;
            this._mail = mail;
        }

        public Firmante(Int64 dni, string nombre, string apellido, string mail, string telefono)
        {
            this._dni = dni;
            this._nombre = nombre;
            this._apellido = apellido;
            this._mail = mail;
            this._telefono = telefono;
        }

        //EMPIEZA CAMBIOS EN EL PRESENTANTE V1.8
        public Firmante(Int64 dni, string nombre, string apellido, string mail, string telefono, string tipodoc, string caracter, string autorizado)
        {
            this._dni = dni;
            this._nombre = nombre;
            this._apellido = apellido;
            this._mail = mail;
            this._telefono = telefono;
            this._tipoDOC = tipodoc;
            this._caracter = caracter;
            this._autorizado = autorizado;
        }
        
        private string _tipoDOC;
        public string TipoDOC
        {
            get { return _tipoDOC; }
            set { _tipoDOC = value; }
        }

        private string _caracter;
        public string Caracter
        {
            get { return _caracter; }
            set { _caracter = value; }
        }

        private string _autorizado;
        public string Autorizado
        {
            get { return _autorizado; }
            set { _autorizado = value; }
        }
        //TERMINA CAMBIOS EN EL PRESENTANTE V1.8

        private Int64 _dni;
        public Int64 DNI
        {
            get { return _dni; }
            set { _dni = value; }
        }

        private string _nombre;
        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        private string _apellido;

        public string Apellido
        {
            get { return _apellido; }
            set { _apellido = value; }
        }

        private string _mail;
        public string Mail
        {
            get { return _mail; }
            set { _mail = value; }
        }

        private string _telefono;
        public string Telefono
        {
            get { return _telefono; }
            set { _telefono = value; }
        }
    }
}
