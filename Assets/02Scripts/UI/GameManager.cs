using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[AddComponentMenu("MyGame/GameManager")]
public class GameManager : MonoBehaviour {
    /// <summary>
    /// 游戏的三种状态
    /// </summary>
    public enum GameState
    {
        GAME,
        END
    }
    //存储三个ui界面的引用
    private GameObject mStartUI;
    private GameObject mGameUI;
    private GameObject mEndUI;
    //把所有的文本引用
    private Text mEDText;//击杀数
    private Text mLNText;//急救包的数量
    private Text mBNText;//子弹数
    private Text mOText;//操作提示
    private Text mSHText;//系统消息
    private Text mScore;//最终得分
    //生成成绩用的变量
    private int score;
    //开始标志
    private bool startCompute=false;
    //获取准星
    private Image mPosImage;
    //获取生命值进度条
    private RectTransform lifeMaxUI;
    private RectTransform lifeUI;
    private Image lifeImage;
    //当前的游戏状态
    private GameState gameState;
    //敌人生成器
    private EnemyManager mEnemyManager;
    //急救包生成
    private LifePSManager mLPSManager;
    //子弹补给生成
    private BBSManager mBBSManager;
    //玩家的背包
    private Backpacks mBackpack;
    //获取玩家
    private FPSPlayer mPlayer;
	void Start () {
        //角色及物品的引用
        mEnemyManager = GameObject.Find("Enemys").GetComponent<EnemyManager>();
        mLPSManager = GameObject.Find("LifePS").GetComponent<LifePSManager>();
        mBackpack = GameObject.Find("FPSController").GetComponent<Backpacks>();
        mBBSManager = GameObject.Find("BulletBS").GetComponent<BBSManager>();
        mPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<FPSPlayer>();
        //三个ui的引用
        mStartUI = GameObject.Find("StartUI");
        mGameUI = GameObject.Find("GameUI");
        mEndUI = GameObject.Find("EndUI");
        //文本引用
        mEDText = GameObject.Find("Canvas/GameUI/EDImage/EDText").GetComponent<Text>();
        mLNText = GameObject.Find("Canvas/GameUI/LifeImage/LifeNumText").GetComponent<Text>();
        mBNText = GameObject.Find("Canvas/GameUI/BulletImage/BullNumText").GetComponent<Text>();
        mOText = GameObject.Find("Canvas/GameUI/OText").GetComponent<Text>();
        mSHText = GameObject.Find("Canvas/GameUI/SHText").GetComponent<Text>();
        mScore = GameObject.Find("Score").GetComponent<Text>();
        //获取准星
        mPosImage = GameObject.Find("Canvas/GameUI/PosImage").GetComponent<Image>();
        //获取生命值进度条
        lifeUI = GameObject.Find("Canvas/GameUI/Life1/Life2").GetComponent<RectTransform>();
        lifeMaxUI = GameObject.Find("Canvas/GameUI/Life1").GetComponent<RectTransform>();
        lifeImage = GameObject.Find("Canvas/GameUI/Life1/Life2").GetComponent<Image>();
        //改变游戏状态
        ChangeGameState(GameState.GAME);
    }
	
	// Update is called once per frame
	void Update () {
        if(startCompute)
        {
            //更新分数
            mEDText.text = score.ToString();
            //如果玩家的生命值为零则游戏结束
            if (mPlayer.life <= 0)
            {
                //Cursor.lockState = CursorLockMode.None;
                ChangeGameState(GameState.END);
                mScore.text = mEDText.text;
                //重置
                score = 0;
                startCompute = false;
            }//将玩家生命值与生命值进度条关联
            else
            {
                lifeUI.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, mPlayer.life * 400 / mPlayer.lifeMax);
                if (mPlayer.life == 1)
                {
                    lifeImage.color = Color.red;
                    OperationHints("按Tab使用急救包");
                }
                else lifeImage.color = Color.white;
            }
        }
    }
    private void StartCompute()
    {
        startCompute = true;
    }
    /// <summary>
    /// 游戏状态转换方法
    /// </summary>
    /// <param name="state"></param>
    public void ChangeGameState(GameState state)
    {
        //存储传递过来的状态
        gameState = state;
        if (gameState == GameState.GAME)
        {
            mGameUI.SetActive(true);
            mEndUI.SetActive(false);
            //生成场内角色及物品
            //怪物
            mEnemyManager.StartCreateEnemy();
            //急救包
            mLPSManager.StartCreatePrefab();
            //子弹补给
            mBBSManager.StartCreatePrefab();
            //人物血量
            mPlayer.RemoveDamage();
            //初始化背包
            mBackpack.Remove();
            //同步ui
            LNUpdate(mBackpack.lPNum.ToString());
            BNUpdate(mBackpack.bNum.ToString());
            PosUpdate02();
            StartCompute();
        }
        else if (gameState == GameState.END)
        {
            mGameUI.SetActive(false);
            mEndUI.SetActive(true);
            mEnemyManager.StopCreateEnemy();
            mEnemyManager.RemoveEnemy();
            mLPSManager.RemoveLP();
            mBBSManager.StopCreatePrefab();
            mBBSManager.RemovePrefab();
        }
    }
    /// <summary>
    /// 更新提示
    /// </summary>
    /// <param name="operationHints">提示文本内容</param>
    public void OperationHints(string operationHints)
    {
        mOText.text = operationHints;
        Invoke("RemoveOperationHints", 2.0f);
    }
    /// <summary>
    /// 更新系统消息
    /// </summary>
    /// <param name="systemHints"></param>
    public void SystemHints(string systemHints)
    {
        mSHText.text = systemHints;
        Invoke("RemoveSystemHints", 2.0f);
    }
    /// <summary>
    /// 更新急救包数量
    /// </summary>
    public void LNUpdate(string s)
    {
        mLNText.text = s+"/" + mBackpack.lPMaxNum;
    }
    /// <summary>
    /// 更新子弹数量
    /// </summary>
    /// <param name="s"></param>
    public void BNUpdate(string s)
    {
        mBNText.text = s+"/"+mBackpack.bMaxNum;
    }
    
    /// <summary>
    /// 更新击杀数量
    /// </summary>
    public void EDUpdate()
    {
        Debug.Log("qian"+score);
        score++;
        Debug.Log("hou"+score);
    }
    /// <summary>
    /// 更新ui，准星变成红色
    /// </summary>
    public void PosUpdate01()
    {
        mPosImage.color = Color.red;
    }
    /// <summary>
    /// 更新ui，准星变回黄色
    /// </summary>
    public void PosUpdate02()
    {
        mPosImage.color = Color.yellow;
    }

    /// <summary>
    /// 重置提示
    /// </summary>
    private void RemoveOperationHints()
    {
        mOText.text = " ";
    }
    private void RemoveSystemHints()
    {
        mSHText.text = " ";
    }

}
