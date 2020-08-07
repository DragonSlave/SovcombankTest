CREATE TABLE t1
    ([a] INT, [b] INT)
;

CREATE TABLE t2
    ([a] INT, [b] INT)
;

INSERT INTO t1
    ([a], [b])
VALUES
    (1,1),
    (1,1),
    (4,4),
    (3,3),
    (3,3),
    (1,1)
;


INSERT INTO t2
    ([a], [b])
VALUES
    (4,4),
    (1,1),
    (4,4),
    (1,1),
    (3,3),
    (1,1)
;

select 'union', count(*) from (
  select distinct t1.[a], t1.[b] from t1 
  union 
  select distinct t2.[a], t2.[b] from t2) t
union 
select 't1', count(*) from (select distinct t1.[a], t1.[b] from t1) t1
union
select 't2', count(*) from (select distinct t2.[a], t2.[b] from t2) t2



