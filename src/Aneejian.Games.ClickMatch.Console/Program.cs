using Aneejian.Games.ClickMatch.Models;
using Aneejian.Games.ClickMatch.Services;
using System.Net.Http.Json;




int[] numbers = Enumerable.Range(1, 10).ToArray();

Random.Shared.Shuffle(numbers);

var numString = string.Join(",", numbers);

var randomNumbers = numbers.Take(5);

var httpClient = new HttpClient();

var fetchedThemes = await httpClient.GetFromJsonAsync<IEnumerable<ThemeData>>("https://aneejian.com/memory-game-resources/theme_info.json");

var selectedTheme = fetchedThemes?.FirstOrDefault(x => x.ThemeId == "animals");

var gameSettings = new GameSettings(16, selectedTheme!);

var tiles = gameSettings.GenerateTiles();
