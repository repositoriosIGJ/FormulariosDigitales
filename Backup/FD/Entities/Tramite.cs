using System;
using System.Collections.Generic;
using System.Text;

namespace FD.Entities
{
    public class Tramite
    {
        public Tramite()
        { }
        //Constructor para cargar datos de reporte
        public Tramite(string descripcion, string flarUrg)
        {
            this._descripcion = descripcion;
            if (flarUrg == "NO")
            {
                this._flagUrg = false;    
            }
            else
            {
                this._flagUrg = true;    
            }
        }

        //Constructor para REPORTE RESERVA
        public Tramite(string descripcion)
        {
            this._descripcion = descripcion;
        }

        public Tramite(int id, string descripcion)
        {
            this._id = id;
            this._descripcion = descripcion;
        }

        public Tramite(int id,string descripcion, bool flarUrg)
        {
            this._id = id;
            this._descripcion = descripcion;
            this._flagUrg = flarUrg;
        }

        public Tramite(int id, string descripcion, bool flagNumCorr, bool flarUrg)
        {
            this._id = id;
            this._descripcion = descripcion;
            this._flagNumCorr = flagNumCorr;
            this._flagUrg = flarUrg;            
        }

        //TODO_CUIT: Nuevo Constructor para traer si se pide cuit o no
        //public Tramite(int id, string descripcion, bool flarUrg, bool flagCuit)
        //{
        //    this._id = id;
        //    this._descripcion = descripcion;
        //    this._flagUrg = flarUrg;
        //    this._flagCuit = flagCuit;
        //}

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

        private bool _flagUrg;

        public bool FlagUrgente
        {
            get { return _flagUrg; }
            set { _flagUrg = value; }
        }

        private bool _flagNumCorr;

        public bool FlagNumeroCorrelativo
        {
            get { return _flagNumCorr; }
            set { _flagNumCorr = value; }
        }

        private bool _flagCuit;

        public bool flagCuit
        {
            get { return _flagCuit; }
            set { _flagCuit = value; }
        }

        //TODO_RESERVA: Atributo del Flag de Reserva
        //private bool _flagReserva;

        //public bool FlagReserva
        //{
        //    get { return _flagReserva; }
        //    set { _flagReserva = value; }
        //}
    }
}
