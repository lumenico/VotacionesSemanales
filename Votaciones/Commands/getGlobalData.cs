using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Votaciones.AuxEntities;
using Votaciones.Core;

namespace Votaciones.Commands
{

    public class getHistoric : BaseCommand<List<BarHistoric>>
    {
        public List<BarHistoric> execute()
        {
            return BarHistoric.getData();
           
        }
    }
}