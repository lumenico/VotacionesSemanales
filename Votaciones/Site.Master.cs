using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Votaciones
{
    public partial class SiteMaster : MasterPage
    {
        public enum fondo{
            Winner,
            Looser
        }

        public global::System.Web.UI.WebControls.Image imgUser;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        internal void setBackground(fondo fondo)
        {
            switch (fondo)
            {
                case SiteMaster.fondo.Winner:
                    body.Attributes.Add("class", "backgroundwin");
                    break;
                case SiteMaster.fondo.Looser:
                    break;
            }
        }
    }
}