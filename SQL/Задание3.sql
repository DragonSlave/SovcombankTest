WITH tree(id, pid, nam, path)
AS(
  SELECT id, pid, nam, CAST('root/' AS VARCHAR)
  FROM t
  WHERE pid IS NULL
  UNION ALL
  SELECT t.id, t.pid, t.nam, CAST(tree.[path]  + t.nam + '/' AS VARCHAR)
  FROM t INNER JOIN tree ON t.pid = tree.id 
  WHERE CAST(tree.[path]  + t.nam + '/' AS VARCHAR) NOT LIKE '%/node5/%'
)
SELECT * FROM tree
