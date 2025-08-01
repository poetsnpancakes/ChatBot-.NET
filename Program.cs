using Microsoft.EntityFrameworkCore;
using ChatBot.Database;
using ChatBot.LLM.Factory;
using ChatBot.LLM.Services.Absolute;
using ChatBot.LLM.Services.Interfaces;
using Qdrant.Client;
using Qdrant.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Configure CORS to allow requests from your React app's URL.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy => policy.WithOrigins("http://localhost:3000")
      .AllowAnyHeader()
      .AllowAnyMethod()
      .AllowCredentials());
});

//Add Context with connection-string
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("name_of_connection_string")));

builder.Services.AddHttpClient<QdrantClient>();

builder.Services.AddScoped<IOpenAiFactory>(sp =>
{
    var config = sp.GetRequiredService<IConfiguration>();
    return new OpenAiFactory(config, "gpt-4o-mini"); // or pick model dynamically if needed
});

builder.Services.AddScoped<IOpenAIService>(sp =>
{
    var config = sp.GetRequiredService<IConfiguration>();
    return new OpenAIService(config, "gpt-4o-mini");
});

builder.Services.AddScoped<IQueryClassifier, QueryClassifier>();
builder.Services.AddScoped<ILlm_rephrase, Llm_rephrase>();

builder.Services.AddScoped<IEmbeddingService, OpenAiEmbeddingService>();
builder.Services.AddScoped<SessionIdProvider>();


builder.Services.AddScoped<IBotService, BotService>();

builder.Services.AddScoped<IQdrantService, QdrantService>();
builder.Services.AddScoped<IChatMemoryService, ChatMemoryService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowReactApp");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
