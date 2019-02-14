using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[AddComponentMenu("MyGame/EnemyAttackTrigger")]
public class EnemyAttackTrigger : MonoBehaviour {
    private FPSPlayer mPlayer;
    private Animator animator;//动画组件
    public GameObject myParent;
    void Start () {
        mPlayer = GameObject.Find("FPSController").GetComponent<FPSPlayer>();
        //获取动画组件
        animator = myParent.GetComponent<Animator>();
    }
   
    private void OnTriggerEnter(Collider other)
    {
        //获取当前动画状态
        AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);
        if (info.fullPathHash == Animator.StringToHash("Base Layer.Attack") && !animator.IsInTransition(0))
        {
            //判断进入碰撞器的是不是玩家
            if (other.gameObject.tag == "Player")
            {
                mPlayer.OnDamage(1);
                //Debug.Log("碰到了");
            }
        }
            
    }
}
