WITH cte AS (
    SELECT 
        a,b, 
        ROW_NUMBER() OVER (
            PARTITION BY 
                a,b
            ORDER BY 
                a,b
        ) row_num
     FROM 
        t1
)

DELETE FROM cte
WHERE row_num > 1;

select * from t1;
