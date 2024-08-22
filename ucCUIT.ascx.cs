using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using FD.Entities;
using FD.BusinessLayer;


public partial class ucCUIT : System.Web.UI.UserControl
{
    public event EventHandler StatusUpdated;

    private void FunctionThatRaisesEvent()
    {        //Null check makes sure the main page is attached to the event
        if (this.StatusUpdated != null)
            this.StatusUpdated(new object(), new EventArgs());
    }

    public string CuitText
    {
        get { return txtCUIT.Text.Trim(); }
        set { txtCUIT.Text = value; }
    }

    public string NomEntAfipText
    {
        get { return txtNomEntAfip.Text.Trim(); }
        set { txtNomEntAfip.Text = value; }
    }

    public string TituloCuit
    {
        get { return lblTituloCuit.Text.Trim(); }
        set { lblTituloCuit.Text = value; }
    }

    public bool NoEncontrado
    {
        get { return lblNoEncontrado.Visible; }
        set { lblNoEncontrado.Visible = value; }
    }

    public bool Enable
    {
        get { return txtCUIT.Enabled; }
        set { txtCUIT.Enabled = value; }
    }

    //public bool rfvNomEntAfipVisible
    //{
    //    get { return rfvNomEntAfip.Visible; }
    //    set { rfvNomEntAfip.Visible = value; }
    //}   

    public void btnBuscarNomEntAfip_Click(object sender, ImageClickEventArgs e)
    {
        Page.Validate();

        //Reinicio los mensajes de error de la razon social
        lblNoEncontrado.Text = "";
        cvNomEntAfip.ErrorMessage = "";

        //Pregunto si el validador de campo cuit es correcto
        if (cvCUIT.IsValid)
        {
            //Antes de buscar en la AFIP pregunto si el cuit es valido y no esta vacio
            if (txtCUIT.Text.Trim() != "")
            {
                bool errorWS = false;
                long CUITaux = (this.txtCUIT.Text.Trim() == "No Registrado") ? 0 : Convert.ToInt64(this.txtCUIT.Text.Trim());

                //TODO_NOCUIT: No se usa el pedido de CUIT
                //Traigo los datos de la entidad provenientes de AFIP mediante el CUIT y Cargo el objeto PadronAfip
                //PadronAfip padron = BLPadronAfip.Cargar(CUITaux, out errorWS);
                PadronAfip padron = null;

                //Pregunto si no fallo el ws de afip
                if (!errorWS)
                {
                    this.lblNoEncontrado.Text = "No se encontro la razón social para el numero de CUIT ingresado";

                    //Pregunto si los datos de la entidad retornaron correctamente desde AFIP
                    if (padron.success == true)
                    {
                        if (padron.data.tipoClave == "CUIT" || padron.data.tipoClave == "CDI")
                        {
                            //Cargo el nombre de la entidad que proviene de AFIP en el TEXTBOX
                            this.txtNomEntAfip.Text = padron.data.nombre.ToString(); //Razón Social
                            this.lblNoEncontrado.Visible = false;
                            //Evento que muestra el btnConfirmar
                            FunctionThatRaisesEvent();
                        }
                    }
                    else
                    {
                        //Borro el nombre de la entidad
                        this.txtNomEntAfip.Text = "";
                        this.lblNoEncontrado.Visible = true;
                    }
                }
                else
                {
                    //TODO: Revisar el Manejo del fallo del ws del afip
                    this.txtNomEntAfip.Text = "";
                    this.lblNoEncontrado.Text = "Tenemos problemas para acceder a los datos en AFIP, reintente en otro momento";
                    this.lblNoEncontrado.Visible = true;
                }
            }
        }
    }

    public static bool ValidarCuit(string numerocuit, out string mensaje)
    {
        mensaje = "";
        int suma, longitud, valor1, valor2;

        suma = 0;

        string final;

        longitud = numerocuit.Length;

        string patron = "5432765432";

        if (longitud == 11)
        {
            if (numerocuit.Substring(0, 2).CompareTo("20") == 0 || numerocuit.Substring(0, 2).CompareTo("23") == 0 || numerocuit.Substring(0, 2).CompareTo("24") == 0 || numerocuit.Substring(0, 2).CompareTo("27") == 0 || numerocuit.Substring(0, 2).CompareTo("30") == 0 || numerocuit.Substring(0, 2).CompareTo("33") == 0 || numerocuit.Substring(0, 2).CompareTo("34") == 0)
            {
                final = numerocuit[10].ToString();

                for (int i = 0; i <= 9; i++)
                {
                    try
                    {
                        suma += int.Parse(patron[i].ToString()) * int.Parse(numerocuit[i].ToString());
                    }
                    catch
                    {
                        mensaje = "El Cuit no debe tener caracteres";
                        return false;
                    }
                }

                valor1 = suma % 11;
                valor2 = 11 - valor1;

                if (valor2 == 11)
                {
                    valor2 = 0;
                }

                else if (valor2 == 10)
                {
                    valor2 = 9;
                }

                if (String.Equals(Convert.ToString(valor2), final))
                    return true;
                else
                    //mensaje = " Fin del Cuit erroneo";
                    mensaje = "Cuit Erroneo";
                return false;
            }
            else
            {
                //mensaje = " Comienzo del Cuit erroneo";
                mensaje = "Cuit Erroneo";
                return false;
            }
        }
        else if (longitud == 0)
        {
            mensaje = "Ingrese el CUIT";
            return false;
        }
        else
        {
            mensaje = "Longitud del Cuit Erronea";
            return false;
        }
    }

    protected void cvCUIT_ServerValidate(object source, ServerValidateEventArgs args)
    {
        string mensaje = "";

        if (ValidarCuit(this.txtCUIT.Text.Trim(), out mensaje))
        {
            args.IsValid = true;
            //cvCUIT.IsValid = true;
        }
        else
        {
            args.IsValid = false;
            //cvCUIT.IsValid = false;
            cvCUIT.ErrorMessage = mensaje;
            txtNomEntAfip.Text = "";
        }
    }

    protected void cvNomEntAfip_ServerValidate(object source, ServerValidateEventArgs args)
    {
        cvNomEntAfip.ErrorMessage = "";

        if (this.txtNomEntAfip.Text.Trim() == "")
        {
            args.IsValid = false;
            if (lblNoEncontrado.Visible)
            {
                lblNoEncontrado.Text = "No se encontro la razón social para el numero de CUIT ingresado. Campo Obligatorio";
                cvNomEntAfip.ErrorMessage = "";
            }
            else
            {
                lblNoEncontrado.Text = "No se encontro la razón social para el numero de CUIT ingresado";
                cvNomEntAfip.ErrorMessage = ""; //Campo Obligatorio
            }
        }
        else
        {
            args.IsValid = true;
        }
    }
    protected void txtCUIT_TextChanged(object sender, EventArgs e)
    {
        this.txtNomEntAfip.Text = "";
    }
}
