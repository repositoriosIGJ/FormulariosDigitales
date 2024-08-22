using System;
using System.Collections.Generic;
using System.Text;

namespace FD.Entities
{
    public class Banco
    {
        public Banco()
        { }

        public Banco(string id, string descripcion)
        {
            this._id = id;
            this._descripcion = descripcion;
        }

        private string _id;

        public string Id
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
    }
}
