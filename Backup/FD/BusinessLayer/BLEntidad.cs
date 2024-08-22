using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using FD.DataAccessLayer;
using FD.Entities;


namespace FD.BusinessLayer
{
    public class BLEntidad
    {
        DAEntidad oDAEntidad;

        #region "GetSociedades Trae los datos de una Sociedades"
        public Entidad GetSociedad(Int32 NumCorrelativo, Int32 tipoEnt)
        {
            //sacar el using, ver la mejor manera para borrar los objetos
            //si no sale nada..... volver al try-catch-finally
            try
            {
                Entidad oEntidad;
                oDAEntidad = new DAEntidad();
                DataTable oDT = oDAEntidad.GetSociedad(NumCorrelativo, tipoEnt).Tables[0];

                long cuit = 0;

                if (oDT.Rows.Count != 0)
                {
                    //Llena objeto de Entidad con el CUIT
                    if (oDT.Rows[0].ItemArray[3] is DBNull) //CUIT o 0 Si es null
                        cuit = 0;
                    else
                        cuit = Convert.ToInt64(oDT.Rows[0].ItemArray[3]);

                    oEntidad = new Entidad(Convert.ToInt32(oDT.Rows[0].ItemArray[0]),
                                                    oDT.Rows[0].ItemArray[1].ToString(),
                        //new TipoEntidad(oDT.Rows[0].ItemArray[2].ToString()),
                                                    new TipoEntidad(tipoEnt, oDT.Rows[0].ItemArray[2].ToString()),
                                                    cuit);

                    return oEntidad;
                }
                else
                {
                    return oEntidad = null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion "GetSociedades Trae los datos de una Sociedades"

        #region "GetSociedades Trae los datos de las Sociedades"
        public List<Entidad> GetSociedades(string RazonSo, Int32 tipoEnt)
        {
            DAEntidad oDAEntidad = new DAEntidad();
            Entidad oEntidad;
            List<Entidad> lEntidad = new List<Entidad>();

            try
            {
                //convierto el DS en una lista tipada de entidades 
                //para despues llenar mas facil el gridView
                DataTable oDT = oDAEntidad.GetSociedades(RazonSo, tipoEnt).Tables[0];

                long cuit = 0;

                for (int i = 0; i < oDT.Rows.Count; i++)
                {
                    //Llena objeto de Entidad con el CUIT
                    if (oDT.Rows[i].ItemArray[3] is DBNull)  //CUIT o 0 Si es null
                        cuit = 0;
                    else
                        cuit = Convert.ToInt64(oDT.Rows[i].ItemArray[3]);

                    oEntidad = new Entidad(Convert.ToInt32(oDT.Rows[i].ItemArray[0]),
                                                       oDT.Rows[i].ItemArray[1].ToString(),
                        //new TipoEntidad(oDT.Rows[i].ItemArray[2].ToString()),
                                                       new TipoEntidad(Int32.Parse(oDT.Rows[i].ItemArray[4].ToString()), oDT.Rows[i].ItemArray[2].ToString()),
                                                       cuit);
                    lEntidad.Add(oEntidad);
                }
                return lEntidad;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oDAEntidad = null;
                oEntidad = null;
                lEntidad = null;
            }
        }
        #endregion "GetSociedades Trae los datos de las Sociedades"

        //TODO_NOCUIT: No se usa el pedido de CUIT
        #region "GetSociedad con Pedido de CUIT"
        //public Entidad GetSociedad(Int32 NumCorrelativo, Int32 tipoEnt) //
        //{
        //    //sacar el using, ver la mejor manera para borrar los objetos
        //    //si no sale nada..... volver al try-catch-finally
        //    try
        //    {
        //        Entidad oEntidad;
        //        oDAEntidad = new DAEntidad();
        //        DataTable oDT = oDAEntidad.GetSociedad(NumCorrelativo, tipoEnt).Tables[0];

        //        long cuit = 0;

        //        if (oDT.Rows.Count != 0)
        //        {
        //            //oEntidad = new Entidad(Convert.ToInt32(oDT.Rows[0].ItemArray[0]),
        //            //                                oDT.Rows[0].ItemArray[1].ToString(),
        //            //                                new TipoEntidad(oDT.Rows[0].ItemArray[2].ToString()));
        //            //TODO_CUIT: Lleno objeto de Entidad con el CUIT 06/10/16
        //            if (oDT.Rows[0].ItemArray[3] is DBNull) //CUIT o 0 Si es null
        //                cuit = 0;
        //            else
        //                cuit = Convert.ToInt64(oDT.Rows[0].ItemArray[3]);

        //            oEntidad = new Entidad(Convert.ToInt32(oDT.Rows[0].ItemArray[0]),
        //                                            oDT.Rows[0].ItemArray[1].ToString(),
        //                                            new TipoEntidad(oDT.Rows[0].ItemArray[2].ToString()),
        //                                            cuit,
        //                                            0,     //idTipoOpeCuit
        //                                            cuit); //CuitViejo

        //            return oEntidad;
        //        }
        //        else
        //        {
        //            return oEntidad = null;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        #endregion "GetSociedad con Pedido de CUIT"
        #region "GetSociedades con Pedido de CUIT"
        //public List<Entidad> GetSociedades(string RazonSo, Int32 tipoEnt)
        //{
        //    DAEntidad oDAEntidad = new DAEntidad();
        //    Entidad oEntidad;
        //    List<Entidad> lEntidad = new List<Entidad>();

        //    try
        //    {
        //        //convierto el DS en una lista tipada de entidades 
        //        //para despues llenar mas facil el gridView
        //        DataTable oDT = oDAEntidad.GetSociedades(RazonSo,tipoEnt).Tables[0];

        //        long cuit = 0;

        //        for (int i = 0; i < oDT.Rows.Count; i++)
        //        {
        //            //TODO_CUIT: Lleno objeto de Entidad con el CUIT 06/10/16
        //            if (oDT.Rows[i].ItemArray[3] is DBNull)  //CUIT o 0 Si es null
        //                cuit = 0;
        //            else
        //                cuit = Convert.ToInt64(oDT.Rows[i].ItemArray[3]);

        //            oEntidad = new Entidad(Convert.ToInt32(oDT.Rows[i].ItemArray[0]),
        //                                               oDT.Rows[i].ItemArray[1].ToString(),
        //                                               new TipoEntidad(oDT.Rows[i].ItemArray[2].ToString()),
        //                                               cuit,
        //                                               0, //idTipoOpeCuit
        //                                               cuit); //CuitViejo
        //            lEntidad.Add(oEntidad);
        //        }
        //        return lEntidad;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        oDAEntidad = null;
        //        oEntidad = null;
        //        lEntidad = null;
        //    }
        //}
        #endregion "GetSociedades con Pedido de CUIT"

        public Entidad GetSociedadSel(int nroCorr, TipoEntidad TipoEntidad)
        {
            try
            {
                Entidad oEntidad;
                DAEntidad oDAEntidad = new DAEntidad();
                DataTable oDT = oDAEntidad.GetSociedad(nroCorr, TipoEntidad.Id).Tables[0];

                long cuit = 0;

                if (oDT.Rows.Count != 0)
                {
                    //Llena objeto de Entidad con el CUIT
                    if (oDT.Rows[0].ItemArray[3] is DBNull) //CUIT o 0 Si es null
                        cuit = 0;
                    else
                        cuit = Convert.ToInt64(oDT.Rows[0].ItemArray[3]);

                    oEntidad = new Entidad(Convert.ToInt32(oDT.Rows[0].ItemArray[0]),
                                                    oDT.Rows[0].ItemArray[1].ToString(),
                                                    TipoEntidad,
                        //new TipoEntidad(oDT.Rows[i].ItemArray[2].ToString()),
                        //new TipoEntidad(idTipoEntidad, oDT.Rows[0].ItemArray[2].ToString()),
                                                    cuit);

                    return oEntidad;
                }
                else
                {
                    return oEntidad = null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ValidarEntidadconTramite(FD.Entities.Tramite oTramiteAux, FD.Entities.Entidad oEntidadAux, FD.Entities.Entidad oEntidadSelAux, FD.Entities.TipoEntidad oTipoEntidadAux, out string msg)
        {
            DAEntidad oDAEntidad = new DAEntidad();
            BLEntidad oBLEntidad = new BLEntidad();
            DATramite oDATramite = new DATramite();
            Entidad oEntidadWS = new Entidad();

            bool Constitucion = false;
            bool Resultado = false;
            msg = "ERROR al Validar la Entidad con el Tramite";

            try
            {
                //Valido si el Tipo de Tramite es de Constitución con el FlagNumCorrelativo
                Constitucion = oDATramite.RetornarTramiteConstitucion(oTramiteAux.Id);

                //Si el tramite no es de Constitución
                if (!Constitucion)
                {
                    //Valida que el Numero Correlativo sea igual al de la Entidad Seleccionada
                    if (oEntidadSelAux.NroCorrelativo == oEntidadAux.NroCorrelativo)
                    {
                        //Valida que el Tipo Societario sea igual al de la Entidad Seleccionada
                        if (oEntidadSelAux.TipoSociedad.Id == oEntidadAux.TipoSociedad.Id && oEntidadSelAux.TipoSociedad.Id == oTipoEntidadAux.Id)
                        {
                            //Trae los datos de la Entidad con el WS Zidane.TasasIGJ()
                            oEntidadWS = oBLEntidad.GetSociedadSel(oEntidadSelAux.NroCorrelativo, oEntidadSelAux.TipoSociedad);

                            //Valida que el Numero Correlativo de la Entidad Seleccionada se igual al de la Entidad en el WS
                            if (oEntidadSelAux.NroCorrelativo == oEntidadAux.NroCorrelativo)
                            {
                                //Valida que el Tipo Societario de la Entidad Seleccionada se igual al de la Entidad en el WS
                                if (oEntidadSelAux.TipoSociedad.Id == oEntidadAux.TipoSociedad.Id)
                                {
                                    //Valida si el Tipo societario esta asociado al Tramite
                                    bool ResultadoTramite = oDAEntidad.ValidarEntidadconTramite(oTramiteAux.Id, oEntidadSelAux.TipoSociedad.Id);

                                    if (!ResultadoTramite)
                                    {
                                        msg = "ERR03: El Tipo Societario de la Entidad Seleccionada no está asociado al Tipo de Trámite";
                                    }

                                    Resultado = ResultadoTramite;

                                }
                                else
                                {
                                    msg = "ERR05: El Tipo Societario de la Entidad Seleccionada no se corresponde al de nuestra Base de Datos";
                                }
                            }
                            else
                            {
                                msg = "ERR04: El Nro Correlativo de la Entidad Seleccionada no se corresponde al de nuestra Base de Datos";
                            }
                        }
                        else
                        {
                            msg = "ERR02: El Tipo Societario no se corresponde al de la Entidad Seleccionada";
                        }
                    }
                    else
                    {
                        msg = "ERR01: El Nro Correlativo no se corresponde al de la Entidad Seleccionada";
                    }
                }
                //Si el tramite es de Constitución
                else
                {
                    //Valida que el Tipo Societario sea igual al Seleccionado
                    if (oEntidadSelAux.TipoSociedad.Id == oEntidadAux.TipoSociedad.Id && oEntidadSelAux.TipoSociedad.Id == oTipoEntidadAux.Id)
                    {
                        //Valida si el Tipo societario esta asociado al Tramite
                        bool ResultadoTramite = oDAEntidad.ValidarEntidadconTramite(oTramiteAux.Id, oEntidadSelAux.TipoSociedad.Id);

                        if (!ResultadoTramite)
                        {
                            msg = "ERR03: El Tipo Societario de la Entidad Seleccionada no está asociado al Tipo de Trámite";
                        }

                        Resultado = ResultadoTramite;
                    }
                    else
                    {
                        msg = "ERR02: El Tipo Societario no se corresponde al de la Entidad Seleccionada";
                    }
                }

                if (!Resultado)
                {
                    Log.GrabarAdvertencia(msg, "ValidarEntidadconTramite", "LOCAL");
                }

                return Resultado;
            }
            catch (Exception ex)
            {
                Log.GrabarAdvertencia(msg, "ValidarEntidadconTramite", "LOCAL");
                throw ex;
            }
            finally
            {
                oDAEntidad = null;
                oBLEntidad = null;
                oDATramite = null;
                oEntidadWS = null;
            }
        }
    }
}