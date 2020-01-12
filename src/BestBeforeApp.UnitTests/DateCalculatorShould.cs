using System;
using BestBeforeApp.Helpers;
using FluentAssertions;
using Xunit;

namespace BestBeforeApp.UnitTests
{
    public class DateCalculatorShould
    {

        [Theory]
        [InlineData(1, "vandaag")]
        [InlineData(2, "morgen")]
        [InlineData(3, "overmorgen")]
        [InlineData(4, "4 dagen")]
        [InlineData(7, "1 week")]
        [InlineData(14, "2 weken")]
        public void ReturnDays(int number, string expected) =>
            DateCalculator.HumanizeDate(DateTime.Now.AddDays(number)).Should().Be(expected);

        [Theory]
        [InlineData(1, "1 maand")]
        [InlineData(2, "2 maanden")]
        [InlineData(22, "1 jaar")]
        [InlineData(24, "2 jaar")]
        public void ReturnMonths(int number, string expected) =>
            DateCalculator.HumanizeDate(DateTime.Now.AddMonths(number)).Should().Be(expected);
    }
}
