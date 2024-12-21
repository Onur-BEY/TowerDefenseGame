using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance; // Singleton Design Pattern

    private void Awake()
    {
        if (instance != null)  // Bu sýnýfýn sadece bir örneðini oluþturur.
        {
            Debug.LogError("More than one BuildManager in scene!");
            return;
        }
        instance = this;
    }

    private TurretBlueprint turretToBuild;
    public bool CanBuild {  get {return turretToBuild != null;} }
    public bool HasMoney {  get { return PlayerStats.Money >= turretToBuild.cost; } }
    public GameObject buildEffect;
    public GameObject sellEffect;
    private Node selectedNode;
    public NodeUI nodeUI;

    public void SelectedNode(Node node)
    {
        if(selectedNode == node)
        {
            DeselectNode();
            return;
        }

        selectedNode = node;
        turretToBuild = null;
        nodeUI.SetTarget(node);
    }

    public void DeselectNode()
    {
        selectedNode = null;
        nodeUI.Hide();
    }

    public void SelectTurretToBuild(TurretBlueprint turret)
    {
        turretToBuild = turret;
        DeselectNode();
    }

    public TurretBlueprint GetTurretToBuild() 
    { 
        return turretToBuild;
    }
}
