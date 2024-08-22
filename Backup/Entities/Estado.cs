using System;
using System.Collections.Generic;
using System.Text;

namespace FD.Entities
{
    public class Estado
    {
        public Estado()
        { }

        public Estado(int id)
        {
            this._id = id;
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
	
	
    }
}
