using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Votaciones.Core;

namespace Votaciones.Commands
{
    public class Commands : BaseCommand<string>
    {

        string a;
        public string execute()
        {
            return "hola";
        }
    }
}