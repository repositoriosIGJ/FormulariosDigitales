using System;
using System.Collections.Generic;
using System.Text;

namespace FD.Entities
{
    public class Entidad
    {
        public Entidad()
        { }

        //Constructor para cuando se confecciona una sociedad y no tiene Numero correlativo
        public Entidad(string descripcion, TipoEntidad tipoEnt)
        {
            this._nombre = descripcion;
            this._tipoSociedad = tipoEnt;
            //Agrega Cuit si lo tiene            
        }

        //TODO_NOCUIT: No se usa el pedido de CUIT
        #region "Constructor para pedido de CUIT"
        //TODO_CUIT: Agrego constructor para el Cuit 06/10/16
        //Constructor con CUIT 
        //public Entidad(int numCorr, string desc, TipoEntidad tipoEnt, long Cuit, int IdTipoOpeCuit, long CuitViejo)
        //{
        //    this._nroCorrelativo = numCorr;
        //    this._nombre = desc;
        //    this._tipoSociedad = tipoEnt;
        //    this._Cuit = Cuit;
        //    this._IdTipoOpeCuit = IdTipoOpeCuit;
        //    this._CuitViejo = CuitViejo;
        //}
        #endregion "Constructor para pedido de CUIT"

        //Constructor Con Numero Correlativo
        public Entidad(int numCorr, string desc, TipoEntidad tipoEnt, long Cuit)
        {
            this._nroCorrelativo = numCorr;
            this._nombre = desc;
            this._tipoSociedad = tipoEnt;
            this._Cuit = Cuit;
        }

        //Constructor para Formulario de reserva
        public Entidad(string nombre)//denominacion1
        {
            this._nombre = nombre;
        }

        //constructor para pasar el id de la tipo de entidad
        public Entidad(TipoEntidad tipoentidad)
        {
            this._tipoSociedad = tipoentidad;
        }

        //TODO_CUIT: Agrego propiedad para el Cuit 06/10/16
        private long _Cuit;

        public long Cuit
        {
            get { return _Cuit; }
            set { _Cuit = value; }
        }

        //TODO_NOCUIT: No se usa el pedido de CUIT
        #region "Propiedades del Pedido de CUIT"
        ////CUIT anterior de la entidad traido de la base, sirve para imprimirlo si el usuario no lo rectifica
        //private long _CuitViejo;

        //public long CuitViejo
        //{
        //    get { return _CuitViejo; }
        //    set { _CuitViejo = value; }
        //}

        //TODO_CUIT: Agrego propiedad para el tipo de Operacion Cuit 29/10/16
        //3 estados: 0 = Sin Cambios / 1 = Alta / 2 = Actualizacion
        //private int _IdTipoOpeCuit;

        //public int IdTipoOpeCuit
        //{
        //    get { return _IdTipoOpeCuit; }
        //    set { _IdTipoOpeCuit = value; }
        //}
        #endregion "Propiedades del Pedido de CUIT"
        
        private int _nroCorrelativo;

        public int NroCorrelativo
        {
            get { return _nroCorrelativo; }
            set { _nroCorrelativo = value; }
        }

        private string _nombre;

        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        private TipoEntidad _tipoSociedad;

        public TipoEntidad TipoSociedad
        {
            set { _tipoSociedad = value; }
            get { return _tipoSociedad; }
        }
    }
}
