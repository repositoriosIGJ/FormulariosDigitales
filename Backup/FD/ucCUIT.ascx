<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucCUIT.ascx.cs" Inherits="ucCUIT" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
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
<asp:UpdatePanel ID="updpnlUCCUIT" runat="server">
    <ContentTemplate>
        <table style="margin: 0px auto; margin-top: 2px;">
            <tr>
                <td colspan="2">
                    <asp:Label runat="server" ID="lblTituloCuit" Text="POR FAVOR INGRESE EL NRO. DE CUIT DE LA ENTIDAD"
                        Font-Bold="True" Font-Size="16px" Style="display: block; text-align: center;
                        margin-left: 20px; width: auto; padding-bottom: 15px; padding-top: 5px;" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblCUIT" runat="server" Style="position: relative; text-align: right;
                        margin-left: 55px; margin-bottom: 28px;" Text="Nro. CUIT" Width="80px"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtCUIT" runat="server" MaxLength="11" ToolTip="Número de CUIT"
                        CssClass="txtbanelco" Style="font-size: 16px; letter-spacing: 4px; padding-left: 18px;
                        padding-bottom: 4px; width: 160px;" OnTextChanged="txtCUIT_TextChanged"></asp:TextBox>
                    <asp:ImageButton ID="btnBuscarNomEntAfip" runat="server" ImageUrl="Imagenes/Buscar.png"
                        ToolTip="Buscar en AFIP" ValidationGroup="1" OnClick="btnBuscarNomEntAfip_Click" style="display: inline-block; padding-right: 10px; margin-bottom: -6px;">
                    </asp:ImageButton>
                    <asp:CustomValidator ID="cvCUIT" runat="server" ErrorMessage="CustomValidator" OnServerValidate="cvCUIT_ServerValidate"></asp:CustomValidator>
                    <%--<asp:RequiredFieldValidator ID="rfvCUIT" EnableClientScript="False" runat="server"
                        ErrorMessage="Ingrese el CUIT" CssClass="banelco" ControlToValidate="txtCUIT"
                        Display="Dynamic"></asp:RequiredFieldValidator>--%>                   
                    <cc2:FilteredTextBoxExtender ID="ValidaCUIT" runat="server" FilterType="Numbers"
                        TargetControlID="txtCUIT">
                    </cc2:FilteredTextBoxExtender>
                    <!-- TODO_CUIT: Revisar Validacion del CUIT -->
                    <%--<cc2:ValidatorCalloutExtender ID="msgvalCUIT" runat="server" TargetControlID="valCUIT">
                    </cc2:ValidatorCalloutExtender>--%>
                    <%--<cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" TargetControlID="RequiredFieldValidator1">
                    </cc2:ValidatorCalloutExtender>--%>
                    <br />
                    <asp:Label ID="lblLeyendaBusquedaCuit" runat="server" Font-Size="9px" ForeColor="#404040"
                        Text="Ingrese el número sin guiones y luego haga un click<br/>sobre la lupa para buscar su razón social en afip.<br/>&nbsp&nbsp"
                        Visible="true">
                    </asp:Label>
                </td>
            </tr>
            <!-- Boton para Buscar Denominacion de la Entidad en AFIP -->
            <tr>
                <td>
                    <asp:Label ID="lblNomEntAFIP" runat="server" Style="position: relative;" Text="Razón Social en AFIP"
                        Width="140px"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtNomEntAfip" runat="server" ToolTip="Razón Social AFIP" CssClass="txtbanelco"
                        Enabled="False" Width="350" Font-Size="16px" Height="55px" TextMode="multiLine" style="float: left; color: #000000;"></asp:TextBox>
                </td>
                <%--<td>
                    <asp:ImageButton ID="btnBuscarNomEntAfip" runat="server" ImageUrl="Imagenes/Buscar.png"
                        ToolTip="Buscar en AFIP" ValidationGroup="1" OnClick="btnBuscarNomEntAfip_Click">
                    </asp:ImageButton>
                </td>--%>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:CustomValidator ID="cvNomEntAfip" runat="server" Style="color: Red;
                        margin-left: 155px; display: inline;" ErrorMessage="CustomValidator" OnServerValidate="cvNomEntAfip_ServerValidate"></asp:CustomValidator>
                    <br />
                    <asp:Label ID="lblNoEncontrado" Visible="False" runat="server" Style="color: Red;
                        margin-left: 50px; display: inline;" Text="No se encontro la razón social para el número de CUIT ingresado"
                        Width="406px"></asp:Label>                    
            </tr>
        </table>
        <br />
    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="btnBuscarNomEntAfip" />
    </Triggers>
</asp:UpdatePanel>
