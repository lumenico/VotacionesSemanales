//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Votaciones
{
    using System;
    using System.Collections.Generic;
    
    [Serializable]
    public partial class SemanaComensal
    {
        public int idSemanaComensal { get; set; }
        public int idSemana { get; set; }
        public int idComensal { get; set; }
        public System.DateTime fecha { get; set; }
        public bool ganador { get; set; }
    
        public virtual Comensales Comensales { get; set; }
        public virtual Semanas Semanas { get; set; }
    }
}