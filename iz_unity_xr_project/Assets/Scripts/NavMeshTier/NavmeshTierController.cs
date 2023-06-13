using UnityEngine;
using UnityEngine.AI;

// https://docs.unity3d.com/ScriptReference/AI.NavMeshAgent.html

public class NavmeshTierController : MonoBehaviour
{
    public Transform goal;
    NavMeshAgent agent;
    public Animator anim;
    int animLayer = 0;
    public bool moveToDestination;
    bool isWalking;
    bool startEating;
    bool isEating;
    public bool isWaiting = true;

    void Start () {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if(goal == null){
            ResetBools();
        }

        if(moveToDestination) {
            agent.destination = goal.position; 
            anim.SetBool("walk", true);
            anim.SetBool("eat", false);
            isWaiting = false;

            if (agent.velocity.magnitude > 0.01) isWalking = true;
            float goalScale = goal.localScale.x/2;
            float myScale = transform.localScale.x/2;
            if (agent.remainingDistance <= goalScale+myScale && isWalking)
            {
                moveToDestination = false;
                anim.SetBool("eat", true);
                anim.SetBool("walk", false);
                isWalking = false;
                startEating = true;
            }
        }

        if(isPlaying(anim, "Armature_eat") && startEating) {
            startEating = false;
            isEating = true;
        }

        if (!isPlaying(anim, "Armature_eat") && isEating) {
            anim.SetBool("eat", false);
            isWalking = false;
            isEating = false;
            EatBall();
            print("now destrox");
        }
    }

    public void EatBall() {
        Destroy(goal.gameObject);
        isWaiting = true;
    }

    void ResetBools(){
        moveToDestination = false;
        isWaiting = true;
        isWalking = false;
        startEating = false;
        isEating = false;
        anim.SetBool("walk", false);
        anim.SetBool("eat", false);
    }

    bool isPlaying(Animator anim, string stateName) {
        bool isName = anim.GetCurrentAnimatorStateInfo(animLayer).IsName(stateName);
        float nTime = anim.GetCurrentAnimatorStateInfo(animLayer).normalizedTime;

        if (isName && nTime < 1.0f) return true;
        else return false;
    }
}
