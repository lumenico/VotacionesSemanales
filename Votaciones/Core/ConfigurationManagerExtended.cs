using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Votaciones.Core
{
    public class ConfigurationManagerExtended
    {
        public  struct Keys
        {
            public  const string dayOfWeekClousure = "dayOfWeekClousure";
            public  const string hourClousure = "hourClousure";
            public  const string minuteClousure = "minuteClousure";
        }
    }
}