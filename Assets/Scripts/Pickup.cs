using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] List<Tadpoles> tadpolesInRange = new List<Tadpoles>();
    [SerializeField] Altar altar;
    public int holding = 0;
    public int holdMax = 10;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            for (int i = tadpolesInRange.Count - 1; i >= 0; i--)
            {
                if (holding < holdMax)
                {
                    holding += 1;
                    Destroy(tadpolesInRange[i].gameObject);
                    tadpolesInRange.RemoveAt(i);
                }
                else
                {
                    break;
                }
            }
        }
        else if (Input.GetMouseButtonDown(1))
        {
            if (altar)
            {
                holding -= altar.Sacrifice(holding);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Tadpoles"))
        {
            Tadpoles t = other.gameObject.GetComponent<Tadpoles>();

            if (t)
            {
                if (!tadpolesInRange.Contains(t))
                {
                    tadpolesInRange.Add(t);
                    t.Highlight();
                }
            }
        }

        if (other.gameObject.CompareTag("Altar"))
        {
            altar = other.gameObject.GetComponent<Altar>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Tadpoles"))
        {
            Tadpoles t = other.gameObject.GetComponent<Tadpoles>();

            if (t)
            {
                if (tadpolesInRange.Contains(t))
                {
                    tadpolesInRange.Remove(t);
                    t.UnHighlight();
                }
            }
        }

        if (other.gameObject.CompareTag("Altar"))
        {
            altar = null;
        }
    }
}
