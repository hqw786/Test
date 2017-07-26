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
        //FadeInOutEffect leftFade = leftEye.GetComponent<FadeInOutEffect>();
        //FadeInOutEffect rightFade = rightEye.GetComponent<FadeInOutEffect>();
        //淡出
        //leftFade.isFadeIn = false;
        //rightFade.isFadeIn = false;

        //leftFade.isFadeOut = true;
        //rightFade.isFadeOut = true;

        leftEye.transform.Find("Image").GetComponent<ImageFadeInOut>().StartFadeOut();
        yield return new WaitForSeconds(time);

        //float angle = Vector3.Angle(leftEye.transform.parent.parent.forward, Vector3.forward);
        //if (angle > 45)
        //{
        //    Quaternion q = GameObject.Find("/Pvr_UnitySDK").transform.rotation;
        //    q.y = 0;
        //    GameObject.Find("/Pvr_UnitySDK").transform.rotation = q;
        //}

        Quaternion q = leftEye.transform.parent.parent.rotation;
        q = q * Quaternion.Euler(0, 90, 0);
        leftEye.transform.parent.parent.rotation = q;

        //执行动作
        //SYSManager.Instance.isFadeOut = true;
        
        //淡入
        //leftFade.isFadeIn = true;
        //rightFade.isFadeIn = true;

        //leftFade.isFadeOut = false;
        //rightFade.isFadeOut = false;

        leftEye.transform.Find("Image").GetComponent<ImageFadeInOut>().StartFadeIn();
        leftEye.transform.parent.GetComponent<CameraScale>().ReturnOriginPosition();

        yield return new WaitForSeconds(time);
        //SYSManager.Instance.isFadeIn = true;
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
        string str = "孵化期";
        for (int i = 1; i < content.Length; i++)
        {
            string[] temp = content[i].Split(","[0]);
            switch(str)
            {
                case "孵化期":
                    {
                        if(temp[0].Contains("苗鸡1"))
                        {
                            str = "苗鸡1";
                        }
                        else
                        {
                            ConfigData.Instance.FuHQ.Add(temp);
                        }
                    }
                    break;
                case "苗鸡1":
                    {
                        if (temp[0].Contains("苗鸡2"))
                        {
                            str = "苗鸡2";
                        }
                        else
                        {
                            ConfigData.Instance.MiaoJ1.Add(temp);
                        }
                    }
                    break;
                case "苗鸡2":
                    {
                        if (temp[0].Contains("青年鸡"))
                        {
                            str = "青年鸡";
                        }
                        else
                        {
                            ConfigData.Instance.MiaoJ2.Add(temp);
                        }
                    }
                    break;
                case "青年鸡":
                    {
                        if (temp[0].Contains("成年鸡"))
                        {
                            str = "成年鸡";
                        }
                        else
                        {
                            ConfigData.Instance.QingNJ.Add(temp);
                        }
                    }
                    break;
                case "成年鸡":
                    {
                        if (temp[0].Contains("产蛋鸡"))
                        {
                            str = "产蛋鸡";
                        }
                        else
                        {
                            ConfigData.Instance.ChengNJ.Add(temp);
                        }
                    }
                    break;
                case "产蛋鸡":
                    {
                        if (temp[0].Contains("蛋种类"))
                        {
                            str = "蛋种类";
                        }
                        else
                        {
                            ConfigData.Instance.ChanDJ.Add(temp);
                        }
                    }
                    break;
                case "蛋种类":
                    {
							ConfigData.Instance.DAN.Add(temp);
                    }
                    break;
            }
        }
    }
}