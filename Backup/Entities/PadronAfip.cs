using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
//using System.Threading.Tasks;

namespace FD.Entities
{
    public class PadronAfip
    {
        private bool _success;

        public bool success
        {
            get { return _success; }
            set { _success = value; }
        }

        private Data _data;

        public Data data
        {
            get { return _data; }
            set { _data = value; }
        }
    }

    public class Data
    {
        private long _idPersona;

        public long idPersona
        {
            get { return _idPersona; }
            set { _idPersona = value; }
        }

        private string _tipoPersona;

        public string tipoPersona
        {
            get { return _tipoPersona; }
            set { _tipoPersona = value; }
        }

        private string _tipoClave;

        public string tipoClave
        {
            get { return _tipoClave; }
            set { _tipoClave = value; }
        }

        private string _estadoClave;

        public string estadoClave
        {
            get { return _estadoClave; }
            set { _estadoClave = value; }
        }

        private string _nombre;

        public string nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        //Cambio para la V2
        private DomicilioFiscal _domicilioFiscal;

        public DomicilioFiscal domicilioFiscal
        {
            get { return _domicilioFiscal; }
            set { _domicilioFiscal = value; }
        }

        private int _idDependecia;

        public int idDependecia
        {
            get { return _idDependecia; }
            set { _idDependecia = value; }
        }

        private int _mesCierre;

        public int mesCierre
        {
            get { return _mesCierre; }
            set { _mesCierre = value; }
        }
        //Cambio para la V2
        private string _fechaInscripcion;

        public string fechaInscripcion
        {
            get { return _fechaInscripcion; }
            set { _fechaInscripcion = value; }
        }
        //Cambio para la V2
        private string _fechaContratoSocial;

        public string fechaContratoSocial
        {
            get { return _fechaContratoSocial; }
            set { _fechaContratoSocial = value; }
        }        	

        private int[] _impuestos;

        public int[] impuestos
        {
            get { return _impuestos; }
            set { _impuestos = value; }
        }
        //Cambio para la V2
        private int[] _actividades;

        public int[] actividades
        {
            get { return _actividades; }
            set { _actividades = value; }
        }	

        private int[] _caracterizaciones;

        public int[] caracterizaciones
        {
            get { return _caracterizaciones; }
            set { _caracterizaciones = value; }
        }
     }

    public class Impuesto
    {
        private int _idImpuesto;

        public int idImpuesto
        {
            get { return _idImpuesto; }
            set { _idImpuesto = value; }
        }
    }

    public class Caracterizaciones
    {
        private int _idCaracterizacion;

        public int idCaracterizacion
        {
            get { return _idCaracterizacion; }
            set { _idCaracterizacion = value; }
        }
    }

    //Cambio para la V2
    public class DomicilioFiscal
    {
        private string _direccion;

        public string direccion
        {
            get { return _direccion; }
            set { _direccion = value; }
        }

        private string _localidad;

        public string localidad
        {
            get { return _localidad; }
            set { _localidad = value; }
        }

        private string _codPostal;

        public string codPostal
        {
            get { return _codPostal; }
            set { _codPostal = value; }
        }

        private int _idProvincia;

        public int idProvincia
        {
            get { return _idProvincia; }
            set { _idProvincia = value; }
        }
    }
}