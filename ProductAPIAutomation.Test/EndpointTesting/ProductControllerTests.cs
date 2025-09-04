using ProductAPIAutomation.Core.Endpoints.Products;
using ProductAPIAutomation.Core.Endpoints.Products.models;
using ProductAPIAutomation.Core.Utils;
using ProductAPIAutomation.Core.Utils.models;
using ProductAPIAutomation.Test.Utils;

namespace ProductAPIAutomation.Test.EndpointTesting
{
    public class ProductControllerTests 
    {
        private Product createdProduct;
        private readonly string validProductId = "7";
        private readonly string notExistingProductId = "989";
        private readonly Product product;
        private readonly ProductData productData;


        public ProductControllerTests()
        {
            productData = new(2025, 2200.25, "Intel Core i12", "1 TB");
            product = new("Apple MacBook Pro 17", productData);
        }



        [Fact]
        public async Task TestGetProductsReturnsAtLeastOneProduct()
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


        [Fact]
        public async Task TestGetProductByIdReturnsAProductByValidId()
        {

            string schemaJson = "get.product.schema.json";
            string productJson = "product.json";
            string expectedSchema = await FileReader.GetSchema(schemaJson);
            Product expectedProduct = JsonParser.ParseJson<Product>(await FileReader.GetJsonData(productJson));

            Response res = await ProductController.GetProductById(validProductId);
            Product actualProduct = JsonParser.ParseJson<Product>(res.Content);
            bool isValidSchema = SchemaValidator.IsValidSchema(expectedSchema, res.Content);

            Assert.Equal(200, res.StatusCode);
            Assert.True(isValidSchema);
            Assert.Equal(expectedProduct, actualProduct);
        }


        [Fact]
        public async Task TestGetProductByIdReturnsAnErrorForNonExistingId()
        {
            string schemaJson = "error.schema.json";
            string productJson = "error.json";

            string expectedSchema = await FileReader.GetSchema(schemaJson);
            Product expectedProduct = JsonParser.ParseJson<Product>(await FileReader.GetJsonData(productJson));

            Response res = await ProductController.GetProductById(notExistingProductId);
            Product actualProduct = JsonParser.ParseJson<Product>(res.Content);
            bool isValidSchema = SchemaValidator.IsValidSchema(expectedSchema, res.Content);

            Assert.Equal(404, res.StatusCode);
            Assert.True(isValidSchema);
            Assert.Equal(expectedProduct, actualProduct);
        }


        [Fact]
        public async Task TestAddProductReturnsCreatedProduct()
        {
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

        [Fact]
        public async Task TestUpdateProductReturnsUpdatedProduct()
        {
            string schemaJson = "update.product.schema.json";
            string expectedSchema = await FileReader.GetSchema(schemaJson);

            Response res = await ProductController.AddProduct(product);
            createdProduct = JsonParser.ParseJson<Product>(res.Content);

            product.Name = "Apple MacBook Pro 17 Plus";
            Response updateResponse = await ProductController.UpdateProductById(createdProduct.Id, product);
            Product updatedProduct = JsonParser.ParseJson<Product>(updateResponse.Content);
            bool isValidSchema = SchemaValidator.IsValidSchema(expectedSchema, updateResponse.Content);

            Assert.Equal(200, updateResponse.StatusCode);
            Assert.True(isValidSchema);
            Assert.Equal(product.Name, updatedProduct.Name);
            Assert.Equal(product.Data, updatedProduct.Data);
        }


        [Fact]
        public async Task TestUpdateProductReturnsAnErrorUpdateingNonExistingProduct()
        {
            string schemaJson = "error.schema.json";
            string expectedSchema = await FileReader.GetSchema(schemaJson);

            product.Name = "Apple MacBook Pro 17 Plus";
            Response updateResponse = await ProductController.UpdateProductById(notExistingProductId, product);
            bool isValidSchema = SchemaValidator.IsValidSchema(expectedSchema, updateResponse.Content);

            Assert.Equal(404, updateResponse.StatusCode);
            Assert.True(isValidSchema);

        }


        [Fact]
        public async Task TestUpdateProductReturnsPartiallyUpdatedProduct()
        {
            string schemaJson = "update.product.schema.json";
            string expectedSchema = await FileReader.GetSchema(schemaJson);

            Response res = await ProductController.AddProduct(product);
            createdProduct = JsonParser.ParseJson<Product>(res.Content);

            Product p = new() { Name = "Apple MacBook Pro 17 Custom" };
            Response updateResponse = await ProductController.UpdatePartOfProductById(createdProduct.Id, p);
            Product updatedProduct = JsonParser.ParseJson<Product>(updateResponse.Content);
            bool isValidSchema = SchemaValidator.IsValidSchema(expectedSchema, updateResponse.Content);

            Assert.Equal(200, updateResponse.StatusCode);
            Assert.True(isValidSchema);
            Assert.Equal(p.Name, updatedProduct.Name);
            Assert.Equal(product.Data, updatedProduct.Data);
        }



        [Fact]
        public async Task TestUpdateProductReturnsAnErrorPartiallyUpdateingNonExistingProduct()
        {
            string schemaJson = "error.schema.json";
            string expectedSchema = await FileReader.GetSchema(schemaJson);

            product.Name = "Apple MacBook Pro 17 Plus";
            Response updateResponse = await ProductController.UpdateProductById(notExistingProductId, product);
            bool isValidSchema = SchemaValidator.IsValidSchema(expectedSchema, updateResponse.Content);

            Assert.Equal(404, updateResponse.StatusCode);
            Assert.True(isValidSchema);

        }


        [Fact]
        public async Task TestDeleteProductByIdReturnsProductDelete()
        {
            string schemaJson = "delete.product.schema.json";
            string expectedSchema = await FileReader.GetSchema(schemaJson);

            Response res = await ProductController.AddProduct(product);
            createdProduct = JsonParser.ParseJson<Product>(res.Content);
            MessageResponse expectedResponse = new() { Message = $"Object with id = {createdProduct.Id} has been deleted." };

            Response deleteResponse = await ProductController.DeleteProductById(createdProduct.Id);
            MessageResponse actualResponse = JsonParser.ParseJson<MessageResponse>(deleteResponse.Content);
            bool isValidSchema = SchemaValidator.IsValidSchema(expectedSchema, deleteResponse.Content);

            Assert.Equal(200, res.StatusCode);
            Assert.Equal(expectedResponse, actualResponse);
            Assert.True(isValidSchema);
        }


        [Fact]
        public async Task TestUpdateProductDeleteNonExistingProduct()
        {
            string schemaJson = "error.schema.json";
            string expectedSchema = await FileReader.GetSchema(schemaJson);

            Response deleteResponse = await ProductController.DeleteProductById(notExistingProductId);
            MessageResponse actualResponse = JsonParser.ParseJson<MessageResponse>(deleteResponse.Content);
            bool isValidSchema = SchemaValidator.IsValidSchema(expectedSchema, deleteResponse.Content);

            Assert.Equal(404, deleteResponse.StatusCode);
            Assert.True(isValidSchema);

        }

    }
}