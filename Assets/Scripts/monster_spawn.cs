using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monster_spawn : MonoBehaviour
{


    [SerializeField]
    private Transform[] monsters = new Transform[6];//六种怪物缓存
    [SerializeField]
    private Vector3[] spawn_pos = new Vector3[4];//怪物刷新点

    private bool isgameon = false;
    private Rigidbody2D rb_mon;//怪的rigidbody缓存
    private int game_level;//实时难度标记
    private int time_gap;
    [SerializeField]
    private float velocity_coefficient;//出怪的速度系数
                                       // Use this for initialization

    private int difficulties = 1;

    void Start()
    {

        time_gap = Random.Range(4, 6);//第一波怪前的初始时间
        //刷怪点


        spawn_pos[0] = GameObject.Find("monster_nest01").transform.position;
        spawn_pos[1] = GameObject.Find("monster_nest02").transform.position;
        spawn_pos[2] = GameObject.Find("monster_nest03").transform.position;
        spawn_pos[3] = GameObject.Find("monster_nest04").transform.position;
        for (int i_4pos = 0; i_4pos <= 3; i_4pos++)
        {
            if (spawn_pos[i_4pos].x > 0)
                spawn_pos[i_4pos] = spawn_pos[i_4pos] + Vector3.left * 3.0f;
            else
                spawn_pos[i_4pos] = spawn_pos[i_4pos] + Vector3.right * 3.0f;
        }

        //foreach (Vector3 Spawn_pos in spawn_pos)
        //{
        //    if (Spawn_pos.x > 0) Spawn_pos.x = Spawn_pos.x - 1.0f;

        //}

        StartCoroutine(Difficulties());
        //StartCoroutine(Monster_generator());

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {

    }

    //怪物生成的控制器
    IEnumerator Monster_generator()
    {
        while (isgameon)
        {
            yield return new WaitForSeconds(time_gap);
            int Set_number=1;


            if (difficulties >= 12)
            {
                Set_number = Random.Range(4, 6);//6种
            }
            else {
                if(difficulties >9){
                    Set_number = Random.Range(3, 5);
                }
                else{
                    if(difficulties>6){
                        Set_number = Random.Range(2, 4);
                    }
                    else{
                        if(difficulties>3){
                            Set_number = Random.Range(1, 3);
                        }
                        else{
                            if (difficulties>1)
                            {
                                Set_number = 2;
                            }
                        }
                    }
                }
            }

            int[] spawn_point = new int[2];
            spawn_point[0] = Random.Range(0, 3);//随机出生点1    
            spawn_point[1] = Random.Range(0, 3);//随机出生点2

            int[] Multi_pickrange = new int[2];
            Multi_pickrange[0] = Random.Range(0, 1);//辅助确定选择范围
            Multi_pickrange[1] = Random.Range(0, 1);
            switch (Set_number)
            {
                
                case 1://单低阶
                    MonsterCreator1_3(spawn_point[0]);
                    break;


                case 2://俩低阶
                    for (int iterator = 0; iterator <= 1; iterator++)
                    {
                        MonsterCreator1_3(spawn_point[iterator]);
                        yield return new WaitForSeconds(1.5f);
                    }
                    break;
                    
                case 3://单高阶
                    MonsterCreator4_6(spawn_point[0]);
                    break;

                case 4://双高阶
                    for (int iterator = 0; iterator <= 1; iterator++)
                    {
                        MonsterCreator4_6(spawn_point[iterator]);
                        yield return new WaitForSeconds(1.5f);

                    }
                    break;


                case 5://单混合

                    if (Multi_pickrange[0] <= 0) MonsterCreator1_3(spawn_point[0]);
                    else MonsterCreator4_6(spawn_point[0]);
                    break;


                case 6://双混合

                    //foreach(int pick in Multi_pickrange){
                    //    if(pick<=0) MonsterCreator1_3(spawn_point[0]);
                    //    else MonsterCreator4_6(spawn_point[0]);
                    //}
                    for (int iterator = 0; iterator <= 1; iterator++)
                    {
                        if (Multi_pickrange[iterator] <= 0) MonsterCreator1_3(spawn_point[iterator]);
                        else MonsterCreator4_6(spawn_point[iterator]);
                        yield return new WaitForSeconds(2);
                    }
                    break;

                         

                default: break;
            }
            time_gap = Random.Range(3, 6);
        }
    }

    //生成器
    void MonsterCreator1_3(int spawn_point)
    {
        int object_number = Random.Range(0, 2);
        Quaternion direction = Quaternion.identity;
        if(spawn_point%2==0){
            direction = Quaternion.AngleAxis(180.0f,Vector3.up);
        }
        Transform monster_clone = Instantiate(monsters[object_number], spawn_pos[spawn_point], direction);
        rb_mon = monster_clone.GetComponent<Rigidbody2D>();
        if (spawn_point % 2 == 0)
            rb_mon.velocity = (Vector2.left * Mathf.Log(velocity_coefficient * difficulties, 2.0f));
        else
            rb_mon.velocity = (Vector2.right * Mathf.Log(velocity_coefficient * difficulties, 2.0f));
    }

    void MonsterCreator4_6(int spawn_point)
    {
        int object_number = Random.Range(3, 5);
        Transform monster_clone = Instantiate(monsters[object_number], spawn_pos[spawn_point], Quaternion.identity);
        rb_mon = monster_clone.GetComponent<Rigidbody2D>();
        if (spawn_point % 2 == 0)
            rb_mon.velocity = (Vector2.left * Mathf.Log(velocity_coefficient*difficulties,2.0f));
        else
            rb_mon.velocity = (Vector2.right * Mathf.Log(velocity_coefficient*difficulties, 2.0f));
    }

    public void Start_Game()
    {
        difficulties = 1;
        isgameon = true;
        StartCoroutine(Monster_generator());
    }

    IEnumerator Difficulties()
    {
        
        yield return new WaitForSeconds(2);//补偿初始等待时间
        while(isgameon){
            difficulties++;
        yield return new WaitForSeconds(8);
        }
    }


    ////六种,函数数组？
    //void spawn_1in6(int []range, int []pos){
    //    if (range[0] <= 0) MonsterCreator1_3(pos[0]);
    //    else MonsterCreator4_6(pos[0]);
    //}

    //void spawn_1in3L(int[] range, int[] pos)
    //{
    //    MonsterCreator1_3(pos[0]);
    //}

    //void spawn_1in3H(int[] range, int[] pos)
    //{
    //    MonsterCreator4_6(pos[0]);
    //}

    //void spawn_2in6(int[] range, int[] pos)
    //{
    //    if(pos[0]==pos[1]){
    //        if(pos[0]%)
    //    }
    //}

    //void spawn_2in3H(int[] range, int[] pos)
    //{

    //}

    //void spawn_2in3L(int range, float pos)
    //{

    //}
}