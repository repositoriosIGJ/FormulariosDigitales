﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión del motor en tiempo de ejecución:2.0.50727.5485
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FD.BusinessLayer.WsPagosBanelco
{
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "3.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Alta", Namespace="http://schemas.datacontract.org/2004/07/Gateway.Dominio.Entidades")]
    [System.SerializableAttribute()]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(FD.BusinessLayer.WsPagosBanelco.AltaFacturaResponse))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(FD.BusinessLayer.WsPagosBanelco.AltaFactura))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(FD.BusinessLayer.WsPagosBanelco.BajaFactura))]
    public partial class Alta : object, System.Runtime.Serialization.IExtensibleDataObject
    {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<System.DateTime> FechaField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime FechaVencimientoField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int HoraVencimientoField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string IdentificacionClienteField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string IdentificacionFacturaField;
        
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<System.DateTime> Fecha
        {
            get
            {
                return this.FechaField;
            }
            set
            {
                this.FechaField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime FechaVencimiento
        {
            get
            {
                return this.FechaVencimientoField;
            }
            set
            {
                this.FechaVencimientoField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int HoraVencimiento
        {
            get
            {
                return this.HoraVencimientoField;
            }
            set
            {
                this.HoraVencimientoField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Id
        {
            get
            {
                return this.IdField;
            }
            set
            {
                this.IdField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string IdentificacionCliente
        {
            get
            {
                return this.IdentificacionClienteField;
            }
            set
            {
                this.IdentificacionClienteField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string IdentificacionFactura
        {
            get
            {
                return this.IdentificacionFacturaField;
            }
            set
            {
                this.IdentificacionFacturaField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "3.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="AltaFacturaResponse", Namespace="http://schemas.datacontract.org/2004/07/Gateway.Dominio.Entidades")]
    [System.SerializableAttribute()]
    public partial class AltaFacturaResponse : FD.BusinessLayer.WsPagosBanelco.Alta
    {
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private long AuditNumberField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CodigoRespuestaField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int CodigoResultadoField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CodigoTransaccionField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public long AuditNumber
        {
            get
            {
                return this.AuditNumberField;
            }
            set
            {
                this.AuditNumberField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string CodigoRespuesta
        {
            get
            {
                return this.CodigoRespuestaField;
            }
            set
            {
                this.CodigoRespuestaField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int CodigoResultado
        {
            get
            {
                return this.CodigoResultadoField;
            }
            set
            {
                this.CodigoResultadoField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string CodigoTransaccion
        {
            get
            {
                return this.CodigoTransaccionField;
            }
            set
            {
                this.CodigoTransaccionField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "3.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="AltaFactura", Namespace="http://schemas.datacontract.org/2004/07/Gateway.Dominio.Entidades")]
    [System.SerializableAttribute()]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(FD.BusinessLayer.WsPagosBanelco.BajaFactura))]
    public partial class AltaFactura : FD.BusinessLayer.WsPagosBanelco.Alta
    {
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CodigoBancoField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CodigoOrganismoField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DescripcionPantallaField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DescripcionReciboField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private decimal MontoField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private long NumeroDocumentoField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int TipoDocumentoField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string CodigoBanco
        {
            get
            {
                return this.CodigoBancoField;
            }
            set
            {
                this.CodigoBancoField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string CodigoOrganismo
        {
            get
            {
                return this.CodigoOrganismoField;
            }
            set
            {
                this.CodigoOrganismoField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string DescripcionPantalla
        {
            get
            {
                return this.DescripcionPantallaField;
            }
            set
            {
                this.DescripcionPantallaField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string DescripcionRecibo
        {
            get
            {
                return this.DescripcionReciboField;
            }
            set
            {
                this.DescripcionReciboField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal Monto
        {
            get
            {
                return this.MontoField;
            }
            set
            {
                this.MontoField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public long NumeroDocumento
        {
            get
            {
                return this.NumeroDocumentoField;
            }
            set
            {
                this.NumeroDocumentoField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int TipoDocumento
        {
            get
            {
                return this.TipoDocumentoField;
            }
            set
            {
                this.TipoDocumentoField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "3.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="BajaFactura", Namespace="http://schemas.datacontract.org/2004/07/Gateway.Dominio.Entidades")]
    [System.SerializableAttribute()]
    public partial class BajaFactura : FD.BusinessLayer.WsPagosBanelco.AltaFactura
    {
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="FD.BusinessLayer.WsPagosBanelco.IFacturaServicio")]
    public interface IFacturaServicio
    {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFacturaServicio/CrearFacturaConAdhesion", ReplyAction="http://tempuri.org/IFacturaServicio/CrearFacturaConAdhesionResponse")]
        FD.BusinessLayer.WsPagosBanelco.AltaFacturaResponse CrearFacturaConAdhesion(FD.BusinessLayer.WsPagosBanelco.AltaFactura adhesion);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFacturaServicio/CrearFactura", ReplyAction="http://tempuri.org/IFacturaServicio/CrearFacturaResponse")]
        FD.BusinessLayer.WsPagosBanelco.AltaFacturaResponse CrearFactura(FD.BusinessLayer.WsPagosBanelco.AltaFactura alta);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFacturaServicio/DarDeBajaFactura", ReplyAction="http://tempuri.org/IFacturaServicio/DarDeBajaFacturaResponse")]
        FD.BusinessLayer.WsPagosBanelco.AltaFacturaResponse DarDeBajaFactura(FD.BusinessLayer.WsPagosBanelco.BajaFactura baja);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    public interface IFacturaServicioChannel : FD.BusinessLayer.WsPagosBanelco.IFacturaServicio, System.ServiceModel.IClientChannel
    {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    public partial class FacturaServicioClient : System.ServiceModel.ClientBase<FD.BusinessLayer.WsPagosBanelco.IFacturaServicio>, FD.BusinessLayer.WsPagosBanelco.IFacturaServicio
    {
        
        public FacturaServicioClient()
        {
        }
        
        public FacturaServicioClient(string endpointConfigurationName) : 
                base(endpointConfigurationName)
        {
        }
        
        public FacturaServicioClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress)
        {
        }
        
        public FacturaServicioClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress)
        {
        }
        
        public FacturaServicioClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress)
        {
        }
        
        public FD.BusinessLayer.WsPagosBanelco.AltaFacturaResponse CrearFacturaConAdhesion(FD.BusinessLayer.WsPagosBanelco.AltaFactura adhesion)
        {
            return base.Channel.CrearFacturaConAdhesion(adhesion);
        }
        
        public FD.BusinessLayer.WsPagosBanelco.AltaFacturaResponse CrearFactura(FD.BusinessLayer.WsPagosBanelco.AltaFactura alta)
        {
            return base.Channel.CrearFactura(alta);
        }
        
        public FD.BusinessLayer.WsPagosBanelco.AltaFacturaResponse DarDeBajaFactura(FD.BusinessLayer.WsPagosBanelco.BajaFactura baja)
        {
            return base.Channel.DarDeBajaFactura(baja);
        }
    }
}