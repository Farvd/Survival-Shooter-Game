using Mono.Cecil;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    public GameObject LocatorPrefab;
    public TrailRenderer BulletTrailPrefab;
    public float LastShootTime;
    public float ShootDelay = 0.001f;
    private 
    void Awake()
    {
        LastShootTime = Time.time;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if ((Mouse.current.leftButton.isPressed))
        {
            Shoot();
        }
        
    }
    void Shoot()
    {
        if (LastShootTime + ShootDelay < Time.time)
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 100f))
            {
                GameObject locator = Instantiate(LocatorPrefab, hit.point, Quaternion.identity);
                if (locator != null)
                {
                    Destroy(locator, 10f);
                }
                EnemyHealth targetHit = hit.collider.gameObject.GetComponent<EnemyHealth>();
                if (targetHit != null)
                {
                    targetHit.TakeDamage(12f);
                    Destroy(locator);
                }


                TrailRenderer trail = Instantiate(BulletTrailPrefab, Camera.main.transform.position, Quaternion.identity);
                StartCoroutine(spawnTrail(trail, hit.point));
            }
            LastShootTime = Time.time;
        }
    }

    private IEnumerator spawnTrail(TrailRenderer trail, Vector3 hitPoint)
    {
        float time = 0;
        Vector3 startPosition = trail.transform.position;
        while (time < 1)
        {
            trail.transform.position = Vector3.Lerp(startPosition, hitPoint, time);
            time += Time.deltaTime / trail.time;
            yield return null;
        }
        trail.transform.position = hitPoint;
        Destroy(trail.gameObject, trail.time);
    }
}

