using UnityEngine;


//程序入口
public class Main : MonoBehaviour
{
    void Update()
    {
        Manager.Instance.statusMachine.Run();
    }
}