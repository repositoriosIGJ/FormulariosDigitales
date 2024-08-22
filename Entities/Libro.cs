using System;
using System.Collections.Generic;
using System.Text;

namespace FD.Entities
{
    public class Libro
    {
        #region "Propiedades del Libro"

        private int _LibroObOp;

        //2 estados: 0 = Obligatorio / 1 = Opcional
        public int LibroObOp
        {
            get { return _LibroObOp; }
            set { _LibroObOp = value; }
        }

        //Id del Tipo de Libro (Crear Tabla o Enum con los Tipos de Libros)
        private int _IdTipoLibro;

        public int IdTipoLibro
        {
            get { return _IdTipoLibro; }
            set { _IdTipoLibro = value; }
        }

        //Descripcion del Tipo de Libro (Crear Tabla o Enum con los Tipos de Libros)
        private string _TipoLibro;

        public string TipoLibro
        {
            get { return _TipoLibro; }
            set { _TipoLibro = value; }
        }

        //"SI" o "NO"
        private string _Copiador;

        public string Copiador
        {
            get { return _Copiador; }
            set { _Copiador = value; }
        }

        private long? _Paginas;

        public long? Paginas
        {
            get { return _Paginas; }
            set { _Paginas = value; }
        }

        private long? _Fojas;

        public long? Fojas
        {
            get { return _Fojas; }
            set { _Fojas = value; }
        }
        #endregion "Propiedades del Libro"


        //Constructor
        public Libro(int p_libroobop, int p_idtipolibro, string p_tipolibro, string p_copiador, long? p_paginas, long? p_fojas)
        {
            this._LibroObOp = p_libroobop;
            this._IdTipoLibro = p_idtipolibro;
            this._TipoLibro = p_tipolibro;
            this._Copiador = p_copiador;
            this._Paginas = p_paginas;
            this._Fojas = p_fojas;
        }
    }
}
