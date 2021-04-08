using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private ProductManager productManager;
    [SerializeField]
    private RectTransform categoryContainer;
    [SerializeField]
    private CategoryUI categoryUIPrefab;

    private void Start()
    {
        CreateUI();
    }

    private void CreateUI()
    {
        var categories = productManager.Catalog.products.Select(p => p.category).Distinct().OrderBy(s => s).ToList();
        categories.ForEach(category =>
        {
            var categoryUI = Instantiate(categoryUIPrefab, categoryContainer);
            categoryUI.CategoryLabel = category;

            var subCategories = productManager.Catalog.products
                .Where(p => p.category == category)
                .Select(p => p.subCategory)
                .Distinct()
                .OrderBy(s => s)
                .ToArray();
            categoryUI.SetSubCategories(subCategories);
        });
        
    }
}
