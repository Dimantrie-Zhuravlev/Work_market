using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ListItemRender : MonoBehaviour
{
    [SerializeField] int indexData;
    [SerializeField] TMP_Text _nameLabel;
    [SerializeField] TMP_Text _labelPriceBox;
    [SerializeField] TMP_Text _labelPriceProduct;
    [SerializeField] TMP_Text _labelSupplyBoxes;
    [SerializeField] private ShopUICOntroller _shopUIController;

    [Header ("Ссылки на изображение")]
    [SerializeField] private GameObject _image;
    [SerializeField] private Sprite _spriteImage;

    private GlobalProductsObject currentData;
    private void Start()
    {
        _image.GetComponent<Image>().sprite = _spriteImage;
    }
    private void OnEnable()
    {
        if (indexData!=-1)
        {
            currentData = ProductsGlobalData.Instance.ProductsGlobal[indexData];
            _nameLabel.text = currentData.Title;
            _labelPriceBox.text = $"Цена коробки {currentData.PriceBox}";
            _labelPriceProduct.text = $"Цена товара {currentData.PriceProduct}";
            _labelSupplyBoxes.text = $"Коробок {currentData.SupplyPark.CurrentCountProductBoxes}/4";
        } else
        {
            _nameLabel.text = "";
            _labelPriceBox.text = "";
            _labelPriceProduct.text = "";
            _labelSupplyBoxes.text = "";
        }

    }

    public void  BuyBox()
    {
        if (indexData != -1 && currentData.SupplyPark.CurrentCountProductBoxes < 4 && PlayerWallet.Instance.CanPayShoping(currentData.PriceBox, true))
        {
            currentData.SupplyPark.AddBoxOnSupplyPark();
            _shopUIController.ResetViewBalance();
            _labelSupplyBoxes.text = $"Коробок {currentData.SupplyPark.CurrentCountProductBoxes}/4";
        }
    }
}
