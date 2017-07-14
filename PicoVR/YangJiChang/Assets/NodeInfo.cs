using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class NodeInfo
{
    public int startNum;//起点数值
    public int endNum;//终点数值
    public bool isStart;//是否起点
    public bool isEnd;//是否终点
    public bool isMain;//主节点
    public bool isAssist;//辅助节点（在地图上不显示）
    public string showContext;//显示内容
    public int showContextLength;//显示内容的长度
}
