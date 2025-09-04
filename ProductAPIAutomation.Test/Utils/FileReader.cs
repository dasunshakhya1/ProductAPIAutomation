namespace ProductAPIAutomation.Test.Utils
{
    public static class FileReader
    {


        public static Task<string> ReadFile(string path)
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path);
            return File.ReadAllTextAsync(filePath);

        }


        public static Task<string> GetSchema(string relativePath)
        {
            string schemaPath = @$"..\..\..\Resources\Schemas\{relativePath}";
            return ReadFile(schemaPath);
        }

        public static Task<string> GetJsonData(string relativePath)
        {
            string schemaPath = @$"..\..\..\Resources\TestData\{relativePath}";
            return ReadFile(schemaPath);
        }
    }
}
