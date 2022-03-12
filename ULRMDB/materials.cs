using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ULRMDB
{
    class materials
    {
        private int materialNumber { get;set; }
        private string materialName { get; set; }
        private string desc { get; set; }
        private string permitNumber { get; set; }
        private string rmCode { get; set; }
        private string productCode { get; set; }
        private string grade { get; set; }
        private bool batchManaged { get; set; }
        private bool processOrderRequired { get; set; }
        private bool isRawMaterial { get; set; }
        private bool carbonDrumRequired { get; set; }
        private int daysAllowed { get; set; }
        private int weightAllowed { get; set; }
    
        
    }
}
