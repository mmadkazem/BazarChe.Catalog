namespace Catalog.Features.CatalogCategory.Common;

public class Category
{
    public const string TableName = "CatalogCategories";

    public int Id { get; private set; }

    public string Name { get; private set; } = null!;

    public int? ParentId { get; private set; }

    public string? Path => GetPath(this);

    private string? GetPath(Category category)
    {
        if (category.Parent is not null)
            return $"{GetPath(category.Parent)}/{category.Name}";

        if (category.Id == Id)
            return null;

        return category.Name;
    }

    public static Category Create(string category, int? parentId)
        => new Category
        {
            Name = category,
            ParentId = parentId
        };

    public void Update(string category)
    {
        Name = category;
    }

    public Category Parent { get; private set; } = null!;

    public ICollection<Category> Children { get; private set; } = null!;
}
