using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Votaciones.Controls;
using Votaciones.Core;
using Votaciones.Utils;

namespace Votaciones
{
    public partial class _Default : BasePage
   {                   
            

        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender,e);
            try
            {
                if (!this.IsPostBack)
                {                                           
                    if (currentUser.datosSemana.semanaComensal != null)
                    {
                        setImagenPostVotacion();
                        cargarGraficas();
                        cargarDatosFinales();
                    }
                    if (currentUser.datosSemana.puedeVetar)
                    {
                        liVetaciones.Visible = true;
                        divVvetaciones.Visible = true;
                    }
                }
            }catch(Exception ex)
            {
                log.Error(ex);
            }
        }

        private void setImagenPostVotacion()
        {
            if (this.currentUser.datosSemana.esGanador.Value)
            {
                imagenResultadoVotacion.Src = "~/img/wingif.gif";
                ((SiteMaster)this.Master).setBackground(SiteMaster.fondo.Winner);
            }
        }

        private void cargarDatosFinales()
        {
            try
            {
               
                using (VotacionBaresEntities ve = new Votaciones.VotacionBaresEntities())
                {
                    int votos = 0;
                    //restaurante ganador
                    var listaResGanador = ve.Restaurante_Ganador(semanaActual.numSemana).ToList();
                    var resGanadores = (from c in listaResGanador
                                               join com in ve.Bar
                                               on c.IdBar equals com.idBar
                                               select (Votaciones.IPosibleGanador)com).ToList();
                    if (listaResGanador.Any())
                    {
                        //var bar = ve.Bar.Where(t => t.idBar == listaResGanador.FirstOrDefault().IdBar).First();
                        //this.palmaresBarGanador.setData(Palmares.TipoFondo.success, bar, listaResGanador.FirstOrDefault().Votos);
                        votos = listaResGanador.FirstOrDefault().Votos;
                        foreach (var item in resGanadores)
                        {
                            Control palmaresMachoAlfa = (Control)this.LoadControl("~\\Controls\\Palmares.ascx");
                            ((Palmares)palmaresMachoAlfa).setData(Palmares.TipoFondo.success, item, votos);
                            contenedorBar.Controls.Add(palmaresMachoAlfa);

                        }
                    }

                    //macho alfa
                    var listaMachoAlfa = ve.SemanaInforme.Where(t => t.idSemanaInforme == semanaActual.idSemana).Select(t => t.idComensal_Alfa).ToList();
                    var comensals = ve.Comensal_Ganador(semanaActual.numSemana).ToList();
                    var comensalesGanadores = (from c in comensals
                                               join com in ve.Comensales
                                               on c.IdComensal equals com.IdComensal
                                               select (Votaciones.IPosibleGanador)com).ToList();                   
                    if (comensals.Any())
                    {
                        votos = comensals.FirstOrDefault().Votos;
                        foreach (var item in comensalesGanadores)
                        {
                            Control palmaresMachoAlfa = (Control)this.LoadControl("~\\Controls\\Palmares.ascx");
                            ((Palmares)palmaresMachoAlfa).setData(Palmares.TipoFondo.info, item, votos);
                            contenedorAlfa.Controls.Add(palmaresMachoAlfa);

                        }
                    }

                    //macho Omega
                    var comensalsp = ve.Comensal_Perdedor(semanaActual.numSemana).ToList();
                    var comensalesGanadoresp = (from c in comensalsp
                                                join com in ve.Comensales
                                                on c.IdComensal equals com.IdComensal
                                                select (Votaciones.IPosibleGanador)com).ToList();
                    if (comensalsp.Count > 0)
                    {
                        votos = comensalsp.FirstOrDefault().Votos;
                        foreach (var item in comensalesGanadoresp)
                        {
                            Control palmaresMachOmega = (Control)this.LoadControl("~\\Controls\\Palmares.ascx");
                            ((Palmares)palmaresMachOmega).setData(Palmares.TipoFondo.danger, item, votos);
                            contenedorOmega.Controls.Add(palmaresMachOmega);
                        }
                    }
                    else
                        contenedorOmega.Visible = false;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }


        private void cargarGraficas()
        {
            string mierdaParseada = string.Empty;
            try
            {
                using (VotacionBaresEntities ve = new Votaciones.VotacionBaresEntities())
                {
                    //grafica bares votados
                    var lista = (from c in ve.VotacionesSemanales
                                 join b in ve.Bar
                                 on c.idBar equals b.idBar
                                 where c.idSemana == SemanasObserver.semanaActual.idSemana
                                 group c by b.nombre into grupo
                                 select new ClasesList.Votaciones.EstadisticasItem_Value
                                 {
                                     bar_Comensal = grupo.Key,
                                     Votos = (from g in grupo select g.votacion ? 1 : 0).Sum()
                                 });
                    
                    mierdaParseada = Votaciones.Utils.Parsers.ListToJSONParser(lista);
                    ScriptManager.RegisterStartupScript(this, new Bar().GetType(), "graficaBar", string.Format(GlobalConsts.C_SCRIPTCHART, bares.ClientID, mierdaParseada,"Bares ganadores"), true);

                    //grafica comensales votaron
                    lista = (from c in ve.VotacionesSemanales
                                 join b in ve.Comensales
                                 on c.idComensal equals b.IdComensal
                                 where c.idSemana == SemanasObserver.semanaActual.idSemana                                   
                                 group c by b.nombre into grupo
                                 select new ClasesList.Votaciones.EstadisticasItem_Value
                                 {
                                     bar_Comensal = grupo.Key,
                                     Votos = (from g in grupo select g.votacion ? 1 : 0).Sum()
                                 });
                    mierdaParseada = Votaciones.Utils.Parsers.ListToJSONParser(lista);
                    ScriptManager.RegisterStartupScript(this, new Comensales().GetType(), "graficaComensal", string.Format(GlobalConsts.C_SCRIPTCHART, comensales.ClientID, mierdaParseada,"Comensales"), true);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }        
    }
}