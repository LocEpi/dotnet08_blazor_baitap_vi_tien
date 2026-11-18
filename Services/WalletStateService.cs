using dotnet08_blazor_baitap_vi_tien.Models;

namespace dotnet08_blazor_baitap_vi_tien.Services;

/// <summary>
/// State dùng chung trong một Blazor Server circuit.
/// Đăng ký service bằng AddScoped trong Program.cs.
/// </summary>
public class WalletStateService
{
    public string OwnerName { get; private set; } = "Nguyễn Văn A";
    public decimal Balance { get; private set; } = 2_499_730m;
    public List<Transaction> Transactions { get; } = new();

    public event Action? OnChange;

    public WalletStateService()
    {
        // Dữ liệu mẫu để giao diện có lịch sử ngay lần chạy đầu.
        Transactions.AddRange(new[]
        {
            new Transaction { Type = "Nạp tiền", Amount = 500_000m, CreatedAt = DateTime.Today.AddHours(15), BalanceAfter = 2_499_730m },
            new Transaction { Type = "Rút tiền", Amount = 200_000m, CreatedAt = DateTime.Today.AddDays(-1).AddHours(10), BalanceAfter = 1_999_730m },
            new Transaction { Type = "Rút tiền", Amount = 200_000m, CreatedAt = DateTime.Today.AddDays(-1).AddHours(9), BalanceAfter = 2_199_730m },
            new Transaction { Type = "Nạp tiền", Amount = 500_000m, CreatedAt = DateTime.Today.AddDays(-2).AddHours(15), BalanceAfter = 2_399_730m }
        });
    }

    public (bool IsSuccess, string Message) Deposit(decimal amount)
    {
        if (amount <= 0)
            return (false, "Số tiền nạp phải lớn hơn 0.");

        if (amount > decimal.MaxValue - Balance)
            return (false, "Số tiền vượt quá giới hạn cho phép.");

        Balance += amount;
        AddTransaction("Nạp tiền", amount);
        NotifyStateChanged();
        return (true, $"Nạp thành công {amount:N0} ₫.");
    }

    public (bool IsSuccess, string Message) Withdraw(decimal amount)
    {
        if (amount <= 0)
            return (false, "Số tiền rút phải lớn hơn 0.");

        if (amount > Balance)
            return (false, "Số dư không đủ để thực hiện giao dịch.");

        Balance -= amount;
        AddTransaction("Rút tiền", amount);
        NotifyStateChanged();
        return (true, $"Rút thành công {amount:N0} ₫.");
    }

    private void AddTransaction(string type, decimal amount)
    {
        Transactions.Insert(0, new Transaction
        {
            Type = type,
            Amount = amount,
            BalanceAfter = Balance,
            CreatedAt = DateTime.Now
        });
    }

    private void NotifyStateChanged() => OnChange?.Invoke();
}
