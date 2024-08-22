<%@ Page Language="C#" MasterPageFile="~/DigiForm.master" AutoEventWireup="true"
    CodeFile="Anexo.aspx.cs" Inherits="Anexo" Title="Inspección General de Justicia - Formulario Digital" %>

<%@ Register Src="ucLIB.ascx" TagName="ucLIB" TagPrefix="uc1" %>
<%--enableEventValidation="false"--%>
<%@ Register Assembly="MSCaptcha" Namespace="MSCaptcha" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--    <script language="javascript" type="text/javascript">
        function ValidarCaracteres(textareaControl,maxlength)
        {
            if (textareaControl.value.length > maxlength)
            {
                textareaControl.value = textareaControl.value.substring(0,maxlength);
                alert("Debe ingresar hasta un maximo de "+maxlength+" caracteres");
            }
        }
    </script>--%>
    <div id="contenedorForm">
        <asp:Panel ID="PnlDataContainer" runat="server">
            <asp:Panel ID="PanelTituloAnexo" runat="server">
                <div id="encabezadoDatosAnexo">
                    <asp:Image ID="imgAnexoHeader" runat="server" ImageUrl="Imagenes/datosRubrica.jpg" />
                </div>
                <div class="contenido anexos">
                    <div>
                        <br />
                        <table style="width: 80%; margin: 0px auto; border: none;">
                            <tr>
                                <td colspan="2" class="tituloanexo">
                                    <asp:Label runat="server" ID="lblTituloAnexo" Text="" Width="100%" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" class="tituloformulario">
                                    <asp:Label runat="server" ID="lblTituloFormulario" Text="" Width="100%" />
                                </td>
                            </tr>
                        </table>
                        <br />
                        <asp:Panel runat="server" ID="pnlLibOb" Visible="false">
                            <table class="tablalibro">
                                <tr>
                                    <td colspan="2" class="titulolibro">
                                        <asp:Label runat="server" ID="lblLibOb" Text="Libros Obligatorios" />
                                    </td>
                                </tr>
                            </table>
                            <uc1:ucLIB ID="UcLIBOB1" runat="server" Visible="false" />
                            <uc1:ucLIB ID="UcLIBOB2" runat="server" Visible="false" />
                            <table id="tablachkAnexo1" class="tablalibro" runat="server">
                                <tr>
                                    <td colspan="2" class="filachk">
                                        <asp:CheckBox ID="chkAnexo1" CssClass="chk" runat="server" Text="Marque aquí para rubricar 'por separado' el libro actas reuniones de gerentes y socios"
                                            AutoPostBack="True" OnCheckedChanged="chkAnexo1_CheckedChanged" />
                                    </td>
                                </tr>
                            </table>
                            <uc1:ucLIB ID="UcLIBOB3" runat="server" Visible="false" />
                            <uc1:ucLIB ID="UcLIBOB4" runat="server" Visible="false" />
                            <table id="tablachkAnexo2" class="tablalibro" runat="server">
                                <tr>
                                    <td colspan="2" class="filachk">
                                        <asp:CheckBox ID="chkAnexo2" CssClass="chk" runat="server" Text="Marque aquí para rubricar 'juntos' los libros actas de reuniones de asambleas y directorio"
                                            AutoPostBack="True" OnCheckedChanged="chkAnexo2_CheckedChanged" />
                                    </td>
                                </tr>
                            </table>
                            <uc1:ucLIB ID="UcLIBOB5" runat="server" Visible="false" />
                            <uc1:ucLIB ID="UcLIBOB6" runat="server" Visible="false" />
                            <uc1:ucLIB ID="UcLIBOB7" runat="server" Visible="false" />
                        </asp:Panel>
                        <br />
                        <br />
                        <asp:Panel runat="server" ID="pnlLibOp" Visible="false">
                            <table class="tablalibro">
                                <tr>
                                    <td colspan="2" class="titulolibro">
                                        <asp:Label runat="server" ID="lblLibOp" Text="Libros Opcionales" />
                                    </td>
                                </tr>
                            </table>
                            <uc1:ucLIB ID="UcLIBOP1" runat="server" Visible="false" />
                            <uc1:ucLIB ID="UcLIBOP2" runat="server" Visible="false" />
                        </asp:Panel>
                        <br />
                        <br />
                    </div>
                </div>
            </asp:Panel>
            <div class="botonesPieSeccion">
                <asp:Button ID="btnPrincipal" runat="server" CssClass="bluebutton" Text="Volver a la Página Principal"
                    OnClick="btnPrincipal_Click" />&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnConfirmarAnexo" runat="server" CssClass="bluebutton" Text="Confirmar"
                    OnClick="btnConfirmar_Click" Visible="true" />&nbsp;
            </div>
        </asp:Panel>
    </div>
</asp:Content>
