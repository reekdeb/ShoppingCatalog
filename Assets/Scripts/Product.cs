using System;
using System.Collections.Generic;
using System.Linq;

[Serializable]
public class ProductCatalog
{
    public Product[] products;
}

[Serializable]
public class Product
{
    public string productName;
    public string category;
    public string subCategory;
    public string prefab;
}

public class ProductRuntime
{
    public class Category
    {
        public string categoryName;
        public SubCategory subCategory;

        public CategoryUI categoryUI;
    }

    public class SubCategory
    {
        public bool enabled;

        public SubCategoryUI subCategoryUI;
    }

    public List<Category> Categories { get; } = new List<Category>();
}

public static class ProductCatalogExtensions
{
    public static string[] GetCategories(this ProductCatalog catalog) =>
        catalog.products.Select(p => p.category).Distinct().OrderBy(s => s).ToArray();

    public static string[] GetSubCategories(this ProductCatalog catalog, string category) =>
        catalog.products
                .Where(p => p.category == category)
                .Select(p => p.subCategory)
                .Distinct()
                .OrderBy(s => s)
                .ToArray();
}