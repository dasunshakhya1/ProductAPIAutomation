using Xunit.Abstractions;
using Xunit.Sdk;

namespace ProductAPIAutomation.Test.CustomOrders
{
    public class PriorityOrderer : ITestCaseOrderer
    {
        public IEnumerable<TTestCase> OrderTestCases<TTestCase>(IEnumerable<TTestCase> testCases)
            where TTestCase : ITestCase
        {
        
            var sortedByPriority = new SortedDictionary<int, List<TTestCase>>();
            foreach (var testCase in testCases)
            {
                int priority = testCase.TestMethod.Method
                    .GetCustomAttributes(typeof(TestPriorityAttribute).AssemblyQualifiedName!)
                    .FirstOrDefault()
                    ?.GetNamedArgument<int>(nameof(TestPriorityAttribute.Priority)) ?? 0;

                if (!sortedByPriority.ContainsKey(priority))
                    sortedByPriority[priority] = new List<TTestCase>();

                sortedByPriority[priority].Add(testCase);
            }

     
            foreach (var priority in sortedByPriority.Keys)
            {
                var sortedTests = sortedByPriority[priority].OrderBy(t => t.TestMethod.Method.Name);
                foreach (var testCase in sortedTests)
                {
                    yield return testCase;
                }
            }
        }
    }
}