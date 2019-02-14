using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[AddComponentMenu("MyGame/PlayerAnimation")]
public class PlayerAnimation : MonoBehaviour
{
    private FPSPlayer mPlayer;
    private Animator myAnimator;//动画组件
    void Start()
    {
        //获取动画组件
        myAnimator = GetComponent<Animator>();
        mPlayer = GameObject.Find("FPSController").GetComponent<FPSPlayer>();
    }
    void Update()
    {
        float v = System.Math.Abs(Input.GetAxis("Vertical"));
        float h = System.Math.Abs(Input.GetAxis("Horizontal"));
        myAnimator.SetFloat("v", v);
        myAnimator.SetFloat("h", h);
        Debug.Log(mPlayer.shootTimer);
        if (Input.GetMouseButtonDown(0) && mPlayer.shootTimer <= 0)
        {
            myAnimator.SetTrigger("attack");
        }
    }
}
