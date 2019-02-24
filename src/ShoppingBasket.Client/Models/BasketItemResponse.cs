// <auto-generated>
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace Swagger.Models
{
    using Newtonsoft.Json;
    using System.Linq;

    public partial class BasketItemResponse
    {
        /// <summary>
        /// Initializes a new instance of the BasketItemResponse class.
        /// </summary>
        public BasketItemResponse()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the BasketItemResponse class.
        /// </summary>
        public BasketItemResponse(string productName = default(string), double? price = default(double?), int? quantity = default(int?))
        {
            ProductName = productName;
            Price = price;
            Quantity = quantity;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "productName")]
        public string ProductName { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "price")]
        public double? Price { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "quantity")]
        public int? Quantity { get; set; }

    }
}
