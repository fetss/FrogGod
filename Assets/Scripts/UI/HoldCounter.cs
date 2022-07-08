using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoldCounter : MonoBehaviour
{
    Text text;
    public Pickup pickup;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = pickup.holding.ToString() + "/" + pickup.holdMax.ToString();
    }
}
