CREATE VIEW "View_RemainingDeposit"
as
select 
    d."Id", 
    CAST(d."Amount" AS decimal(24, 24)) "Amount",
    CAST(d."Amount" - coalesce(r."Used", 0) AS decimal(24, 24)) "Remaining",
    CAST(d."UsdCostBasis" AS decimal(16, 2)) "UsdCostBasis",
    CAST(CAST(coalesce(r."Used", 0) AS double precision) / CAST(d."Amount" AS double precision) * d."UsdCostBasis" AS decimal(16, 2)) "RemainingUsdCostBasis"
from "Deposit" d
left join (
    select wt."DepositId", sum(wt."Amount") "Used"
    from "WithdrawalTransaction" wt
    group by wt."DepositId"
) r on r."DepositId" = d."Id"
where d."Amount" - coalesce(r."Used", 0) > 0 
order by d."PerformedAt" ASC;