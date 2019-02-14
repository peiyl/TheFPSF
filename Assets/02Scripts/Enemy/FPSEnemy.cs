using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[AddComponentMenu("MyGame/FPSEnemy")]
public class FPSEnemy : MonoBehaviour
{
    private Transform m_Transform;
    private Animator animator;//动画组件
    private FPSPlayer Player;//主角
    private UnityEngine.AI.NavMeshAgent agent;//寻路组件
    private float moveSpeed = 2.5f;//移动速度
    private float rotateSpeed = 30;//角色旋转速度
    private float timer = 2;//计时器
    private bool life = true;//生命值
    void Start()
    {
        m_Transform = GetComponent<Transform>();
        //获取寻路组件
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        //获取动画组件
        animator = GetComponent<Animator>();
        //指定寻路器的行走速度
        agent.speed = moveSpeed;
    }
    /// <summary>
    /// 更新敌人行为
    /// </summary>
    void Update()
    {
        //更新获取主角
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<FPSPlayer>();
        //如果主角生命为0，则什么都不做
        if (Player.life <= 0)
        {
            return;
        }
        //获取当前动画状态
        AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);
        //待机状态
        if (info.fullPathHash == Animator.StringToHash("Base Layer.Idle") && !animator.IsInTransition(0))
        {
            animator.SetBool("idle", false);

            //待机一定时间
            timer -= Time.deltaTime;
            if (timer > 0)
            {
                return;
            }
            //如果距离主角1.5米以内，则进入攻击状态
            if (Vector3.Distance(transform.position, Player.transform.position) < 3.0f)
            {
                animator.SetBool("attack", true);
            }
            else
            {
                //重置定时器
                timer = 1;
                //恢复寻路
                agent.Resume();
                //设置寻路目标
                agent.SetDestination(Player.transform.position);
                //面向主角
                RotateTo();
                //Debug.Log(agent.destination);
                //进入行走状态
                animator.SetBool("run", true);
            }

        }
        //行走状态
        if (info.fullPathHash == Animator.StringToHash("Base Layer.Run") && !animator.IsInTransition(0))
        {
            animator.SetBool("run", false);
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                timer = 1;
                RotateTo();
                agent.SetDestination(Player.transform.position);
            }
            //
            if (Vector3.Distance(transform.position, Player.transform.position) < 3.0f)
            {
                agent.Stop();
                animator.SetBool("attack", true);
            }
        }
        //Debug.Log("攻击");
        if (info.fullPathHash == Animator.StringToHash("Base Layer.Attack") && !animator.IsInTransition(0))
        {
            animator.SetBool("attack", false);
            RotateTo();
            //如果动画播放完，重新进入待机
            if (info.normalizedTime >= 1)
            {
                //Debug.Log("idle");
                animator.SetBool("idle", true);
                //重置计时器
                timer = 2;
                //更新主角生命值
                //Player.OnDamage(1);
            }
        }
    }
    private void RotateTo()
    {
        //获取目标方向
        Vector3 targetDirection = Player.transform.position - transform.position;
        //计算旋转角度
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, rotateSpeed * Time.deltaTime, 0);
        //旋转至新方向
        transform.rotation = Quaternion.LookRotation(newDirection);
    }
    private void AgentPlayer()
    {
        //设置寻路目标
        agent.SetDestination(Player.transform.position);
    }
    public void OnDamage()
    {
        life = false;
        //Debug.Log(tag + life);
        //如果生命为0，进入死亡状态
        if (!life)
        {
            agent.Stop();
            animator.SetBool("death", true);
        }
    }
}

