using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;
    public float speed = 70f;
    public GameObject impactEffect;
    public float explosionRadius = 0f;
    public int damage = 50;

    public void Seek(Transform _target) 
    {
        target = _target; 
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position-transform.position;
        float distanceThisFrame=speed*Time.deltaTime;

        if(dir.magnitude <= distanceThisFrame) // Vekt�r�n uzunlu�unu bulur.
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized*distanceThisFrame,Space.World); // Belirtilen y�nde ve h�zda hareket sa�lar.
        transform.LookAt(target); // Kur�un/F�ze 'nin hedefe bakmas�n� sa�lar.
    }

    void HitTarget()
    {
        GameObject effectsIns = Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effectsIns,5f);

        if(explosionRadius > 0f)
        {
            Explode();    
        }
        else
        {
            Damage(target);
        }

        Destroy(gameObject);
    }

    void Explode()
    {
        Collider [] colliders=Physics.OverlapSphere(transform.position, explosionRadius);

        foreach(Collider collider in colliders)
        {
            if (collider.tag == "Enemy")
            {
                Damage(collider.transform);
            }
        }
    }

    void Damage(Transform enemy)
    {
        Enemy e=enemy.GetComponent<Enemy>();

        if (e != null)
        {
            e.TakeDamage(damage);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
