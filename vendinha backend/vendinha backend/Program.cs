using System;
using System.Collections.Generic;
using vendinha_backend;
using vendinha_backend.Models;
using vendinha_backend.Services;
using vendinha_backend.Repository.Implementations;
using vendinha_backend.Interfaces;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/", () => "Vendinha API rodando!");

app.Run();

