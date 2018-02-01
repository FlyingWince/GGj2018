using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monster_manage : MonoBehaviour {

    public Animator animator;

    public int damage = 0;

    public int bounty_energy = 0;
    public int upgrade_bounty = 0;

    [SerializeField]
    private int enemy_index=0;

    [SerializeField]
    private Transform[] Lv2_monster = new Transform[3];
	// Use this for initialization
	void Start () {
		


	}
	
	// Update is called once per frame
	void Update () {
        
	}

    public void Die()
    {
        BoxCollider2D c = gameObject.GetComponent<BoxCollider2D>();
        if (c != null) c.enabled = false;
        animator.SetTrigger("Die");
        AudioManger.instance.PlayEffectAudio(0);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Transform Colliding_object;
        Colliding_object = collision.GetComponent<Collider2D>().transform;
        //Debug.Log("1111");
        int Collider_index;
        if(Colliding_object.CompareTag("bullet")){
            Collider_index = Colliding_object.GetComponent<Bullet_Attributes>().Colour_index;
            if (Collider_index == enemy_index)
            {
                if (enemy_index >= 3)
                    SourceManager.instance.ChangeSoure(upgrade_bounty);
                else
                    SourceManager.instance.ChangeSoure(bounty_energy);

                Destroy(Colliding_object.gameObject);
                Die();
                //Destroy(gameObject);
            }
            else
            {
                if (Collider_index <= 3 && enemy_index <= 3 && Collider_index != 0 && enemy_index != 0)
                {
                    if (Collider_index > enemy_index)
                    {
                        int temp_index = enemy_index;
                        enemy_index = Collider_index;
                        Collider_index = temp_index;
                    }
                    if (Collider_index == 1)
                    {
                        switch (enemy_index)
                        {
                            case 2: enemy_index = 6; break;
                            case 3: enemy_index = 5; break;
                        }
                    }
                    if (Collider_index == 2)
                    {
                        enemy_index = 4;
                    }

                    Transform new_monster;
                    Quaternion direction= Quaternion.identity;
                    if (transform.position.x > 0) direction = Quaternion.AngleAxis(180.0f, Vector3.up);
                    switch (enemy_index)
                    {

                        case 4:
                            new_monster = Instantiate(Lv2_monster[0], transform.position, direction);
                            new_monster.GetComponent<Rigidbody2D>().velocity = transform.GetComponent<Rigidbody2D>().velocity;
                            break;

                        case 5:
                            new_monster = Instantiate(Lv2_monster[1], transform.position, direction);
                            new_monster.GetComponent<Rigidbody2D>().velocity = transform.GetComponent<Rigidbody2D>().velocity;
                            break;

                        case 6:
                            new_monster = Instantiate(Lv2_monster[2], transform.position, direction);
                            new_monster.GetComponent<Rigidbody2D>().velocity = transform.GetComponent<Rigidbody2D>().velocity;
                            break;
                    }
                    Destroy(Colliding_object.gameObject);
                    
                    Destroy(gameObject);
                }
                else
                {
                    Destroy(Colliding_object.gameObject);
                }
            }



            //Destroy(Colliding_object.gameObject);
        }

        if (Colliding_object.CompareTag("Base"))
        { 
            SourceManager.instance.ChangeSoure(damage);

            AudioManger.instance.ShowEffect(gameObject.transform.position);
            Destroy(gameObject);
        }

    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    Transform Colliding_object;
    //    Colliding_object = collision.collider.transform;
    //    if (Colliding_object.CompareTag("Base"))
    //    {
    //        SourceManager.instance.ChangeSoure(damage);
    //        Destroy(gameObject);
    //    }
    //}
}
