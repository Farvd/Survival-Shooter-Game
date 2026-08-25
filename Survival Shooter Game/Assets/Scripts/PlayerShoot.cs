using Mono.Cecil;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;


[System.Serializable]
public class PlayerWeapon
{

    public string Name;
    public float Damage;
    public float ShootDelay;
    public float Range;
    public bool isShotgun;
    public bool isAutomatic;
    public Vector3 Spread;
    public GameObject WeaponPrefab;
}
public class PlayerShoot : MonoBehaviour
{
    [Header("Main")]
    public GameObject LocatorPrefab;
    public TrailRenderer BulletTrailPrefab;
    public float LastShootTime;
    public float ShootDelay = 0.001f;
    public int CurrentWeapon = 0;


    [Header("Weapons")]

    public PlayerWeapon[] Weapons;


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
        CheckWeaponType();
        
    }
    void Shoot()
    {
        if (LastShootTime + ShootDelay < Time.time)
        {

            Vector3 direction = GetDirection();
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.transform.position, direction, out hit, Weapons[CurrentWeapon].Range))
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
    private Vector3 GetDirection()
    {
        Vector3 direction = Camera.main.transform.forward;
        direction += new Vector3(
            Random.Range(-Weapons[CurrentWeapon].Spread.x, Weapons[CurrentWeapon].Spread.x), 
            Random.Range(-Weapons[CurrentWeapon].Spread.y, Weapons[CurrentWeapon].Spread.y), 
            Random.Range(-Weapons[CurrentWeapon].Spread.z, Weapons[CurrentWeapon].Spread.z));
        return direction.normalized;
    }

    public void CheckWeaponType()
    {
        for (int i = 0; i < Weapons.Length; i++)
        {
                GameObject currentHeldWeapon = Camera.main.transform.GetChild(i).gameObject;
                if(currentHeldWeapon.activeInHierarchy && currentHeldWeapon.name != Weapons[CurrentWeapon].WeaponPrefab.name)
                {
                    currentHeldWeapon.SetActive(false);
                }
                if (!currentHeldWeapon.activeInHierarchy && currentHeldWeapon.name == Weapons[CurrentWeapon].WeaponPrefab.name)
                {
                    currentHeldWeapon.SetActive(true);
                }
            }
    }




}

