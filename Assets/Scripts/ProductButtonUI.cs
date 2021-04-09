using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ProductButtonUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI productName;
    public string ProductName { get => productName.text; set => productName.text = value; }

    [SerializeField]
    private TextMeshProUGUI categoryLabel;
    public string CategoryLabel { get => categoryLabel.text; set => categoryLabel.text = value; }

    [SerializeField]
    private TextMeshProUGUI subCategoryLabel;
    public string SubCategoryLabel { get => subCategoryLabel.text; set => subCategoryLabel.text = value; }


}
