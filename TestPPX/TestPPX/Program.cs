using PPX_Pos;
using System;

namespace TestPPX
{
    class Program
    {
        static void Main(string[] args)
        {
            string Country = "Italy";
            Console.WriteLine("Input your country; \n");
            Country = Console.ReadLine();

            //you can change this line
            var Pos = new PassportX_POS();
            var Pos_New = new PPX_POS_Extension(Pos,Country);

            POS_Process.Load(Pos_New);

        }
    }


}
