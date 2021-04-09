using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CategoryUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI categoryLabel;
    public string CategoryLabel { get => categoryLabel.text; set => categoryLabel.text = value; }

    [SerializeField]
    private Button buttonToggleCategory;
    [SerializeField]
    private SubCategoryUI subCategoryPrefab;

    public Dictionary<string, SubCategoryUI> SubCategoryUIs { get; } = new Dictionary<string, SubCategoryUI>();
    private bool doneSubCategories = false;

    private Animator[] animators;
    private int animatorBoolSwitch = Animator.StringToHash("Switch");

    public void SetSubCategories(string[] subCategories)
    {
        if(!doneSubCategories)
        {
            doneSubCategories = true;
            foreach(var sc in subCategories)
            {
                var scUI = Instantiate(subCategoryPrefab, transform);
                scUI.SubCategoryLabel = sc;
                SubCategoryUIs.Add(sc, scUI);
            }

            List<Animator> allAnimators = new List<Animator>();
            foreach (Transform t in transform)
            {
                var a = t.GetComponent<Animator>();
                if (a is object) allAnimators.Add(a);
            }
            animators = allAnimators.ToArray();

            buttonToggleCategory.onClick.AddListener(() =>
            {
                foreach (var a in animators) a.SetBool(animatorBoolSwitch, !a.GetBool(animatorBoolSwitch));
            });
        }
    }
}
