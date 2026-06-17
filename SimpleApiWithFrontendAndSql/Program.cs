var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
app.UseHttpsRedirection();

app.MapGet("/api/contacts", () =>
{
	Console.WriteLine("Fetching contacts");
	return "Hello";
});

app.UseStaticFiles();
app.Run();