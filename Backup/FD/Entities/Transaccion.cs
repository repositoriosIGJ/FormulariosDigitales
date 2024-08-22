using System;
using System.Collections.Generic;
using System.Text;

namespace FD.Entities
{
    public class Transaccion
    {
        //Constructor para armado de reporte.
        public Transaccion(Formulario formulario,
                           Firmante firmante,
                           Entidad entidad,
                           Tramite tramite,
                           string motivo,
                           DateTime fechaAlta)
        {
            this._formulario = formulario;
            this._firmante = firmante;
            this._entidad = entidad;
            this._tramite = tramite;
            this._motivo = motivo;
            this._fechaAlta = fechaAlta;
        }

        //constructor para persistir
        public Transaccion(Estado estado,
                           Formulario formulario,
                           Firmante firmante,
                           Entidad entidad,
                           Tramite tramite,
                           string motivo)
        {
            this._estado = estado;
            this._formulario = formulario;
            this._firmante = firmante;
            this._entidad = entidad;
            this._tramite = tramite;
            this._motivo = motivo;
        }

        ////Constructor para armado de reporte (CON LISTA DE LIBROS DE RUBRICA)
        //public Transaccion(Formulario formulario,
        //                   Firmante firmante,
        //                   Entidad entidad,
        //                   Tramite tramite,
        //                   List<Libro> lstlibros,
        //                   string motivo,
        //                   DateTime fechaAlta)
        //{
        //    this._formulario = formulario;
        //    this._firmante = firmante;
        //    this._entidad = entidad;
        //    this._tramite = tramite;
        //    this._lstlibros = lstlibros;
        //    this._motivo = motivo;
        //    this._fechaAlta = fechaAlta;
        //}

        ////constructor para persistir (CON LISTA DE LIBROS DE RUBRICA)
        //public Transaccion(Estado estado,
        //                   Formulario formulario,
        //                   Firmante firmante,
        //                   Entidad entidad,
        //                   Tramite tramite,
        //                   List<Libro> lstlibros,
        //                   string motivo)
        //{
        //    this._estado = estado;
        //    this._formulario = formulario;
        //    this._firmante = firmante;
        //    this._entidad = entidad;
        //    this._tramite = tramite;
        //    this._lstlibros = lstlibros;
        //    this._motivo = motivo;
        //}

        //constructor para formulario de reserva PARA PERSISTIR.(Sin Entidad / Con Denominacion1)
        public Transaccion(Estado estado,
                           Formulario formulario,
                           Firmante firmante,
                           Tramite tramite,
                           Entidad entidadTipo,
                           string denominacion1)
        {
            this._estado = estado;
            this._formulario = formulario;
            this._firmante = firmante;
            this._tramite = tramite;
            this._entidad = entidadTipo; //de aca saco el tipo de entidad.
            this._denominacion1 = denominacion1;
        }

        //Constructor para armado de REPORTE de formulario de reserva
        public Transaccion(Formulario formulario,
                           Firmante firmante,
                           Tramite tramite,
                           string denominacion1,
                           DateTime fechaAlta)
        {
            this._formulario = formulario;
            this._firmante = firmante;
            this._tramite = tramite;
            this._denominacion1 = denominacion1;
            this._fechaAlta = fechaAlta;
        }

        //TODO: Agrego la propiedad del codigo de referencia de pago para el codigo de banelco
        private string _codrefpago;

        public string CodRefPago
        {
            get { return _codrefpago; }
            set { _codrefpago = value; }
        }

        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private Estado _estado;

        public Estado Estado
        {
            get { return _estado; }
            set { _estado = value; }
        }

        private Formulario _formulario;

        public Formulario Formulario
        {
            get { return _formulario; }
            set { _formulario = value; }
        }

        private Firmante _firmante;

        public Firmante Firmante
        {
            get { return _firmante; }
            set { _firmante = value; }
        }

        private Entidad _entidad;

        public Entidad Entidad
        {
            get { return _entidad; }
            set { _entidad = value; }
        }

        private Tramite _tramite;

        public Tramite Tramite
        {
            get { return _tramite; }
            set { _tramite = value; }
        }

        //private List<Libro> _lstlibros;

        //public List<Libro> lstLibros
        //{
        //    get { return _lstlibros; }
        //    set { _lstlibros = value; }
        //}

        private string _denominacion1;

        public string Denominacion
        {
            get { return _denominacion1; }
            set { _denominacion1 = value; }
        }

        private string _motivo;

        public string Motivo
        {
            get { return _motivo; }
            set { _motivo = value; }
        }

        private DateTime _fechaAlta;

        public DateTime FechaAlta
        {
            get { return _fechaAlta; }
            set { _fechaAlta = value; }
        }

        private DateTime _fechaIngreso;

        public DateTime FechaIngreso
        {
            get { return _fechaIngreso; }
            set { _fechaIngreso = value; }
        }

        private byte[] _imagenCodBarra;

        public byte[] ImagenCodBarra
        {
            get { return _imagenCodBarra; }
            set { _imagenCodBarra = value; }
        }
    }
}
