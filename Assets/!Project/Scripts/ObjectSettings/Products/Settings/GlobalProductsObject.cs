using UnityEngine;

[CreateAssetMenu]
public class GlobalProductsObject : ScriptableObject, IGlobalProductData
{
    [Header("General Info")]
    [SerializeField] private string title;
    [SerializeField] private Money priceBox;
    [SerializeField] private Money priceProduct;
    public AbstractSupplyPark supplyPark;
    public string Title => title;
    public Money PriceBox => priceBox;

    public Money PriceProduct => priceProduct;// стоимости товаров я привязал к заказам, но вручную все указывается (надо сделать системно)

    public AbstractSupplyPark SupplyPark => supplyPark;


    //Сделать стрингу для наименования коробок, поменять сообщения при наведении сразу
}