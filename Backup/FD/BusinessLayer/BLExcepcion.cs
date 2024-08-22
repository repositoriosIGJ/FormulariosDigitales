using System;
using System.Collections.Generic;
using System.Text;

namespace FD.BusinessLayer
{
    public class BLExcepcion : Exception
    {
        //private string _Mensaje;

        //public string Mensaje
        //{
        //    get { return _Mensaje; }
        //    set { _Mensaje = value; }
        //}

        public BLExcepcion(string mensaje)
            : base(mensaje)
        {
            //_Mensaje = mensaje;
        }

    }
}
