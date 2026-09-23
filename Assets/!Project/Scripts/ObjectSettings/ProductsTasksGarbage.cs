using System.Collections.Generic;
using UnityEngine;

public class ProductsTasksGarbage : MonoBehaviour
{
    public static ProductsTasksGarbage Instance;

    private StructureTasksGarbage structureGarbageProducts;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        structureGarbageProducts = new StructureTasksGarbage(0);
    }

    public void AddProductInStructure(ShelfController newShelf)
    {
        switch (newShelf.ShelfProductName)
        {
            case (EnumBoxesName.MakaronsProduct):
                if (!structureGarbageProducts.makaron.Exists(o => o.shelf._unicShelfName == newShelf._unicShelfName))
                {
                    structureGarbageProducts.makaron.Add(new StructureTasksGarbageObjects(newShelf, newShelf.transform.parent.transform.parent.gameObject));
                }
                break;
            case (EnumBoxesName.GoroxProduct):
                if (!structureGarbageProducts.gorox.Exists(o => o.shelf._unicShelfName == newShelf._unicShelfName))
                {
                    structureGarbageProducts.gorox.Add(new StructureTasksGarbageObjects(newShelf, newShelf.transform.parent.transform.parent.gameObject));
                }
                break;
        }
    }

    public void CheckProduct(string objectName, int objectCount)
    {
        CanvasPauseController.Instance.PauseButtonChangeInteractable(false);
        switch (objectName)
        {
            case (EnumBoxesName.MakaronsProduct):
                for (int i = 0; i < objectCount; i++)
                {
                    if (structureGarbageProducts.makaron.Count > 0)
                    {
                        structureGarbageProducts.makaron[0].shelf.TakeoverOneObject();
                        if (!structureGarbageProducts.makaron[0].shelf.HasActiveElements())
                        {
                            structureGarbageProducts.makaron.RemoveAt(0);
                        }
                        TaskBoards.Current.TaskBoardController.Instance.CompletePartOfTask(EnumBoxesName.MakaronsProduct, 1);
                    }
                    else
                    {
                        PersonMessageLifeCycle.Instance.SendLifeCycleMessage("Не хватает макарон");
                        TaskBoards.Current.TaskBoardController.Instance.CompletePartOfTask(EnumBoxesName.MakaronsProduct, 0);
                        break;
                    }
                }
                break;
            case (EnumBoxesName.GoroxProduct):
                for (int i = 0; i < objectCount; i++)
                {
                    if (structureGarbageProducts.gorox.Count > 0)
                    {
                        structureGarbageProducts.gorox[0].shelf.TakeoverOneObject();
                        if (!structureGarbageProducts.gorox[0].shelf.HasActiveElements())
                        {
                            structureGarbageProducts.gorox.RemoveAt(0);
                        }
                        TaskBoards.Current.TaskBoardController.Instance.CompletePartOfTask(EnumBoxesName.GoroxProduct, 1);
                    }
                    else
                    {
                        PersonMessageLifeCycle.Instance.SendLifeCycleMessage("Не хватает гороха");
                        TaskBoards.Current.TaskBoardController.Instance.CompletePartOfTask(EnumBoxesName.GoroxProduct, 0);
                        break;
                    }
                }
                break;
        }

    }
}
