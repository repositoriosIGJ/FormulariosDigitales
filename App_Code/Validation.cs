using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;

/// <summary>
/// Summary description for Validation
/// </summary>
public class Validation
{
    public Validation()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static ServerValidateEventArgs NullValueValidation(ServerValidateEventArgs args)
    {
        args.IsValid = !string.IsNullOrEmpty(args.Value);
        return args;
    }
    ///<summary>
    ///Sobrecargado con msj de descripcion
    ///<param name="args">Parametro args del CustomValidator</param>
    ///<param name="msj">Parametro de salida que retorna el mensaje: "Dato obligatorio", que devuelve para asignar al ErrorMessage del CustomValidator</param>
    ///</summary>
    public static ServerValidateEventArgs NullValueValidation(ServerValidateEventArgs args,out string msj)
    {
        args.IsValid = !string.IsNullOrEmpty(args.Value);
        if (!args.IsValid) { 
            msj = "Dato obligatorio";
            return args;
        }// else { msj = ""; };
        msj="";
        return args;
    }
    public static ServerValidateEventArgs StringValueValidation(ServerValidateEventArgs args)
    {
        foreach (char ch in args.Value)
        {
            if (char.IsNumber(ch) || char.IsPunctuation(ch) || char.IsSymbol(ch))
            {
                args.IsValid = false;
                return args;
            }
        }
        return args;        
    }
    public static ServerValidateEventArgs NumericValueValidation(ServerValidateEventArgs args)
    {
        int p;
        args.IsValid = int.TryParse(args.Value, out p);
        return args;
    }
    ///<summary>
    ///Sobrecargado con msj de descripcion
    ///<param name="args">Parametro args del CustomValidator</param>
    ///<param name="msj">Parametro de salida que retorna el mensaje: "E-Mail no valido", que devuelve para asignar al ErrorMessage del CustomValidator</param>
    ///</summary>
    public static ServerValidateEventArgs EMailValueValidator(ServerValidateEventArgs args, out string msj)
    {
        Regex _isNumber = new Regex(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
        
        Match ok = _isNumber.Match(args.Value);
       
        args.IsValid = ok.Success;
        if (!args.IsValid) { msj = "E-Mail no valido"; } else { msj = "";}
        return args;
    }

    public static ServerValidateEventArgs StringNumericValidation(ServerValidateEventArgs args)
    {
        foreach (char ch in args.Value)
        {
            if (char.IsPunctuation(ch) || char.IsSymbol(ch))
            {
                args.IsValid = false;
                return args;
            }
        }
        return args;
    }
    public static ServerValidateEventArgs StringNumericValidation(ServerValidateEventArgs args, out string msj)
    {
        foreach (char ch in args.Value)
        {
            if (char.IsPunctuation(ch) || char.IsSymbol(ch))
            {
                args.IsValid = false;
                msj = "Dato Incorrecto.";
                return args;
            }
        }
        msj = "";
        return args;
    }


    public static ServerValidateEventArgs MaxCharactersValidation(ServerValidateEventArgs args, int cantMax)
    {
        if (args.Value.Length > cantMax)
        {
            args.IsValid = false;
        }
        return args;
    
    }
}
