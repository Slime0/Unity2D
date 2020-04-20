using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 弹药包相关
/// </summary>
public class BulletBag : MonoBehaviour
{
    public int bulletCount = 10;//可以增加的子弹数量

    public ParticleSystem collectEffect;//拾取特效

    public AudioClip collectClip;//拾取音效

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerControl pc = collision.GetComponent<PlayerControl>();
        if(pc != null)
        {
            if(pc.MyCurBulletCount < pc.MyMaxBulletCount)
            {
                pc.ChangeBulletCount(bulletCount);//增加子弹的数量
                Instantiate(collectEffect, transform.position, Quaternion.identity);//生成特效
                AudioManager.Instance.AudioPlay(collectClip);//播放音效
                Destroy(this.gameObject);
            }
        }
    }
}
