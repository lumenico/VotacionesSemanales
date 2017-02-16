using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Votaciones.Core;

namespace Votaciones.Controls
{
   

    public partial class Vxtaciones : BaseUserControl
    {

        public enum VotacionOVetacion
        {
            Votacion,
            Vetacion
        }

        public string  tipo;
        private string C_VOTACION = "Votacion" + new Guid().ToString();
        private const string C_VOTAR = "votar";
        private const string C_EVENTARGUMENT = "__EVENTARGUMENT";

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

        protected override void Page_Load(object sender, EventArgs e)
        {            
            base.Page_Load(sender, e);
            try
            {
                if (!this.IsPostBack)
                {
                    cargarGrid();                    
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
                ((SiteMaster)this.Page.Master).imgUser.ImageUrl = currentUser.comensal.image;                    
                if (currentUser.datosSemana.semanaComensal != null || semanaActual.cerrada)
                {                      
                    btnEnviarVotacion.Enabled = false;
                    btnEnviarVotacion.CssClass = "btn-info btn-lg";
                    btnEnviarVotacion.Text = string.Format("Votado: {0} a las {1}", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString());
                }

                var lista = getData();

                if (!this.IsPostBack)
                    dicVotaciones = lista.ToDictionary(t => t.idvotacion, u => u.Votacion);
                gvVotaciones.DataSource = lista.ToList();
                gvVotaciones.DataBind();
                upVotacion.Update();                
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        private List<IVotaciones> getData()
        {
            using (VotacionBaresEntities ve = new Votaciones.VotacionBaresEntities())
            {
                if (this.tipo == VotacionOVetacion.Votacion.ToString())
                    return ve.getVotacionByUser(currentUser.comensal.IdComensal, semanaActual.idSemana).Cast<IVotaciones>().ToList();
                else
                    return ve.getVetacionByUser(currentUser.comensal.IdComensal, semanaActual.idSemana).Cast<IVotaciones>().ToList();
            }
        }

        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            chk.Checked = bool.Parse(this.Context.Request[C_EVENTARGUMENT].ToString());
            int rowIndex = int.Parse(chk.Attributes["itemindex"]);
            int idVotacion = int.Parse(((HiddenField)gvVotaciones.Items[rowIndex].FindControl("hidNombre")).Value.ToString());
            if (chk.Checked)
            {
                if (this.tipo == VotacionOVetacion.Vetacion.ToString() && dicVotaciones.Where(t => t.Value).Count() >= 3)
                {
                    cargarGrid();
                    return;
                }
            }
            

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
                if (currentUser.datosSemana.semanaComensal == null && !semanaActual.cerrada)
                {
                    XElement xmlElements = new XElement("B", dicVotaciones.Select(i => new XElement("ITEM", new XElement("Key", i.Key), new XElement("Value", i.Value ? 1 : 0))));                   
                    setShit(xmlElements.ToString());
                    currentUser.set(currentUser.comensal.nombre);
                    cargarGrid();                    
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        private void setShit(string v)
        {
            using (VotacionBaresEntities ve = new Votaciones.VotacionBaresEntities())
            {
                if (this.tipo == VotacionOVetacion.Votacion.ToString())
                    ve.setVotacionComensalSemana(v);
                else
                    ve.setVetacionComensalSemana(v);
            }
        }


        protected void gvVotaciones_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Panel cb = ((Panel)e.Item.FindControl("divItem"));
                CheckBox chk = ((CheckBox)e.Item.FindControl("chkVoto"));
                cb.CssClass = "thumbnail";
                chk.Checked = dicVotaciones[((IVotaciones)e.Item.DataItem).idvotacion];
                if (chk.Checked)
                    if(this.tipo == VotacionOVetacion.Votacion.ToString())
                        cb.CssClass += " itemSelected";
                    else
                        cb.CssClass += " itemSelectedVetao";
                if (currentUser.datosSemana.semanaComensal == null)
                    cb.Attributes.Add("onclick", "seleccionarCheck(this);");
            }
        }
    }
}