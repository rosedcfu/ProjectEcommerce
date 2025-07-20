namespace ProjectEcommerce.Models.ViewModels
{
    public class CustomerDashboardViewModel
    {
        public int CartItemCount { get; set; }
        public List<OrderSummaryDto> RecentOrders { get; set; }
    }

    public class OrderSummaryDto
    {
        public int OrderId { get; set; }
        public DateTime DatePlaced { get; set; }
        public decimal Total { get; set; }
    }

}
