using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_EnemyManager : MonoBehaviour
{
    struct ColourGenerator
    {

        public bool red;
        public bool yellow;
        public bool blue;


       
        public void Set_colour(int colour_index1,int colour_index2, ref ColourGenerator Power_status)
        {
            Power_status.red = false;
            Power_status.blue = false;
            Power_status.yellow = false;
            if (colour_index1 == -1)
            {
                switch (colour_index2)
                {
                    case 0: Power_status.red = true; break;
                    case 1: Power_status.yellow = true; break;
                    case 2: Power_status.blue = true; break;

                }
            }
            else
            {
                if (colour_index2 == -1)
                {
                    switch (colour_index1)
                    {
                        case 0: Power_status.red = true; break;
                        case 1: Power_status.yellow = true; break;
                        case 2: Power_status.blue = true; break;

                    }
                }
                else
                {
                    switch (colour_index1)
                    {
                        case 0: Power_status.red = true; break;
                        case 1: Power_status.yellow = true; break;
                        case 2: Power_status.blue = true; break;
                    }
                    switch (colour_index2)
                    {
                        case 0: Power_status.red = true; break;
                        case 1: Power_status.yellow = true; break;
                        case 2: Power_status.blue = true; break;
                    }
                }
            }

            if (Power_status.red && Power_status.blue && Power_status.yellow)
                Debug.Log("three primary can not all be true");
        }

        public Vector4 Generated_colour(){
            Vector4 colour= new Vector4(0.0f,0.0f,0.0f,1.0f);
            if (red && !yellow && !blue) colour = new Vector4(1, 0, 0, 1);//红 1
            else if (!red && yellow && !blue) colour = new Vector4(1, 1, 0, 1);//黄 2
            else if (!red && !yellow && blue) colour = new Vector4(0, 0, 1, 1);//蓝 3
            else if (red && yellow && !blue) colour = new Vector4(1, 0.5f, 0, 1);//橙 6
            else if (red && !yellow && blue) colour = new Vector4(1, 0, 1, 1);//紫 5
            else if (!red && yellow && blue) colour = new Vector4(0, 1, 0, 1);//绿 4
            return colour;
        }
    }

    public int singleColorEnergy = -10;
    public int doubleColorEnergy = -15;

    public Vector3 speed;

    public Transform bullet_red_prefab;

    private ColourGenerator power_status = new ColourGenerator();

    private Connectable[] Energy_port;//能量接口组件

	private bool isShoot=false;

    private Vector3 shootpos;

    private Animator animator;
	// Use this for initialization
	void Start () {

        animator = GetComponent<Animator>();
        Energy_port=GetComponentsInChildren<Connectable>() ;

        //shootpos = transform.position - (new Vector3(0.0f, 0.3f, 0.0f));

        if(transform.position.x>=0)	shootpos=transform.position+(new Vector3(0.5f,-0.3f,0.0f));//射击初始位
        else shootpos = transform.position + (new Vector3(-0.5f, -0.3f, 0.0f));
        //StartGame();
        StartCoroutine(Shoot());
	}

    public void StartGame()
    {
        //初始化接通能量状态
        power_status.red = false;
        power_status.yellow = false;
        power_status.blue = false;

        animator.SetBool("isHold", false);
        isShoot = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Energy_port[0].isConnected || Energy_port[1].isConnected)
        {
            isShoot = true;
            animator.SetBool("isHold", true);
        }
        else
        {
            isShoot = false;
            animator.SetBool("isHold", false);
        }
        power_status.Set_colour(Energy_port[0].colorIndex, Energy_port[1].colorIndex, ref power_status);

    }

    void FixedUpdate() {
        //if (isShoot)
        //{
        //    Transform clone = Instantiate(bullet_red_prefab, shootpos, Quaternion.identity);
        //    clone.GetComponent<SpriteRenderer>().color=power_status.Generated_colour();
        //    isShoot = false;
        //    Rigidbody2D temp_rb = clone.GetComponent<Rigidbody2D>();
        //    if (shootpos.x >= 0)
        //    {
        //        temp_rb.AddForce(new Vector3(1.0f * force_coefficient, 0.0f, 0.0f));
        //    }
        //    else{
        //        temp_rb.AddForce(new Vector3(-1.0f * force_coefficient, 0.0f, 0.0f));
        //    }
        //}

	}

    IEnumerator Shoot(){
        while (true)
        {
            if (isShoot)
            {
                if (Energy_port[0].isConnected && Energy_port[1].isConnected)
                    SourceManager.instance.ChangeSoure(doubleColorEnergy);
                else
                    SourceManager.instance.ChangeSoure(singleColorEnergy);

                isShoot = false;
                if (SourceManager.instance.source <= 0) continue;

                animator.SetTrigger("attack");
                Transform clone = Instantiate(bullet_red_prefab, shootpos, Quaternion.identity);
                clone.GetComponent<SpriteRenderer>().color = power_status.Generated_colour();
                AudioManger.instance.PlayEffectAudio(1);

                Bullet_Attributes attr = clone.GetComponent<Bullet_Attributes>();
                attr.InitBullet(speed);
            }
            yield return new WaitForSeconds(1.0f);
        }
    }
}
