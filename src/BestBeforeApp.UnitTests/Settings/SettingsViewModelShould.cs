using BestBeforeApp.Settings;
using BestBeforeApp.Shared;
using FluentAssertions;
using MediatR;
using Moq;
using Xunit;

namespace BestBeforeApp.UnitTests.Settings
{
    public class SettingsViewModelShould
    {
        [Fact]
        public void Construct() =>
            new SettingsViewModel(
                new Mock<IMediator>().Object,
                new Mock<IPreferenceService>().Object).Should().BeOfType<SettingsViewModel>();

        [Fact]
        public void ExecutePlusCommand()
        {
            var preferenceServiceMock = new Mock<IPreferenceService>();
            preferenceServiceMock
                .SetupSet(x => x.NumberOfDaysBeforeExpirationDate = 1)
                .Verifiable();

            var viewModel = new SettingsViewModel(new Mock<IMediator>().Object, preferenceServiceMock.Object);
            viewModel.PlusCommand.CanExecute(null).Should().BeTrue();
            viewModel.PlusCommand.Execute(null);

            preferenceServiceMock.VerifySet(x => x.NumberOfDaysBeforeExpirationDate = 1, Times.Once);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void CannotExecuteMinusCommand(int value)
        {
            var preferenceServiceMock = new Mock<IPreferenceService>();
            preferenceServiceMock
                .SetupGet(x => x.NumberOfDaysBeforeExpirationDate).Returns(value)
                .Verifiable();

            var viewModel = new SettingsViewModel(new Mock<IMediator>().Object, preferenceServiceMock.Object);
            viewModel.PlusCommand.CanExecute(null).Should().BeFalse();
        }

        [Fact]
        public void ExecuteMinusCommand()
        {
            var preferenceServiceMock = new Mock<IPreferenceService>();
            preferenceServiceMock
                .SetupSet(x => x.NumberOfDaysBeforeExpirationDate = 1)
                .Verifiable();

            var viewModel = new SettingsViewModel(new Mock<IMediator>().Object, preferenceServiceMock.Object);
            viewModel.PlusCommand.CanExecute(null).Should().BeTrue();
            viewModel.PlusCommand.Execute(null);

            preferenceServiceMock.VerifySet(x => x.NumberOfDaysBeforeExpirationDate = 1, Times.Once);
        }
    }
}