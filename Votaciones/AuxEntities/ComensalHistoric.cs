using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Votaciones.AuxEntities
{
    [DataContract]
    public class ComensalHistoric
    {
        [DataMember]
        dynamic comensal;
        [DataMember]
        List<dynamic> listaVotaciones;


        public static List<ComensalHistoric> getData()
        {
            var votacionBaresComensal = DbQueries.getVotacionBaresComensal(null);
            var comensalGanoXVeces = DbQueries.comensalGanoXVeces(null);

            return (from c in votacionBaresComensal
                    join t in
                        (from k in comensalGanoXVeces
                         group k by k.IdComensal into g
                         select new
                         {
                             IdComensal = g.Key,
                             veces = g.Count()
                         }
                        )
                    on c.IdComensal equals t.IdComensal
                    group new { c.IdComensal, c.nombreComensal, c.image, c.votos } by new { c.IdComensal, c.image, c.nombreComensal, t.veces } into g
                    select new ComensalHistoric
                    {
                        comensal = new
                        {
                            idComensal = g.Key.IdComensal,
                            image = g.Key.image.Substring(g.Key.image.IndexOf("~/") + 1),
                            nombre = g.Key.nombreComensal,
                            vecesGanador = g.Key.veces,

                        },
                        listaVotaciones = g.ToList<dynamic>().Select(c => new { c.IdComensal, c.nombreComensal, image = c.image.Substring(g.Key.image.IndexOf("~/") + 1), c.votos }).ToList<dynamic>()
                    }).ToList();
        }
    }
}
