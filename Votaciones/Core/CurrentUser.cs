using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Votaciones.Core
{
    [Serializable]
    public class CurrentUser
    {   
        public  Comensales comensal;
        public datosSemanaActual datosSemana;
        [Serializable]
        public class datosSemanaActual
        {
            public  SemanaComensal semanaComensal;
            public  VotacionesSemanales votacionesSemanales;
            public  bool? esGanador;
            public  bool puedeVetar;
        }

        internal  void set(string username)
        {
            using (VotacionBaresEntities ve = new Votaciones.VotacionBaresEntities())
            {
                comensal = ve.Comensales.ToList().Where(t => t.nombre.Trim().ToLower() == username.Split(' ')[0].ToLower()).ToList().FirstOrDefault();
                var semanaAnterior = ve.Semanas.Where(t => t.numSemana == SemanasObserver.semanaActual.numSemana-1).FirstOrDefault();
                this.datosSemana = new Core.CurrentUser.datosSemanaActual();
                var semanaComensal = ve.SemanaComensal.ToList().Where(t => t.idSemana == semanaAnterior.idSemana && t.idComensal == comensal.IdComensal).FirstOrDefault();
                this.datosSemana.esGanador = semanaComensal?.ganador;
                
                this.datosSemana.puedeVetar = semanaComensal != null ? semanaComensal.ganador : false &&
                                               !SemanasObserver.semanaActual.cerrada;

                this.datosSemana.semanaComensal = ve.SemanaComensal.Where(t => t.idSemana == SemanasObserver.semanaActual.idSemana && t.idComensal == comensal.IdComensal).ToList().FirstOrDefault();
                this.datosSemana.votacionesSemanales = ve.VotacionesSemanales.Where(t => t.idSemana == SemanasObserver.semanaActual.idSemana && t.idComensal == comensal.IdComensal).ToList().FirstOrDefault();
            }
        }
    }
}