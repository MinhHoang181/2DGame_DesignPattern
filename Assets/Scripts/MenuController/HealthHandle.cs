using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
public class HealthHandle : MonoBehaviour
{
    [SerializeField]  HealthBar healthBar;
    void Start()
    {
        float health = 0.5f;
        FunctionPeriodic.Create(() =>
        {
            if (health > .01f)
            {
                health -= .01f;
                healthBar.setSize(health);
            }
            if (health < .2f)
            {
                if ((health * 100f) % 3 == 0)
                {
                    healthBar.SetColor(Color.white);
                }
                else
                {
                    healthBar.SetColor(Color.red);
                }
            }
        }, .1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
