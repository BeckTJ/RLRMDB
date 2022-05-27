using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RLRMWF
{
    internal class Selection
    {
        public List<Materials> getMaterialName()
        {
           Materials materialName = new Materials();
            return materialName.getMaterialNameList();
        }
    }
}
