using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[AddComponentMenu("MyGame/JCText")]
public class JCText : MonoBehaviour
{
    private int mNum;
    private string[] jC = { "只有攻击敌人的右胸口才能击毙它","按键“W、A、S、D”控制方向","空格键可以跳跃","按住tab可以使用急救包","补给只有新的弹药没有新的急救包"};
    void Start()
    {
        InvokeRepeating("JC", 0.0f, 3.0f);
    }
    private void JC()
    {
        if (mNum >=jC.Length)
        {
            Destroy(gameObject);
        }
        else
        {
            gameObject.GetComponent<Text>().text = jC[mNum];
            mNum++;
        }
    }
}
