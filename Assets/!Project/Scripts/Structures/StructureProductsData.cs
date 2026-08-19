using UnityEngine;

public struct StructureProductsData 
{
    public AbstractPoolShelf _ProductShelfPool;
    public AbstractPoolBoxes _ProductsBoxPool;
    public AbstractPoolProducts _ProductPool;

    public StructureProductsData(AbstractPoolShelf productShelfPool, AbstractPoolBoxes productBoxPool, AbstractPoolProducts productPool)
    {
        _ProductShelfPool = productShelfPool;
        _ProductsBoxPool = productBoxPool;
        _ProductPool = productPool;
    }
}
