using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using FD.DataAccessLayer;
using FD.Entities;
using System.Threading;

namespace FD.BusinessLayer
{
    public class BLLibro
    {
        #region "Alta de Libro"

        public void AltaLibro(Libro oLibro, long IdTransaccion)
        {
            DALibro oDALibro = new DALibro();
            try
            {
                oDALibro.AltaLibro(oLibro, IdTransaccion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oDALibro = null;
            }
        }

        #endregion "Alta de Libro"

        #region "Trae el Listado de Libros"

        public List<TipoLibro> RetornarTiposLibros()
        {
            DALibro oDALibro = new DALibro();
            try
            {
                return oDALibro.RetornarTiposLibros();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oDALibro = null;
            }
        }

        #endregion "Trae el Listado de Libros"
    }
}
