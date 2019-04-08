using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class sticky_resize : MonoBehaviour
{
    public float Length;

    public ParticleSystem lightning;
    public ParticleSystem zaps;
    public SpriteRenderer magnet;
    public CapsuleCollider2D col;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        magnet.size = new Vector2(Length + 2, 1.8f);

        var shape = lightning.shape;
        shape.radius = Length;

        var sparkShape = zaps.shape;
        sparkShape.radius = Length;

        //var colSize = col.size;
        col.size = new Vector2((Length * 2) + 4, 2f);
        
    }
}
