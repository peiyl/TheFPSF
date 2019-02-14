using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[AddComponentMenu("MyGame/LifePSTrigger")]
public class LifePSTrigger : MonoBehaviour
{
    private Backpacks mBackpack;
    private GameManager mGameManager;
    void Start()
    {
        mBackpack = GameObject.Find("FPSController").GetComponent<Backpacks>();
        mGameManager = GameObject.Find("Canvas").GetComponent<GameManager>();
    }
    private void OnTriggerStay(Collider other)
    {
        //判断进入碰撞器的是不是玩家
        if (other.gameObject.tag == "Player")
        {
            //判断玩家背包是否已满
            if (mBackpack.lPNum == mBackpack.lPMaxNum)
            {
                mGameManager.OperationHints("急救包已经很多了,请使用已有急救包后再拾取");
                return;
            }
            else
            {
                //提示拾取并更新背包
                mGameManager.OperationHints("按F键拾取");
                if (Input.GetKeyDown(KeyCode.F))
                {
                    mBackpack.LPNumAdd();
                    GameObject.Destroy(gameObject);
                    mGameManager.OperationHints(" ");
                }
            }
        }
    }
}
