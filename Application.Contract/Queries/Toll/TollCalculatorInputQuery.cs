using MediatR;

namespace Application.Contract.Queries.Toll;
public class TollCalculatorInputQuery:IRequest<decimal>
{
    public string Vehicle { get; set; }

    public DateTime[] PassTimes { get; set; }
}
