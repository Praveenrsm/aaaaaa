using FarmTradeEntity;

namespace FarmApi.Model
{
    public class ProductCreationModel
    {
        public Product Product { get; set; }
        public List<String> ImageList { get; set; }
    }
}
