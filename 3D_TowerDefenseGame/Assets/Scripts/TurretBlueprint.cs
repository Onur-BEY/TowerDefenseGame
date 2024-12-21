using System.Collections;
using UnityEngine;


[System.Serializable] // Bu sýnýfýn özelliklerinin ve deðiþkenlerinin inspector(denetçi)'da görünmesi
                      // ve isteðe baðlý deðiþtirilebilmesini saðlar.
public class TurretBlueprint
{
    public GameObject prefab;
    public int cost;
    public GameObject upgradedPrefab;
    public int upgradeCost;

    public int GetSellAmount()
    {
        return cost/2;
    }
}
