using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[AddComponentMenu("MyGame/FPSPlayer")]
public class FPSPlayer : MonoBehaviour
{
    public int lifeMax = 5;
    public int life = 5;
    private Transform m_Transform;
    private CharacterController m_Controller;
    //枪
    public LayerMask layer = (1 << 8 | 1 << 9);//射击时射线能射到的碰撞层
    public Transform fx;//射中目标后的粒子效果
    public AudioClip clip;//射击音效
    public float shootTimer = 0;//射击间隔的计时器
    //摄像机
    private Transform cameraTransform;
    //玩家的背包
    private Backpacks mBackpack;
    //ui更新
    private GameManager mGameManager;

    void Start()
    {
        m_Transform = GetComponent<Transform>();
        m_Controller = GetComponent<CharacterController>();
        //获取摄像机
        cameraTransform = Camera.main.GetComponent<Transform>();
        //获取ui
        mGameManager = GameObject.Find("Canvas").GetComponent<GameManager>();
        //获取背包
        mBackpack = GameObject.Find("FPSController").GetComponent<Backpacks>();

    }
    /// <summary>
    /// 控制主角行动，如果生命为0则生命都不做
    /// </summary>
    void Update()
    {
        if (life <= 0)
        {
            return;
        }

        float v = System.Math.Abs(Input.GetAxis("Vertical"));
        float h = System.Math.Abs(Input.GetAxis("Horizontal"));
        //更新射击间隔时间
        shootTimer -= Time.deltaTime;
        Debug.Log("playertime:" + shootTimer);
        if (Input.GetMouseButtonDown(0) && shootTimer <= 0)
        {
            //如果有子弹就可以进行射击
            if (mBackpack.bNum > 0)
            {
                shootTimer = 1.0f;
                //播放射击音效
                GetComponent<AudioSource>().PlayOneShot(clip);
                //更新ui，减少弹药数量
                mBackpack.BNumReduce();
                // 更新ui，准星变成红色
                mGameManager.PosUpdate01();
                //用一个RaycasHit对象保存射线的碰撞结果
                RaycastHit info;
                //该射线只与layer指定的层发生碰撞
                #region 原代码
                if (Physics.Raycast(cameraTransform.position, cameraTransform.TransformDirection(Vector3.forward), out info, 100, layer))
                {
                    //Debug.Log("shexian"+info.point);
                    //判断是否射中tag为Enemy的物体
                    if (info.transform.tag.Equals("Enemy"))
                    {
                        Debug.Log(info.transform.tag);
                        //敌人减少生命
                        info.transform.GetComponent<FPSEnemy>().OnDamage();
                        //在射中的地方释放一个粒子效果
                        Instantiate(fx, info.point, info.transform.rotation);
                    }
                }
                #endregion
            }
            else
            {
                mGameManager.OperationHints("没有子弹了");
            }
        }// 更新ui，准星变回黄色
        else mGameManager.PosUpdate02();
        //按键使用急救包
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (mBackpack.lPNum <= 0)
            {
                mGameManager.OperationHints("没有急救包");
            }
            else
            {
                Debug.Log("huixue");
                mBackpack.LPNumReduce();
                life = lifeMax;
            }
        }
    }
    /// <summary>
    /// 更新角色的生命值
    /// </summary>
    /// <param name="damage">减少的生命值</param>
    public void OnDamage(int damage)
    {
        life -= damage;
        Debug.Log("主角" + life);
        //更新ui生命值
        //如果生命为0，游戏结束，取消鼠标锁定
        if (life <= 0)
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
    public void RemoveDamage()
    {
        life = lifeMax;
    }
}
