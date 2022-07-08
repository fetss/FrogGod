using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject tadpoles;
    float t = 0;
    public float spawntime = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        t -= Time.deltaTime;
        if(t < 0)
        {
            t += 1 / spawntime;
            Instantiate(tadpoles, transform.position + Random.insideUnitSphere / 2, Quaternion.identity);
        }
    }
}
