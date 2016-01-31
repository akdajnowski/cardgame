using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;

public class WaveBehaviour : MonoBehaviour
{
    public float Lifetime = 2;
    public Transform detractor;
    public float repelForce;
    private List<SpriteRenderer> sprites;
    private Rigidbody2D rigid;

    // Use this for initialization
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        sprites = new List<SpriteRenderer>();
        foreach (Transform sprite in transform)
        {
            sprites.Add(sprite.GetComponent<SpriteRenderer>());
        }

        sprites.ForEach(s => s.color = Color.clear);

        sprites.ForEach(Tween);
    }

    void Update()
    {
        var f = transform.position - detractor.transform.position;
        rigid.AddForce(f.normalized * repelForce);
    }

    private void Tween(SpriteRenderer s)
    {
        s
         .DOColor(Color.white, Lifetime / 2f)
         .AppendIntoSequence(s.DOColor(Color.clear, Lifetime / 2f))
         .OnComplete(() => DestroyImmediate(this.gameObject));
    }

}
