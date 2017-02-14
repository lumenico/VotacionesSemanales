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

namespace Votaciones
{
    public partial class _Default : Page
    {

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private const string C_VOTACION = "Votacion";
        private const string C_VOTAR = "votar";
        private const string C_SEMANA = "IDSEMANA";
        private const string C_SEMANACOMENSAL = "SemanaComensal";
        private const string C_SCRIPTCHART = "$(document).ready(function () {{ google.charts.load('current', {{ packages: ['corechart']}});google.charts.setOnLoadCallback(function(){{drawChart(document.getElementById('{0}'), '{1}', '{2}');}});}});";
        private const string C_EVENTARGUMENT = "__EVENTARGUMENT";
        private SemanaComensal haVotado
        {
            get
            {
                if (ViewState[C_SEMANACOMENSAL] == null)
                    return null;
                return (SemanaComensal)ViewState[C_SEMANACOMENSAL];
            }
            set
            {
                ViewState[C_SEMANACOMENSAL] = value;
            }
        }

        private Dictionary<int, bool> dicVotaciones
        {
            get
            {
                if (ViewState[C_VOTACION] == null)
                    ViewState[C_VOTACION] = new Dictionary<int, bool>();
                return (Dictionary<int, bool>)ViewState[C_VOTACION];
            }
            set
            {
                ViewState[C_VOTACION] = value;
            }
        }

        private Semanas semanaActual
        {
            get
            {
                if (ViewState[C_SEMANA] == null)
                    return null;
                return (Semanas)ViewState[C_SEMANA];
            }
            set
            {
                ViewState[C_SEMANA] = value;
            }
        }
  

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.IsPostBack)
                {                                        
                    semanaActual = SemanasObserver.semanaActual;                  
                    cargarGrid();
                    if (haVotado != null)
                    {
                        setImagenPostVotacion();
                        cargarGraficas();
                        cargarDatosFinales();
                    }
                }
            }catch(Exception ex)
            {
                log.Error(ex);
            }
        }

        private void setImagenPostVotacion()
        {
            if (CurrentUser.datosSemanaActual.esGanador.Value)
            {
                imagenResultadoVotacion.Src = "~/img/wingif.gif";
                //((SiteMaster)this.Master).setBackground(SiteMaster.fondo.Winner);
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
                    var listaResGanador = ve.Restaurante_Ganador(null).ToList();
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
                    var comensals = ve.Comensal_Ganador(null).ToList();
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
                    var comensalsp = ve.Comensal_Perdedor(null).ToList();
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
                    ScriptManager.RegisterStartupScript(this, new Bar().GetType(), "graficaBar", string.Format(C_SCRIPTCHART, bares.ClientID, mierdaParseada,"Bares ganadores"), true);


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
                    ScriptManager.RegisterStartupScript(this, new Comensales().GetType(), "graficaComensal", string.Format(C_SCRIPTCHART, comensales.ClientID, mierdaParseada,"Comensales"), true);

                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        

        private void cargarGrid()
        {
            try
            {
                using (VotacionBaresEntities ve = new Votaciones.VotacionBaresEntities())
                {


                    var comensal = CurrentUser.comensal;
                    var idcomensal = comensal.IdComensal;
                    ((SiteMaster)this.Master).imgUser.ImageUrl = comensal.image;
                    var list = ve.SemanaComensal.Where(t => t.idComensal == idcomensal && t.idSemana == semanaActual.idSemana).ToList();
                   

                    if (list.Any())
                    {
                        haVotado = list.First();
                        btnEnviarVotacion.Enabled = false;
                        btnEnviarVotacion.CssClass = "btn-info btn-lg";
                        btnEnviarVotacion.Text = string.Format("Votado: {0} a las {1}", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString());
                    }
                    var lista = ve.getVotacionByUser(idcomensal, semanaActual.idSemana).ToList();
                    
                    if (!this.IsPostBack)
                        dicVotaciones = lista.ToDictionary(t => t.idvotacion, u => u.Votacion);
                    gvVotaciones.DataSource = lista.ToList();
                    gvVotaciones.DataBind();
                    upVotacion.Update();              
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {

            CheckBox chk = (CheckBox)sender;
            chk.Checked = bool.Parse(this.Context.Request[C_EVENTARGUMENT].ToString());
            int rowIndex = int.Parse(chk.Attributes["itemindex"]);
            int idVotacion = int.Parse(((HiddenField)gvVotaciones.Items[rowIndex].FindControl("hidNombre")).Value.ToString());           
            if (dicVotaciones.Keys.Where(t => t.ToString() == idVotacion.ToString()).Count() > 0)
                dicVotaciones[idVotacion] = bool.Parse(this.Context.Request[C_EVENTARGUMENT].ToString());
            else
                dicVotaciones.Add(idVotacion, bool.Parse(this.Context.Request[C_EVENTARGUMENT].ToString()));
            cargarGrid();
        }


        protected void btnEnviarVotacion_Click(object sender, EventArgs e)
        {
            try
            {
                if (haVotado == null)
                {
                    XElement xmlElements = new XElement("B", dicVotaciones.Select(i => new XElement("ITEM", new XElement("Key", i.Key), new XElement("Value", i.Value ? 1 : 0))));
                    using (VotacionBaresEntities ve = new Votaciones.VotacionBaresEntities())
                    {
                       ve.setVotacionComensalSemana(xmlElements.ToString());
                       CurrentUser.set(CurrentUser.comensal.nombre);
                        cargarGrid();
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        protected void gvVotaciones_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Panel cb = ((Panel)e.Item.FindControl("divItem"));
                CheckBox chk = ((CheckBox)e.Item.FindControl("chkVoto"));
                cb.CssClass = "thumbnail";                                
                chk.Checked = dicVotaciones[((getVotacionByUser_Result)e.Item.DataItem).idvotacion];
                if (chk.Checked)
                    cb.CssClass += " itemSelected";
                if (haVotado == null)
                    cb.Attributes.Add("onclick","seleccionarCheck(this);");
            }
        }
    }
}