namespace EShop.Domain;
public class TollRule : BaseAuditableEntity
{
    public int Hour { get; set; }
    public int Minute { get; set; }
    public decimal Fee { get; set; }
}
