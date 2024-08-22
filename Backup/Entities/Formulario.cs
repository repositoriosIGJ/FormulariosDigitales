using System;
using System.Collections.Generic;
using System.Text;

namespace FD.Entities
{
    public class Formulario
    {
        //para cargado de datos de reporte.
        public Formulario(string descripcion, int precio)
        {
            this._descripcion = descripcion;
            this._precio = precio;
        }

        public Formulario(string descripcion)
        {
            this._descripcion = descripcion;
        }

        public Formulario(int id, string descripcion, int precio)
        {
            this._id = id;
            this._descripcion = descripcion;
            this._precio = precio;
        }

        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _descripcion;

        public string Descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }

        private int _precio;

        public int Precio
        {
            get { return _precio; }
            set { _precio = value; }
        }	
    }
}
