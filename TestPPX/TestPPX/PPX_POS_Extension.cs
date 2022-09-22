using PPX_Pos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestPPX
{
    public class PPX_POS_Extension : IPOS
    {
        private string WELCOME_MESSAGE = "Hello Passport-X {0} customer";
        private PassportX_POS Pos;
        
        public PPX_POS_Extension(PassportX_POS Pos, string Country)
        {
            WELCOME_MESSAGE = $"Pos.DisplayWelcomeScreen {Country} customer";
        }
        //public PPX_POS_Extension(string Country)
        //{
        //    WELCOME_MESSAGE = string.Format(WELCOME_MESSAGE, Country);
        //}

        public string DisplayWelcomeScreen()
        {
            return WELCOME_MESSAGE;
        }

        public void Load()
        {
            DisplayWelcomeScreen();
        }

        public Guid CreateCustomerOrder() { return Guid.NewGuid(); }



    }
}
