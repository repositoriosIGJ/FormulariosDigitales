using System;
using System.Collections.Generic;
using System.Text;

using FD.DataAccessLayer;
using FD.Entities;

namespace FD.BusinessLayer
{
    public class BLTipoEntidad
    {
        public void RetornarTiposEntidad(List<TipoEntidad> lTipoEntidad)
        {
            try
            {
                DATipoEntidad oDLTipoEntidad = new DATipoEntidad();
                //Retorna todos los Tipos de Entidad
                oDLTipoEntidad.retornatTiposSociedad(lTipoEntidad);
                oDLTipoEntidad = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
       
        //public void RetornarTiposEntidadPorGrupo(List<TipoEntidad> lTipoSoc, string grupoTipoEnt)
        //{
        //    try
        //    {
        //        bool lFlagSocAhorro = false; //"Sociedad de Ahorro" = 1
        //        bool lFlagArt299 = false;    //"Sociedad Comprendida en el Art. 299 (Controlada)" = 2
        //        //bool lFlagNoContro = false;  //"Sociedad No Controlada" = 3

        //        switch (grupoTipoEnt)
        //        {
        //            case "1":
        //                lFlagSocAhorro = true;
        //                lFlagArt299 = false;
        //                //lFlagNoContro = false;
        //                break;
        //            case "2":
        //                lFlagSocAhorro = false;
        //                lFlagArt299 = true;
        //                //lFlagNoContro = false;
        //                break;
        //            //case "3": //"Sociedad No Controlada" = 3
        //            //    lFlagSocAhorro = false;
        //            //    lFlagArt299 = false;
        //            //    lFlagNoContro = true;
        //            //    break;
        //            default:
        //                lFlagSocAhorro = false;
        //                lFlagArt299 = false;
        //                //lFlagNoContro = false;
        //                break;
        //        }

        //        DATipoEntidad oDLTipoEntidad = new DATipoEntidad();
        //        //Retorna todos los Tipos de Entidad
        //        //oDLTipoEntidad.RetornarTiposEntidad(lTipoSoc);
        //        //Retorna todos los Tipos de Entidad por Grupo de Tipo de Entidad
        //        oDLTipoEntidad.RetornarTiposEntidadPorGrupo(lTipoSoc, lFlagSocAhorro, lFlagArt299);
        //        oDLTipoEntidad = null;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public string getTipoEntidadXIdTramite(int IdTipoEnt)
        {
            DATipoEntidad oDLTipoEntidad;
            try
            {
                oDLTipoEntidad = new DATipoEntidad();
                return oDLTipoEntidad.getTipoEntidadXIdTramite(IdTipoEnt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oDLTipoEntidad = null;
            }
        }
    }
}
