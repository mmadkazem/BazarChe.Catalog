var builder = WebApplication.CreateBuilder(args);

builder.Services.AddServiceCollection();

var app = builder.Build();
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseRouting();
    app.UseExceptionHandler();
    app.GetFullName();
    app.Run();
}