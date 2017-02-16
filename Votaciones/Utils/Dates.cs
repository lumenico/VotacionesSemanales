using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Votaciones.Utils
{
    public class Dates 
    {

        public static string NowStr
        {
            get
            {
                return DateTime.Now.ToString("YYYYMMdd hh:mm:ss");
            }
        }       
    }
}