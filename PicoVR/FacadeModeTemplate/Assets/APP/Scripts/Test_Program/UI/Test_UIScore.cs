using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;


/*  
 * 使用流程说明：
 *      1  创建新的子功能类，继承UIBase
 *      2  实现两个抽象方法和一个抽象属性
 *      3  在Consts中增加新功能类的Panel的字符串，详见Consts类中的示例
 *      4  在Consts中增加该功能的具体作用字符串，详见Consts类中的示例
 *      5  构造函数和增加相应的方法，来实现该类的具体作用。
 */

public class Test_UIScore : UIBase
{
    Text scoreText;
    Button button;
    /// <summary>
    /// 默认构造函数
    /// </summary>
    public Test_UIScore()
    {
        //UI管理器的注册消息事件关联到方法
        UIManager.RegisterMessageEvent += RegisterRelateMessage;

        scoreText = GameObject.Find("ScorePanel/Text").GetComponent<Text>();

        button = GameObject.Find("ScorePanel/Button").GetComponent<Button>();
        button.onClick.AddListener(OnButtonClick);

    }
    public override string Name
    {
        get { return Consts.UI_Score; }
    }
    /// <summary>
    /// 注册关联消息，是分开注册关联，还是集合到一起注册关联，决定分开注册关联
    /// </summary>
    public override void RegisterRelateMessage()
    {
        UIMessageArgs ma = new UIMessageArgs();
        ma.Name = Consts.Msg_UI_Score;
        ma.CallBack += DisplayScore;
        RelateMessage.Add(ma);
    }
    public override void HandleMessage(string message)
    {
        foreach(UIMessageArgs ma in RelateMessage)
        {
            if(ma.Name.Contains(message))
            {
                ma.CallBack();
            }
        }
    }
    void DisplayScore()
    {
        scoreText.text = "hehe";
    }
    void OnButtonClick()
    {
        if (scoreText.text.Contains("haha"))
        {
            DisplayScore();
        }
        else
        {
            scoreText.text = "haha";
        }
    }
}