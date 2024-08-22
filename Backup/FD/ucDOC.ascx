<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucDOC.ascx.cs" Inherits="ucDOC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<style type="text/css">
    .textboxwatermark
    {
	    border-right: #bfcddb 1px solid;
	    border-top: #bfcddb 1px solid;
	    border-left: #bfcddb 1px solid;
	    color: #a9a9a9;
	    border-bottom: lightsteelblue 1px solid;
	    font-family: Calibri,Arial,Verdana;
	    background-color: #f5f5f5;
	    font-size:small;
    }
</style>
<tr>
    <td>
        <asp:Label ID="lblTipoDoc" runat="server" Text="Tipo de Documento" Font-Strikeout="False"
            Width="160px" CssClass="lblpresentante"></asp:Label>
    </td>
    <td>
        <asp:DropDownList ID="ddlTipoDoc" runat="server" Width="285px" DataTextField="Descripcion" DataValueField="Codigo" AutoPostBack="false">
            <asp:ListItem Value="DNI" Text="DOCUMENTO NACIONAL DE IDENTIDAD" Selected="True"></asp:ListItem>
            <asp:ListItem Value="CDI" Text="CEDULA DE IDENTIDAD"></asp:ListItem>
            <asp:ListItem Value="LIC" Text="LIBRETA CIVICA"></asp:ListItem>
            <asp:ListItem Value="LIE" Text="LIBRETA DE ENROLAMIENTO"></asp:ListItem>
            <asp:ListItem Value="PAS" Text="PASAPORTE"></asp:ListItem>
        </asp:DropDownList>
    </td>
</tr>
<tr>
    <td>
        <asp:Label ID="lblNroDoc" runat="server" Text="Nro. de Documento" Font-Strikeout="False"
            Width="160px" CssClass="lblpresentante"></asp:Label>
    </td>
    <td>
        <asp:TextBox ID="txtDNI" runat="server" MaxLength="11" ToolTip="Número de documento"
            Width="250px" CssClass="txtbanelco"></asp:TextBox>
        <cc1:FilteredTextBoxExtender ID="ValidaDNI" runat="server" FilterType="Numbers" TargetControlID="txtDNI">
        </cc1:FilteredTextBoxExtender>
        <asp:RegularExpressionValidator ID="valDNI" runat="server" EnableClientScript="False"
            ErrorMessage="Ingrese un DNI válido (7-11 dígitos)" ControlToValidate="txtDNI"
            Display="Dynamic" ValidationExpression="\d{7,11}" CssClass="banelco"></asp:RegularExpressionValidator>
        <cc1:ValidatorCalloutExtender ID="msgvaldni" runat="server" TargetControlID="valDNI">
        </cc1:ValidatorCalloutExtender>
        <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" WatermarkCssClass="textboxwatermark"
            runat="server" TargetControlID="txtDNI" WatermarkText="Ingrese aquí">
        </cc1:TextBoxWatermarkExtender>
        <asp:RequiredFieldValidator ID="rfvDNI" EnableClientScript="False"
            runat="server" ErrorMessage="Ingrese el DNI" CssClass="banelco" ControlToValidate="txtDNI"
            Display="Dynamic"></asp:RequiredFieldValidator>
        <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" TargetControlID="rfvDNI">
        </cc1:ValidatorCalloutExtender>
        <%--
        <asp:ImageButton ID="imOK" runat="server" ImageUrl="~/imagenes/OK.png" CausesValidation="False"
            ToolTip="Comprobar DNI" Visible="False"></asp:ImageButton>
        <asp:ImageButton ID="imCancel" OnClick="imCancel_Click" runat="server" ImageUrl="~/imagenes/CANCEL.png"
            CausesValidation="False" ToolTip="Limpiar campos" Visible="False"></asp:ImageButton>
        <asp:Label ID="lblDescTilde" runat="server" Style="position: relative" Text="Presione el tilde una vez ingresado el DNI"
            Visible="False"></asp:Label>
            --%>
    </td>
</tr>
