using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// UI管理相关
/// </summary>
public class UIManager : MonoBehaviour
{
    //单例模式
    public static UIManager instance { get; private set; }

    private void Awake()
    {
        instance = this;
    }

    public Image healthBar;//角色血条

    public Text bulletCountText;//子弹数量内容显示

    /// <summary>
    /// 更新血条
    /// </summary>
    /// <param name="curAmount"></param>
    /// <param name="maxAmount"></param>
    public void UpdateHealthBar(int curAmount, int maxAmount)
    {
        healthBar.fillAmount = (float)curAmount / (float)maxAmount;
    }
    /// <summary>
    /// 更新子弹数量文本的显示
    /// </summary>
    /// <param name="curAmount"></param>
    /// <param name="maxAmount"></param>
    public void UpdateBulletCount(int curAmount, int maxAmount)
    {
        bulletCountText.text = curAmount.ToString() + " / " + maxAmount.ToString();
        
    }
}
