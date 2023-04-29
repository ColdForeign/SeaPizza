using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace SeaPizza.Shared.Authorization;

public static class SeaPizzaAction
{
    public const string View = nameof(View);
    public const string Search = nameof(Search);
    public const string Create = nameof(Create);
    public const string Update = nameof(Update);
    public const string Delete = nameof(Delete);
    public const string Export = nameof(Export);
    public const string Generate = nameof(Generate);
    public const string Clean = nameof(Clean);
    public const string UpgradeSubscription = nameof(UpgradeSubscription);
}

public static class SeaPizzaResource
{
    public const string Dashboard = nameof(Dashboard);
    public const string Hangfire = nameof(Hangfire);
    public const string Users = nameof(Users);
    public const string UserRoles = nameof(UserRoles);
    public const string Roles = nameof(Roles);
    public const string RoleClaims = nameof(RoleClaims);
    public const string Products = nameof(Products);
    public const string Brands = nameof(Brands);
}

public static class SeaPizzaPermissions
{
    private static readonly SeaPizzaPermission[] _all = new SeaPizzaPermission[]
    {
        new("View Dashboard", SeaPizzaAction.View, SeaPizzaResource.Dashboard),
        new("View Hangfire", SeaPizzaAction.View, SeaPizzaResource.Hangfire),
        new("View Users", SeaPizzaAction.View, SeaPizzaResource.Users),
        new("Search Users", SeaPizzaAction.Search, SeaPizzaResource.Users),
        new("Create Users", SeaPizzaAction.Create, SeaPizzaResource.Users),
        new("Update Users", SeaPizzaAction.Update, SeaPizzaResource.Users),
        new("Delete Users", SeaPizzaAction.Delete, SeaPizzaResource.Users),
        new("Export Users", SeaPizzaAction.Export, SeaPizzaResource.Users),
        new("View UserRoles", SeaPizzaAction.View, SeaPizzaResource.UserRoles),
        new("Update UserRoles", SeaPizzaAction.Update, SeaPizzaResource.UserRoles),
        new("View Roles", SeaPizzaAction.View, SeaPizzaResource.Roles),
        new("Create Roles", SeaPizzaAction.Create, SeaPizzaResource.Roles),
        new("Update Roles", SeaPizzaAction.Update, SeaPizzaResource.Roles),
        new("Delete Roles", SeaPizzaAction.Delete, SeaPizzaResource.Roles),
        new("View RoleClaims", SeaPizzaAction.View, SeaPizzaResource.RoleClaims),
        new("Update RoleClaims", SeaPizzaAction.Update, SeaPizzaResource.RoleClaims),
        new("View Products", SeaPizzaAction.View, SeaPizzaResource.Products, IsBasic: true),
        new("Search Products", SeaPizzaAction.Search, SeaPizzaResource.Products, IsBasic: true),
        new("Create Products", SeaPizzaAction.Create, SeaPizzaResource.Products),
        new("Update Products", SeaPizzaAction.Update, SeaPizzaResource.Products),
        new("Delete Products", SeaPizzaAction.Delete, SeaPizzaResource.Products),
        new("Export Products", SeaPizzaAction.Export, SeaPizzaResource.Products),
        new("View Brands", SeaPizzaAction.View, SeaPizzaResource.Brands, IsBasic: true),
        new("Search Brands", SeaPizzaAction.Search, SeaPizzaResource.Brands, IsBasic: true),
        new("Create Brands", SeaPizzaAction.Create, SeaPizzaResource.Brands),
        new("Update Brands", SeaPizzaAction.Update, SeaPizzaResource.Brands),
        new("Delete Brands", SeaPizzaAction.Delete, SeaPizzaResource.Brands),
        new("Generate Brands", SeaPizzaAction.Generate, SeaPizzaResource.Brands),
        new("Clean Brands", SeaPizzaAction.Clean, SeaPizzaResource.Brands)
    };

    public static IReadOnlyList<SeaPizzaPermission> All { get; } = new ReadOnlyCollection<SeaPizzaPermission>(_all);
    public static IReadOnlyList<SeaPizzaPermission> Admin { get; } = new ReadOnlyCollection<SeaPizzaPermission>(_all.Where(p => !p.IsRoot).ToArray());
    public static IReadOnlyList<SeaPizzaPermission> Basic { get; } = new ReadOnlyCollection<SeaPizzaPermission>(_all.Where(p => p.IsBasic).ToArray());
}

public record SeaPizzaPermission(string Description, string Action, string Resource, bool IsBasic = false, bool IsRoot = false)
{
    public string Name => NameFor(Action, Resource);
    public static string NameFor(string action, string resource) => $"Permissions.{resource}.{action}";
}
