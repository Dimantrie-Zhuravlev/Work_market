using TMPro;
using UnityEngine;

public class ListItemRender : MonoBehaviour
{
    [SerializeField] int indexData;
    [SerializeField] TMP_Text _nameLabel;
    [SerializeField] TMP_Text _labelPriceBox;
    [SerializeField] TMP_Text _labelPriceProduct;

    private GlobalProductsObject currentData;

    private void OnEnable()
    {
        currentData = ProductsGlobalData.Instance.ProductsGlobal[indexData];
        _nameLabel.text = currentData.Title;
        _labelPriceBox.text = $"Цена коробки {currentData.PriceBox}";
        _labelPriceProduct.text = $"Цена товара {currentData.PriceProduct}";
    }
}
