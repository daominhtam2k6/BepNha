namespace BepNha.Web.Models.ViewModels
{
    public class SearchResultVm
    {
        public List<OrderSummaryVm> Orders { get; set; } = new();
        public List<BookingVm> Bookings { get; set; } = new();
        public string Phone { get; set; } = string.Empty;
    }
}
