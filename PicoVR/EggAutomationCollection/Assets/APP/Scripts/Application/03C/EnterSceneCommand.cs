using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterSceneCommand : Control 
{
    public override void Execute(object data = null)
    {
        SceneArgs sa = data as SceneArgs;
        switch(sa.SceneIndex)
        {
            case 1:
                {
                    RegisterView(GameObject.Find("RawImage").GetComponent<ShowMyLogoView>());
                }
                break;
            case 2:
                {
                    RegisterView(GameObject.Find("LogoPanel").GetComponent<ShowCompanyLogoView>());
                }
                break;
            case 3:
                {
                    RegisterView(GameObject.Find("TitlePanel").GetComponent<ShowTitleView>());
                }
                break;
            case 4:
                {
                    //RegisterView(GameObject.Find("SpawnPoints").GetComponent<SpawnPoints>());
                }
                break;
            case 5:
                {
                    RegisterView(GameObject.Find("SpawnPoints").GetComponent<SpawnPoints>());
                }
                break;
            case 6:
                {

                }
                break;
            case 7:
                {

                }
                break;
        }
    }
}
