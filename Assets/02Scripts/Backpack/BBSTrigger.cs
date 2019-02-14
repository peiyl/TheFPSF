using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[AddComponentMenu("MyGame/BBSTrigger")]
public class BBSTrigger : MonoBehaviour {
    private Backpacks mBackpack;
    private GameManager mGameManager;
    private BBSManager mBBSManager;
    private int mNum;
    void Start () {
        mBackpack = GameObject.Find("FPSController").GetComponent<Backpacks>();
        mGameManager = GameObject.Find("Canvas").GetComponent<GameManager>();
        mBBSManager = GameObject.Find("BulletBS").GetComponent<BBSManager>();
        mNum = Random.Range(1, 11);
        //Debug.Log("生成了一个子弹数为" + mNum);
    }
    private void OnTriggerStay(Collider other)
    {
        //判断进入碰撞器的是不是玩家
        if (other.gameObject.tag == "Player")
        {
            //判断玩家背包是否已满
            if (mBackpack.bNum == mBackpack.bMaxNum)
            {
                mGameManager.OperationHints("子弹已经很多了,请使用已有子弹后再拾取");
                return;
            }
            else
            {
                //Debug.Log("背包没有满");
                //提示拾取并更新背包
                mGameManager.OperationHints("按F键拾取");
                if (Input.GetKey(KeyCode.F))
                {
                    mBackpack.BNumAdd(mNum);
                    //Debug.Log("我被吃掉了" + mNum);
                    mBBSManager.PrefabReduce();
                    GameObject.Destroy(gameObject);
                }
            }
        }
    }
}
