using System;
using System.Collections.Generic;
using System.Text;
using log4net;
using log4net.Config;


namespace FD.Entities
{
    public class Log
    {
        static ILog infoLog = log4net.LogManager.GetLogger("Log4Net.InfoLog");
        static ILog fatalLog = log4net.LogManager.GetLogger("Log4Net.FatalLog");
        static ILog warnLog = log4net.LogManager.GetLogger("Log4Net.WarnLog");

        public Log(string mensaje, string operacion)
        {
            this._mensaje = mensaje;
            this._operacion = operacion;
        }

        private string _mensaje;

        public string Mensaje
        {
            get { return _mensaje; }
            set { _mensaje = value; }
        }

        private string _operacion;

        public string Operacion
        {
            get { return _operacion; }
            set { _operacion = value; }
        }

        #region Metodos Log


        //INFO (metodo OK)
        public static void GrabarLog(string mensaje, string operacion)
        {
            log4net.MDC.Set("operacion", operacion);
            Log.infoLog.InfoFormat(mensaje);
        }

        //ADVERTENCIA (Problema en metodo)
        public static void GrabarAdvertencia(string mensaje, string operacion, string usuario )
        {
            log4net.MDC.Set("operacion", operacion);
            log4net.MDC.Set("user",usuario);
            Log.warnLog.WarnFormat(mensaje);
        }

        //FATAL (Archivo de texto)
        public static void GrabarExcepcion(string mensaje)
        {
            fatalLog.FatalFormat(mensaje);
        }
                
        #endregion
    }
}
