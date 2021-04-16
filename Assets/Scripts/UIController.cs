using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.Management;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private ARSession aRSession;
    [SerializeField]
    private ARCameraBackground aRCameraBackground;
    [SerializeField]
    private ARFaceManager aRFaceManager;
    [SerializeField]
    private ProductManager productManager;
    [SerializeField]
    private Animator panelShoppingCatalog;
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
        CreateUI();
        ClickShowPanelShoppingCatalog(true);
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

            pbUI.Button.onClick.AddListener(() =>
            {
                aRFaceManager.facePrefab = Resources.Load<GameObject>(p.prefab);
                ClickShowPanelShoppingCatalog(false);
            });
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

    //private bool isXRInitialized;
    public void ClickShowPanelShoppingCatalog(bool value)
    {
        panelShoppingCatalog.SetBool(animParamBoolSwitch, value);
        //if (val) aRSession.matchFrameRateRequested = false;
        //else aRSession.matchFrameRateRequested = true;
        aRSession.enabled = !value;
        aRCameraBackground.enabled = !value;
        aRFaceManager.enabled = !value;
#if !UNITY_EDITOR
        if (value) XRGeneralSettings.Instance.Manager.StopSubsystems();
        else XRGeneralSettings.Instance.Manager.StartSubsystems();
#endif
        if (value) Application.targetFrameRate = -1;
    }

    public void ClickTogglePanelFilters() => panelFilters.SetBool(animParamBoolSwitch, !panelFilters.GetBool(animParamBoolSwitch));

    public void ClickFiltersApply()
    {
        // All unchecked - Show all
        if (categoryUIs.Values.All(cui => !cui.SubCategoryUIs.Any(scui => scui.Value.SubCategoryToggle.isOn)))
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

        ClickFiltersApply();
    }
}
