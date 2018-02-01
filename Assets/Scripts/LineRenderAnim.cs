using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRenderAnim : MonoBehaviour {

    public LineRenderer lineRender;

    public float minOffset;
    public float maxOffset;
    public float speed;

    private float offset;

    void Start()
    {
        offset = minOffset;
    }


    void Update()
    {
        offset += speed * Time.deltaTime;
        if (offset >= maxOffset) offset = minOffset;

        lineRender.material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
    }
}
