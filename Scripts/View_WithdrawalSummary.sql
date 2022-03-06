CREATE VIEW "View_WithdrawalSummary"
as
select 
    w."Id", 
    CAST(coalesce(r."Used", 0) AS decimal(24, 24)) "Known",
    CAST(coalesce(r."UsedUsdCostBasis", 0) AS decimal(16, 2)) "KnownUsdCostBasis",
    CAST(w."Amount" - COALESCE(r."Used", 0) AS decimal(24, 24)) "Unknown"
from "Withdrawal" w
left join (
    select wt."WithdrawalId", sum(wt."Amount") "Used", sum(CAST(CAST(wt."Amount" AS double precision) / CAST(d."Amount" AS double precision) * d."UsdCostBasis" AS decimal(16, 2))) "UsedUsdCostBasis"
    from "WithdrawalTransaction" wt
    JOIN "Deposit" d
    	ON wt."DepositId" = d."Id"
    group by wt."WithdrawalId"
) r on r."WithdrawalId" = w."Id" 
order by w."PerformedAt" DESC;