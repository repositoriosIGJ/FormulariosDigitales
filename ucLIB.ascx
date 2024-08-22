<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucLIB.ascx.cs" Inherits="ucLIB" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:UpdatePanel ID="updPagFoj" runat="server">
    <ContentTemplate>
        <asp:HiddenField ID="hfIdTipoLibro" runat="server" />
        <table class="tablalibro">
            <tr>
                <td colspan="2" class="filalibro">
                    <asp:Label ID="lblLibro" runat="server" Text="Libro" Font-Strikeout="False" Font-Size="14px"
                        Font-Bold="true" CssClass="lblpresentante"></asp:Label>                    
                </td>
            </tr>
            <tr>
                <td class="fila1blanca">
                    <asp:Label ID="lblCopiador" runat="server" Text="Copiador" Font-Strikeout="False"
                        Width="160px" CssClass="lblpresentante"></asp:Label>
                </td>
                <td class="fila2blanca">
                    <asp:RadioButtonList ID="rblCopiador" runat="server" RepeatDirection="Horizontal"
                        AutoPostBack="false" DataTextField="Descripcion" DataValueField="Codigo" OnSelectedIndexChanged="rblCopiador_SelectedIndexChanged">
                        <asp:ListItem Value="SI" Text="SI" Selected="True"></asp:ListItem>
                        <asp:ListItem Value="NO" Text="NO"></asp:ListItem>
                    </asp:RadioButtonList>
                    <%--<asp:DropDownList ID="ddlCopiador" runat="server" Width="100px" DataTextField="Descripcion"
            DataValueField="Codigo" AutoPostBack="false">
            <asp:ListItem Value="SI" Text="SI" Selected="True"></asp:ListItem>
            <asp:ListItem Value="NO" Text="NO"></asp:ListItem>
        </asp:DropDownList>--%>
                </td>
            </tr>
            <tr>
                <td class="fila1celeste">
                    <asp:Label ID="lblPagFoj" runat="server" Text="Tipo" Font-Strikeout="False"
                        Width="160px" CssClass="lblpresentante"></asp:Label>
                </td>
                <td class="fila2celeste">
                    <asp:RadioButtonList ID="rblPagFoj" runat="server" RepeatDirection="Horizontal" AutoPostBack="true"
                        DataTextField="Descripcion" DataValueField="Codigo" OnSelectedIndexChanged="rblPagFoj_SelectedIndexChanged">
                        <asp:ListItem Value="PAG" Text="Páginas" Selected="True"></asp:ListItem>
                        <asp:ListItem Value="FOJ" Text="Fojas"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr id="trPaginas" runat="server" visible="true">
                <td class="fila1blanca">
                    <asp:Label ID="lblPaginas" runat="server" Text="Cantidad de Páginas" Font-Strikeout="False"
                        Width="160px" CssClass="lblpresentante"></asp:Label>
                </td>
                <td class="fila2blanca">
                    <asp:TextBox ID="txtPaginas" runat="server" MaxLength="11" ToolTip="Cantidad de Páginas"
                        Width="100px" CssClass="txtanexo">
                    </asp:TextBox>
                    <cc1:FilteredTextBoxExtender ID="ValidaPAG" runat="server" FilterType="Numbers" TargetControlID="txtPaginas">
                    </cc1:FilteredTextBoxExtender>
                    <asp:RegularExpressionValidator ID="valPAG" runat="server" EnableClientScript="False"
                        ErrorMessage="Ingrese una cantidad válida (0-4 dígitos)" ControlToValidate="txtPaginas"
                        Display="Dynamic" ValidationExpression="\d{0,4}" CssClass="banelco">
                    </asp:RegularExpressionValidator>
                    <cc1:ValidatorCalloutExtender ID="msgvalpag" runat="server" TargetControlID="valPAG">
                    </cc1:ValidatorCalloutExtender>
                    <%--<cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" WatermarkCssClass="textboxwatermark"
            runat="server" TargetControlID="txtPaginas" WatermarkText="Ingrese aquí">
        </cc1:TextBoxWatermarkExtender>--%>
                    <asp:RequiredFieldValidator ID="rfvPAG" EnableClientScript="False" Enabled="true" runat="server"
                        ErrorMessage="Ingrese la cantidad de páginas" CssClass="banelco" ControlToValidate="txtPaginas"
                        Display="Dynamic">
                    </asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="msgrfvpag" runat="server" TargetControlID="rfvPAG">
                    </cc1:ValidatorCalloutExtender>
                </td>
            </tr>
            <tr id="trFojas" runat="server" visible="false">
                <td class="fila1blanca">
                    <asp:Label ID="lblFojas" runat="server" Text="Cantidad de Fojas" Font-Strikeout="False"
                        Width="160px" CssClass="lblpresentante"></asp:Label>
                </td>
                <td class="fila2blanca">
                    <asp:TextBox ID="txtFojas" runat="server" MaxLength="11" ToolTip="Cantidad Fojas"
                        Width="100px" CssClass="txtanexo">
                    </asp:TextBox>
                    <cc1:FilteredTextBoxExtender ID="ValidaFOJ" runat="server" FilterType="Numbers" TargetControlID="txtFojas">
                    </cc1:FilteredTextBoxExtender>
                    <asp:RegularExpressionValidator ID="valFOJ" runat="server" EnableClientScript="False"
                        ErrorMessage="Ingrese una cantidad válida (0-3 dígitos)" ControlToValidate="txtFojas"
                        Display="Dynamic" ValidationExpression="\d{0,3}" CssClass="banelco">
                    </asp:RegularExpressionValidator>
                    <cc1:ValidatorCalloutExtender ID="msgvalfoj" runat="server" TargetControlID="valFOJ">
                    </cc1:ValidatorCalloutExtender>
                    <asp:RequiredFieldValidator ID="rfvFOJ" EnableClientScript="False" Enabled="true" runat="server"
                        ErrorMessage="Ingrese la cantidad de fojas" CssClass="banelco" ControlToValidate="txtFojas"
                        Display="Dynamic">
                    </asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="msgrfvfoj" runat="server" TargetControlID="rfvFOJ">
                    </cc1:ValidatorCalloutExtender>
                </td>
            </tr>
        </table>
    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="rblPagFoj" EventName="SelectedIndexChanged" />
    </Triggers>
</asp:UpdatePanel>
