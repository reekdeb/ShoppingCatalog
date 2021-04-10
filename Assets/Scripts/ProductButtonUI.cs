using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ProductButtonUI : MonoBehaviour
{
    [SerializeField]
    private Button button;
    public Button Button => button;

    [SerializeField]
    private TextMeshProUGUI productName;
    public string ProductName { get => productName.text; set => productName.text = value; }

    [SerializeField]
    private TextMeshProUGUI categoryLabel;
    public string CategoryLabel { get => categoryLabel.text; set => categoryLabel.text = value; }

    [SerializeField]
    private TextMeshProUGUI subCategoryLabel;
    public string SubCategoryLabel { get => subCategoryLabel.text; set => subCategoryLabel.text = value; }

    private void Awake()
    {
        if (button is null) button = GetComponent<Button>();
    }

    private void Reset() => button = GetComponent<Button>();
}
