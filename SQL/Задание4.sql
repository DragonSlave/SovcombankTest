SELECT * FROM rates 
WHERE curr_id = 2 and date_rate = (
  SELECT MAX(date_rate) FROM rates 
  WHERE
  date_rate<='20100111'
)
