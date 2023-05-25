using UnityEngine;

public class PlayAnimationReverse : MonoBehaviour
{
    public Animator anim;
    public bool playBackwards;

    // Update is called once per frame
    void Update()
    {
        if(playBackwards){
             anim.SetFloat("animSpeed", -1);
        }else{
            anim.SetFloat("animSpeed", 1);
        }
    }
}
