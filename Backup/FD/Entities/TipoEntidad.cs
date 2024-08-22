using System;
using System.Collections.Generic;
using System.Text;

namespace FD.Entities
{
    public class TipoEntidad
    {
        public TipoEntidad()
        {
        }

        //Constructor para el WS
        public TipoEntidad(string descripcion)
        {
            this._descripcion = descripcion;
        }

        public TipoEntidad(int id, string descripcion)
        {
            this._id = id;
            this._descripcion = descripcion;
        }

        public TipoEntidad(int id, string descripcion, bool flagsocahorro, bool flagart299)
        {
            this._id = id;
            this._descripcion = descripcion;
            this._flagsocahorro = flagsocahorro;
            this._flagart299 = flagart299;
        }

        public TipoEntidad(int id)
        {
            this._id = id;
        }

        private int _id;

        public int Id
        {
            set { _id = value; }
            get { return _id; }
        }

        private string _descripcion;

        public string Descripcion
        {
            set { _descripcion = value; }
            get { return _descripcion; }
        }

        private bool _flagsocahorro;

        public bool FlagSocAhorro
        {
            set { _flagsocahorro = value; }
            get { return _flagsocahorro; }
        }

        private bool _flagart299;

        public bool FlagArt299
        {
            set { _flagart299 = value; }
            get { return _flagart299; }
        }

        public override string ToString()
        {
            return _descripcion;
        }
    }
}
