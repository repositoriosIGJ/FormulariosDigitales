using System;
using System.Collections.Generic;
using System.Text;

namespace FD.Entities
{
    public class TipoLibro
    {
        #region "Propiedades del Tipo de Libro"

        //Id del Tipo de Libro
        private int _IdTipoLibro;

        public int IdTipoLibro
        {
            get { return _IdTipoLibro; }
            set { _IdTipoLibro = value; }
        }

        //Descripcion del Tipo de Libro
        private string _TipodeLibro;

        public string TipodeLibro
        {
            get { return _TipodeLibro; }
            set { _TipodeLibro = value; }
        }

        private bool _Obligatorio;

        public bool Obligatorio
        {
            get { return _Obligatorio; }
            set { _Obligatorio = value; }
        }

        private bool _Habilitado;

        public bool Habilitado
        {
            get { return _Habilitado; }
            set { _Habilitado = value; }
        }

        #endregion "Propiedades del Tipo de Libro"

        //Constructor
        public TipoLibro()
        {
        }

        //Constructor
        public TipoLibro(int p_idtipolibro, string p_tipodelibro, bool p_obligatorio, bool p_habilitado)
        {
            this._IdTipoLibro = p_idtipolibro;
            this._TipodeLibro = p_tipodelibro;
            this._Obligatorio = p_obligatorio;
            this._Habilitado = p_habilitado;
        }
    }
}
