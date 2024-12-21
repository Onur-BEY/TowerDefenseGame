using UnityEngine;

// Bu scriptin �al��mas� i�in Enemy Component'i gerekli demektir.
[RequireComponent(typeof(Enemy))] 
// �imdi istesek de istemesek de ilgili bile�enden Enemy scriptini kald�ramay�z.
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
        // transform.Translate: Nesnenin konumunu belirli bir miktar kayd�rmak i�in kullan�l�r.
        // dir.normalized: dir vekt�r� birim vekt�re d�n��t�r�l�r.Bu sayede vekt�r�n uzunlu�u 1 olur ve hareket h�z� sadece speed de�eri ile kontrol edilir.
        // speed: Nesnenin hareket h�z�n� belirleyen sabit bir de�erdir.
        // Time.deltaTime: Kareler aras�ndaki ge�en s�reyi temsil eder. Hareketin kare h�z�ndan ba��ms�z olmas�n� sa�lar.
        // Space.World: Hareketin d�nya uzay�nda yap�laca��n� belirtir.Yani hareket, d�nya koordinat sistemine g�re hesaplan�r.

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
            return;  // Game objesini yok edene kadar biraz zaman alaca�� i�in OutOfRange hatas� vermesin alt sat�ra da ge�mesin diye yazd�k.
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
