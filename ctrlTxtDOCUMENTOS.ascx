<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctrlTxtDOCUMENTOS.ascx.cs" Inherits="ctrlTxtDOCUMENTOS" %>
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
<asp:TextBox ID="txtDOCUMENTOS" runat="server" MaxLength="11" ToolTip="Número de documento" Width="100px" CssClass="txtbanelco"></asp:TextBox>
<%--<cc1:FilteredTextBoxExtender ID="ValidaDOCUMENTOS" runat="server" FilterType=Numbers TargetControlID="txtDOCUMENTOS">
</cc1:FilteredTextBoxExtender>--%>
<asp:RegularExpressionValidator ID="valDOCUMENTOS" runat="server" EnableClientScript="False"
    ErrorMessage="Ingrese un documento válido (7-11 dígitos)"
    ControlToValidate="txtDOCUMENTOS" Display="Dynamic" ValidationExpression="\w{7,11}" CssClass="banelco"></asp:RegularExpressionValidator>
<cc1:ValidatorCalloutExtender ID="msgvalDOCUMENTOS" runat="server" TargetControlID="valDOCUMENTOS">
</cc1:ValidatorCalloutExtender>
<cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtenderDOCUMENTOS" WatermarkCssClass="textboxwatermark"
    runat="server" TargetControlID="txtDOCUMENTOS" WatermarkText="Ingrese aquí">
</cc1:TextBoxWatermarkExtender>
<asp:RequiredFieldValidator ID="RFVDocumentos" EnableClientScript="False"
    runat="server" ErrorMessage="Ingrese el Documento" CssClass="banelco" ControlToValidate="txtDOCUMENTOS" Display="Dynamic"></asp:RequiredFieldValidator>
<cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" TargetControlID="RFVDocumentos">
</cc1:ValidatorCalloutExtender>


