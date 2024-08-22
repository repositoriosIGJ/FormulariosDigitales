using System;
using System.Data;
using System.Configuration;
using System.Web;

/// <summary>
/// Summary description for Operador
/// </summary>
public class Operador
{
    public Operador()
    { }

    //public Operador(double IdTrans, string pathImagen)
    //{
    //    this._idTrans = IdTrans;
    //    this._pathImagen = pathImagen;
    //}

    private double _idTrans;

    public double IdTrans
    {
        get { return _idTrans; }
        set { _idTrans = value; }
    }

    private string _pathImagen;

    public string pathImagen
    {
        get { return _pathImagen; }
        set { _pathImagen = value; }
    }

    private string  _codebar;

    public string  CodeBar
    {
        get { return _codebar; }
        set { _codebar = value; }
    }

    private bool _reserva;

    public bool Reserva
    {
        get { return _reserva; }
        set { _reserva = value; }
    }
	

   
	
	
	

}
