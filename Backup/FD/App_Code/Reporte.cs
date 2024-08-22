using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
//using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using FD.Entities;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;
//using System.Drawing;

/// <summary>
/// Summary description for Reporte
/// </summary>

//public class Reporte
//{
//    private bool bRet = false;
//    //Creo la instancia del objeto iTextSharp.text.Document:
//    Document oPDF;
//    Font oFontTitulo;
//    Font oFontClave;
//    Font oFontValor;
//    Cell cell;

//    public Reporte(string path, Transaccion oTransaccion)
//    {
//        this.Path = path;
//        this._transaccion = oTransaccion;
//        //this._tramite = oTramite;
//        oPDF = new Document(PageSize.A4, 0f, 0f, 50f, 50f);
//    }

//    private string _path;

//    public string Path
//    {
//        get { return _path; }
//        set { _path = value; }
//    }

//    private Transaccion _transaccion;

//    public Transaccion Transaccion
//    {
//        get { return _transaccion; }
//        set { _transaccion = value; }
//    }
	
//    //private Firmante _firmante;

//    //public Firmante Firmante
//    //{
//    //    get { return _firmante; }
//    //    set { _firmante = value; }
//    //}

//    //private Tramite _tramite;

//    //public Tramite Tramite
//    //{
//    //    get { return _tramite; }
//    //    set { _tramite = value; }
//    //}

//    public bool CrearPDF()
//    {
//        bRet = false;

//        try
//        {
//            //Creo un escritor que "escucha" al documento y escribe el documento en el Stream que queramos
//            PdfWriter.GetInstance(oPDF, new FileStream(this._path, FileMode.Create));
//            //Abro el documento
//            oPDF.Open();
            
//            //Configuro las fuentes
//            BaseFont bFontTitulo = BaseFont.CreateFont(BaseFont.COURIER, BaseFont.CP1252, BaseFont.EMBEDDED);
//            BaseFont bFontClave = BaseFont.CreateFont(BaseFont.COURIER_BOLD, BaseFont.CP1252, BaseFont.EMBEDDED);
//            BaseFont bFontValor = BaseFont.CreateFont(BaseFont.COURIER_OBLIQUE, BaseFont.CP1252, BaseFont.EMBEDDED);
//            //TITULO
//            oFontTitulo = new Font(bFontTitulo);
//            oFontTitulo.Size = 17f;
//            //CLAVE
//            oFontClave = new Font(bFontClave);
//            oFontClave.Size = 15f;
//            //VALOR
//            oFontValor = new Font(bFontValor);
//            oFontValor.Size = 9f;


//            //Armo la tabla Header
//            armadoTableHeader();
//            //SEPARADOR
//            armadoTableSeparadora();
//            //Armo la tabla de firmantes
//            armadoTableFirmante();
//            //SEPARADOR
//            armadoTableSeparadora();
//            //Armo tabla Tramite
//            armadoTableTramite();
//            //SEPARADOR    
//            armadoTableSeparadora();
//            //Armo tabla entidad
//            armadoTableEntidad();

//            bRet = true;
//        }
//        catch (Exception ex)
//        {
//            throw ex;
//        }
//        finally
//        {
//            oPDF.Close();
//        }
//        return bRet;
//    }

//    private void armadoTableHeader()
//    {
//        //La tabla sale de iTextSharp.text.Table
//        Table tablaHeader = new Table(3);
//        tablaHeader.Padding = 1;
//        tablaHeader.TableFitsPage = true;
//        tablaHeader.AutoFillEmptyCells = true;

//        cell = new Cell(new Phrase("INSPECCION GENERAL DE JUSTICIA", oFontTitulo));
//        cell.Header = true;
//        cell.HorizontalAlignment = Element.ALIGN_CENTER;
//        tablaHeader.AddCell(cell);

//        cell = new Cell(new Phrase("FORMULARIO: " + this._transaccion.Formulario.Id.ToString(), oFontTitulo));
//        cell.Header = true;
//        cell.HorizontalAlignment = Element.ALIGN_CENTER;
//        tablaHeader.AddCell(cell);

//        cell = new Cell(new Phrase("FECHA", oFontTitulo));
//        cell.Header = true;
//        cell.HorizontalAlignment = Element.ALIGN_CENTER;
//        tablaHeader.AddCell(cell);

//        oPDF.Add(tablaHeader);
//    }

//    private void armadoTableFirmante()
//    {
//        Table tableFirmante = new Table(2);
//        tableFirmante.Padding = 1;
//        tableFirmante.TableFitsPage = true;
//        tableFirmante.AutoFillEmptyCells = true;

//        //TITULO
//        cell = new Cell(new Phrase("DATOS DEL FIRMANTE", oFontClave));
//        cell.Header = true;
//        cell.Colspan = 2;
//        cell.HorizontalAlignment = Element.ALIGN_CENTER;
//        tableFirmante.AddCell(cell);

