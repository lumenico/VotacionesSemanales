using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Votaciones
{
   
        public interface IVotaciones {
        int idvotacion { get; }
        bool Votacion { get; }
        string Nombre { get; set; }
        string descripcion { get; set; }

    }
    public partial class getVotacionByUser_Result : IVotaciones    {  }
    public partial class getVetacionByUser_Result : IVotaciones    {  }


    public interface IPosibleGanador { }
    public partial class Comensales : IPosibleGanador { }
    public partial class Bar : IPosibleGanador { }

}