using PPX_Pos;


namespace TestPPX
{
    class Program
    {
        static void Main(string[] args)
        {
          //you can change this line
          var Pos = new PassportX_POS(); 
           
          POS_Process.Load(Pos);
           
        }
    }


}
