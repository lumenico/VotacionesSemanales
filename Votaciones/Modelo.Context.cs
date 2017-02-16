﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class VotacionBaresEntities : DbContext
    {
        public VotacionBaresEntities()
            : base("name=VotacionBaresEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Bar> Bar { get; set; }
        public virtual DbSet<Comensales> Comensales { get; set; }
        public virtual DbSet<SemanaComensal> SemanaComensal { get; set; }
        public virtual DbSet<SemanaInforme> SemanaInforme { get; set; }
        public virtual DbSet<Semanas> Semanas { get; set; }
        public virtual DbSet<VotacionesSemanales> VotacionesSemanales { get; set; }
    
        public virtual int crearSemana()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("crearSemana");
        }
    
        public virtual ObjectResult<getVotacionByUser_Result> getVotacionByUser(Nullable<int> idcomensal, Nullable<int> idsemana)
        {
            var idcomensalParameter = idcomensal.HasValue ?
                new ObjectParameter("idcomensal", idcomensal) :
                new ObjectParameter("idcomensal", typeof(int));
    
            var idsemanaParameter = idsemana.HasValue ?
                new ObjectParameter("idsemana", idsemana) :
                new ObjectParameter("idsemana", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<getVotacionByUser_Result>("getVotacionByUser", idcomensalParameter, idsemanaParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> setVotacionComensalSemana(string votacion)
        {
            var votacionParameter = votacion != null ?
                new ObjectParameter("votacion", votacion) :
                new ObjectParameter("votacion", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("setVotacionComensalSemana", votacionParameter);
        }
    
        [DbFunction("VotacionBaresEntities", "Restaurante_Ganador")]
        public virtual IQueryable<Restaurante_Ganador_Result> Restaurante_Ganador(Nullable<int> nUMERO_SEMANA)
        {
            var nUMERO_SEMANAParameter = nUMERO_SEMANA.HasValue ?
                new ObjectParameter("NUMERO_SEMANA", nUMERO_SEMANA) :
                new ObjectParameter("NUMERO_SEMANA", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<Restaurante_Ganador_Result>("[VotacionBaresEntities].[Restaurante_Ganador](@NUMERO_SEMANA)", nUMERO_SEMANAParameter);
        }
    
        [DbFunction("VotacionBaresEntities", "Comensal_Ganador")]
        public virtual IQueryable<Comensal_Ganador_Result> Comensal_Ganador(Nullable<int> nUMERO_SEMANA)
        {
            var nUMERO_SEMANAParameter = nUMERO_SEMANA.HasValue ?
                new ObjectParameter("NUMERO_SEMANA", nUMERO_SEMANA) :
                new ObjectParameter("NUMERO_SEMANA", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<Comensal_Ganador_Result>("[VotacionBaresEntities].[Comensal_Ganador](@NUMERO_SEMANA)", nUMERO_SEMANAParameter);
        }
    
        [DbFunction("VotacionBaresEntities", "Comensal_Perdedor")]
        public virtual IQueryable<Comensal_Perdedor_Result> Comensal_Perdedor(Nullable<int> nUMERO_SEMANA)
        {
            var nUMERO_SEMANAParameter = nUMERO_SEMANA.HasValue ?
                new ObjectParameter("NUMERO_SEMANA", nUMERO_SEMANA) :
                new ObjectParameter("NUMERO_SEMANA", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<Comensal_Perdedor_Result>("[VotacionBaresEntities].[Comensal_Perdedor](@NUMERO_SEMANA)", nUMERO_SEMANAParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> cerrarSemana(Nullable<int> idSemana)
        {
            var idSemanaParameter = idSemana.HasValue ?
                new ObjectParameter("idSemana", idSemana) :
                new ObjectParameter("idSemana", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("cerrarSemana", idSemanaParameter);
        }
    
        public virtual ObjectResult<getVetacionByUser_Result> getVetacionByUser(Nullable<int> idcomensal, Nullable<int> idsemana)
        {
            var idcomensalParameter = idcomensal.HasValue ?
                new ObjectParameter("idcomensal", idcomensal) :
                new ObjectParameter("idcomensal", typeof(int));
    
            var idsemanaParameter = idsemana.HasValue ?
                new ObjectParameter("idsemana", idsemana) :
                new ObjectParameter("idsemana", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<getVetacionByUser_Result>("getVetacionByUser", idcomensalParameter, idsemanaParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> setVetacionComensalSemana(string vetacion)
        {
            var vetacionParameter = vetacion != null ?
                new ObjectParameter("vetacion", vetacion) :
                new ObjectParameter("vetacion", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("setVetacionComensalSemana", vetacionParameter);
        }
    }
}
