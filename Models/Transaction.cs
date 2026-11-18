namespace dotnet08_blazor_baitap_vi_tien.Models;

public class Transaction
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    /// <summary>Loại giao dịch: "Nạp tiền" hoặc "Rút tiền".</summary>
    public string Type { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public decimal BalanceAfter { get; set; }
}
