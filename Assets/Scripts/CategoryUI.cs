using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CategoryUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI categoryLabel;
    [SerializeField]
    private Button buttonToggleCategory;
    [SerializeField]
    private SubCategoryUI subCategoryPrefab;

    public string CategoryLabel { get => categoryLabel.text; set => categoryLabel.text = value; }

    private Animator[] animators;
    private int animatorBoolSwitch = Animator.StringToHash("Switch");

    private bool doneSubCategories = false;

    public void SetSubCategories(string[] subCategories)
    {
        if(!doneSubCategories)
        {
            doneSubCategories = true;
            foreach(var sc in subCategories)
            {
                var scUI = Instantiate(subCategoryPrefab, transform);
                scUI.SubCategoryLabel = sc;
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
