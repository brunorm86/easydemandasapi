// Program.cs

using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using easydemandasapi.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

// =====================================================================
// BUILDER — fase de configuração
// Aqui registramos todos os serviços que a aplicação vai usar.
// =====================================================================
// Enable legacy timestamp behavior to allow storing non-UTC dates (like UTC-3) in PostgreSQL
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var builder = WebApplication.CreateBuilder(args);

// Registra os Controllers no sistema de Injeção de Dependência.
// Sem isso, o .NET não sabe que existem Controllers na aplicação.
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

// Registra o AppDbContext no sistema de Injeção de Dependência.
// Isso permite que os Controllers recebam o AppDbContext automaticamente
// no construtor (isso é chamado de Injeção de Dependência).
//
// options.UseNpgsql(...) diz ao EF para usar o PostgreSQL como banco.
// builder.Configuration.GetConnectionString("DefaultConnection") lê
// a connection string do appsettings.json.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Adiciona o Swagger/OpenAPI — interface web para testar a API.
// AddEndpointsApiExplorer() descobre os endpoints disponíveis.
// AddSwaggerGen() gera a documentação interativa da API.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "EasyDemandas API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// Configura JWT Authentication
var key = Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"] ?? "super_secret_easydemandas_token_key_123456!");
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"] ?? "easydemandasapi",
        ValidateAudience = true,
        ValidAudience = builder.Configuration["Jwt:Audience"] ?? "easydemandasfront",
        ValidateLifetime = true
    };
});
builder.Services.AddAuthorization();

// Configura CORS (Cross-Origin Resource Sharing).
// Isso permite que o frontend React (que roda em outra porta)
// faça requisições para a API sem ser bloqueado pelo navegador.
builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirTudo", policy =>
    {
        policy.AllowAnyOrigin()   // Permite qualquer origem (domínio/porta)
              .AllowAnyMethod()   // Permite GET, POST, PUT, DELETE, etc.
              .AllowAnyHeader();  // Permite qualquer cabeçalho HTTP
    });
});

// =====================================================================
// APP — fase de execução
// Aqui configuramos o pipeline de middlewares (o que acontece com cada
// requisição HTTP antes de chegar no Controller).
// =====================================================================
var app = builder.Build();

// Ativa o Swagger apenas no ambiente de desenvolvimento.
// Em produção, a documentação seria protegida ou desativada.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Ativa o CORS com a política que definimos acima.
// IMPORTANTE: deve vir ANTES do MapControllers().
app.UseCors("PermitirTudo");

// Ativa Autenticação e Autorização no Pipeline
app.UseAuthentication();
app.UseAuthorization();

// Ativa o roteamento de requisições para os Controllers.
// É aqui que o .NET olha para a URL da requisição e decide
// qual Controller e qual método deve ser chamado.
app.MapControllers();

// Applica as migrations do Entity Framework Core automaticamente na inicialização
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    context.Database.Migrate();

    // Fix initial seed departments for managers to avoid circular dependency on creation
    var gestores = context.Empregados.Where(e => e.Id <= 7 && e.DepartamentoId == null).ToList();
    if (gestores.Any())
    {
        foreach (var g in gestores)
        {
            g.DepartamentoId = g.Id; // Empregado 'i' é responsável pelo Departamento 'i'
        }
        context.SaveChanges();
    }
}

// Inicia a aplicação e fica escutando requisições HTTP.
app.Run();