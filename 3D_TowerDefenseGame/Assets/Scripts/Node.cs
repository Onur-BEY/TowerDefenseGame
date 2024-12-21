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
        // Renderer objesinin bir defa rengini al�r.
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
        buildManager=BuildManager.instance;        
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }

    // Mouse, Node'un �zerine geldi�inde rengi de�i�ir.
    private void OnMouseEnter()
    {
        if(EventSystem.current.IsPointerOverGameObject()) 
            // E�er imle� bir GameObject'in �st�ndeyse hi�bir �ey yapmaz.
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

    // Mouse'a t�kland���nda o Node'un �zerinde turret yoksa turret in�a edebilir varsa edemez.
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

        // Fare ile t�klan�lan Node'un bilgilerini g�nderir.
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

        // Yeni y�kseltilmi� turret'i in�a eder.
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

    // Mouse, Node'un �zerinden ��kt���nda rengi normal rengine geri d�ner.
    private void OnMouseExit()
    {
        rend.material.color=startColor;
    }
}