//        //CLAVE DNI
//        cell = new Cell(new Phrase("DNI: ", oFontClave));
//        cell.HorizontalAlignment = Element.ALIGN_LEFT;
//        cell.Width = Element.AUTHOR;
//        tableFirmante.AddCell(cell);
//        //VALOR DNI
//        cell = new Cell(new Phrase(this._transaccion.Firmante.DNI.ToString(), oFontValor));
//        cell.HorizontalAlignment = Element.ALIGN_LEFT;
//        tableFirmante.AddCell(cell);
//        //CLAVE NOMBRE
//        cell = new Cell(new Phrase("Nombre: ", oFontClave));
//        cell.HorizontalAlignment = Element.ALIGN_LEFT;
//        tableFirmante.AddCell(cell);
//        //VALOR NOMBRE
//        cell = new Cell(new Phrase(this._transaccion.Firmante.Nombre.ToString(), oFontValor));
//        cell.HorizontalAlignment = Element.ALIGN_LEFT;
//        tableFirmante.AddCell(cell);
//        //CLAVE APELLIDO
//        cell = new Cell(new Phrase("Apellido: ", oFontClave));
//        cell.HorizontalAlignment = Element.ALIGN_LEFT;
//        tableFirmante.AddCell(cell);
//        //VALOR APELLIDO
//        cell = new Cell(new Phrase(this._transaccion.Firmante.Apellido.ToString(), oFontValor));
//        cell.HorizontalAlignment = Element.ALIGN_LEFT;
//        tableFirmante.AddCell(cell);
//        //CLAVE MAIL
//        cell = new Cell(new Phrase("Mail: ", oFontClave));
//        cell.HorizontalAlignment = Element.ALIGN_LEFT;
//        tableFirmante.AddCell(cell);
//        //VALOR MAIL
//        cell = new Cell(new Phrase(this._transaccion.Firmante.Mail.ToString(), oFontValor));
//        cell.HorizontalAlignment = Element.ALIGN_LEFT;
//        tableFirmante.AddCell(cell);

//        oPDF.Add(tableFirmante);
//    }

//    private void armadoTableSeparadora()
//    {
//        Table tableSeparadora = new Table(1);
//        tableSeparadora.Padding = 5;
//        tableSeparadora.Border = Rectangle.NO_BORDER;
//        cell = new Cell("");
//        cell.Border = Rectangle.NO_BORDER;
//        tableSeparadora.AddCell(cell);
//        oPDF.Add(tableSeparadora);
//    }

//    private void armadoTableTramite()
//    {
//        Table tableTramite = new Table(2, 4);
//        tableTramite.Padding = 1;
//        tableTramite.TableFitsPage = true;
//        tableTramite.AutoFillEmptyCells = true;

//        cell = new Cell(new Phrase("DATOS DEL TRAMITE", oFontClave));
//        cell.Colspan = 2;
//        cell.Header = true;
//        cell.HorizontalAlignment = Element.ALIGN_CENTER;
//        tableTramite.AddCell(cell);

//        cell = new Cell(new Phrase("Tipo de Entidad: " + this._transaccion.Entidad.Nombre.ToString(), oFontClave));
//        cell.Colspan = 2;
//        cell.HorizontalAlignment = Element.ALIGN_LEFT;
//        tableTramite.AddCell(cell);

//        //cell = new Cell(new Phrase("N° formulario: " + this._transaccion.Formulario.Id.ToString(), oFontClave));
//        //cell.HorizontalAlignment = Element.ALIGN_LEFT;
//        //tableTramite.AddCell(cell);
//        cell = new Cell(new Phrase("Precio: $" + this._transaccion.Formulario.Precio.ToString(), oFontClave));
//        cell.HorizontalAlignment = Element.ALIGN_LEFT;
//        tableTramite.AddCell(cell);
//        cell = new Cell(new Phrase("Urgente: " + this._transaccion.Tramite.FlagUrgente.ToString(), oFontClave));
//        cell.HorizontalAlignment = Element.ALIGN_LEFT;
//        tableTramite.AddCell(cell);

//        cell = new Cell(new Phrase("Descripcion Trámite: " + this._transaccion.Tramite.Descripcion.ToString(), oFontClave));
//        cell.Colspan = 2;
//        cell.HorizontalAlignment = Element.ALIGN_LEFT;
//        tableTramite.AddCell(cell);

//        oPDF.Add(tableTramite);
//    }

//    private void armadoTableEntidad()
//    {
//        Table tableEntidad = new Table(1);
//        tableEntidad.Padding = 1;
//        tableEntidad.TableFitsPage = true;
//        tableEntidad.AutoFillEmptyCells = true;

//        cell = new Cell(new Phrase("DATOS DE LA ENTIDAD", oFontClave));
//        cell.Header = true;
//        cell.HorizontalAlignment = Element.ALIGN_CENTER;
//        tableEntidad.AddCell(cell);

//        cell = new Cell(new Phrase("N° Correlativo: " + this._transaccion.Entidad.NroCorrelativo.ToString(), oFontClave));
//        cell.HorizontalAlignment = Element.ALIGN_LEFT;
//        tableEntidad.AddCell(cell);

//        cell = new Cell(new Phrase("Nombre de la entidad: " + this._transaccion.Entidad.Nombre.ToString(), oFontClave));
//        cell.HorizontalAlignment = Element.ALIGN_LEFT;
//        tableEntidad.AddCell(cell);

//        cell = new Cell(new Phrase("Motivo: " + this._transaccion.Motivo.ToString(), oFontClave));
//        cell.HorizontalAlignment = Element.ALIGN_LEFT;
//        tableEntidad.AddCell(cell);

//        oPDF.Add(tableEntidad);
//    }

//}
