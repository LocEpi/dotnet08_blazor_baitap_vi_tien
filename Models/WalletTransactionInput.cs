using System.ComponentModel.DataAnnotations;

namespace dotnet08_blazor_baitap_vi_tien.Models;

/// <summary>Model nhập liệu dùng chung cho thao tác nạp và rút tiền.</summary>
public class WalletTransactionInput
{
    [Required(ErrorMessage = "Vui lòng nhập số tiền.")]
    [Range(typeof(decimal), "0.01", "999999999999", ErrorMessage = "Số tiền phải lớn hơn 0.")]
    public decimal? Amount { get; set; }
}
