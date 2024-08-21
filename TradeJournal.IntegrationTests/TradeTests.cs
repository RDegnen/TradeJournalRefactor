using System.Text;
using System.Text.Json;
using System.Net;
using TradeJournal.API.Controllers;
using TradeJournal.Domain.Aggregates.TradeAggregate;
using Xunit.Abstractions;
using TradeJournal.API.Application.DataTranserObjects;

namespace TradeJournal.IntegrationTests;

public class TradeTests : IClassFixture<TradeJournalApiFixture<Program>>
{
  private readonly HttpClient _httpClient;
  private readonly TradeJournalApiFixture<Program> _factory;
  private readonly ITestOutputHelper _outputHelper;
  private readonly JsonSerializerOptions _jsonSerializerOptions = new JsonSerializerOptions
  {
    PropertyNameCaseInsensitive = true,
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
  };

  public TradeTests(TradeJournalApiFixture<Program> factory, ITestOutputHelper outputHelper)
  {
    _factory = factory;
    _httpClient = factory.CreateClient();
    _outputHelper = outputHelper;
  }

  private async Task<(string, HttpResponseMessage)> createTrade()
  {
    var createTradeRequest = new CreateTradeRequest("EURUSD", 100, Direction.Buy, DateTime.Now, 100.0, 1);
    var content = new StringContent(JsonSerializer.Serialize(createTradeRequest), UTF8Encoding.UTF8, "application/json");
    var response = await _httpClient.PostAsync("api/trades", content);
    var body = await response.Content.ReadAsStringAsync();
    return (body, response);
  }

  [Fact]
  [Trait("Endpoint", "CreateTrade")]
  public async Task AddNewTrade()
  {
    var createTradeResponse = await createTrade();
    Assert.Equal(HttpStatusCode.OK, createTradeResponse.Item2.StatusCode);
  }

  [Fact]
  [Trait("Endpoint", "UpdateTrade")]
  public async Task UpdateTrade()
  {
    var createTradeResponse = await createTrade();
    var tradeId = createTradeResponse.Item1;
    var updateTradeRequest = new UpdateTradeRequest(90.0, null, 110.0, DateTime.UtcNow, 110.0, 10.0, 2.0, TimeSpan.FromDays(1.0));
    var content = new StringContent(JsonSerializer.Serialize(updateTradeRequest), UTF8Encoding.UTF8, "application/json");
    var response = await _httpClient.PutAsync($"api/trades/{tradeId}", content);
    var body = await response.Content.ReadAsStringAsync();
    var dto = JsonSerializer.Deserialize<TradeDTO>(body, _jsonSerializerOptions)!;
    
    Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    Assert.True(dto.StopLoss == 90.0);
    Assert.True(dto.TakeProfit == 110.0);
    Assert.True(dto.ExitPrice == 110.0);
    Assert.True(dto.Duration == TimeSpan.FromDays(1.0));
  }

  [Fact]
  [Trait("Endpoint", "UpdateTrade")]
  public async Task UpdateTradeNotFound()
  {
    var updateTradeRequest = new UpdateTradeRequest(90.0, null, 110.0, DateTime.UtcNow, 110.0, 10.0, 2.0, TimeSpan.FromDays(1.0));
    var content = new StringContent(JsonSerializer.Serialize(updateTradeRequest), UTF8Encoding.UTF8, "application/json");
    var response = await _httpClient.PutAsync("api/trades/0", content);
    var body = await response.Content.ReadAsStringAsync();
    
    Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
  }
}
