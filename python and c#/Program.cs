WebApplicationBuilder builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    Args = args,
    WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")
});
WebApplication app = builder.Build();
app.UseDefaultFiles();
app.UseStaticFiles();
string serverId = Guid.NewGuid().ToString();

int csc = 0;
int pythonc = 0;

app.MapPost("/api/cs", cvote);
app.MapPost("/api/python", pythonv);
app.MapPost("/api/dv", dvote);
app.MapGet("/api/cs", cget);
app.MapGet("/api/python", pget);

app.Run();

IResult cvote(HttpContext context)
{
    if (context.Request.Cookies.ContainsKey("serverSession") &&
        context.Request.Cookies["serverSession"] == serverId)
        return Results.Text("you have voted already");

    csc++;
    context.Response.Cookies.Append("serverSession", serverId, new CookieOptions
    {
        Expires = DateTimeOffset.Now.AddDays(15)
    });
    context.Response.Cookies.Append("voteval", "cs", new CookieOptions
    {
        Expires = DateTimeOffset.Now.AddDays(15)
    });
    return Results.Redirect("/vote.html");
}

IResult dvote(HttpContext context)
{
    if (!context.Request.Cookies.ContainsKey("voteval")&& context.Request.Cookies["serverSession"] != serverId)
        return Results.Text("you haven't voted yet");

    if (context.Request.Cookies["voteval"] == "cs")
        csc--;
    else
        pythonc--;

    context.Response.Cookies.Delete("voteval");
    context.Response.Cookies.Delete("serverSession");
    return Results.Redirect("/vote.html");
}

IResult pythonv(HttpContext context)
{
    if (context.Request.Cookies.ContainsKey("serverSession") &&
        context.Request.Cookies["serverSession"] == serverId)
        return Results.Text("you have voted already");

    pythonc++;
    context.Response.Cookies.Append("serverSession", serverId, new CookieOptions
    {
        Expires = DateTimeOffset.Now.AddDays(15)
    });
    context.Response.Cookies.Append("voteval", "python", new CookieOptions
    {
        Expires = DateTimeOffset.Now.AddDays(15)
    });
    return Results.Redirect("/vote.html");
}
IResult pget()
{
    return Results.Text(pythonc.ToString());
}

IResult cget()
{
    return Results.Text(csc.ToString());
}