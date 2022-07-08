using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tadpoles : MonoBehaviour
{
    public Material highlight;
    public Material nothighlight;

    public int state = 0; //0 idle, 1 runaway 

    Rigidbody rid;

    // Start is called before the first frame update
    void Start()
    {
        rid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (state == 0)
        {

        }
        else if (state == 1)
        {

        }
    }

    public void Highlight()
    {
        GetComponent<MeshRenderer>().material = highlight;
    }

    public void UnHighlight()
    {
        GetComponent<MeshRenderer>().material = nothighlight;
    }
}
