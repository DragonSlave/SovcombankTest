SELECT isnull(p2.pay_type,'') as 'pay_type', isnull(p2.month_y,'') as 'month', sum(pay_sum) as 'sum'
  FROM (SELECT p1.pay_type,
               format(p1.pay_date, 'MM.yyyy') AS month_y,
               p1.pay_sum
          FROM payments p1) p2
 GROUP BY ROLLUP(p2.pay_type, p2.month_y);
