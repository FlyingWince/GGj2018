using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Attributes : MonoBehaviour {
    private Color Colourcache;

    private Vector4 colour_data;

    private int colour_index;

    private Vector3 speed;

    public int Colour_index{
        get{
            return colour_index;
        }
    }

	// Use this for initialization
	void Start () {
        Colourcache = transform.GetComponent<SpriteRenderer>().color;
        colour_data = new Vector4(Colourcache.r, Colourcache.g, Colourcache.b, Colourcache.a);

        if (colour_data == new Vector4(1, 0, 0, 1)) colour_index = 1;//红 1

        if (colour_data == new Vector4(1, 1, 0, 1)) colour_index = 2;//黄 2

        if (colour_data == new Vector4(0, 0, 1, 1)) colour_index = 3;//蓝 3

        if (colour_data == new Vector4(0, 1, 0, 1)) colour_index = 4;//绿 4

        if (colour_data == new Vector4(1, 0, 1, 1)) colour_index = 5;//紫 5

        if (colour_data == new Vector4(1, 0.5f, 0, 1)) colour_index = 6;//橙 6


	}

    public void InitBullet(Vector3 speed)
    {
        this.speed = speed;
    }
	
	void Update () {
        
        transform.position += speed * Time.deltaTime;
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.gameObject.CompareTag("Nest")){
            Destroy(gameObject);
        }
    }

}
