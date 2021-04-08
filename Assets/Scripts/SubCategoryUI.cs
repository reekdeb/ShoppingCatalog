using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SubCategoryUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI subCategoryLabel;
    public string SubCategoryLabel { get => subCategoryLabel.text; set => subCategoryLabel.text = value; }

}
