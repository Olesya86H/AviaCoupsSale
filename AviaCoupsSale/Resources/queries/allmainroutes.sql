select ak.Name_AK as Авиакомпания, 
	   (select c.Name_City from S_Cities c where c.ID_City = rt.ID_City_1) as Город_вылета, 
	   case when isnull(tf.ID_Parent_Flight, 0) = 0 
			then (select c.Name_City from S_Cities c where c.ID_City = rt.ID_City_2) 
			else (select c.Name_City from S_Cities c where c.ID_City = (select rtt.id_city_2 
																	    from t_routes rtt 
																		where rtt.ID_route = tf.id_route
																		and tf.ID_Flight = (select max(id_flight)
																							from t_flights tff
																							where tff.ID_First_Parent_Flight = tf.ID_First_Parent_Flight))) 
	   end as Город_прилета,
	   Date_Departure, Date_Arriving	   
from O_AK_Route o, S_AK ak, T_Flights tf, t_routes rt, S_Aircrafts sa
where o.ID_AK = ak.ID_AK
and tf.ID_AK = o.ID_AK
and tf.id_route= rt.ID_route
and o.ID_Route = rt.ID_route
and sa.id_aircraft = tf.id_aircraft
--and rt.ID_City_1 = (select sc.ID_City from S_Cities sc where sc.Name_City = @city1)
--and rt.ID_City_2 = (select sc.ID_City from S_Cities sc where sc.Name_City = @city2)