using UnityEngine;

public struct StructureProductsParameters
{
    public GlobalProductsObject Gorox;
    public GlobalProductsObject Makaron;

    public StructureProductsParameters(GlobalProductsObject makaron, GlobalProductsObject gorox)
    {
        Makaron = makaron; //лучше соблюдать порядок со ссылкой на ProductsGlobalData
        Gorox = gorox;
    }
}
