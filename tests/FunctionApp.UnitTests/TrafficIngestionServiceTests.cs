using FunctionApp.Services;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace FunctionApp.UnitTests;

public class TrafficIngestionServiceTests
{
    [Fact]
    public async Task RunAsync_CompletesSuccessfully()
    {
        // Arrange
        var loggerMock = Substitute.For<ILogger<TrafficIngestionService>>();
        var service = new TrafficIngestionService(loggerMock);

        // Act & Assert
        await service.RunAsync();
    }
}