using UnityEngine;
using UnityEngine.AI;


public class EnemyController : MonoBehaviour
{

    public float lookRadius = 10f;
    public float timeBetweenAttacks = 1f;

    Transform target;
    NavMeshAgent agent;
    Damage damage;

    private bool atackeReady;
    float time = 1.0f;

    void Start()
    {
        target = PlayerController.instanse.player.transform;
        agent = GetComponent<NavMeshAgent>();
        damage = GetComponent<Damage>();
    }

    void Update()
    {

        float distance = Vector3.Distance(target.position, transform.position);


        if (distance <= lookRadius )
        {
            agent.SetDestination(target.position);
            if (!atackeReady)
            {
                if (distance <= agent.stoppingDistance)
                {

                    damage.EnemyAttack();
                    FaceTarget();

                    atackeReady = true;
                    Invoke(nameof(ResetAttack), timeBetweenAttacks);
                    Debug.Log(atackeReady);
                }
                
            }

        }
    }
    private void ResetAttack()
    {
        atackeReady = false;
    }
    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
    
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

}