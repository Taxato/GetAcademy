using System.Text.Json;
using System.Text.Json.Serialization;
using ClaimTheSquareAPI.DTOs;

var builder = WebApplication.CreateBuilder(args);
builder.Services.ConfigureHttpJsonOptions(options =>
{
	options.SerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
});
var app = builder.Build();
app.UseHttpsRedirection();

List<SquareDto> squares =
[
	new() { Index = 0, Text = "Terje", ForeGround = ConsoleColor.White, BackGround = ConsoleColor.Blue },
	new() { Index = 4, Text = "Per", ForeGround = ConsoleColor.Green, BackGround = ConsoleColor.Red }
];

app.MapGet("/squares", () => squares);

app.Run();