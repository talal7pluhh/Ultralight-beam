using System.Collections;
using UnityEngine;
using UnityEngine.AI;
public class PlayerChase : MonoBehaviour
{

    [SerializeField] private Light flashLight;
    [SerializeField] private float dimIntensity = 10f;
    public float jumpScareDistance = 0.7f;
    public Transform zombieFace;
    public Transform playerCamera;
    private bool gameOverTriggered = false;
    private Animator animator;
    public NavMeshAgent enemy;
    public Transform Player;
    public float chaseRange = 10f;
    public float attackRange = 3f;
    public float roamRadius = 10f;
    public GameObject touchLook;
    public JoyStickMove joyStickMove;
    private float AttackStartTime;
    private bool jumpScareActive = false;
    public AudioSource source;
    public AudioClip Jumpscare;

    private enum State
    {
        Roaming,
        Chasing,
        Attacking
    }
    private State state = State.Roaming;
    private Vector3 roamTarget;

    void Start()
    {
        animator = GetComponent<Animator>();
        enemy = GetComponent<NavMeshAgent>();

        if (enemy != null && enemy.stoppingDistance != 0)
        {
            enemy.stoppingDistance = 0f;
        }
        SetNewRoamTarget();
    }
    void Update()
    {
        if (gameOverTriggered)
        {
            forceJumpscareCamera();
            return;
        }
        ;

        float distance = Vector3.Distance(transform.position, Player.position);

        if (distance <= attackRange)
        {
            state = State.Attacking;
        } 
        else if (distance <= chaseRange)
        {
            state = State.Chasing;
        } 
        else
        {
            state = State.Roaming;
        }

        switch (state)
        {
            case State.Roaming:
                Roam();
                animator.SetBool("isRunning", false);
                animator.SetBool("isAttacking", false);
                break;

            case State.Chasing:
                enemy.SetDestination(Player.position);
                animator.SetBool("isAttacking", false);
                animator.SetBool("isRunning", true);
                break;

            case State.Attacking:
                if (!gameOverTriggered)
                {
                    gameOverTriggered = true;
                    jumpScareActive = true;
                    AttackStartTime = Time.time;
                    StartCoroutine(AttackAndGameOverCouroutine());
                }
                break;
    }   }      
    

    private void forceJumpscareCamera()
    {
        if (playerCamera == null || zombieFace == null) 
        {
            return;
        }

        if (flashLight != null)
        {
            // flashLight.color = Color.darkRed;
            flashLight.intensity = dimIntensity;
        }
        if (source != null && Jumpscare != null)
        {
            source.PlayOneShot(Jumpscare);
            source.volume = 0.5f;
        } else
        {
            Debug.Log("nothing was found");
        }
            Vector3 targetPos = zombieFace.position - zombieFace.forward * 0.85f;
        playerCamera.position = targetPos;
        playerCamera.LookAt(zombieFace.position);
        playerCamera.SetParent(zombieFace);
    }

    private void Roam()
    {
        if (enemy == null) return;

        if (!enemy.hasPath || enemy.remainingDistance < 0.5f)
        {
            SetNewRoamTarget();
        }
    }

    private void SetNewRoamTarget()
    {
        if (enemy == null) return;
        Vector3 randomDirection = Random.insideUnitSphere * roamRadius;
        randomDirection += transform.position;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, roamRadius, NavMesh.AllAreas))
        {
            roamTarget = hit.position;
            enemy.SetDestination(roamTarget);
        }
    }

    // really tuff code i barely understood (i'm fucked) // 
    IEnumerator AttackAndGameOverCouroutine()
    {

        gameOverTriggered = true;
        AttackStartTime = Time.time;

        // Stop zombie movement
        if (enemy != null)
        {
            enemy.isStopped = true;
            enemy.ResetPath();
            enemy.velocity = Vector3.zero;
        }

        // Stop player input completely
        if (touchLook != null)
        {
            touchLook.SetActive(false);
            var touchLookScript = touchLook.GetComponent<TouchLook>();
            if (touchLookScript != null)
            {
                touchLookScript.enabled = false;
            }
        }
        if (joyStickMove != null)
        {
            joyStickMove.enabled = false;
        }

        // Detach camera completely

        if (playerCamera != null)
        {
            playerCamera.SetParent(null);
            MonoBehaviour[] scripts = playerCamera.GetComponentsInChildren<MonoBehaviour>();
            string[] scriptsToDisable = { "TouchLook", "JoyStickMove" };
            foreach (var script in scripts)
            {
                if (System.Array.IndexOf(scriptsToDisable, script.GetType().Name) >= 0)
                {
                    script.enabled = false;
                }
            }
        }
        jumpScareActive = true;
        forceJumpscareCamera();

        yield return new WaitForSeconds(0.06f);
        forceJumpscareCamera();

#if UNITY_ANDROID || UNITY_IOS
        Handheld.Vibrate();
#endif
        
        // btw i don't understand this yield shit but it looks tuff //
        
        yield return new WaitForSecondsRealtime(1f);

        GameOverManager.Instance.ShowGameOver();
    }
}