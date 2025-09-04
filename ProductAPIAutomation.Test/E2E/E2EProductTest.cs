using ProductAPIAutomation.Core.Endpoints.Products;
using ProductAPIAutomation.Core.Endpoints.Products.models;
using ProductAPIAutomation.Core.Utils;
using ProductAPIAutomation.Core.Utils.models;
using ProductAPIAutomation.Test.CustomOrders;
using ProductAPIAutomation.Test.Utils;


namespace ProductAPIAutomation.Test.E2E
{
    [TestCaseOrderer("ProductAPIAutomation.Test.CustomOrders.PriorityOrderer", "ProductAPIAutomation.Test")]
    public class E2EProductTest
    {
        private static Product createdProduct;
        private static Product updatedProduct;

        [Fact, TestPriority(1)]
        public async Task Test_GetProducts_ReturnsAtLeastOneProduct()
        {
            string schemaJson = "get.products.schema.json";
            string productJson = "products.json";
            string expectedSchema = await FileReader.GetSchema(schemaJson);
            List<Product> expectedProduct = JsonParser.ParseJson<List<Product>>(await FileReader.GetJsonData(productJson));

            Response res = await ProductController.GetProducts();
            List<Product> actualProduct = JsonParser.ParseJson<List<Product>>(res.Content);
            bool isValidSchema = SchemaValidator.IsValidSchema(expectedSchema, res.Content);

            Assert.Equal(200, res.StatusCode);
            Assert.True(isValidSchema);
            Assert.True(actualProduct.Count > 0);
            Assert.Equal(expectedProduct, actualProduct);
        }


        [Fact, TestPriority(2)]
        public async Task Test_AddProduct_ReturnsCreatedProduct()
        {
            ProductData productData = new(2025, 2200.25, "Intel Core i12", "1 TB");
            Product product = new("Apple MacBook Pro 17", productData);

            string schemaJson = "add.product.schema.json";
            string expectedSchema = await FileReader.GetSchema(schemaJson);

            Response res = await ProductController.AddProduct(product);
            createdProduct = JsonParser.ParseJson<Product>(res.Content);
            bool isValidSchema = SchemaValidator.IsValidSchema(expectedSchema, res.Content);

            Assert.Equal(200, res.StatusCode);
            Assert.True(isValidSchema);
            Assert.Equal(product.Name, createdProduct.Name);
            Assert.Equal(product.Data, createdProduct.Data);
        }


        [Fact, TestPriority(3)]
        public async Task Test_GetProductById_ReturnsAProductByValidId()
        {

            string schemaJson = "get.product.schema.json";
            string expectedSchema = await FileReader.GetSchema(schemaJson);

            Response res = await ProductController.GetProductById(createdProduct.Id);
            Product actualProduct = JsonParser.ParseJson<Product>(res.Content);
            bool isValidSchema = SchemaValidator.IsValidSchema(expectedSchema, res.Content);

            Assert.Equal(200, res.StatusCode);
            Assert.True(isValidSchema);
            Assert.Equal(createdProduct, actualProduct);
        }


        [Fact, TestPriority(4)]
        public async Task Test_UpdateProduct_ReturnsUpdatedProduct()
        {
            ProductData productData = new(2025, 2200.25, "Intel Core i12", "1 TB");
            Product product = new("Apple MacBook Pro 17 Plus", productData);

            string schemaJson = "update.product.schema.json";
            string expectedSchema = await FileReader.GetSchema(schemaJson);

            Response res = await ProductController.UpdateProductById(createdProduct.Id, product);
            updatedProduct = JsonParser.ParseJson<Product>(res.Content);
            bool isValidSchema = SchemaValidator.IsValidSchema(expectedSchema, res.Content);

            Assert.Equal(200, res.StatusCode);
            Assert.True(isValidSchema);
            Assert.Equal(product.Name, updatedProduct.Name);
            Assert.Equal(product.Data, updatedProduct.Data);
        }


        [Fact, TestPriority(5)]
        public async Task Test_GetProductById_ReturnsUpdatedProduct()
        {
            string schemaJson = "get.product.schema.json";
            string expectedSchema = await FileReader.GetSchema(schemaJson);

            Response res = await ProductController.GetProductById(createdProduct.Id);
            Product actualProduct = JsonParser.ParseJson<Product>(res.Content);
            bool isValidSchema = SchemaValidator.IsValidSchema(expectedSchema, res.Content);

            Assert.Equal(200, res.StatusCode);
            Assert.True(isValidSchema);
            Assert.Equal(updatedProduct, actualProduct);
        }


        [Fact, TestPriority(6)]
        public async Task Test_DeleteProductById_ReturnsProductDelete()
        {
            string schemaJson = "delete.product.schema.json";
            string expectedSchema = await FileReader.GetSchema(schemaJson);
            MessageResponse expectedResponse = new() { Message = $"Object with id = {createdProduct.Id} has been deleted." };

            Response res = await ProductController.DeleteProductById(createdProduct.Id);
            MessageResponse actualResponse = JsonParser.ParseJson<MessageResponse>(res.Content);
            bool isValidSchema = SchemaValidator.IsValidSchema(expectedSchema, res.Content);

            Assert.Equal(200, res.StatusCode);
            Assert.Equal(expectedResponse, actualResponse);
            Assert.True(isValidSchema);
        }


        [Fact, TestPriority(7)]
        public async Task Test_GetProductById_ReturnsProductNotFoundError()
        {
            string schemaJson = "error.schema.json";
            string expectedSchema = await FileReader.GetSchema(schemaJson);
            ErrorResponse expectedResponse = new() { Error = $"Oject with id={createdProduct.Id} was not found." };

            Response res = await ProductController.GetProductById(createdProduct.Id);
            ErrorResponse actualResponse = JsonParser.ParseJson<ErrorResponse>(res.Content);
            bool isValidSchema = SchemaValidator.IsValidSchema(expectedSchema, res.Content);

            Assert.Equal(404, res.StatusCode);
            Assert.Equal(expectedResponse, actualResponse);
            Assert.True(isValidSchema);

        }
    }
}
