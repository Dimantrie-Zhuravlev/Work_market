using UnityEngine;

public struct StructureProductsData 
{
    public AbstractPoolShelf _ProductShelfPool;
    public AbstractPoolBoxes _ProductsBoxPool;
    public AbstractPoolProducts _ProductPool;
    public GameObject _BoxPoolGameObject;

    public StructureProductsData(AbstractPoolShelf productShelfPool, AbstractPoolBoxes productBoxPool, AbstractPoolProducts productPool, GameObject boxPoolGameobject)
    {
        _ProductShelfPool = productShelfPool;
        _ProductsBoxPool = productBoxPool;
        _ProductPool = productPool;
        _BoxPoolGameObject = boxPoolGameobject;
    }
}
