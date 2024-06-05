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

  private async Task<(string, HttpResponseMessage)> _createJournal()
  {
    var createJournalRequest = new CreateJournalRequest("Test", "A test journal");
    var content = new StringContent(JsonSerializer.Serialize(createJournalRequest), UTF8Encoding.UTF8, "application/json")
    {
      Headers = { { "x-requestid", Guid.NewGuid().ToString() } }
    };
    var response = await _httpClient.PostAsync("api/journals", content);
    var body = await response.Content.ReadAsStringAsync();
    return (body, response);
  }

  [Fact]
  public async Task AddNewJournal()
  {
    var createJournalResponse = await _createJournal();
    Assert.Equal(HttpStatusCode.OK, createJournalResponse.Item2.StatusCode);
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

  [Fact]
  public async Task AddNewAccount()
  {
    var createResponse = await _createJournal();
    var createAccountRequest = new CreateAccountRequest(Int32.Parse(createResponse.Item1), 50000.00);
    var content = new StringContent(JsonSerializer.Serialize(createAccountRequest), UTF8Encoding.UTF8, "application/json")
    {
      Headers = { { "x-requestid", Guid.NewGuid().ToString() } }
    };
    var response = await _httpClient.PostAsync("api/journals/account", content);
    await response.Content.ReadAsStringAsync();

    Assert.Equal(HttpStatusCode.OK, response.StatusCode);
  }
}
