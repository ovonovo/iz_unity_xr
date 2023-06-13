using UnityEngine;


public class SetAsTierTarget : MonoBehaviour
{
    void OnTriggerStay(Collider col)
    {
        if(col.CompareTag("Tier")){
            NavmeshTierController thisTNC = col.gameObject.GetComponent<NavmeshTierController>();
            if(thisTNC.isWaiting){
                thisTNC.goal = transform;
                thisTNC.moveToDestination = true;
            }
        }
    }
}
