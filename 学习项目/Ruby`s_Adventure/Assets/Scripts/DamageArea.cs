using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 伤害陷阱相关
/// </summary>
public class DamageArea : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        PlayerControl pc = collision.GetComponent<PlayerControl>();
        if(pc != null)
        {
            pc.ChangeHealth(-1);
        }
    }
}
