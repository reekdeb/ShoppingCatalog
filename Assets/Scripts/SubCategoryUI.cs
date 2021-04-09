using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SubCategoryUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI subCategoryLabel;
    public string SubCategoryLabel { get => subCategoryLabel.text; set => subCategoryLabel.text = value; }

    [SerializeField]
    private Toggle subCategoryToggle;
    public Toggle SubCategoryToggle => subCategoryToggle;
}
