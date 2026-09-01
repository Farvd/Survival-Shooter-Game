using Unity.VisualScripting;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public int Damage = 10;
    public float AttackRange = 10f; 
    private float attackCooldown = 0f;
    public float AttackDelay = 1f;
    private Transform player;

    void Start()
    {
        
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    void Update()
    {
        
        if (attackCooldown > 0)
        {
            attackCooldown -= Time.deltaTime;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))

                if (attackCooldown <= 0)
                {
                    PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
                    attackPlayer(playerHealth);
                    attackCooldown = AttackDelay;
                }
            }
        
    

//    bool CheckIsPlayerInSight(Transform playerTransform)
//    {
//        Vector3 directionToPlayer = (playerTransform.position - transform.position).normalized;
//        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
//
//        if (distanceToPlayer > AttackRange)
//        {
//            return false;
//        }
//
//        RaycastHit hit;
//        if (Physics.Raycast(transform.position, directionToPlayer, out hit, AttackRange))
//        {
//            
//            if (hit.collider.CompareTag("Player"))
//            {
//                print("Player in sight ");
//                return true;
//            }
//            else
//            {
//                return false;
//            }
//        }
//        
//        return false;
//    }

    void attackPlayer(PlayerHealth player)
    {
        print("Attacking player");
        if (player != null)
        {
            player.TakeDamage(Damage);
        }
    }


}
