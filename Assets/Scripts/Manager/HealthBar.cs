using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private Transform bar;
    private void Start()
    {
        bar = transform.Find("Bar");
    }

    // Update is called once per frame
    public void setSize(float size)
    {
        bar.localScale = new Vector3(size, 1f);        
    }

    public void SetColor( Color color)
    {
        bar.Find("BarSpirte").GetComponent<SpriteRenderer>().color = color;
    }
}
