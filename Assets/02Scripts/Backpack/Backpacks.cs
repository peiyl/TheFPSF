using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[AddComponentMenu("MyGame/Backpacks")]
public class Backpacks : MonoBehaviour {
    //背包最大值
    public int lPMaxNum;
    public int bMaxNum;
    //背包现有的物品值
    public int lPNum;
    public int bNum;
    private GameManager mGameManager;
    void Start()
    {
        mGameManager = GameObject.Find("Canvas").GetComponent<GameManager>();
    }
    //进行增减操作
    /// <summary>
    /// 记录急救包数量增加
    /// </summary>
    public void LPNumAdd()
    {
        lPNum++;
        mGameManager.LNUpdate(lPNum.ToString());
    }
    /// <summary>
    /// 记录急救包数量减少
    /// </summary>
    public void LPNumReduce()
    {
        lPNum--;
        mGameManager.LNUpdate(lPNum.ToString());
    }
    /// <summary>
    /// 记录子弹数量增加
    /// </summary>
    /// <param name="num">增加的子弹数</param>
    public void BNumAdd(int num)
    {
        bNum += num;
        if(bNum>bMaxNum)
        {
            bNum = bMaxNum;
        }
        mGameManager.BNUpdate(bNum.ToString());
    }
    /// <summary>
    /// 记录子弹数量减少
    /// </summary>
    public void BNumReduce()
    {
        bNum--;
        mGameManager.BNUpdate(bNum.ToString());
    }
    /// <summary>
    /// 将背包数据初始化
    /// </summary>
    public void Remove()
    {
        lPNum = 0;
        bNum = 10;
        //Debug.Log("急救包数量"+lPNum);
        //Debug.Log("子弹数量" + bNum);
        //mGameManager.LNUpdate(lPNum.ToString());
        //mGameManager.BNUpdate(bNum.ToString());
    }
}
