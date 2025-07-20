namespace ProjectEcommerce.Models.ViewModels
{
    public class AdminDashboardViewModel
    {
        public int TotalProducts { get; set; }
        public int TotalOrders { get; set; }
        public decimal TotalSales { get; set; }
        public List<ProductSalesDto> TopProducts { get; set; }
        public int TotalCustomers { get; set; }
    }

    public class ProductSalesDto
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public int UnitsSold { get; set; }
        public decimal Revenue { get; set; }
    }
}
