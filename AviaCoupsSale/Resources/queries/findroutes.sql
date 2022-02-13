select distinct case when (select count (tff.ID_AK) from T_Flights tff where tff.ID_First_Parent_Flight = tf.ID_First_Parent_Flight) > 1 
			then  stuff((select ','+ sa.Name_AK from s_ak sa where sa.ID_AK = tf.ID_AK group by sa.name_ak for xml path(''), type).value('.','VARCHAR(max)'), 1, 1, '')
			else ak.Name_AK
			end as Авиакомпания, 
	   'Сургут' as Город_вылета, 
	   'Москва' as Город_прилета,
	   (select count(id_flight) - 1
	    from t_flights tff 
		where tff.ID_First_Parent_Flight = tf.ID_First_Parent_Flight) as кол_во_пересадок,
	   tf.id_first_parent_flight,
	   Date_Departure, Date_Arriving	   
from O_AK_Route o, S_AK ak, T_Flights tf, t_routes rt, S_Cities sc
where o.ID_AK = ak.ID_AK
and tf.ID_AK = o.ID_AK
and tf.id_route= rt.ID_route
and o.ID_Route = rt.ID_route
and rt.ID_City_1 = (select sc.ID_City from S_Cities sc where sc.Name_City = 'Сургут')
and rt.ID_City_2 = (select sc.ID_City from S_Cities sc where sc.Name_City = 'Москва')
