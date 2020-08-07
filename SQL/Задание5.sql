select p.pay_type,p.pay_date, p.pay_sum, sum(t.pay_sum) as 'total'
from payments p inner join payments t on p.pay_type = t.pay_type 
and p.pay_date >= t.pay_date
inner join 
( 
  select p1.pay_type, 
  sum(p1.pay_sum) as 'sum'
  from payments p1 
  group by p1.pay_type
  having sum(p1.pay_sum) in 
  (
    select max(pp.pay_sum) 
    from (select sum(pp.pay_sum) as 'pay_sum' from payments pp group by pp.pay_type) pp
  )
) t1 
  on p.pay_type = t1.pay_type
 
group by p.pay_type,p.pay_date, p.pay_sum
order by p.pay_type,p.pay_date,p.pay_sum
