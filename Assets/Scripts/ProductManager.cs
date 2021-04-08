using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductManager : MonoBehaviour
{
    [SerializeField]
    private TextAsset productCatalogAsset;
    [SerializeField]
    private ProductCatalog productCatalog;
    public ProductCatalog Catalog { get { if (productCatalog != null) return productCatalog; else FetchProducts(); return productCatalog; } }

    private void Awake()
    {
        FetchProducts();
    }

    private void FetchProducts()
    {
        productCatalog = JsonUtility.FromJson<ProductCatalog>(productCatalogAsset.text);
    }
}
