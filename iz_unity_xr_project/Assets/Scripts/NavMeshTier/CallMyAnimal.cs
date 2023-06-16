using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallMyAnimal : MonoBehaviour
{
    public GameObject[] myBalls;
    public Transform targetPosition;
    public GameObject myAnimal;
    bool moveBall;
    GameObject currentBall;
    bool calledAnimal = false;

    void Update()
    {
        if (!moveBall) return;
        var ballPos = currentBall.transform.position;
        currentBall.transform.position = Vector3.Lerp(ballPos, targetPosition.position, 0.01f);

        float distance = Vector3.Distance(ballPos, targetPosition.position);
        if (distance <= 0.1)
        {
            moveBall = false;
            if (!calledAnimal)
            {
                calledAnimal = true;
                NavmeshTierController thisTNC = myAnimal.gameObject.GetComponent<NavmeshTierController>();
                if (thisTNC.isWaiting)
                {
                    thisTNC.ballToEat = currentBall;
                    thisTNC.goal = targetPosition;
                    thisTNC.moveToDestination = true;
                    currentBall = null;
                }
            }
        }
    }

    void OnTriggerEnter(Collider col)
    {
        for (int i = 0; i < myBalls.Length; i++)
        {
            if (col.gameObject == myBalls[i])
            {
                currentBall = myBalls[i];
                myBalls[i].GetComponent<Rigidbody>().isKinematic = true;
                moveBall = true;
            }
        }

    }
}
