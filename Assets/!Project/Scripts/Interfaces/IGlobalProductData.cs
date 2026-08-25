using UnityEngine;

public interface IGlobalProductData 
{
    string Title { get; }

    Money PriceBox { get; }
    Money PriceProduct { get; }
}