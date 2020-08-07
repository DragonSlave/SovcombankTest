with [dates] as (
    select convert(datetime, '2013-01-10') as [date] --start
    union all
    select dateadd(day, 1, [date])
    from [dates]
    where [date] < '2013-02-10' --end
)
select [date]
from [dates]
where [date] between '2013-01-10' and '2013-02-10'
option (maxrecursion 0)
