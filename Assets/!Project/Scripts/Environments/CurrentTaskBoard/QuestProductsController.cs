using UnityEngine;

public class QuestProductsController : MonoBehaviour
{
    public static QuestProductsController Instance { get; private set; }

    private StructureTrayObjects ProductsData;

    public StructureTrayObjects QuestData => ProductsData;

    private GameObject[] arrayGhosts = new GameObject[2];

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        Transform ghostsContainer = transform.GetChild(0);
        arrayGhosts[0] = ghostsContainer.GetChild(0).gameObject; //Макароны
        arrayGhosts[1] = ghostsContainer.GetChild(1).gameObject; //Горох

        ClearCurrentQuest(); //проверить нужно ли при старте чистить
    }

    public void AddQuestGhostsProducts(StructureTrayObjects taskDetails)
    {
        ProductsData = taskDetails;
        if (taskDetails.Makarons > 0)
        {
            arrayGhosts[0].SetActive(true);
            arrayGhosts[0].GetComponent<EnvironmentsPersonMessage>().SetCurrentMessage($"Нужно еще {taskDetails.Makarons} макарон");
        }
        if (taskDetails.Gorox > 0)
        {
            arrayGhosts[1].SetActive(true);
            arrayGhosts[1].GetComponent<EnvironmentsPersonMessage>().SetCurrentMessage($"Нужно еще {taskDetails.Gorox} гороха");
        }
    }

    public StructureTrayObjects CurrentQuestAddObject(string productName)
    {
        switch (productName)
        {
            case EnumBoxesName.MakaronsProduct: ProductsData.Makarons--; break ;
            case EnumBoxesName.GoroxProduct: ProductsData.Gorox--; break;
        }
        ProductsData.TotalProductsFroQuest--;
        return ProductsData;
    }

    public void ClearCurrentQuest()
    {
        ProductsData = new StructureTrayObjects(0, 0, 0);
        foreach (var item in arrayGhosts)
        {
            item.SetActive(false);
        }
        Transform products = transform.GetChild(1);
        for (int i = 0; i < products.childCount; i++) {
            products.GetChild(i).gameObject.SetActive(false);
        }
    }

}
