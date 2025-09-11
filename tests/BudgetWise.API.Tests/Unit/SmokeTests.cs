using Xunit;
using FluentAssertions;

namespace BudgetWise.API.Tests.Unit;

public class SmokeTests
{
    [Fact]
    public void Truth_should_hold()
    {
        true.Should().BeTrue();
    }
}

