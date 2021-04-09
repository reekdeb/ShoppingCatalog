using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private ProductManager productManager;
    [SerializeField]
    private Animator panelFilters;
    [SerializeField]
    private RectTransform productContainter;
    [SerializeField]
    private ProductButtonUI productButtonPrefab;
    [SerializeField]
    private RectTransform categoryContainer;
    [SerializeField]
    private CategoryUI categoryUIPrefab;

    private Dictionary<string, CategoryUI> categoryUIs = new Dictionary<string, CategoryUI>();
    private Dictionary<Product, ProductButtonUI> productButtons = new Dictionary<Product, ProductButtonUI>();

    private int animParamBoolSwitch = Animator.StringToHash("Switch");

    private void Start()
    {
        //panelFilters.gameObject.SetActive(false);
        CreateUI();
    }

    private void CreateUI()
    {
        foreach (var p in productManager.Catalog.products)
        {
            var pbUI = Instantiate(productButtonPrefab, productContainter);
            pbUI.ProductName = p.productName;
            pbUI.CategoryLabel = p.category;
            pbUI.SubCategoryLabel = p.subCategory;

            productButtons.Add(p, pbUI);
        }

        var categories = productManager.Catalog.GetCategories();

        categories.ToList().ForEach(category =>
        {
            var categoryUI = Instantiate(categoryUIPrefab, categoryContainer);
            categoryUI.CategoryLabel = category;

            categoryUIs.Add(category, categoryUI);

            var subCategories = productManager.Catalog.GetSubCategories(category);
            categoryUI.SetSubCategories(subCategories);
        });
    }

    public void ClickTogglePanelFilters() => panelFilters.SetBool(animParamBoolSwitch, !panelFilters.GetBool(animParamBoolSwitch));

    public void ClickFiltersApply()
    {
        // All unchecked - Show all
        if(categoryUIs.Values.All(cui => !cui.SubCategoryUIs.Any(scui => scui.Value.SubCategoryToggle.isOn)))
        {
            productButtons.Values.ToList().ForEach(pb => pb.gameObject.SetActive(true));
            return;
        }

        // Show filtered
        productButtons.Values.ToList()
            .ForEach(pb =>
            {
                var result = categoryUIs[pb.CategoryLabel].SubCategoryUIs[pb.SubCategoryLabel].SubCategoryToggle.isOn;
                pb.gameObject.SetActive(result);
            });
    }

    public void ClickFiltersReset()
    {
        categoryUIs.Values
            .SelectMany(cui => cui.SubCategoryUIs)
            .Select(kv => kv.Value)
            .Select(scui => scui.SubCategoryToggle)
            .ToList()
            .ForEach(t => t.isOn = false);
    }
}
