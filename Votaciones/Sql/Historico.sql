alter procedure votacionesBaresComensal(@idBar int =  null)
as
begin
	-- Votaciones de bares por comensales siempre
	select 
		c.IdComensal,
		c.nombre as nombreComensal,
		c.image,
		b.idbar,
		b.nombre as nombreBar,
		b.descripcion,
		--s.idsemana,
		count(*) as votos
	from
		comensales c 
		join VotacionesSemanales vc
			on vc.idComensal = c.IdComensal
		join bar b
			on b.idbar = vc.idBar
		join semanas s
			on s.idsemana = vc.idsemana
	where 
		votacion = 1
		and 1 = case when b.idBar = @idBar then 1
					 when @idBar is null then 1
					 else 0 end
	group by
		b.descripcion,
		c.IdComensal,
		c.nombre,
		b.nombre,
		c.image,
		b.idbar
		--s.idsemana
	order by
		c.nombre,b.nombre 
end
go
alter procedure barGanoXVeces(@idBar int)
	as
	begin
--Restaurante ganador x veces

;WITH CTE_GRAL AS (
		SELECT 
			B.IDBAR,
			b.nombre as nombreBar,
			s.idsemana,
			s.nombre as nombreSemana,
			sum(convert(int,votacion)) AS 'VOTOS'
		FROM 
			VOTACIONESSEMANALES V
			JOIN SEMANAS S 
				ON S.IDSEMANA = V.IDSEMANA
			JOIN BAR B 
				ON B.IDBAR = V.IDBAR
		WHERE 
			(V.VOTACION = 1)
		GROUP BY 
			B.IDBAR,
			b.nombre ,
			s.idsemana,
			s.nombre 
		)	
	SELECT 
		l.IDBAR, 
		l.nombreSemana,
		l.idsemana,		
		l.nombreBar,
		max(VOTOS) as votos
	FROM 
		CTE_GRAL L	
	--where
		--idsemana = @idsemana
		--1 = case when @idbar is null then 1
		--		 when l.idBar = @idBar then 1
		--		 else 0 end	
	GROUP BY 
		l.IDBAR, 
		l.nombreSemana,
		l.idsemana,		
		l.nombreBar	
	HAVING 
		MAX(VOTOS) = (SELECT MAX(VOTOS) FROM CTE_GRAL H where h.idSemana = l.idsemana)
	order by
		idsemana,idbar

end


		select 
		c.IdComensal,
		c.nombre,
		c.image,
		b.idbar,
		b.nombre,
		--s.idsemana,
		count(*) as votos
	from
		comensales c 
		join VotacionesSemanales vc
			on vc.idComensal = c.IdComensal
		join bar b
			on b.idbar = vc.idBar
		join semanas s
			on s.idsemana = vc.idsemana
	where 
		votacion = 1
	
	group by
		c.IdComensal,
		c.nombre,
		b.nombre,
		c.image,
		b.idbar
		--s.idsemana
	order by
		c.nombre,b.nombre 
--Bares votados por semana
select
	b.idbar,
	b.nombre,
	s.idsemana,
	s.nombre,
	count(*)
from
	bar b
	join VotacionesSemanales vc
		on vc.idbar = b.idbar	
	join comensales c
		on c.IdComensal = vc.IdComensal
	join semanas s
		on s.idsemana = vc.idsemana
where 
	s.idsemana = 5
	and votacion = 1
group by
	b.idbar,
	b.nombre,
	s.idsemana,
	s.nombre
go
--select 
--	*
--from
--	semanacomensal
--where
--	ganador = 1

alter procedure comensalGanoXVeces(@idcomensal int = null)
as
begin

;WITH CTE_GRAL AS (
		SELECT 
			B.IDBAR,
			b.nombre as nombreBar,
			s.idsemana,
			s.nombre as nombreSemana,
			sum(convert(int,votacion)) AS 'VOTOS',
			c.IdComensal,
			c.nombre as nombreComensal,
			c.image
		FROM 
			VOTACIONESSEMANALES V
			JOIN SEMANAS S 
				ON S.IDSEMANA = V.IDSEMANA
			JOIN BAR B 
				ON B.IDBAR = V.IDBAR
			join SemanaComensal sc
				on sc.idsemana = s.idsemana
			join comensales c
				on c.IdComensal = sc.idcomensal

		WHERE 
			(V.VOTACION = 1 and sc.ganador = 1)
			and 1 = case when @idcomensal is null then 1
						 when @idcomensal = c.idcomensal then 1
						 else 0 end
		GROUP BY 
			B.IDBAR,
			b.nombre ,
			s.idsemana,
			s.nombre,
			c.nombre,
			c.image,
			c.IdComensal	)	

	SELECT 
		 
		l.nombreSemana,
		l.idsemana,		
		l.IdComensal,
		l.nombreComensal,
		l.image,
		max(VOTOS) as votos
	FROM 
		CTE_GRAL L	

	GROUP BY 
	
		l.nombreSemana,
		l.idsemana,		
		l.IdComensal,
		l.nombreComensal,
		l.image	
	HAVING 
		MAX(VOTOS) = (SELECT MAX(VOTOS) FROM CTE_GRAL H where h.idSemana = l.idsemana)
	order by
		idsemana,nombrecomensal
end




