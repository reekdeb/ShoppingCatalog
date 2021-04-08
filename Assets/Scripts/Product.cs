using System;

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
}
