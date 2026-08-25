using TMPro;
using UnityEngine;

public class ListItemRender : MonoBehaviour
{
    [SerializeField] int indexData;
    [SerializeField] TMP_Text _nameLabel;
    [SerializeField] TMP_Text _labelPriceBox;
    [SerializeField] TMP_Text _labelPriceProduct;
    [SerializeField] TMP_Text _labelSupplyBoxes;
    [SerializeField] private ShopUICOntroller _shopUIController;

    private GlobalProductsObject currentData;

    private void OnEnable()
    {
        currentData = ProductsGlobalData.Instance.ProductsGlobal[indexData];
        _nameLabel.text = currentData.Title;
        _labelPriceBox.text = $"Цена коробки {currentData.PriceBox}";
        _labelPriceProduct.text = $"Цена товара {currentData.PriceProduct}";
        _labelSupplyBoxes.text = $"Коробок {currentData.SupplyPark.CurrentCountProductBoxes}/4";
    }

    public void  BuyBox()
    {
        if (currentData.SupplyPark.CurrentCountProductBoxes < 4 && PlayerWallet.Instance.CanPayShoping(currentData.PriceBox, true))
        {
            currentData.SupplyPark.AddBoxOnSupplyPark();
            _shopUIController.ResetViewBalance();
            _labelSupplyBoxes.text = $"Коробок {currentData.SupplyPark.CurrentCountProductBoxes}/4";
        }
    }
}
