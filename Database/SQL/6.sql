SELECT  ORG_NAME,COUNT(*)
FROM company as c JOIN people as p
ON c.ORG_CODE = p.ORG_CODE
GROUP BY ORG_NAME
ORDER BY COUNT(*) DESC