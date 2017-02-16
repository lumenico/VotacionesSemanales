using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Votaciones.Core;

namespace Votaciones.Controls
{
    public partial class SemanaActual : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblsemana.Text = SemanasObserver.semanaActual.numSemana.ToString();
            lblIni.Text = SemanasObserver.semanaActual.fechaIni.ToShortDateString();
            lblFin.Text = SemanasObserver.semanaActual.fechaFin.ToShortDateString();
            imagenCerrao.Visible = SemanasObserver.semanaActual.cerrada;
        }
    }
}