<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctrlTxtDNI.ascx.cs" Inherits="ctrlTxtDNI" %>
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
<asp:TextBox ID="txtDNI" runat="server" MaxLength="11" ToolTip="Número de documento"
    Width="100px" CssClass="txtbanelco"></asp:TextBox>

<cc1:FilteredTextBoxExtender ID="ValidaDNI" runat="server" FilterType="Numbers" TargetControlID="txtDNI">
</cc1:FilteredTextBoxExtender>
<asp:RegularExpressionValidator ID="valDNI" runat="server" EnableClientScript="False"
    ErrorMessage="Ingrese un DNI válido (7-11 dígitos)"
    ControlToValidate="txtDNI" Display="Dynamic" ValidationExpression="\d{7,11}" CssClass="banelco"></asp:RegularExpressionValidator>
<cc1:ValidatorCalloutExtender ID="msgvaldni" runat="server" TargetControlID="valDNI">
</cc1:ValidatorCalloutExtender>
<cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" WatermarkCssClass="textboxwatermark"
    runat="server" TargetControlID="txtDNI" WatermarkText="Ingrese aquí">
</cc1:TextBoxWatermarkExtender>
<asp:RequiredFieldValidator ID="RequiredFieldValidator1" EnableClientScript="False"
    runat="server" ErrorMessage="Ingrese el DNI" CssClass="banelco" ControlToValidate="txtDNI" Display="Dynamic"></asp:RequiredFieldValidator>
<cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" TargetControlID="RequiredFieldValidator1">
</cc1:ValidatorCalloutExtender>


