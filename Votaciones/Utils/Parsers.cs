using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Votaciones.Utils
{
    public class Parsers
   {
        public static string ListToJSONParser(IQueryable<ClasesList.Votaciones.EstadisticasItem_Value> lista)
        {
            string mierdaParseada = string.Empty;
            mierdaParseada = "[";
            bool necesitaComa = false;
            foreach (var item in lista)
            {
                if (necesitaComa)
                    mierdaParseada += ",";
                necesitaComa = true;
                mierdaParseada += "[\"" + item.bar_Comensal + "\"," + item.Votos.ToString() + "]";
            }
            mierdaParseada += "]";
            return mierdaParseada;
        }
    }
}