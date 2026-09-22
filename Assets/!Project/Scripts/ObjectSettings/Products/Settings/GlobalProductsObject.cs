using UnityEngine;

[CreateAssetMenu]
public class GlobalProductsObject : ScriptableObject, IGlobalProductData
{
    [Header("General Info")]
    [SerializeField] private string title;
    [SerializeField] private Money priceBox;
    [SerializeField] private Money priceProduct;
    [HideInInspector]
    public AbstractSupplyPark supplyPark; //Это ссылка на стеллаж, на котором закупается данная коробка
    public string Title => title;
    public Money PriceBox => priceBox;

    public Money PriceProduct => priceProduct;

    public AbstractSupplyPark SupplyPark => supplyPark;
}