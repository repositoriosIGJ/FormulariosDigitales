<%@ Page Language="C#" MasterPageFile="~/DigiForm.master" AutoEventWireup="true"
    CodeFile="Entidad.aspx.cs" Inherits="Entidad" Title="Inspección General de Justicia - Formulario Digital" %>

<%--enableEventValidation="false"--%>
<%@ Register Src="ctrlTxtDNI.ascx" TagName="ctrlTxtDNI" TagPrefix="uc1" %>
<%@ Register Assembly="MSCaptcha" Namespace="MSCaptcha" TagPrefix="cc2" %>
<%@ Register Src="ctrlTxtDOCUMENTOS.ascx" TagName="ctrlTxtDOCUMENTOS" TagPrefix="uc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%--<%@ Register Src="ucCUIT.ascx" TagName="ucCUIT" TagPrefix="uc3" %>--%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript">
        function ValidarCaracteres(textareaControl,maxlength)
        {
            if (textareaControl.value.length > maxlength)
            {
                textareaControl.value = textareaControl.value.substring(0,maxlength);
                alert("Debe ingresar hasta un maximo de "+maxlength+" caracteres");
            }
        }
    </script>

    <div id="contenedorForm">
        <asp:Panel ID="PnlDataContainer" runat="server">
            <asp:Panel ID="PanelTituloEntidad" runat="server">
                <div id="encabezadoDatosEntidad">
                    <asp:Image ID="imgEntidadHeader" runat="server" ImageUrl="Imagenes/datosEntidad.jpg" />
                </div>
                <div class="contenido" style="">
                    <div>
                        <br />
                        <asp:Label ID="lblEntidad" runat="server" Text=""></asp:Label>
                        <br />
                        <asp:Panel runat="server" ID="pnlEspecial" Visible="False">
                            <table style="width: 80%; margin: 0px auto;">
                                <tr>
                                    <td colspan="2">
                                        <br />
                                        <asp:Label runat="server" ID="lblDenomProp" Text="Denominaciones propuestas" Font-Bold="True"
                                            Font-Size="Medium" Width="224px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: middle">
                                        <asp:Label runat="server" ID="lblOp1" Text="Opción 1: " />
                                    </td>
                                    <td style="vertical-align: middle; width: 409px;">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Campo obligatorio"
                                            ControlToValidate="txtOp1" Display="Dynamic"></asp:RequiredFieldValidator>
                                        <asp:TextBox runat="server" ID="txtOp1" Width="400px" MaxLength="120" /><br />
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="txtOp1"
                                            ValidChars="0123456789qQwWeErRtTyYuUiIoOpPaAsSdDfFgGhHjJkKlLñÑzZxXcCvVbBnNmM ">
                                        </cc1:FilteredTextBoxExtender>
                                        <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtOp1"
                                            WatermarkText="Ingrese la 1° Opción." WatermarkCssClass="textboxwatermark">
                                        </cc1:TextBoxWatermarkExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: middle">
                                        <asp:Label runat="server" ID="lblOp2" Text="Opción 2: " />
                                    </td>
                                    <td style="vertical-align: middle; width: 409px;">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Campo obligatorio"
                                            ControlToValidate="txtOp2" Display="Dynamic"></asp:RequiredFieldValidator>
                                        <asp:TextBox runat="server" ID="txtOp2" Width="400px" MaxLength="120" /><br />
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" TargetControlID="txtOp2"
                                            ValidChars="0123456789qQwWeErRtTyYuUiIoOpPaAsSdDfFgGhHjJkKlLñÑzZxXcCvVbBnNmM ">
                                        </cc1:FilteredTextBoxExtender>
                                        <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtOp2"
                                            WatermarkText="Ingrese la 2° Opción." WatermarkCssClass="textboxwatermark">
                                        </cc1:TextBoxWatermarkExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: middle">
                                        <asp:Label runat="server" ID="lblOp3" Text="Opción 3: " />
                                    </td>
                                    <td style="vertical-align: middle; width: 409px;">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Campo obligatorio"
                                            ControlToValidate="txtOp3" Display="Dynamic"></asp:RequiredFieldValidator><asp:TextBox
                                                runat="server" ID="txtOp3" Width="400px" MaxLength="120" />
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" TargetControlID="txtOp3"
                                            ValidChars="0123456789qQwWeErRtTyYuUiIoOpPaAsSdDfFgGhHjJkKlLñÑzZxXcCvVbBnNmM ">
                                        </cc1:FilteredTextBoxExtender>
                                        <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server" TargetControlID="txtOp3"
                                            WatermarkText="Ingrese la 3° Opción" WatermarkCssClass="textboxwatermark">
                                        </cc1:TextBoxWatermarkExtender>
                                    </td>
                                </tr>
                            </table>
                            <asp:Panel runat="server" ID="pnlEspacialDatosConst" Width="80%">
                                <div style="margin-left: 70px;">
                                    <br />
                                    <br />
                                    <asp:Label runat="server" ID="lblConstituyentes" Text="Constituyentes" Font-Bold="True"
                                        Font-Size="Medium" />
                                    <br />
                                    <asp:RadioButtonList ID="rblSeleccionOpcion" runat="server" RepeatDirection="Horizontal"
                                        Width="551px" AutoPostBack="True" OnSelectedIndexChanged="rblSeleccionOpcion_SelectedIndexChanged">
                                        <asp:ListItem Text="Personas Físicas" Value="0" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="Personas Jurídicas" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Mixto (Personas Físicas + Personas Jurídicas)" Value="2"></asp:ListItem>
                                    </asp:RadioButtonList>&nbsp;
                                    <br />
                                    <!-- PANEL NOMBRE Y APELLIDO -->
                                    <asp:Panel runat="server" ID="pnlNomApe" Visible="False" Width="711px">
                                        <table>
                                            <tr>
                                                <td style="width: 15px">
                                                    <asp:Label ID="lblNroDoc1" runat="server" Style="position: relative" Text="DNI / LE / LC / PASAPORTE / CDI: "
                                                        Width="78px"></asp:Label>
                                                </td>
                                                <td>
                                                    <uc2:ctrlTxtDOCUMENTOS ID="txtDNIConsti1" runat="server" />
                                                    <%--<uc1:ctrlTxtDNI ID="txtDNIConsti1" runat="server" />--%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 15px;">
                                                    <asp:Label ID="lblNyAConsti1" runat="server" Style="position: relative" Text="Nombre y Apellido: "
                                                        Width="90px"></asp:Label>
                                                </td>
                                                <td style="width: 400px">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Dato obligatorio"
                                                        ControlToValidate="txtNyAConsti1" Display="Dynamic"></asp:RequiredFieldValidator><cc1:FilteredTextBoxExtender
                                                            ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtNyAConsti1"
                                                            ValidChars="qQwWeErRtTyYuUiIoOpPaAsSdDfFgGhHjJkKlLñÑzZxXcCvVbBnNmM ">
                                                        </cc1:FilteredTextBoxExtender>
                                                    <asp:TextBox ID="txtNyAConsti1" runat="server" Style="position: relative; left: 0px;
                                                        top: 0px;" Width="400px" MaxLength="160"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 15px">
                                                    <asp:Label ID="lblNroDoc2" runat="server" Style="position: relative" Text="DNI / LE / LC / PASAPORTE / CDI: "
                                                        Width="78px"></asp:Label>
                                                </td>
                                                <td>
                                                    <uc2:ctrlTxtDOCUMENTOS ID="txtDNIConsti2" runat="server" />
                                                    <%--<uc1:ctrlTxtDNI ID="txtDNIConsti2" runat="server" />--%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 15px">
                                                    <asp:Label ID="lblNyAConsti2" runat="server" Style="position: relative" Text="Nombre y Apellido: "
                                                        Width="91px"></asp:Label>
                                                </td>
                                                <td style="width: 400px">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Dato obligatorio"
                                                        ControlToValidate="txtNyAConsti2" Display="Dynamic"></asp:RequiredFieldValidator><cc1:FilteredTextBoxExtender
                                                            ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtNyAConsti2"
                                                            ValidChars="qQwWeErRtTyYuUiIoOpPaAsSdDfFgGhHjJkKlLñÑzZxXcCvVbBnNmM ">
                                                        </cc1:FilteredTextBoxExtender>
                                                    <asp:TextBox ID="txtNyAConsti2" runat="server" Style="position: relative" Width="400px"
                                                        MaxLength="160"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                    <!-- PANEL DENOMINACION -->
                                    <asp:Panel runat="server" ID="pnlDenom" Visible="False" Width="713px">
                                        <table>
                                            <tr>
                                                <td style="vertical-align: middle;">
                                                    <asp:Label ID="lblDenominacion1" runat="server" Style="position: relative" Text="Denominación 1:"
                                                        Width="80px"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="Dato obligatorio"
                                                        ControlToValidate="txtDenom1" Display="Dynamic"></asp:RequiredFieldValidator>
                                                    <asp:TextBox ID="txtDenom1" runat="server" Width="400px" MaxLength="120"></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" TargetControlID="txtDenom1"
                                                        ValidChars="0123456789qQwWeErRtTyYuUiIoOpPaAsSdDfFgGhHjJkKlLñÑzZxXcCvVbBnNmM ">
                                                    </cc1:FilteredTextBoxExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="vertical-align: middle;">
                                                    <asp:Label ID="lblDenominacion2" runat="server" Text="Denominación 2:" Width="80px"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="Dato obligatorio"
                                                        ControlToValidate="txtDenom2" Display="Dynamic"></asp:RequiredFieldValidator>
                                                    <asp:TextBox ID="txtDenom2" runat="server" Width="400px" MaxLength="120"></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" TargetControlID="txtDenom2"
                                                        ValidChars="0123456789qQwWeErRtTyYuUiIoOpPaAsSdDfFgGhHjJkKlLñÑzZxXcCvVbBnNmM ">
                                                    </cc1:FilteredTextBoxExtender>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                    <!-- PANEL MIXTO -->
                                    <asp:Panel runat="server" ID="pnlMixto" Visible="false" Width="713px">
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblDNIMixto" runat="server" Style="position: relative; left: 0px;"
                                                        Text="DNI / LE / LC / PASAPORTE / CDI: "></asp:Label>
                                                </td>
                                                <td>
                                                    <uc2:ctrlTxtDOCUMENTOS ID="txtDNIMixto" runat="server" />
                                                    <%--<uc1:ctrlTxtDNI ID="txtDNIMixto" runat="server" />--%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblNyAMixto" runat="server" Style="position: relative" Text="Nombre y Apellido: "
                                                        Width="90px"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Dato obligatorio"
                                                        ControlToValidate="txtNyAMixto" Display="Dynamic"></asp:RequiredFieldValidator>
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" TargetControlID="txtNyAMixto"
                                                        ValidChars="qQwWeErRtTyYuUiIoOpPaAsSdDfFgGhHjJkKlLñÑzZxXcCvVbBnNmM ">
                                                    </cc1:FilteredTextBoxExtender>
                                                    <asp:TextBox ID="txtNyAMixto" runat="server" Style="position: relative" Width="400px"
                                                        MaxLength="160"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblDenomMixto" runat="server" Text="Denominación: "></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="Dato obligatorio"
                                                        ControlToValidate="txtDenomMixto" Display="Dynamic"></asp:RequiredFieldValidator>
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server" TargetControlID="txtDenomMixto"
                                                        ValidChars="0123456789qQwWeErRtTyYuUiIoOpPaAsSdDfFgGhHjJkKlLñÑzZxXcCvVbBnNmM ">
                                                    </cc1:FilteredTextBoxExtender>
                                                    <asp:TextBox ID="txtDenomMixto" runat="server" Width="400px" MaxLength="120"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </div>
                                <div style="text-align: right;">
                                    <br />
                                    <br />
                                    <asp:Button ID="Button1" OnClick="btnAceptarDemonSoc_Click" CssClass="bluebutton"
                                        runat="server" Text="Aceptar"></asp:Button>
                                    <br />
                                    <br />
                                </div>
                            </asp:Panel>
                        </asp:Panel>
                    </div>
                    <div class="contenidoBottom">
                    </div>
                </div>
            </asp:Panel>
            <asp:Panel ID="PanelCuerpoEntidad" runat="server">
                <div class="contenido" style="margin-top: 1px;">
                    <div style="margin: 0px auto; text-align: center; margin-top: -3px;">
                        <asp:RadioButtonList ID="rblBuscarEntidad" runat="server" OnSelectedIndexChanged="rblBuscarEntidad_SelectedIndexChanged"
                            AutoPostBack="True" RepeatDirection="Horizontal">
                            <asp:ListItem Value="1" Text="Buscar por n&#250;mero correlativo"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Buscar por nombre de entidad"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                    <asp:HiddenField ID="HiddenField1" runat="server" />
                    <asp:Label ID="lblTipoSoc" runat="server" Style="position: relative" Visible="False"
                        Width="221px"></asp:Label>
                    <div class="campo">
                        <asp:Panel ID="pnlNumCorrNomSoc" runat="server">
                            <table style="margin: 0px auto; margin-top: 15px;">
                                <tr>
                                    <td style="vertical-align: top;">
                                        <br />
                                        <asp:Label ID="lblNroCorrelativo" runat="server" Text="Número Correlativo: "></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtNroCorrelativo" runat="server" ValidationGroup="1" MaxLength="7"
                                            Width="150px"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtNroCorrelativo"
                                            FilterType="Numbers">
                                        </cc1:FilteredTextBoxExtender>
                                        <br />
                                        <asp:Label ID="lblLeyendaBusquedaNumCorrelativo" runat="server" Font-Size="10px"
                                            ForeColor="#404040" Text="Ingrese el número sin puntos y <br/> luego haga un click sobre la lupa. "
                                            Visible="False">
                                        </asp:Label>
                                    </td>
                                    <td rowspan="2">
                                        <asp:ImageButton ID="imBuscarSocXNum" OnClick="imBuscarSocXNum_Click" runat="server"
                                            ImageUrl="Imagenes/Buscar.png" ToolTip="Buscar entidad" Style="margin-bottom: 35px;"
                                            ValidationGroup="1"></asp:ImageButton>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top">
                                        <br />
                                        <asp:Label ID="lblNomSociedad" runat="server" Text="Nombre entidad: "></asp:Label>
                                    </td>
                                    <td style="vertical-align: top">
                                        <asp:TextBox ID="txtNomSociedad" runat="server" ValidationGroup="1" MaxLength="60"
                                            Width="381px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Campo obligatorio"
                                            Display="Dynamic" ControlToValidate="txtNomSociedad"></asp:RequiredFieldValidator>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" runat="server" TargetControlID="txtNomSociedad"
                                            ValidChars="0123456789qQwWeErRtTyYuUiIoOpPaAsSdDfFgGhHjJkKlLñÑzZxXcCvVbBnNmM ">
                                        </cc1:FilteredTextBoxExtender>
                                        <br />
                                        <asp:Label ID="lblLeyendaBusquedaNonSociedad" runat="server" Font-Size="10px" ForeColor="#404040"
                                            Text="No incluya el tipo de entidad. Tenga en cuenta que la denominación puede <br /> encontrarse abreviada. Luego de ingresar la denominación haga un click sobre <br /> la lupa"
                                            Visible="false">
                                        </asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <!-- DATOS REFERENCIALES DE LA SOCIEDAD EN IGJ-->
                            <div id="TDatosRefEntidad" runat="server" style="text-align: right;" visible="false">
                                <table class="tdatosent" width="auto">
                                    <tr>
                                        <td colspan="2" style="text-align: center;">
                                            <img src="Imagenes/datosentigj.png" style="padding-bottom: 5px;" alt="DATOS DE LA ENTIDAD EN IGJ" />
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="120px">
                                            <label style="text-align: right;">
                                                Nro Correlativo:&nbsp</label>
                                        </td>
                                        <td width="auto">
                                            <asp:Label ID="lblNumCorrEnt" runat="server" Font-Bold="True" Style="text-align: left;
                                                display: block;"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label style="text-align: right;">
                                                Razón Social:&nbsp</label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblNomEntidadSelecc" runat="server" Font-Bold="True" Style="text-align: left;
                                                display: block;"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label style="text-align: right;">
                                                Tipo Societario:&nbsp</label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblTipoEntidad" runat="server" Font-Bold="True" Style="text-align: left;
                                                display: block; height: 1px;"></asp:Label><br />
                                        </td>
                                    </tr>
                                    <!--TODO_NOCUIT: No se pide Cuit-->
                                    <!--TODO_CUIT: Agrego label del Cuit Referencial-->
                                    <%--<tr>
                                        <td>
                                            <label style="text-align: right;font-size:16px;">
                                                Nro de Cuit:&nbsp
                                            </label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblCuitRef" runat="server" Font-Bold="True" Style="font-size:16px; color: Red;text-align: left;
                                                display: block;"></asp:Label>
                                        </td>
                                    </tr>--%>
                                </table>
                            </div>
                            <!--TODO_NOCUIT: No se pide Cuit-->
                            <!-- DATOS REFERENCIALES DE LA SOCIEDAD EN AFIP-->
                            <%--
                            <div id="TDatosRefAfip" runat="server" style="text-align: right;" visible="false">
                                <table class="tdatosent" width="auto">
                                    <tr>
                                        <td colspan="2" style="text-align: center;">
                                            <img src="Imagenes/datosentafip.png" style="padding-bottom: 5px;" alt="DATOS DE LA ENTIDAD EN AFIP" />
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="110px">
                                            <label style="text-align: right;">
                                                Razón Social:&nbsp</label>
                                        </td>
                                        <td width="auto">
                                            <asp:Label ID="lblNomEntidadAfip" runat="server" Font-Bold="True" Style="text-align: left;
                                                display: block;"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label style="text-align: right;">
                                                Nro de Cuit:&nbsp
                                            </label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblCuitRefAfip" runat="server" Font-Bold="True" Style="text-align: left;
                                                display: block;"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            --%>
                            <!--TODO_NOCUIT: No se pide Cuit-->
                            <!-- MENSAJE DE ERROR POR CUIT -->
                            <%--
                            <div id="divValidezCuit" runat="server" visible="false">
                                <center>
                                    <br />
                                    <asp:Label ID="lblValidezCuit" runat="server" CssClass="validezcuiterror"></asp:Label>
                                    <br />
                                </center>
                            </div>
                            <asp:UpdatePanel ID="updpnlCUIT" runat="server" Visible="False">
                                <ContentTemplate>
                                    <div id="divCambioCuit" runat="server" visible="false">
                                        <table class="tdatosent" width="auto">
                                            <tr>
                                                <td>
                                                    <asp:CheckBox ID="chkCambioCuit" CssClass="chkCambioCuit" Text="Rectificar el numero de CUIT" runat="server" Visible="false"
                                                        Enabled="false" OnCheckedChanged="chkCambioCuit_CheckedChanged" AutoPostBack="True" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <!-- TODO_CUIT: Empieza Pedido de Cuit -->
                                    <div id="TIngresoCuit" class="tdatosent" runat="server" visible="false" style="width: 650px">
                                        <uc3:ucCUIT ID="ucCUIT1" runat="server" OnStatusUpdated="ucCUIT_StatusUpdated" />
                                    </div>
                                    <!-- Termina Pedido de Cuit -->
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="chkCambioCuit" EventName="CheckedChanged" />
                                    <asp:PostBackTrigger ControlID="ucCUIT1" />
                                </Triggers>
                            </asp:UpdatePanel>--%>
                        </asp:Panel>
                        <div class="grilla">
                            <table style="text-align: center;">
                                <tr>
                                    <td style="height: 21px">
                                        <asp:Label ID="lblComentarioEnt" runat="server" Text="Seleccione la denominación correspondiente clickeando sobre el signo: &nbsp;&nbsp;<img src='imagenes/addTemp.png' alt=''"
                                            Visible="False" Font-Bold="True" Font-Italic="True" Width="722px"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <table style="margin: 0px auto;">
                                <tr>
                                    <td colspan="3">
                                        <asp:GridView ID="gvSociedades" runat="server" OnSelectedIndexChanged="gvSociedades_SelectedIndexChanged"
                                            GridLines="None" CellPadding="4" AutoGenerateColumns="False" ForeColor="#333333"
                                            OnPageIndexChanging="gvSociedades_PageIndexChanging" AllowPaging="True" HeaderStyle-BackColor="#348FDF"
                                            BorderStyle="Solid" BorderColor="#C8CAC9" BorderWidth="1">
                                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                            <RowStyle HorizontalAlign="Left" BackColor="#EFF3FB" Height="35px" />
                                            <EditRowStyle BackColor="#2461BF" />
                                            <SelectedRowStyle Font-Bold="False" />
                                            <PagerStyle BackColor="#5F9FDE" ForeColor="White" HorizontalAlign="Center" CssClass="PagerStyle" />
                                            <HeaderStyle BackColor="#C0CFDE" Font-Bold="True" ForeColor="Black" />
                                            <AlternatingRowStyle BackColor="White" />
                                            <Columns>
                                                <asp:BoundField DataField="NroCorrelativo" HeaderText="Nro Correlativo" SortExpression="NroCorrelativo">
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="Nombre"></asp:BoundField>
                                                <asp:BoundField DataField="TipoSociedad" HeaderText="Tipo" SortExpression="TipoSociedad">
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Cuit" HeaderText="Cuit" SortExpression="Cuit" ItemStyle-CssClass="HiddenItemGrilla"
                                                    HeaderStyle-CssClass="HiddenItemGrilla"></asp:BoundField>
                                                <asp:TemplateField HeaderText="Nro. Tipo" SortExpression="IdTipoSociedad" ItemStyle-CssClass="HiddenItemGrilla"
                                                    HeaderStyle-CssClass="HiddenItemGrilla">
                                                    <ItemTemplate>                            
                                                            <asp:Label ID="IdTipoSociedad" runat="server" Text=<%#DataBinder.Eval(Container.DataItem, "TipoSociedad.Id")%> ></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:CommandField ButtonType="Image" HeaderText="Seleccionar" SelectImageUrl="~/imagenes/add.png"
                                                    ShowSelectButton="True">
                                                    <ItemStyle BackColor="White" HorizontalAlign="Center" />
                                                </asp:CommandField>
                                            </Columns>
                                        </asp:GridView>
                                        <asp:ObjectDataSource ID="ODSEntidad" runat="server" OldValuesParameterFormatString="original_{0}"
                                            SelectMethod="GetSociedad" TypeName="FD.BusinessLayer.BLEntidad">
                                            <SelectParameters>
                                                <asp:ControlParameter ControlID="txtNroCorrelativo" Name="NumCorrelativo" PropertyName="Text"
                                                    Type="Int32" />
                                                <asp:ControlParameter ControlID="HiddenField1" Name="tipoEnt" PropertyName="Value"
                                                    Type="Int32" />
                                            </SelectParameters>
                                        </asp:ObjectDataSource>
                                        <asp:ObjectDataSource ID="ODSRazonEntidad" runat="server" TypeName="FD.BusinessLayer.BLEntidad"
                                            SelectMethod="GetSociedades" OldValuesParameterFormatString="original_{0}">
                                            <SelectParameters>
                                                <asp:ControlParameter ControlID="txtNomSociedad" Name="RazonSo" PropertyName="Text"
                                                    Type="String" />
                                                <asp:ControlParameter ControlID="HiddenField1" Name="tipoEnt" PropertyName="Value"
                                                    Type="Int32" />
                                            </SelectParameters>
                                        </asp:ObjectDataSource>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: justify;">
                                        <asp:Label ID="lblNroCorrelativoError" runat="server" Text="" Visible="false" ForeColor="red"
                                            Font-Size="14px" Width="650px"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <table style="margin: 0px auto;">
                            <tr>
                                <td style="margin-left: 3px;" colspan="2">
                                    <img src="Imagenes/dIV.jpg" alt="" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 129px">
                                    <asp:Label ID="lblMotivo" runat="server" Text="Observaciones: "></asp:Label>
                                </td>
                                <td>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblCantCaracteres" runat="server" Text="Utilice el siguiente campo para especificar la documentación que se presenta y/o efectuar las observaciones que crea conveniente (máximo: 300 caracteres). "
                                                    Font-Bold="True" Font-Size="10px" ForeColor="DimGray" Width="450px"></asp:Label>
                                                <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="Escriba como máximo 300 caracteres"
                                                    OnServerValidate="ValidarDatosAdjuntos" Style="position: relative" ControlToValidate="txtMotivo"
                                                    Display="Dynamic"></asp:CustomValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtMotivo" runat="server" MaxLength="5" Width="450px" Columns="5"
                                                    Rows="5" TextMode="MultiLine" BackColor="#C0CEDD" BorderStyle="Groove" Height="131px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div class="contenidoBottom">
                </div>
            </asp:Panel>
            <!-- TODO: PRUEBO QUITANDO updpnlConfirmarEntidad -->
            <%--<asp:UpdatePanel ID="updpnlConfirmarEntidad" runat="server">
                <ContentTemplate>--%>
            <div class="botonesPieSeccion">
                <asp:Button ID="btnConfirmarEntidad" runat="server" CssClass="bluebutton" Text="Confirmar"
                    OnClick="btnConfirmar_Click" Visible="False" />&nbsp;
            </div>
            <%--</ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnConfirmarEntidad" EventName="Click" />                    
                </Triggers>
            </asp:UpdatePanel>--%>
            <asp:Panel ID="PanelCaptcha" runat="server" Visible="false">
                <center>
                    <div id="divchkFormaPago" class="divchkFormaPago">
                        <table>
                            <tr>
                                <td>
                                    <asp:Image ID="imgBanelco1" runat="server" ImageUrl="Imagenes/imgBanelco1.jpg" />
                                    <asp:CheckBox ID="chkFormaPago" runat="server" OnCheckedChanged="chkFormaPago_CheckedChanged"
                                        AutoPostBack="True" />
                                    <asp:Image ID="imgBanelco2" runat="server" ImageUrl="Imagenes/imgBanelco2.jpg" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <asp:UpdatePanel ID="updpnlBanelco" runat="server">
                        <ContentTemplate>
                            <div class="contenidobanelco">
                                <div class="campo">
                                    <table style="margin-top: 10px; margin-bottom: 10px;">
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="lblDNIpago1" runat="server" Text="Número DNI: "></asp:Label>
                                            </td>
                                            <td align="left">
                                                <uc1:ctrlTxtDNI ID="txtDNIpago1" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="lblDNIpago2" runat="server" Text="Reingrese Nro. DNI: "></asp:Label>
                                            </td>
                                            <td align="left">
                                                <uc1:ctrlTxtDNI ID="txtDNIpago2" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="lblBANCOpago" runat="server" Text="Banco: "></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="ddlBANCOpago" runat="server" Width="250px" OnSelectedIndexChanged="ddlBANCOpago_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <center>
                                                    <br />
                                                    <div id="divErrorBanelco" runat="server" class="divErrorBanelco" visible="false">
                                                        <asp:Label ID="lblErrorBanelco" runat="server" Text=""></asp:Label>
                                                    </div>
                                                </center>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="chkFormaPago" EventName="CheckedChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                    <br />
                    <br />
                </center>
                <div style="text-align: center; width: 600px; height: 250px; margin: 0px auto;">
                    <img src="Imagenes/AvisoIGJ.jpg" alt="" />
                </div>
                <br />
                <br />
                <div id="captcha">
                    <center>
                        <label id="lbltitCaptcha" runat="server">
                            Por favor, ingrese el c&oacute;digo de seguridad:</label>
                    </center>
                    <fieldset style="margin: 0px auto; width: 350px;">
                        <div id="divimgCaptcha" runat="server" style="width: 300px;">
                            <table style="width: 100%;">
                                <tr>
                                    <td>
                                        <asp:Image ID="Image1" runat="server" ImageUrl="~/imgCaptcha.aspx" AlternateText="Si no puede ver la imagen que va aquí, presione el botón que dice 'Recargar imagen'" />
                                    </td>
                                    <td>
                                        <asp:ImageButton Style="position: relative" ID="imgRefreshCaptcha" OnClick="imgRefreshCaptcha_Click"
                                            runat="server" ImageUrl="~/imagenes/arrow_refresh.png" CausesValidation="False">
                                        </asp:ImageButton>
                                    </td>
                                    <td rowspan="3" style="text-align: center;">
                                        Por favor ingrese el c&oacute;digo
                                        <br />
                                        <asp:TextBox ID="txtCaptcha" runat="server" Width="85px" MaxLength="6"></asp:TextBox>
                                        <br />
                                        <asp:Label ID="lblErrorCaptcha" runat="server" Text="" ForeColor="red" Visible="false"
                                            Font-Size="X-Small"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div id="divbtnCaptcha" runat="server">
                            <br />
                            <asp:Button ID="btnCaptcha" OnClick="btnCaptcha_Click" CssClass="bluebutton" runat="server"
                                Text="Finalizar"></asp:Button>
                            <br />
                            <br />
                        </div>
                    </fieldset>
                    <hr />
                    <table style="margin: 0px auto;">
                        <tr>
                            <td>
                                <asp:Button ID="btnNuevoFormulario" CssClass="bluebutton" runat="server" Style="position: relative;
                                    left: 0px;" Visible="False" Text="Nuevo Formulario" OnClientClick="window.open('Default.aspx', '_self'); return false;" />
                            </td>
                        </tr>
                    </table>
                </div>
            </asp:Panel>
        </asp:Panel>
        <asp:Panel ID="pnlDownLoadForm" runat="server" Visible="False">
            <br />
            <center>
                <img src="Imagenes/AvisoIGJ.jpg" alt="" />
            </center>
            <br />
            <br />
            <fieldset style="margin: 0px auto; width: 300px;">
                <table style="margin: 0px auto;">
                    <tr>
                        <td>
                            <asp:ImageButton ID="ImgBtnDescForm" runat="server" ImageUrl="Imagenes/DescForm.png"
                                ToolTip="Descargar el formulario creado." OnClick="ImgBtnDescForm_Click" />
                        </td>
                        <td>
                            <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="Imagenes/NuevoForm.png"
                                ToolTip="Crear un nuevo formulario" OnClick="ImageButton2_Click" />
                        </td>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>
    </div>
</asp:Content>
