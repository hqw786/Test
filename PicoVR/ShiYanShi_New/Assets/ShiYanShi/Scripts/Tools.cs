using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class Tools
{
    //public static Material fadeMate;
    /// <summary>
    /// 淡入淡出效果
    /// </summary>
    /// <param name="leftEye">左摄像机</param>
    /// <param name="rightEye">右摄像机</param>
    /// <param name="time">时长</param>
    /// <returns></returns>
    public static IEnumerator FadeInFadeOut(GameObject leftEye, GameObject rightEye, float time)
    {
        leftEye.transform.Find("Image").GetComponent<ImageFadeInOut>().StartFadeOut();
        yield return new WaitForSeconds(time);

        Quaternion q = leftEye.transform.parent.parent.rotation;
        q = q * Quaternion.Euler(0, 90, 0);
        leftEye.transform.parent.parent.rotation = q;

        leftEye.transform.Find("Image").GetComponent<ImageFadeInOut>().StartFadeIn();
        leftEye.transform.parent.GetComponent<CameraScale>().ReturnOriginPosition();

        yield return new WaitForSeconds(time);
        SYSManager.Instance.StartShowFlow();
    }
    /// <summary>
    /// 显示屏上显示内容
    /// </summary>
    /// <param name="cons">每一阶段数据</param>
    /// <param name="time">显示时间</param>
    /// <returns></returns>
    public static IEnumerator DisplayContent(List<string[]> cons,float time, float alphaTime)
    {
        for (int i = 0; i < cons.Count; i++)
        {

            //显示屏内容的淡出
            SYSManager.Instance.isContentAlphaHide = true;
            yield return new WaitForSeconds(alphaTime);
            SYSManager.Instance.isContentAlphaHide = false;
            SYSManager.Instance.HideContent();

            yield return new WaitForSeconds(1f);//淡出淡入的1S停顿
            //翻页周数
            SYSManager.Instance.TransWeek("Bottom", cons[i][0]);

            //显示屏内容的淡入
            SYSManager.Instance.contentDisplay(cons[i]);
            SYSManager.Instance.isContentAlphaDisplay = true;
            yield return new WaitForSeconds(alphaTime);
            SYSManager.Instance.isContentAlphaDisplay = false;
            //显示屏内容的显示时间
            yield return new WaitForSeconds(time);
        }
		SYSManager.Instance.isContentAlphaHide = true;
		yield return new WaitForSeconds(alphaTime);
		SYSManager.Instance.isContentAlphaHide = false;
        SYSManager.Instance.HideContent();
    }

    public static IEnumerator TransModelAlpha(float time)
    {
	    yield return new WaitForSeconds(0.5f);
        SYSManager.Instance.isLastModelAlpha = true;
        yield return new WaitForSeconds(time * 0.8f);
        SYSManager.Instance.isCurModelAlpha = true;
        yield return new WaitForSeconds(time * 0.15f);
        SYSManager.Instance.isModelAlphaDone = true;
    }


    /// <summary>
    /// 将指定文件中的内容保存到类中
    /// </summary>
    /// <param name="name"></param>
    public static void GetTextData(string name)
    {
        GetFileContent(name);
    }
    /// <summary>
    /// 从指定文件中读取文件内容
    /// </summary>
    /// <param name="name"></param>
    public static void GetFileContent(string name)
    {
        TextAsset ta = Resources.Load<TextAsset>(name);
        string[] content = ta.text.Split("\n"[0]);
        ParseContent(content);
    }
    /// <summary>
    /// 根据文件内容保存出数据
    /// </summary>
    /// <param name="content"></param>
    public static void ParseContent(string[] content)
    {
        int index = -1;
        for (int i = 0; i < content.Length; i++)
        {
            string[] temp = content[i].Split(","[0]);

            if(ConfigData.Instance.strStage.Contains(temp[0].ToString().Substring(0,3)))
            {
                index++;
                StageInfo si = new StageInfo();
                ConfigData.Instance.Data.Add(si);
                ConfigData.Instance.Data[index].ID = index;
                ConfigData.Instance.Data[index].Name = ConfigData.Instance.strStage[index];
                ConfigData.Instance.Data[index].isLock = index == 0 ? false : true;
            }
            else
            {
                 ConfigData.Instance.Data[index].Context.Add(temp);
            }
        }
    }
}