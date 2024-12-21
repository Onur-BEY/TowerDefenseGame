using System.Collections;
using UnityEngine;


[System.Serializable] // Bu s�n�f�n �zelliklerinin ve de�i�kenlerinin inspector(denet�i)'da g�r�nmesi
                      // ve iste�e ba�l� de�i�tirilebilmesini sa�lar.
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
