using Business.Abstract;
using Business.Concrete;
using Core.Utilities.Chaching.Redis.Abstract;
using Core.Utilities.Chaching.Redis.Concrete;
using DataAccess.Abstarct;
using DataAccess.Concrete;
using DataAccess.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.


builder.Services.AddControllers();
builder.Services.AddSingleton<IProductDal, EfProductDal>();
builder.Services.AddSingleton<IProductService, ProductManager>();
builder.Services.AddSingleton<ICacheManager,RedisCacheService>();
builder.Services.AddSingleton<IOrderDal, EfOrderDal>();
builder.Services.AddSingleton<IOrderDetailDal, EfOrderDetailDal>();
builder.Services.AddSingleton<IOrderService, OrderManager>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle



builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
