using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Net;
using System.Text;
using System.Text.Json;
using TradeJournal.API.Controllers;
using Xunit.Abstractions;

namespace TradeJournal.IntegrationTests;

public class JournalTests : IClassFixture<TradeJournalApiFixture<Program>>
{
  private readonly HttpClient _httpClient;
  private readonly TradeJournalApiFixture<Program> _factory;
  private readonly ITestOutputHelper _outputHelper;

  public JournalTests(TradeJournalApiFixture<Program> factory, ITestOutputHelper outputHelper)
  {
    _factory = factory;
    _httpClient = factory.CreateClient();
    _outputHelper = outputHelper;
  }

  [Fact]
  public async Task AddNewJournal()
  {
    var createJournalRequest = new CreateJournalRequest("Test", "A test journal");
    var content = new StringContent(JsonSerializer.Serialize(createJournalRequest), UTF8Encoding.UTF8, "application/json")
    { 
      Headers = { { "x-requestid", Guid.NewGuid().ToString() } }
    };
    var response = await _httpClient.PostAsync("api/journals", content);
    var body = await response.Content.ReadAsStringAsync();

    Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    Assert.Equal("1", body);
  }

  [Fact]
  public async Task AddNewJournalWithoutRequestId()
  {
    var createJournalRequest = new CreateJournalRequest("Test", "A test journal");
    var content = new StringContent(JsonSerializer.Serialize(createJournalRequest), UTF8Encoding.UTF8, "application/json")
    {
      Headers = { { "x-requestid", Guid.Empty.ToString() } }
    };
    var response = await _httpClient.PostAsync("api/journals", content);
    await response.Content.ReadAsStringAsync();

    Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
  }
}
