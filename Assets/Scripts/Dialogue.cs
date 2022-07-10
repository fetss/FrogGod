using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    [System.Serializable]
    public struct dialog
    {
        public List<string> texts;
    }

    public List<dialog> dialogues;
    public Transform player;

    int count = 0;

    public Text text;

    // Start is called before the first frame update
    void Start()
    {
        PlayNext();
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player.position);
        transform.rotation *= Quaternion.Euler(0, -90, 90);
    }

    IEnumerator Play(int c)
    {
        if (c < dialogues.Count)
        {
            float T = 0;
            while(T < 1)
            {
                transform.localScale = new Vector3(4, 0.08f, 10) * T;
                T += Time.deltaTime * 2;
                yield return null;
            }
            transform.localScale = new Vector3(4, 0.08f, 10);

            for (int i = 0; i < dialogues[c].texts.Count; i++)
            {
                for (int t = 0; t < dialogues[c].texts[i].Length; t++)
                {
                    text.text += dialogues[c].texts[i][t];
                    //sfx
                    yield return new WaitForSeconds(0.1f);
                }
                yield return new WaitForSeconds(3);
                text.text = "";
            }

            while (T > 0)
            {
                transform.localScale = new Vector3(4, 0.08f, 10) * T;
                T -= Time.deltaTime * 2;
                yield return null;
            }
            transform.localScale = Vector3.zero;
        }
    }

    public void PlayNext()
    {
        StopAllCoroutines();
        text.text = "";
        StartCoroutine(Play(count));
        count++;
    }
}
