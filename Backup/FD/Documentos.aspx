<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Documentos.aspx.cs" Inherits="Documentos"
    MasterPageFile="~/DigiForm.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="contenedorForm">
        <asp:Panel ID="pnlDocumentos" runat="server" Width="100%">
            <table style="margin-top: 0px; margin-left: 45px;">
                <tr>
                    <td>
                        <center>
                            <h1>
                                DESCARGA DE FORMULARIOS DE RUBRICA
                            </h1>
                            <br />
                            <br />
                            <input class="bluebutton iebb" style="padding: 10px; font-size: 12pt;" type="button" 
                            value="(PDF) Formulario de solicitud rúbrica asociación civil"
                                onclick="window.open('Pdf/AnexoRubricaAsociacionCivil.pdf','_blank');" />
                            <br />
                            <br />
                            <br />
                            <br />
                            <input class="bluebutton iebb" style="padding: 10px; font-size: 12pt;" type="button"
                                value="(PDF) Formulario de solicitud rúbrica sociedades accionarias (SA y SAU)" 
                                onclick="window.open('Pdf/AnexoRubricaSocAccionaria.pdf','_blank');" />
                            <br />
                            <br />
                            <br />
                            <br />
                            <input class="bluebutton iebb" style="padding: 10px; font-size: 12pt;" type="button"
                                value="(PDF) Formulario de solicitud rúbrica sociedades no accionarias (SRL)" 
                                onclick="window.open('Pdf/AnexoRubricaSocNoAccionarias.pdf','_blank');" />
                            <br />
                            <br />
                            <br />
                            <br />
                            <asp:Button ID="BtnPrincipal" runat="server" Text="Volver a la Página Principal"
                                CssClass="bluebutton iebb" OnClick="BtnPrincipal_Click" />
                            <br />
                            <br />
                            <br />
                            <br />
                        </center>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
</asp:Content>
