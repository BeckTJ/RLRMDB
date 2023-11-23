﻿using RavenDB.Models;

namespace RavenDB.DTO
{
    public class RawMaterialDTO
    {
        public string? ProductId { get; set; }
        public int BatchNumber { get; set; }
        public string? ContainerNumber { get; set; }
        public string? SampleSubmitNumber { get; set; } 
        public int DrumWeight { get; set; }
        public long InspectionLotNumber { get; set; }
    }
}