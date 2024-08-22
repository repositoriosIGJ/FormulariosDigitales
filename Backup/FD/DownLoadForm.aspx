<%@ Page Language="C#" MasterPageFile="~/DigiForm.master" AutoEventWireup="true"
    CodeFile="DownLoadForm.aspx.cs" Inherits="Default3" Title="Inspección General de Justicia - Formulario Digital" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <center>
        <img src="Imagenes/AdvertenciaIGJ.jpg" alt="" />
    </center>
    <br />
    <br />
    <fieldset style="margin: 0px auto; width:300px;">
        <table style="margin: 0px auto;">
            <tr>
                <td>
                    <asp:ImageButton ID="ImgBtnDescForm" runat="server" ImageUrl="Imagenes/DescForm.png" ToolTip="Descargar el formulario creado." OnClick="ImgBtnDescForm_Click" />
                </td>
                <td>
                    <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="Imagenes/NuevoForm.png" ToolTip="Crear un nuevo formulario" OnClick="ImageButton2_Click" />
                </td>
            </tr>
        </table>
    </fieldset>
</asp:Content>
