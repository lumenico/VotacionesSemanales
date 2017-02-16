using System;
using System.Web.UI;
using Votaciones.Core;
using Votaciones.Utils;

namespace Votaciones
{
    public class BaseUserControl: UserControl
    {
        internal static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        internal CurrentUser currentUser
        {
            get
            {
                if (ViewState[GlobalConsts.C_CURRENTUSER] == null)
                    return null;
                return (CurrentUser)ViewState[GlobalConsts.C_CURRENTUSER];
            }
            set
            {
                ViewState[GlobalConsts.C_CURRENTUSER] = value;
            }
        }

        internal Semanas semanaActual
        {
            get
            {
                if (ViewState[GlobalConsts.C_SEMANA] == null)
                    return null;
                return (Semanas)ViewState[GlobalConsts.C_SEMANA];
            }
            set
            {
                ViewState[GlobalConsts.C_SEMANA] = value;
            }
        }

        protected virtual void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                currentUser = (CurrentUser)Session[GlobalConsts.C_CURRENTUSER];
                semanaActual = SemanasObserver.semanaActual;
            }
        }
    }
}