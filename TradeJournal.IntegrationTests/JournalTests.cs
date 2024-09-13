using System.Net;
using System.Text;
using System.Text.Json;
using TradeJournal.API.Application.DataTranserObjects;
using TradeJournal.API.Controllers;
using Xunit.Abstractions;

namespace TradeJournal.IntegrationTests;

public class JournalTests : IClassFixture<TradeJournalApiFixture<Program>>
{
  private readonly HttpClient _httpClient;
  private readonly TradeJournalApiFixture<Program> _factory;
  private readonly ITestOutputHelper _outputHelper;
  private readonly JsonSerializerOptions _jsonSerializerOptions = new JsonSerializerOptions
  {
    PropertyNameCaseInsensitive = true,
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
  };

  public JournalTests(TradeJournalApiFixture<Program> factory, ITestOutputHelper outputHelper)
  {
    _factory = factory;
    _httpClient = factory.CreateClient();
    _outputHelper = outputHelper;
  }

  private async Task<(string, HttpResponseMessage)> createJournal()
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
    var createJournalResponse = await createJournal();
    Assert.Equal(HttpStatusCode.Created, createJournalResponse.Item2.StatusCode);
  }

  [Fact]
  public async Task AddNewAccount()
  {
    var createJournalResponse = await createJournal();
    var dto = JsonSerializer.Deserialize<JournalDTO>(createJournalResponse.Item1, _jsonSerializerOptions);
    var createAccountRequest = new CreateAccountRequest(dto.Id, 50000.00);
    var content = new StringContent(JsonSerializer.Serialize(createAccountRequest), UTF8Encoding.UTF8, "application/json")
    {
      Headers = { { "x-requestid", Guid.NewGuid().ToString() } }
    };
    var response = await _httpClient.PostAsync("api/journals/account", content);
    await response.Content.ReadAsStringAsync();

    Assert.Equal(HttpStatusCode.Created, response.StatusCode);
  }
}
