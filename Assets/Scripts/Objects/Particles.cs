using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particles : MonoBehaviour
{
    public ParticleSystem partrot;
    public ParticleSystem partfloat;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Activate(Transform target, float size, float a)
    {
        var s = partrot.shape;
        s.radius = size;
        var m = partrot.main;
        m.startSize = size / 4f;
        m.startColor = new Color(1, 1, 0, a * 0.5f);

        s = partfloat.shape;
        s.radius = size * 1.2f;
        m = partfloat.main;
        m.startSize = size / 8f;
        m.startColor = new Color(1, 1, 0, a * 0.5f);

        transform.SetParent(target);
        transform.localPosition = Vector3.zero;
        transform.localScale = Vector3.one;
    }
}
