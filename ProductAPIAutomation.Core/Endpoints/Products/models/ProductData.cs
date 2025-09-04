using System.Text.Json.Serialization;

namespace ProductAPIAutomation.Core.Endpoints.Products.models
{
    public class ProductData
    {
        [JsonPropertyName("year")]
        public int Year { get; set; }

        [JsonPropertyName("price")]
        public double Price { get; set; }

        [JsonPropertyName("CPU model")]
        public string? CpuModel { get; set; }

        [JsonPropertyName("Hard disk size")]
        public string? HardDiskSize { get; set; }

        public ProductData() { }
        public ProductData(int year, double price, string? cpuModel, string? hardDiskSize)
        {
            Year = year;
            Price = price;
            CpuModel = cpuModel;
            HardDiskSize = hardDiskSize;
        }

        public override bool Equals(object? obj)
        {
            return obj is ProductData data &&
                   Year == data.Year &&
                   Price == data.Price &&
                   CpuModel == data.CpuModel &&
                   HardDiskSize == data.HardDiskSize;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Year, Price, CpuModel, HardDiskSize);
        }
    }



}
