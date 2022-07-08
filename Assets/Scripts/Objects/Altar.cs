using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Altar : MonoBehaviour
{
    [SerializeField] int sacrificed = 0;
    [SerializeField] Text text;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Sacrifice(int tadpoles)
    {
        sacrificed += tadpoles;
        text.text = sacrificed.ToString();
    }
}
