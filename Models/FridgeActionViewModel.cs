namespace FridgeManagement.Models
{
    public class FridgeActionViewModel
    {
        public IEnumerable<Fridge> Fridges { get; set; }
        public IEnumerable<FridgeAction> FridgeActions { get; set; }
    }

}
