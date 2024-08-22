<%@ Page Language="C#" MasterPageFile="~/DigiForm.master" AutoEventWireup="true"
    CodeFile="Error.aspx.cs" Inherits="Error" Title="Inspección General de Jucticia - Formulario Digital" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <center>
        <div style="text-align: center">
            <asp:Image ID="Image1" runat="server" ImageUrl="~/imagenes/error.jpg" />
            <br />
            <hr />
            <h1>
                <asp:Label ID="lblError" Style="color: #E59B10;" runat="server" Text="SE HA GENERADO UN ERROR" />
            </h1>
            <br />
            <h2 style="padding: 5px; border-radius: 25px; word-wrap: break-word; word-break: normal;
                border: 2px dashed red !important;">
                <asp:Label ID="lblErrorMsg" Style="color: Red; padding: 5px;" runat="server" Text="Mensaje de Error"
                    Visible="false" />
            </h2>
            <br />
            <div style="padding: 5px; border-radius: 25px; word-wrap: break-word; word-break: normal;
                border: 2px solid green !important;">
                <asp:Label ID="lblMail" Style="color: Green; padding: 5px;"  runat="server" Text="Por favor, revise el mensaje de error e intente generar el formulario nuevamente en unos minutos. 
    Disculpe las molestias ocasionadas." Font-Size="Large"></asp:Label>
                <asp:Label ID="lblMailDesc" Style="color: Green; padding: 5px;" runat="server" Text="<a href='mailto:infoigj@jus.gov.ar?subject=Formulario'> infoigj@jus.gov.ar</a>"
                    Font-Size="Large"></asp:Label>
            </div>
            <br />
            <br />
        </div>
    </center>
</asp:Content>
