using UnityEngine;

// Bu scriptin çalýþmasý için Enemy Component'i gerekli demektir.
[RequireComponent(typeof(Enemy))] 
// Þimdi istesek de istemesek de ilgili bileþenden Enemy scriptini kaldýramayýz.
public class EnemyMovement : MonoBehaviour
{
    private Transform target;
    private int wavepointIndex = 0;
    private Enemy enemy;

    void Start()
    {
        enemy = GetComponent<Enemy>();
        target = Waypoints.points[0];
    }

    void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * enemy.speed * Time.deltaTime, Space.World);

        // transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
        // transform.Translate: Nesnenin konumunu belirli bir miktar kaydýrmak için kullanýlýr.
        // dir.normalized: dir vektörü birim vektöre dönüþtürülür.Bu sayede vektörün uzunluðu 1 olur ve hareket hýzý sadece speed deðeri ile kontrol edilir.
        // speed: Nesnenin hareket hýzýný belirleyen sabit bir deðerdir.
        // Time.deltaTime: Kareler arasýndaki geçen süreyi temsil eder. Hareketin kare hýzýndan baðýmsýz olmasýný saðlar.
        // Space.World: Hareketin dünya uzayýnda yapýlacaðýný belirtir.Yani hareket, dünya koordinat sistemine göre hesaplanýr.

        enemy.speed = enemy.startSpeed;

        if (Vector3.Distance(transform.position, target.position) <= 0.2f)
        {
            GetNextWaypoint();
        }
    }

    void GetNextWaypoint()
    {
        if (wavepointIndex >= Waypoints.points.Length - 1)
        {
            EndPath();
            return;  // Game objesini yok edene kadar biraz zaman alacaðý için OutOfRange hatasý vermesin alt satýra da geçmesin diye yazdýk.
        }

        wavepointIndex++;
        target = Waypoints.points[wavepointIndex];
    }

    void EndPath()
    {
        PlayerStats.Lives--;
        WaveSpawner.EnemiesAlive--;
        Destroy(gameObject);
    }
}
