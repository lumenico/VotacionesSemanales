using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Votaciones
{
    public class DbQueries
    {
        public static List<votacionesBaresComensal_Result> getVotacionBaresComensal(int? idBar)
        {
            using (VotacionBaresEntities ve = new Votaciones.VotacionBaresEntities())
            {
                return ve.votacionesBaresComensal(idBar).ToList();
            }
        }

        public static List<barGanoXVeces_Result> barGanoXVeces(int? idBar)
        {
            using (VotacionBaresEntities ve = new Votaciones.VotacionBaresEntities())
            {
                return ve.barGanoXVeces(idBar).ToList();
            }
        }
    }
}