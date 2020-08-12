using System;
using System.Collections.Generic;

namespace NeroliTech.Server.Models
{
    public partial class ExporterShippingDetail
    {
        public Guid Id { get; set; }
        public string CciNo { get; set; }
        public DateTime ShipmentDate { get; set; }
        public string ShippingAgent { get; set; }
        public string CarrierVessel { get; set; }
        public string LoadingRefNo { get; set; }
        public string ExitPoint { get; set; }
        public string Destination { get; set; }
        public string ContainerNo { get; set; }

        public virtual ExporterDeclaration CciNoNavigation { get; set; }
    }
}
