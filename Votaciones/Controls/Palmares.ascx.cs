using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Votaciones.Controls
{
    public partial class Palmares : System.Web.UI.UserControl
    {
        private const string COMENSAL_TYPE = "Comensales";
        private const string BAR_TYPE = "Bar";

        public enum TipoFondo
        {
            success,
            info,
            danger,
            warning
        }

        private TipoFondo fondo { get; set; }
        private IPosibleGanador ganadores { get; set; }
        private int votos { get; set; }
        public void setData(TipoFondo fondo, IPosibleGanador ganadores, int votos)
        {
            this.fondo = fondo;
            this.ganadores = ganadores;
            this.votos = votos;      
                                          
            Palmares control = (Palmares)LoadControl("~/Controls/Palmares.ascx");

            switch (ganadores.GetType().BaseType.Name)
            {
                case COMENSAL_TYPE:
                    imgResGanador.Src = ((Comensales)ganadores).image;
                    lblGanadorOPerdedor.Text = string.Format("Comensal {0}", fondo == TipoFondo.danger ? "perdedor" : "ganador");
                    lblGanador.Text = ((Comensales)ganadores).nombre;
                    break;
                case BAR_TYPE:
                    imgResGanador.Src = ((Bar)ganadores).descripcion;
                    lblGanadorOPerdedor.Text = string.Format("Bar ganador con <span class='badge'><h2>{0}</h2></span> votos", this.votos);
                    lblGanador.Text = ((Bar)ganadores).nombre;
                    break;
            }
        }        
    }

}
