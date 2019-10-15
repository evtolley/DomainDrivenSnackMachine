namespace EvansSnackMachine.WebApi.ViewModels
{
    public class LoadSnackViewModel
    {
        public int Position { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string SnackName { get; set; }
    }
}
