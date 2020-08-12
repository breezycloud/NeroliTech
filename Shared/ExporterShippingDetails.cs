using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NeroliTech.Shared
{
    public class ExporterShippingDetails
    {
        [Key]
        public Guid Id { get; set; }
        public string CciNo { get; set; }
        public DateTime ShipmentDate { get; set; }
        public string ShippingAgent { get; set; }
        public string CarrierVessel { get; set; }
        public string LoadingRefNo { get; set; }
        public string ExitPoint { get; set; }
        public string Destination { get; set; }
        public string ContainerNo { get; set; }

    }
}
