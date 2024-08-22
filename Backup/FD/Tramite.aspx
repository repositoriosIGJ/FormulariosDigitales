<%@ Page Language="C#" MasterPageFile="~/DigiForm.master" AutoEventWireup="true"
    CodeFile="Tramite.aspx.cs" Inherits="Tramite" Title="Inspección General de Justicia - Formulario Digital" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--**************************TRAMITE***************************--%>
    <div style="margin: 0px auto; text-align: center;">
        <asp:Label ID="lblJavaError" runat="server" Text="Label" Visible="False" Font-Size="12px"
            ForeColor="Red"></asp:Label>
    </div>
    <div style="margin-left: 40px; width: 720px;">
        <asp:Panel ID="PanelTituloTramite" runat="server">
            <div id="encabezadoDatosTramites">
            </div>
        </asp:Panel>
        <asp:Panel ID="PanelCuerpoTramite" runat="server">
            <div class="contenido">
                <div class="campo">
                    <asp:Panel ID="pnlTipoBusquedaTipoEntidad" runat="server" Width="100%">
                        <br />
                        <table style="margin: 0 auto; margin-top: 0px;">
                            <tr>
                                <td colspan="3">
                                    <br />
                                    <b>Debe identificar el tipo de entidad y luego seleccionar el nombre del trámite.</b>
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblTipoEntidad" runat="server" Text="Tipo entidad: ">
                                    </asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlTipoEntidad" runat="server" Width="400px" OnSelectedIndexChanged="ddlTipoEntidad_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 152px">
                                    <asp:Button ID="btnBuscarTramite" runat="server" CssClass="bluebutton" Text="Buscar Trámite"
                                        OnClick="btnBuscarTramite_Click" Width="107px" ToolTip="Buscar trámite seleccionado"
                                        Height="25px" />
                                    <asp:ImageButton ID="imgHabilitarBusqueda" runat="server" ImageUrl="~/imagenes/reload14.png"
                                        OnClick="imgHabilitarBusqueda_Click" ToolTip="Habilitar la busqueda" Visible="False" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Panel ID="PnlFormulario" runat="server" Visible="false">
                        <div style="width: 100%">
                            <table style="background-repeat: no-repeat; margin: 0px auto; width: 300px; height: 120px;
                                vertical-align: middle; background-image: url(imagenes/fondoPrecio.png); background-repeat: repeat-y;">
                                <tr>
                                    <td colspan="2" style="font-weight: bold; font-size: large; vertical-align: middle;
                                        color: #526274; text-align: center; width: 296px;">
                                        <br />
                                        <asp:Label ID="lblNroFormulario" runat="server" Text="Formulario:" Font-Size="Medium"
                                            Font-Bold="False">
                                        </asp:Label>
                                        <asp:Label ID="lblNumFormulario" runat="server" Font-Size="Large" Font-Overline="False"
                                            Font-Strikeout="False" Font-Underline="False" Font-Bold="True" ForeColor="#526274">
                                        </asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="font-weight: bold; font-size: large; vertical-align: middle;
                                        color: #526274; text-align: center; width: 296px;">
                                        <br />
                                        <asp:Label ID="lblPrecioForm" runat="server" ForeColor="#526274" Font-Size="Small"
                                            Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Style="position: relative">Valor del Trámite: 
                                        </asp:Label>
                                        $
                                        <asp:Label ID="lblMontoFormulario" runat="server" ForeColor="#526274" Font-Size="Large"
                                            Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Font-Bold="True"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="text-align: center; width: 296px;">
                                        <br />
                                        <asp:CheckBox ID="chkMarcarTramUrg" runat="server" Text="Si el trámite será presentado como URGENTE, marque aquí"
                                            ToolTip="Al ser urgente alterará el precio final del formulario." AutoPostBack="True"
                                            TextAlign="Left" ForeColor="Black" OnCheckedChanged="chkMarcarTramUrg_CheckedChanged"
                                            Width="80%"></asp:CheckBox>
                                        <br />
                                        <asp:Label Style="left: 1px; position: relative" ID="lblUrgente" runat="server" Visible="False"
                                            Font-Size="Small" ForeColor="Red" Font-Names="Tahoma" Font-Bold="True" Font-Underline="True"
                                            Font-Italic="False">Trámite Urgente</asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <div style="width: 300; height: 3px; background-image: url(imagenes/fondoPrecioSombra.png);
                                background-repeat: no-repeat; margin: 0 auto; margin-left: 225px;">
                            </div>
                            <br />
                            <br />
                        </div>
                        <div style="width: 95%;">
                            <table style="margin: 0px auto;">
                                <tr>
                                    <td>
                                        <asp:Label ID="lblIdTramite" runat="server" Visible="False"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblInicioDescTramite" runat="server" Text="TRAMITE SELECCIONADO:"
                                            Font-Bold="True"></asp:Label>
                                    </td>
                                    <td style="font-weight: normal">
                                        <asp:Label ID="lblDescTramite" runat="server" ForeColor="#3B4754"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </asp:Panel>
                    <div class="grilla">
                        <asp:Panel ID="pnlFiltrarTramites" runat="server" Visible="False">
                            <table style="margin: 0 auto; margin-top: 15px;">
                                <tr>
                                    <td>
                                        <img src="Imagenes/Buscar.png" alt="Buscar" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblNombreTramite" runat="server" Text="Filtrar Trámites: ">
                                        </asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtNombreTramite" runat="server" Width="250" MaxLength="100"></asp:TextBox>
                                        <asp:CustomValidator ID="cvTramite" runat="server" ControlToValidate="txtNombreTramite"
                                            Display="Dynamic" ErrorMessage="Caracter Inválido." OnServerValidate="cvTramite_ServerValidate"
                                            Style="position: relative; left: 0px; top: 0px;" Font-Size="Smaller" EnableClientScript="False"></asp:CustomValidator>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnFiltrarTramitexNombre" runat="server" CssClass="bluebutton" Text="Filtrar"
                                            OnClick="btnFiltrarTramitexNombre_Click" Width="107px" ToolTip="Filtrar los trámites seleccionados"
                                            Height="25px" />
                                        <asp:Button ID="bntMostrarTodosTramites" runat="server" Text="Todos los Trámites"
                                            OnClick="bntMostrarTodosTramites_Click" Visible="False" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" style="text-align: center;">
                                        <asp:Label ID="lblMsgFiltro" runat="server" Text="" Font-Size="Small" ForeColor="salmon"></asp:Label>
                                        <asp:Label ID="lblFiltrado" runat="server" Text="Lista de Trámites Filtrada." Font-Size="Smaller"
                                            Visible="false"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <table>
                            <tr>
                                <td style="text-align: right;">
                                    <div style="">
                                        <asp:Label ID="lblComentarioTram" runat="server" Text="Seleccione el trámite correspondiente clickeando sobre el signo: &nbsp;&nbsp;<img src='imagenes/addTemp.png' alt='' />"
                                            Visible="False" Font-Bold="True" Font-Italic="True" Width="690px"></asp:Label>
                                    </div>
                                </td>
                            </tr>
                        </table>
                        <table style="margin: 0px auto;">
                            <tr>
                                <td>
                                    <asp:GridView Style="position: relative" ID="gvTramites" runat="server" OnSelectedIndexChanged="imgCheck_Click"
                                        AllowPaging="True" OnPageIndexChanging="gvTramites_PageIndexChanging" ForeColor="#333333"
                                        AutoGenerateColumns="False" CellPadding="4" GridLines="None" DataKeyNames="FlagNumeroCorrelativo,Id,FlagUrgente"
                                        PagerStyle-CssClass="PagerStyle" HeaderStyle-CssClass="HeaderStyle" BorderStyle="Solid"
                                        BorderColor="#C8CAC9" BorderWidth="1" PagerSettings-Mode="Numeric" PagerSettings-NextPageText="Siguiente"
                                        PagerSettings-PreviousPageText="Atrás" Width="97%" HeaderStyle-BackColor="#C0CFDE">
                                        <FooterStyle BackColor="#929DA8" ForeColor="White" Font-Bold="True" Font-Size="12px"
                                            Font-Underline="False" HorizontalAlign="Right"></FooterStyle>
                                        <Columns>
                                            <asp:TemplateField SortExpression="FlagNumeroCorrelativo" HeaderText="FlagNumeroCorrelativo"
                                                Visible="False">
                                                <EditItemTemplate>
                                                    <asp:CheckBox ID="CheckBox2" runat="server" Checked='<%# Bind("FlagNumeroCorrelativo") %>' />
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkFlagNumCorr" runat="server" Enabled="false" Checked='<%# Bind("FlagNumeroCorrelativo") %>'>
                                                    </asp:CheckBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Id" SortExpression="Id" HeaderText="Id" Visible="False"></asp:BoundField>
                                            <asp:BoundField DataField="Descripcion" SortExpression="Descripcion" HeaderText="Descripcion">
                                            </asp:BoundField>
                                            <asp:TemplateField SortExpression="FlagUrgente" HeaderText="FlagUrgente" Visible="False">
                                                <EditItemTemplate>
                                                    <asp:CheckBox runat="server" Checked='<%# Bind("FlagUrgente") %>' ID="CheckBox1"></asp:CheckBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkFlagUrgente" runat="server" Enabled="false" TextAlign="Left"
                                                        Checked='<%# Bind("FlagUrgente") %>'></asp:CheckBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:CommandField ButtonType="Image" HeaderText="Seleccionar" SelectImageUrl="~/imagenes/add.png"
                                                ShowSelectButton="True">
                                                <ItemStyle BackColor="White" HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:CommandField>
                                        </Columns>
                                        <EditRowStyle BackColor="#2461BF"></EditRowStyle>
                                        <SelectedRowStyle Font-Bold="False"></SelectedRowStyle>
                                        <PagerStyle BackColor="#5F9FDE" ForeColor="White" Font-Bold="True" CssClass="PagerStyle"
                                            BorderStyle="Groove" Width="100%" Wrap="False"></PagerStyle>
                                        <HeaderStyle BackColor="#C0CFDE" ForeColor="Black" Font-Bold="True" CssClass="HeaderStyle">
                                        </HeaderStyle>
                                        <AlternatingRowStyle BackColor="White"></AlternatingRowStyle>
                                        <RowStyle BackColor="#EFF3FB" Height="35px" />
                                        <PagerSettings PreviousPageText="&amp;gt;" FirstPageText="&amp;nbsp;&amp;nbsp;&amp;lt;&amp;lt;&amp;nbsp;&amp;nbsp;"
                                            LastPageText="&amp;nbsp;&amp;nbsp;&amp;gt;&amp;gt;&amp;nbsp;&amp;nbsp;" NextPageText="Siguiente" />
                                    </asp:GridView>
                                    <asp:ObjectDataSource ID="ODSTramite" runat="server" SelectMethod="RetornarTiposTramites"
                                        TypeName="FD.BusinessLayer.BLTramite">
                                        <SelectParameters>
                                            <asp:ControlParameter ControlID="ddlTipoEntidad" Name="tipoEnt" PropertyName="SelectedValue"
                                                Type="String" />
                                        </SelectParameters>
                                    </asp:ObjectDataSource>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <%--DIV grilla--%>
                </div>
            </div>
            <div class="contenidoBottom">
            </div>
            <%--DIV campo--%>
        </asp:Panel>
        <%--<asp:Panel ID="PanelPedirAnexos" runat="server">
            <div id="encabezadoDatosAnexos">
            </div>
            <div class="contenido">
                <div class="campo">
                    <br />
                    <table style="margin: 0 auto; margin-top: 0px;">
                        <tr>
                            <td>
                                <b>¿Desea cargar los libros de rubrica junto a su tramite?.</b>                                
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="btnContinuarConAnexo" CssClass="bluebutton" runat="server" Text="Continuar con Libros"
                                    OnClick="btnConfirmarAnexo_Click" />
                            </td>
                            <td>
                                <asp:Button ID="btnContinuarSinAnexo" CssClass="bluebutton" runat="server" Text="Continuar sin Libros"
                                    OnClick="btnConfirmar_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </asp:Panel>--%>
        <div class="botonesPieSeccion">
            <asp:Button ID="btnConfirmarTramite" CssClass="bluebutton" runat="server" Text="Confirmar"
                OnClick="btnConfirmar_Click" />
        </div>
    </div>
</asp:Content>
