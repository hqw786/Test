using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*  
 * 使用流程说明：
 *      1  创建新的子功能类，继承UIBase
 *      2  实现两个抽象方法和一个抽象属性
 *      3  在Consts中增加新功能类的Panel的字符串，详见Consts类中的示例
 *      4  在Consts中增加该功能的具体作用字符串，详见Consts类中的示例
 *      5  构造函数和增加相应的方法，来实现该类的具体作用。
 */
public abstract class UIBase
{
    //标识名称
    public abstract string Name{get;}
    //关联消息
    public List<UIMessageArgs> RelateMessage = new List<UIMessageArgs>();
    //注册关联消息
    //public abstract void RegisterRelateMessage(string message);
    public abstract void RegisterRelateMessage();
    //处理关联消息
    public abstract void HandleMessage(string message, object arg);
}
