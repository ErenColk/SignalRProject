namespace SignalRWebUI.Dtos.BasketDtos
{
    public class ResultBasketDto
    {
        public int BasketID { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }
        public int Count { get; set; }

        //NAV

        public int ProductID { get; set; }
        public int MenuTableID { get; set; }
    }
}
