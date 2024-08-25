namespace WebApp.Models
{
    public class PayingDetail
    {
       
            public int UserId { get; set; }
            public string UserName { get; set; }
            public string MeterNumber { get; set; }
            public DateOnly ReadingDate { get; set; }
            public decimal MeterReading { get; set; }
            public decimal BillAmount { get; set; }
       
    }
}
