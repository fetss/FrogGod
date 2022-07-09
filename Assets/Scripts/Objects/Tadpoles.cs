using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tadpoles : MonoBehaviour
{
    public Material highlight;
    public Material nothighlight;

    [SerializeField] Particles part;

    public float detectionRange = 5;
    public int n = 8;
    public float runSpeed = 3;

    GameObject player;

    public int state = 0; //0 idle, 1 runaway 

    Vector2 p;
    float timer;

    Rigidbody rid;

    // Start is called before the first frame update
    void Start()
    {
        rid = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
        part.gameObject.SetActive(true);
        part.Activate(transform, 0.5f, 0.5f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        part.Activate(transform, 0.5f, 0.5f - Gamedata.fraction / 2);

        if (state == 0)
        {
            /*
            Vector2 pos = new Vector2(transform.position.x, transform.position.z);
            if ((pos - p).sqrMagnitude < 0.01f)
            {
                p = pos + Random.insideUnitCircle;
            }
            else
            {
                Vector2 pu = (p - pos).normalized;
                rid.velocity = new Vector3(pu.x, 0, pu.y);
            }
            */
            timer -= Time.fixedDeltaTime;

            rid.velocity = new Vector3(p.x, rid.velocity.y, p.y);
            if (timer < 0)
            {
                timer += Random.Range(0.5f, 1.5f);
                p = Random.insideUnitCircle;
            }
            if ((player.transform.position - transform.position).sqrMagnitude < detectionRange * detectionRange)
            {
                if (!Physics.Raycast(transform.position, player.transform.position - transform.position, detectionRange, LayerMask.GetMask("Ground")))
                {
                    state = 1;
                }
            }
        }
        else if (state == 1)
        {
            Vector3 t = transform.position - player.transform.position;
            t.y = 0;
            t.Normalize();


            int p = 0;
            float d = 0;
            for (int i = 0; i < n; i++)
            {
                RaycastHit hit;
                float a = (float)i / n * Mathf.PI * 2;
                float dis = detectionRange * 2;
                if (Physics.Raycast(transform.position, new Vector3(Mathf.Sin(a), 0.1f, Mathf.Cos(a)), out hit, detectionRange * 2, LayerMask.GetMask("Ground")))
                {
                    dis = hit.distance;
                }

                //Debug.Log(new Vector3(dis * Vector3.Dot(t, new Vector3(Mathf.Sin(a), 0, Mathf.Cos(a))), dis, i));

                if (d < dis * (Vector3.Dot(t, new Vector3(Mathf.Sin(a), 0, Mathf.Cos(a))) + 1) )
                {
                    d = dis * (Vector3.Dot(t, new Vector3(Mathf.Sin(a), 0, Mathf.Cos(a))) + 1) ;
                    p = i;
                }
            }

            float a1 = (float)p / n * Mathf.PI * 2;
            t = new Vector3(Mathf.Sin(a1), 0, Mathf.Cos(a1));

            rid.velocity = new Vector3(t.x * runSpeed, rid.velocity.y, t.z * runSpeed);

            if ((player.transform.position - transform.position).sqrMagnitude > detectionRange * detectionRange * 9)
            {
                state = 0;
            }
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
