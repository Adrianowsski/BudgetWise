using System;
using System.Linq;
using Xunit;
using FluentAssertions;

namespace BudgetWise.API.Tests.Unit;

public class BudgetCalculationTests
{
    [Fact]
    public void Remaining_budget_should_not_go_below_zero()
    {
        var remaining = CalculateBudgetRemaining(100m, new[] { 40m, 70m });
        remaining.Should().Be(0m);
    }

    [Fact]
    public void Remaining_budget_should_be_rounded_to_2_decimals()
    {
        var remaining = CalculateBudgetRemaining(1000m, new[] { 200.10m, 99.90m });
        remaining.Should().Be(700.00m);
    }

    private static decimal CalculateBudgetRemaining(decimal monthlyBudget, IEnumerable<decimal> expenses)
    {
        var spent = expenses.Sum();
        var left = monthlyBudget - spent;
        if (left < 0m) left = 0m;
        return Math.Round(left, 2, MidpointRounding.AwayFromZero);
    }
}
