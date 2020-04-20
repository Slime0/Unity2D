using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 控制子弹的移动碰撞
/// </summary>
public class BulletControl : MonoBehaviour
{

    Rigidbody2D rbody;

    public AudioClip hitClip;//命中音效

    // Start is called before the first frame update
    void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
        Destroy(this.gameObject, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// 子弹的移动
    /// </summary>
    public void Move(Vector2 moveDirection, float moveForce)
    {
        rbody.AddForce(moveDirection * moveForce);
    }
    ///碰撞检测
    private void OnCollisionEnter2D(Collision2D collision)
    {
        EnemyControl ec = collision.gameObject.GetComponent<EnemyControl>();
        if(ec != null)
        {
            ec.Fixed();//修复敌人
        }
        AudioManager.Instance.AudioPlay(hitClip);//播放命中音效
        Destroy(this.gameObject);
    }


}
