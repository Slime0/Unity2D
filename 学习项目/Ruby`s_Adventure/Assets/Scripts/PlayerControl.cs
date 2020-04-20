using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 控制角色移动、生命、动画等
/// </summary>
public class PlayerControl : MonoBehaviour
{
    public float speed = 5f;//移动速度

    private int maxHealth = 5;//最大生命值

    private int currentHealth;//当前生命值

    public int MyMaxHealth { get { return maxHealth; } }
    public int MyCurrentHealth { get { return currentHealth; } }

    private float invincibleTime = 2f;//无敌时间2秒

    private float invincibleTimer;//无敌时间计时器

    private bool isInvincible;//是否处于无敌状态

    public GameObject bulletPrefab;//子弹

    [SerializeField]
    private int curBulletCount;//当前子弹数量
    private int maxBulletCount = 99;//子弹最大数量

    public int MyCurBulletCount { get { return curBulletCount; } }
    public int MyMaxBulletCount { get { return maxBulletCount; } }

    //=============玩家的音效

    public AudioClip hitClip;//受伤的音效
    public AudioClip launchClip;//发射齿轮的音效

    //=============玩家的朝向
    private Vector2 lookDirectiion = new Vector2(1, 0);//默认朝向右方



    Rigidbody2D rbody;//刚体组件

    Animator anim;

    

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = 2;
        invincibleTimer = 0;
        curBulletCount = 22;//子弹初始数量为22
        rbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        UIManager.instance.UpdateHealthBar(currentHealth, maxHealth);
        UIManager.instance.UpdateBulletCount(curBulletCount, maxBulletCount);
    }


    // Update is called once per frame
    void Update()
    {
        float move_X = Input.GetAxisRaw("Horizontal");//控制水平移动方向 A:-1 D:1 0
        float move_Y = Input.GetAxisRaw("Vertical");//控制垂直移动方向 W:1 S:-1 0


        Vector2 moveVector = new Vector2(move_X, move_Y);
        if(moveVector.x != 0 || moveVector.y != 0)
        {
            lookDirectiion = moveVector;
        }
        anim.SetFloat("Look X", lookDirectiion.x);
        anim.SetFloat("Look Y", lookDirectiion.y);
        anim.SetFloat("Speed", moveVector.magnitude);




        //================移动================================
        Vector2 position = rbody.position;
        //position.x += move_X * speed * Time.deltaTime;
        //position.y += move_Y * speed * Time.deltaTime;
        position += moveVector * speed * Time.deltaTime;
        

        rbody.MovePosition(position);

        //================无敌计时============================
        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
            {
                isInvincible = false;//倒计时结束之后（2秒），取消无敌状态
            }
        }
        //================按下j键并且子弹数量大于0进行攻击===========
        if (Input.GetKeyDown(KeyCode.J) && curBulletCount > 0)
        {
            ChangeBulletCount(-1);//每次攻击减1个子弹
            anim.SetTrigger("Launch");//播放攻击动画
            AudioManager.Instance.AudioPlay(launchClip);//播放攻击音效
            GameObject bullet = Instantiate(bulletPrefab, rbody.position + Vector2.up * 0.5f, Quaternion.identity);
            BulletControl bc = bullet.GetComponent<BulletControl>();
            if(bc != null)
            {
                bc.Move(lookDirectiion, 300);
            }
        }
        //================按下E键，进行NPC交互
        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit2D hit = Physics2D.Raycast(rbody.position, lookDirectiion, 2f,LayerMask.GetMask("npc"));
            if(hit.collider != null)
            {
                NPCManager npc = hit.collider.GetComponent<NPCManager>();
                if (npc != null)
                {
                    npc.showDialog();
                }
            }
        }

    }
    /// <summary>
    /// 改变玩家的生命值
    /// </summary>
    /// <param name="amount"></param>
    public void ChangeHealth( int amount)
    {
        //如果玩家受到伤害
        if (amount < 0)
        {
            if(isInvincible == true)
            {
                return;
            }
            isInvincible = true;
            anim.SetTrigger("Hit");
            AudioManager.Instance.AudioPlay(hitClip);//播放受伤音效
            invincibleTimer = invincibleTime;
        }
        Debug.Log(currentHealth + "/" + maxHealth);
        //使用Mathf.Clamp(value,min,max)将值约束在min和max的范围里面
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        UIManager.instance.UpdateHealthBar(currentHealth, maxHealth);//更新血条
        Debug.Log(currentHealth + "/" + maxHealth);
    }
    /// <summary>
    /// 改变子弹数量
    /// </summary>
    /// <param name="amount"></param>
    public void ChangeBulletCount(int amount)
    {
        curBulletCount = Mathf.Clamp(curBulletCount + amount, 0, maxBulletCount);//限制子弹数量在0到最大值之间
        UIManager.instance.UpdateBulletCount(curBulletCount, maxBulletCount);
    }
}
