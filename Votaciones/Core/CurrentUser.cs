using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Votaciones.Core
{
    public class CurrentUser
    {   
        public static Comensales comensal;
       
        public class datosSemanaActual
        {
            public static SemanaComensal semanaComensal;
            public static VotacionesSemanales votacionesSemanales;
            public static bool? esGanador;
            public static bool puedeVetar;
        }

        internal static void set(string username)
        {
            using (VotacionBaresEntities ve = new Votaciones.VotacionBaresEntities())
            {
                comensal = ve.Comensales.ToList().Where(t => t.nombre.Trim() == username.Split(' ')[0]).ToList().FirstOrDefault();
                var semanaAnterior = ve.Semanas.Where(t => t.numSemana == SemanasObserver.semanaActual.numSemana-1).FirstOrDefault();
                
                datosSemanaActual.esGanador = ve.SemanaComensal.Where(t => t.idSemana == SemanasObserver.semanaActual.idSemana && t.idComensal == comensal.IdComensal).ToList().FirstOrDefault()?.ganador;
                datosSemanaActual.puedeVetar = ve.SemanaComensal.Where(t => t.idSemana == semanaAnterior.idSemana && t.idComensal == comensal.IdComensal).ToList().FirstOrDefault().ganador &&
                                               !SemanasObserver.semanaActual.cerrada;

                datosSemanaActual.semanaComensal = ve.SemanaComensal.Where(t => t.idSemana == SemanasObserver.semanaActual.idSemana && t.idComensal == comensal.IdComensal).ToList().FirstOrDefault();
                datosSemanaActual.votacionesSemanales = ve.VotacionesSemanales.Where(t => t.idSemana == SemanasObserver.semanaActual.idSemana && t.idComensal == comensal.IdComensal).ToList().FirstOrDefault();
            }
        }
    }
}