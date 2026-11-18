using dotnet08_blazor_baitap_vi_tien.Services;

var builder = WebApplication.CreateBuilder(args);

//Thêm thư viện server cho blazor server
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// Đăng ký WalletStateService
builder.Services.AddScoped<WalletStateService>();


var app = builder.Build();

//Middleware để chạy web .net blazor server side
app.UseRouting();
app.UseStaticFiles(); //Sử dụng thư mục wwwroot để chứa nội dung hình ảnh css cho website
app.MapBlazorHub(); // hub client để cập nhật giao diện
app.MapFallbackToPage("/_Host");//Chọn ra trang mặc định của web

app.Run();