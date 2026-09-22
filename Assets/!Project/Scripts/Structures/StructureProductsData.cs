using UnityEngine;

public struct StructureProductsData 
{
    public AbstractPoolShelf _ProductShelfPool;
    public AbstractPoolBoxes _ProductsBoxPool;
    public AbstractPoolProducts _ProductPool;
    public GlobalProductsObject _ProductParametres;

    public StructureProductsData(AbstractPoolShelf productShelfPool, AbstractPoolBoxes productBoxPool, AbstractPoolProducts productPool, GlobalProductsObject productParametres)
    {
        _ProductShelfPool = productShelfPool;
        _ProductsBoxPool = productBoxPool;
        _ProductPool = productPool;
        _ProductParametres = productParametres;
    }
}
