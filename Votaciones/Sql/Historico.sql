-- Votaciones de bares por comensales siempre
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

--Restaurante ganador cada semana

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
	GROUP BY 
		l.IDBAR, 
		l.nombreSemana,
		l.idsemana,		
		l.nombreBar
	--where
		--idsemana = @idsemana
		--idbar = @idbar
	HAVING 
		MAX(VOTOS) = (SELECT MAX(VOTOS) FROM CTE_GRAL H where h.idSemana = l.idsemana)
	order by
		idbar,idsemana
