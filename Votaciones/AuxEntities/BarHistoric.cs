using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Votaciones.AuxEntities
{
    [DataContract]
    public class BarHistoric
    {
        [DataMember]
        dynamic bar;
        [DataMember]
        List<dynamic> listaVotaciones;
        
     
        public static List<BarHistoric> getData()
        {
            var votacionBaresComensal = DbQueries.getVotacionBaresComensal(null);
            var barGanoxVeces = DbQueries.barGanoXVeces(null);

            return (from c in votacionBaresComensal
                    join t in
                        (from k in barGanoxVeces
                         group k by k.IDBAR into g
                         select new {
                         IdBar = g.Key,
                         veces = g.Count()}
                        )
                    on c.idbar equals t.IdBar
                    group new { c.IdComensal, c.nombreComensal, c.image, c.votos } by new { c.idbar, c.descripcion, c.nombreBar, t.veces } into g
                    select new BarHistoric
                    {
                        bar = new
                        {
                            idBar = g.Key.idbar,
                            descripcion = g.Key.descripcion.Substring(g.Key.descripcion.IndexOf("~/") + 1),
                            nombre = g.Key.nombreBar,
                            vecesGanador = g.Key.veces,

                        },
                        listaVotaciones = g.ToList<dynamic>().Select(c => new { c.IdComensal, c.nombreComensal, image = c.image.Substring(g.Key.descripcion.IndexOf("~/") + 1), c.votos }).ToList<dynamic>()
                    }).ToList();
        }
    }
}
