<%@ Page Language="C#" MasterPageFile="~/DigiForm.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="Default" Title="Inspección General de Justicia - Formulario Digital" %>

<%@ Register Src="ctrlTxtDNI.ascx" TagName="ctrlTxtDNI" TagPrefix="uc1" %>
<%@ Register Src="ucDOC.ascx" TagName="ucDOC" TagPrefix="uc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="contenedorForm">
        <asp:Panel ID="pnlInfoInicial" runat="server" Width="100%">
            <table style="background-image: url('Imagenes/InfoInicial.jpg'); background-repeat: no-repeat;
                width: 600px; height: 256px; text-align: center; margin: -25px auto;">
                <tr>
                    <td style="display: block; padding-top: 280px; margin-bottom: -20px;">
                        <asp:Button CssClass="bluebutton" ID="btnInfoInicialOk" runat="server" Text="Continuar"
                            OnClick="btnInfoInicialOk_Click" />
                        <br />
                        <br />
                        <br />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <%--**************************FIRMANTE***************************--%>
        <asp:Panel ID="PnlInfoFirmante" runat="server" Visible="False">
            <asp:Panel ID="PanelTituloFirmante" runat="server">
                <div id="encabezadoDatosPersonales">
                </div>
            </asp:Panel>
            <asp:Panel ID="PanelCuerpoFirmante" runat="server">
                <div class="contenido">
                    <div class="contenidoInterno">
                        <div class="campo" style="margin-left: -50px;">
                            <div style="margin-top: 0px; margin-left: 100px;">
                                <%--<tr>
                                        <td colspan="2">                                            
                                            <center>
                                                <b>Complete los siguientes campos. Todos son obligatorios.</b>
                                            </center>
                                        </td>
                                    </tr>--%>
                                <asp:UpdatePanel ID="updCaracter" runat="server">
                                    <ContentTemplate>
                                        <br />
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblCaracter" runat="server" Text="Se Presenta en Carácter de" Width="160px"
                                                        CssClass="lblpresentante"></asp:Label>
                                                </td>
                                                <td class="rblcaracterpre">
                                                    <asp:RadioButtonList ID="rblCaracter" runat="server" RepeatDirection="Horizontal"
                                                        AutoPostBack="True" OnSelectedIndexChanged="rblCaracter_SelectedIndexChanged"
                                                        DataTextField="Descripcion" DataValueField="Codigo">
                                                        <asp:ListItem Value="RL" Text="Representante legal" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Value="PD" Text="Profesional dictaminante"></asp:ListItem>
                                                        <asp:ListItem Value="AT" Text="Autorizado a tramitar"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>
                                            <tr id="trAutorizado" runat="server" visible="false">
                                                <td>
                                                    <asp:Label ID="lblAutorizado" runat="server" Text="Autorizado en" Width="160px" CssClass="lblpresentante" style="display:block; margin-top:-18px"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtAutorizado" runat="server" Width="280px" MaxLength="35">
                                                    </asp:TextBox><br /><label style="margin-left: 2px; font-size: 10px; color: Green;">* Indicar
                                                        en qué documento figura dicha autorización</label><br style="margin-bottom:5px;" />
                                                    <asp:RequiredFieldValidator ID="rfvAutorizado" runat="server" ErrorMessage="Dato obligatorio"
                                                        Style="position: relative" ControlToValidate="txtAutorizado" Display="Dynamic"></asp:RequiredFieldValidator>&nbsp;
                                                    <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtAutorizado"
                                                        ID="revAutorizado" ValidationExpression="^[\s\S]{5,35}$" runat="server" ErrorMessage="Este campo requiere como Minimo 5 y Maximo 35 caracteres."></asp:RegularExpressionValidator>
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="rblCaracter" EventName="SelectedIndexChanged" />
                                    </Triggers>
                                </asp:UpdatePanel>
                                <table>
                                    <uc2:ucDOC ID="ucDOC" runat="server"></uc2:ucDOC>
                                    <asp:ImageButton ID="imOK" runat="server" ImageUrl="~/imagenes/OK.png" CausesValidation="False"
                                        ToolTip="Comprobar DNI" Visible="False"></asp:ImageButton><asp:ImageButton ID="imCancel"
                                            OnClick="imCancel_Click" runat="server" ImageUrl="~/imagenes/CANCEL.png" CausesValidation="False"
                                            ToolTip="Limpiar campos" Visible="False"></asp:ImageButton><asp:Label ID="lblDescTilde"
                                                runat="server" Style="position: relative" Text="Presione el tilde una vez ingresado el DNI"
                                                Visible="False"></asp:Label><%--<tr>
                                        <td>
                                            <asp:Label ID="lblTipoDoc" runat="server" Text="Tipo de Documento" Font-Strikeout="False"
                                                Width="160px" CssClass="lblpresentante"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlTipoDoc" runat="server">
                                                <asp:ListItem>DOCUMENTO NACIONAL DE IDENTIDAD</asp:ListItem>
                                                <asp:ListItem Value="CEDULA DE IDENTIDAD"></asp:ListItem>
                                                <asp:ListItem>LIBRETA CIVICA</asp:ListItem>
                                                <asp:ListItem>LIBRETA DE ENROLAMIENTO</asp:ListItem>
                                                <asp:ListItem>PASAPORTE</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>                                            
                                            <asp:Label ID="lblNroDoc" runat="server" Text="Nro. de Documento" Font-Strikeout="False"
                                                Width="160px" CssClass="lblpresentante"></asp:Label>
                                        </td>
                                        <td>
                                            <uc1:ctrlTxtDNI ID="CtrlTxtDNI1" runat="server"></uc1:ctrlTxtDNI>
                                            <asp:ImageButton ID="imOK" runat="server" ImageUrl="~/imagenes/OK.png" CausesValidation="False"
                                                ToolTip="Comprobar DNI" Visible="False"></asp:ImageButton>
                                            <asp:ImageButton ID="imCancel" OnClick="imCancel_Click" runat="server" ImageUrl="~/imagenes/CANCEL.png"
                                                CausesValidation="False" ToolTip="Limpiar campos" Visible="False"></asp:ImageButton>
                                            <asp:Label ID="lblDescTilde" runat="server" Style="position: relative" Text="Presione el tilde una vez ingresado el DNI"
                                                Visible="False"></asp:Label>
                                        </td>
                                    </tr>--%></table>
                                <asp:Panel ID="pnlDatosFirmante" runat="server">
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblNombre" runat="server" Text="Nombre" Width="160px" CssClass="lblpresentante"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtNombre" runat="server" Width="250px" ValidationGroup="1" MaxLength="60"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvNombre" runat="server" ErrorMessage="Dato obligatorio"
                                                    Style="position: relative" ControlToValidate="txtNombre" Display="Dynamic"></asp:RequiredFieldValidator>
                                                <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="Dato inválido"
                                                    Style="left: 0px; position: relative" ControlToValidate="txtNombre" Display="Dynamic"
                                                    OnServerValidate="EmptyAndStringFieldValidator" ValidateEmptyText="True"></asp:CustomValidator>
                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtNombre"
                                                    ValidChars="qQwWeErRtTyYuUiIoOpPaAsSdDfFgGhHjJkKlLñÑzZxXcCvVbBnNmM ">
                                                </cc1:FilteredTextBoxExtender>
                                                <asp:CustomValidator ID="cvNombre" runat="server" ControlToValidate="txtNombre" Display="Dynamic"
                                                    ErrorMessage="Error en el campo." OnServerValidate="cvNombre_ServerValidate"
                                                    Style="position: relative; left: 0px; top: 0px;" Font-Size="Smaller" EnableClientScript="False"></asp:CustomValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblApellido" runat="server" Text="Apellido" Width="160px" CssClass="lblpresentante"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtApellido" runat="server" Width="250px" ValidationGroup="1" MaxLength="100"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvApellido" runat="server" ErrorMessage="Dato obligatorio"
                                                    Style="position: relative" ControlToValidate="txtApellido" Display="Dynamic"></asp:RequiredFieldValidator>
                                                <asp:CustomValidator ID="CustomValidator2" runat="server" ControlToValidate="txtApellido"
                                                    Display="Dynamic" ErrorMessage="Dato inválido" OnServerValidate="EmptyAndStringFieldValidator"
                                                    Style="position: relative" ValidateEmptyText="True"></asp:CustomValidator>
                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtApellido"
                                                    ValidChars="qQwWeErRtTyYuUiIoOpPaAsSdDfFgGhHjJkKlLñÑzZxXcCvVbBnNmM ">
                                                </cc1:FilteredTextBoxExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblMail" runat="server" Text="eMail" Width="160px" CssClass="lblpresentante"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtMail" runat="server" Width="250px" ValidationGroup="1" MaxLength="100"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvMail" runat="server" ErrorMessage="Dato obligatorio"
                                                    Display="Dynamic" ControlToValidate="txtMail"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="revMail" runat="server" ErrorMessage="e-Mail no válido"
                                                    Style="position: relative" ControlToValidate="txtMail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                    Display="Dynamic"></asp:RegularExpressionValidator>
                                                <asp:CustomValidator ID="CustomValidator3" runat="server" ControlToValidate="txtMail"
                                                    Display="Dynamic" OnServerValidate="EMailValidator" Style="position: relative"
                                                    ValidateEmptyText="True"></asp:CustomValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblTelefono" runat="server" Text="Teléfono" Width="160px" CssClass="lblpresentante"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtTelefono" runat="server" Width="150px" ValidationGroup="1" MaxLength="20"></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtTelefono"
                                                    ValidChars="0123456789">
                                                </cc1:FilteredTextBoxExtender>
                                                <asp:RegularExpressionValidator ID="valTelefono" runat="server" EnableClientScript="False"
                                                    ErrorMessage="Ingrese correctamente el número de Teléfono (máx. 20 dígitos)"
                                                    ControlToValidate="txtTelefono" Display="Dynamic" ValidationExpression="\d{0,20}"></asp:RegularExpressionValidator>
                                            </td>
                                        </tr>
                                    </table>
                                    <div class="botonesPieSeccion">
                                        <asp:Button ID="btnConfirmarFirmante" runat="server" CssClass="bluebutton" Text="Confirmar"
                                            OnClick="btnConfirmar_Click" Style="position: relative; margin-bottom: 10px;" />
                                    </div>
                                    <asp:Label ID="lblDetalleMail" runat="server" Style="left: 1px; position: relative"
                                        Width="350px"></asp:Label>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                    <div class="contenidoBottom">
                    </div>
                </div>
            </asp:Panel>
        </asp:Panel>
    </div>
</asp:Content>
