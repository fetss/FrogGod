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

    public float jumpStrenght = 5;

    bool grounded = false;

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
        grounded = Physics.Raycast(transform.position, Vector3.down, 0.55f, LayerMask.GetMask("Ground"));
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
            bool jump = false;
            float d = 0;
            for (int i = 0; i < n; i++)
            {
                RaycastHit hit;
                float a = (float)i / n * Mathf.PI * 2;
                float dis = detectionRange * 2;
                bool j = false;
                if (Physics.Raycast(transform.position, new Vector3(Mathf.Sin(a), 0.1f, Mathf.Cos(a)), out hit, detectionRange * 2, LayerMask.GetMask("Ground")))
                {
                    float Horizontalv = jumpStrenght;
                    for (int seg = 0; seg < 8 - 1; seg++)
                    {
                        RaycastHit hittemp;
                        float time = detectionRange * 2 / runSpeed;
                        Vector3 p1 = transform.position + new Vector3(Mathf.Sin(a) * runSpeed, jumpStrenght, Mathf.Cos(a) * runSpeed) * time * (seg / 8f) + Physics.gravity * time * time * (seg / 8f) * (seg / 8f);
                        Vector3 p2 = transform.position + new Vector3(Mathf.Sin(a) * runSpeed, jumpStrenght, Mathf.Cos(a) * runSpeed) * time * ((seg + 1) / 8f) + Physics.gravity * time * time * ((seg + 1) / 8f) * ((seg + 1) / 8f);

                        if (Physics.Raycast(p1, p2 - p1, out hittemp, (p2 - p1).magnitude, LayerMask.GetMask("Ground")))
                        {
                            p1 = hittemp.point - transform.position;
                            dis = Mathf.Sqrt(p1.x * p1.x + p1.z * p1.z);
                            break;
                        }
                    }

                    j = true;
                    //dis = hit.distance;
                }

                //Debug.Log(new Vector3 (Vector3.Dot(t, new Vector3(Mathf.Sin(a), 0, Mathf.Cos(a))) + 1, dis, i));

                if (d < dis * (Vector3.Dot(t, new Vector3(Mathf.Sin(a), 0, Mathf.Cos(a))) + 1) )
                {
                    jump = j;
                    d = dis * (Vector3.Dot(t, new Vector3(Mathf.Sin(a), 0, Mathf.Cos(a))) + 1) ;
                    p = i;
                }
            }


            float a1 = (float)p / n * Mathf.PI * 2;
            t = new Vector3(Mathf.Sin(a1), 0, Mathf.Cos(a1));
            if (jump && grounded)
            {
                rid.velocity += new Vector3(0, jumpStrenght, 0);
            }

            if (grounded)
            {
                rid.velocity = new Vector3(t.x * runSpeed, rid.velocity.y, t.z * runSpeed);
            }

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
