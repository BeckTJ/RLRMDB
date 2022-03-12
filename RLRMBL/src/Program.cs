using System;
using System.Collections.Generic;

namespace RLRMBL
{
    class main
    {
        static void Main(string[] args)
        {


            material j = new material();
            //List<material> m = j.getMaterial();


            //foreach (material i in m)
            //{
            //    Console.WriteLine(i.number.ToString() + "\t" + i.name + i.nameAbreviation);
            //}

            material m = j.getSingleMaterial(45234);



            Console.WriteLine(m.number.ToString() + "\t" + m.nameAbreviation + "\t" + m.name + m.permitNumber);

        }
    }
}