using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;
using System.Web;
using Votaciones.Utils;

namespace Votaciones.Core
{
    public class SemanasObserver
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
       

        private static Semanas _semanaActual;
        public static Semanas semanaActual
        {
            get
            {
                return _semanaActual;
            }
            private set
            {
                _semanaActual = value;
            }
        }

        private static void update()
        {
            using (VotacionBaresEntities ve = new Votaciones.VotacionBaresEntities())
            {
                semanaActual = ve.Semanas.Where(t => t.fechaIni <= DateTime.Now &&
                                       t.fechaFin >= DateTime.Now).FirstOrDefault<Semanas>();
            }
        }

        public static void semanaHandler()
        {
            using (VotacionBaresEntities ve = new Votaciones.VotacionBaresEntities())
            {
                semanaActual = ve.Semanas.Where(t => t.fechaIni <= DateTime.Now &&
                                              t.fechaFin >= DateTime.Now).FirstOrDefault<Semanas>();
            }
            ThreadStart ts1 = new ThreadStart(aperturaAutomaticaDeSemana);
            Thread tapertura = new Thread(ts1);
            tapertura.Start();

            ThreadStart ts2 = new ThreadStart(clausuraAutomaticaDeSemana);
            Thread tclausura = new Thread(ts2);
            tclausura.Start();
        }
        private static void aperturaAutomaticaDeSemana()
        {
            while (true)
            {
                try
                {
                    using (VotacionBaresEntities ve = new Votaciones.VotacionBaresEntities())
                    {
                       
                        var semanaCreada = ve.VotacionesSemanales.Where(t => t.idSemana == semanaActual.idSemana).FirstOrDefault();
                        if (semanaCreada == null)
                        {
                            try
                            {
                                ve.crearSemana();
                                update();
                            }
                            catch  { }
                        }
                    }
                    System.Threading.Thread.Sleep(3600000);
                }
                catch (Exception ex)
                {
                    log.Error("GLOBALASAX THREAD APERTURA!!!!!!!!_--------------------------------->" + ex);
                }
            }
        }
        private static void clausuraAutomaticaDeSemana()
        {
            while (true)
            {
                try
                {
                    
                        if (DateTime.Now.DayOfWeek == (DayOfWeek)int.Parse(ConfigurationManager.AppSettings[ConfigurationManagerExtended.Keys.dayOfWeekClousure])
                            && DateTime.Now.Hour >= int.Parse(ConfigurationManager.AppSettings[ConfigurationManagerExtended.Keys.hourClousure] )
                            && DateTime.Now.Minute >= int.Parse(ConfigurationManager.AppSettings[ConfigurationManagerExtended.Keys.minuteClousure]))
                        {
                            using (VotacionBaresEntities ve = new Votaciones.VotacionBaresEntities())
                            {
                                ve.cerrarSemana(semanaActual.idSemana);
                                update();                       
                            }
                            if(((CurrentUser)HttpContext.Current.Session[GlobalConsts.C_CURRENTUSER]) != null)
                                ((CurrentUser)HttpContext.Current.Session[GlobalConsts.C_CURRENTUSER]).set(((CurrentUser)HttpContext.Current.Session[GlobalConsts.C_CURRENTUSER]).comensal.nombre);
                        }
                    
                    System.Threading.Thread.Sleep(60000);
                }
                catch (Exception ex)
                {
                    log.Error("GLOBALASAX THREAD CLAUSURA!!!!!!!!_--------------------------------->" + ex);
                }
            }
        }

        
    }
}
