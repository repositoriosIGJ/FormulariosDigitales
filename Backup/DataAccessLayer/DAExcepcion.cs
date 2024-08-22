using System;
using System.Collections.Generic;
using System.Text;

namespace FD.DataAccessLayer
{
    public class DAExcepcion : Exception
    {
        public DAExcepcion(string mensaje): base(mensaje)
        {}
    }
}
