using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    private Renderer rend;
    private Color startColor;
    public Color hoverColor;
    public Vector3 positionOffset;
    BuildManager buildManager;
    public Color notEnoughMoneyColor;

    [HideInInspector]
    public GameObject turret;
    [HideInInspector]
    public TurretBlueprint turretBlueprint;
    [HideInInspector]
    public bool isUpgraded=false;

    private void Start()
    {
        // Renderer objesinin bir defa rengini alýr.
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
        buildManager=BuildManager.instance;        
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }

    // Mouse, Node'un üzerine geldiðinde rengi deðiþir.
    private void OnMouseEnter()
    {
        if(EventSystem.current.IsPointerOverGameObject()) 
            // Eðer imleç bir GameObject'in üstündeyse hiçbir þey yapmaz.
            return;

        if (!buildManager.CanBuild)
        {
            return;
        }

        if (buildManager.HasMoney)
        {
            rend.material.color = hoverColor;
        }
        else
        {
            rend.material.color = notEnoughMoneyColor;
        }
    }

    // Mouse'a týklandýðýnda o Node'un üzerinde turret yoksa turret inþa edebilir varsa edemez.
    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (turret != null)
        {
            buildManager.SelectedNode(this);
            return;
        }

        if (!buildManager.CanBuild)
        {
            return;
        }

        // Fare ile týklanýlan Node'un bilgilerini gönderir.
        // buildManager.BuildTurretOn(this);

        BuildTurret(buildManager.GetTurretToBuild());
    }

    void BuildTurret(TurretBlueprint blueprint)
    {
        if (PlayerStats.Money < blueprint.cost)
        {
            Debug.Log("Not enough money to build that!");
            return;
        }

        PlayerStats.Money -= blueprint.cost;
        GameObject _turret = Instantiate(blueprint.prefab, GetBuildPosition(), Quaternion.identity);
        turret = _turret;
        turretBlueprint = blueprint;
        GameObject effect = Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);
        Debug.Log("Turret build!");
    }

    public void UpgradeTurret()
    {
        if (PlayerStats.Money < turretBlueprint.upgradeCost)
        {
            Debug.Log("Not enough money to upgrade that!");
            return;
        }

        PlayerStats.Money -= turretBlueprint.upgradeCost;

        Destroy(turret); // Eski turret'i yok eder.

        // Yeni yükseltilmiþ turret'i inþa eder.
        GameObject _turret = Instantiate(turretBlueprint.upgradedPrefab, GetBuildPosition(), Quaternion.identity);
        turret = _turret;

        GameObject effect = Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);

        isUpgraded = true;
        Debug.Log("Turret upgraded!");
    }

    public void SellTurret()
    {
        PlayerStats.Money += turretBlueprint.GetSellAmount();
        GameObject effect = Instantiate(buildManager.sellEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);
        Destroy(turret);
        turretBlueprint = null;
    }

    // Mouse, Node'un üzerinden çýktýðýnda rengi normal rengine geri döner.
    private void OnMouseExit()
    {
        rend.material.color=startColor;
    }
}
