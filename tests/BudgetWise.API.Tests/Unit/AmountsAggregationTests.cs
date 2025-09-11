using Xunit;
using FluentAssertions;

namespace BudgetWise.API.Tests.Unit;

public class AmountsAggregationTests
{
    public static IEnumerable<object[]> SumCases() => new[]
    {
        new object[] { new decimal[] { 10.00m, -2.50m, 5.25m }, 12.75m },
        new object[] { new decimal[] { 0m, 0m, 0m }, 0m },
        new object[] { new decimal[] { 19.999m, 0.001m }, 20.00m },
    };

    [Theory]
    [MemberData(nameof(SumCases))]
    public void Sum_with_decimals_should_be_exact_and_rounded_to_2_places(decimal[] values, decimal expected)
    {
        var sum = values.Sum();
        Math.Round(sum, 2, MidpointRounding.AwayFromZero)
            .Should().Be(expected);
    }
}
