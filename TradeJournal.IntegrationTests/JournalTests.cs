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

  private async Task<(string, HttpResponseMessage)> createTag(string name)
  {
    var createJournalTagRequest = new CreateJournalTagRequest(name);
    var content = new StringContent(JsonSerializer.Serialize(createJournalTagRequest), UTF8Encoding.UTF8, "application/json");
    var response = await _httpClient.PostAsync("api/journals/tag", content);
    var body = await response.Content.ReadAsStringAsync();

    return (body, response);
  }

  [Fact]
  public async Task AddNewJournal()
  {
    var createJournalResponse = await createJournal();
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
    var createResponse = await createJournal();
    var createAccountRequest = new CreateAccountRequest(Int32.Parse(createResponse.Item1), 50000.00);
    var content = new StringContent(JsonSerializer.Serialize(createAccountRequest), UTF8Encoding.UTF8, "application/json")
    {
      Headers = { { "x-requestid", Guid.NewGuid().ToString() } }
    };
    var response = await _httpClient.PostAsync("api/journals/account", content);
    await response.Content.ReadAsStringAsync();

    Assert.Equal(HttpStatusCode.OK, response.StatusCode);
  }

  [Fact]
  public async Task AddNewJournalTag()
  {
    var createTagResponse = await createTag("Tag");
    Assert.Equal(HttpStatusCode.OK, createTagResponse.Item2.StatusCode);
  }

  [Fact]
  public async Task AddTagToJournal()
  {
    var createJournalResponse = await createJournal();
    var createTagResponse = await createTag("Cool Tag");
    var journalId = createJournalResponse.Item1;
    var tagId = createTagResponse.Item1;

    var response = await _httpClient.PostAsync($"api/journals/{journalId}/tags/{tagId}", new StringContent(""));
    var body = await response.Content.ReadAsStringAsync();

    var options = new JsonSerializerOptions
    {
      PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
      PropertyNameCaseInsensitive = true
    };

    var journal = JsonSerializer.Deserialize<JournalDTO>(body, options)!;

    Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    Assert.Single(journal.Tags);
    Assert.Equal("Cool Tag", journal.Tags[0].Name);
  }

  [Fact]
  public async Task AddTagToJournalWithInvalidJournalId()
  {
    var createTagResponse = await createTag("Cool Tag");
    var tagId = createTagResponse.Item1;

    var response = await _httpClient.PostAsync($"api/journals/0/tags/{tagId}", new StringContent(""));
    Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
  }

  [Fact]
  public async Task AddTagToJournalWithInvalidTagId()
  {
    var createJournalResponse = await createJournal();
    var journalId = createJournalResponse.Item1;

    var response = await _httpClient.PostAsync($"api/journals/{journalId}/tags/0", new StringContent(""));
    Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
  }
}
