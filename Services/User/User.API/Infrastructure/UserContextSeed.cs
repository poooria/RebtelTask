using User.API.Model;

namespace User.API.Infrastructure;

public static class UserContextSeed
{
    public static async Task SeedAsync(this UserContext context)
    {
        //context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
        if (!context.Users.Any())
        {
            await AddUsers(context);
        }
    }

    private static async Task AddUsers(UserContext context)
    {
        await context.Users.AddRangeAsync(new Model.User(
                "Poorya",
                "Ghajar",
                "poooria@gmail.com",
                "09188708949",
                new Address("Iran", "Kurdistan", "Sanndaj", "Abidar Street", "12345678")),
            new Model.User(
                "Kamyar",
                "Babai",
                "kam.babai@gmail.com",
                "09184526891",
                new Address("Iran", "Kurdistan", "Sanndaj", "Abidar Street", "12345678")),
            new Model.User(
                "Dariush",
                "Zandi",
                "d.zandi@gmail.com",
                "09154708949",
                new Address("Iran", "Kurdistan", "Sanndaj", "Abidar Street", "12345678")),
            new Model.User(
                "Majid",
                "Karimi",
                "majid.karimi@gmail.com",
                "09158418949",
                new Address("Iran", "Kurdistan", "Sanndaj", "Abidar Street", "12345678")),
            new Model.User(
                "Farhad",
                "Yazdan",
                "farhad.yazdan@gmail.com",
                "09158418349",
                new Address("Iran", "Kurdistan", "Sanndaj", "Abidar Street", "12345678")),
            new Model.User(
                "Hamid",
                "Soltanian",
                "hamid.soltan@gmail.com",
                "09158418469",
                new Address("Iran", "Kurdistan", "Sanndaj", "Abidar Street", "12345678")),
            new Model.User(
                "Reza",
                "Mahmoudi",
                "r.mahmoudi@gmail.com",
                "09128418349",
                new Address("Iran", "Kurdistan", "Sanndaj", "Abidar Street", "12345678")));
        await context.SaveChangesAsync();
    }
}