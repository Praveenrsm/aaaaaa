using FarmBusiness.Services;
using FarmTradeDataLayer;
using FarmTradeDataLayer.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Microsoft.AspNetCore.Hosting;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string connectionStr = builder.Configuration.GetConnectionString("sqlConnection");
builder.Services.AddDbContext<FarmContext>(options => options.UseSqlServer(connectionStr));
#region
var jwtSettings = builder.Configuration.GetSection("Jwt");
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        
        // ValidIssuer = jwtSettings["Issuer"],
        //ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]))
    };
});
#endregion
// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddSwaggerGen(c =>
//{
//    c.SwaggerDoc("v1", new OpenApiInfo
//    {
//        Version = "v1",
//        Title = "Farm API",
//        Description = "Online Farm Products management API",
//    });


//});
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Farm API",
        Description = "Online Farm Products management API",
    });

    // Add JWT Authentication to Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please insert JWT with Bearer into field",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});
builder.Services.AddTransient<AddressService, AddressService>();
builder.Services.AddTransient<IAddressRepo, AddressRepo>();
builder.Services.AddTransient<UserService, UserService>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<ProductService, ProductService>();
builder.Services.AddTransient<IproductRepository, ProductRepository>();
builder.Services.AddTransient<RRService, RRService>();
builder.Services.AddTransient<IRatingAndReviewRepository, RatingAndReviewRepository>();
builder.Services.AddTransient<ICartRepo, CartRepo>();
builder.Services.AddTransient<CartService, CartService>();
builder.Services.AddTransient<IOrderRepo, OrderRepo>();
builder.Services.AddTransient<OrderService, OrderService>();
builder.Services.AddTransient<UserCartService, UserCartService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    
    app.UseSwagger();
    app.UseSwaggerUI(options =>
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "Farm API"));
}
//app.UseSwagger();
//app.UseSwaggerUI(options =>
//        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Farm API"));
//app.UseCors("AllowReactApp");
app.UseDeveloperExceptionPage();

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();
app.MapControllers();
app.Run();
