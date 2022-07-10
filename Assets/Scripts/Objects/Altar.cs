using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Altar : MonoBehaviour
{
    [SerializeField] int sacrificed = 0;
    [SerializeField] int currentsacrificed = 0;
    [SerializeField] List<int> sac;

    [SerializeField] Color start;
    [SerializeField] Color end;
    [SerializeField] Particles part;
    Color current;


    [SerializeField] Color lightstart;
    [SerializeField] Color lightend;
    Color lightcurrent;
    Light light;
    //Material Skybox;

    int stage = 0;

    [SerializeField] Text text;
    [SerializeField] Dialogue dialogue;

    // Start is called before the first frame update
    void Start()
    {
        current = start;
        light = FindObjectOfType<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        if (stage < sac.Count)
        {
            if (currentsacrificed >= sac[stage])
            {
                currentsacrificed -= sac[stage];
                dialogue.PlayNext();
                stage++;
            }
        }

        text.text = currentsacrificed.ToString() + "/" + sac[stage].ToString();

        Gamedata.fraction = ((float)sacrificed / 60) * ((float)sacrificed / 60);
        current = Color.Lerp(current, Color.Lerp(start, end, Gamedata.fraction), 0.01f);
        part.Activate(transform, 3, (Gamedata.fraction + 0.1f) / 1.1f);
        lightcurrent = Color.Lerp(lightcurrent, Color.Lerp(lightstart, lightend, Gamedata.fraction), 0.01f);

        light.color = lightcurrent;

        if (RenderSettings.skybox.HasProperty("_Tint"))
            RenderSettings.skybox.SetColor("_Tint", current);
        else if (RenderSettings.skybox.HasProperty("_SkyTint"))
            RenderSettings.skybox.SetColor("_SkyTint", current);


        
    }

    public int Sacrifice(int tadpoles)
    {
        if (stage >= sac.Count)
        {
            return 0;
        }

        if (currentsacrificed + tadpoles > sac[stage])
        {
            sacrificed += sac[stage] - currentsacrificed;
            currentsacrificed += sac[stage] - currentsacrificed;
            return sac[stage] - currentsacrificed;
        }
        else
        {
            sacrificed += tadpoles;
            currentsacrificed += tadpoles;

            return tadpoles;
        }

    }

    
}
