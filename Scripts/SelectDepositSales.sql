SELECT 
	wt."DepositId", 
	sum(wt."Amount"), 
	CAST(CAST(sum(wt."Amount") AS double precision) / cast(d."Amount" AS double precision) * d."UsdCostBasis" AS decimal(16, 2)) "UsdCostBasis"
FROM "WithdrawalTransaction" wt 
JOIN "Deposit" d 
	ON d."Id" = wt."DepositId" 
GROUP BY wt."DepositId", d."Amount", d."UsdCostBasis";

SELECT 
	wt."DepositId", 
	CAST(CAST(sum(wt."Amount") AS double precision) / cast(w."Amount" AS double precision) * w."UsdProceeds"  AS decimal(16, 2)) "UsdProceeds"
FROM "WithdrawalTransaction" wt 
JOIN "Withdrawal" w 
	ON w."Id" = wt."WithdrawalId";
