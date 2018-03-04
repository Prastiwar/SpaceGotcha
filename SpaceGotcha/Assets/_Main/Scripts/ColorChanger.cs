using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    [SerializeField] float changeValue = 0.1f;
    
    float hColor = 0.15f;
    bool shouldDecrease = false;

    ParticleSystem particles;
    ParticleSystem.MainModule main;

    void Awake()
    {
        particles = GetComponent<ParticleSystem>();
        main = particles.main;
    }

    void Update()
    {
        main.startColor = Color.HSVToRGB(hColor, 1, 1);

        hColor = Mathf.LerpAngle(hColor, shouldDecrease ? hColor - changeValue : hColor + changeValue, Time.deltaTime);
        //hColor = Mathf.SmoothDampAngle(hColor, shouldDecrease ? hColor - changeValue : hColor + changeValue, ref hColor, Time.deltaTime);

        if (hColor >= 0.55f)
        {
            shouldDecrease = true;
        }
        else if (hColor <= 0.15f)
        {
            shouldDecrease = false;
        }
    }
}
