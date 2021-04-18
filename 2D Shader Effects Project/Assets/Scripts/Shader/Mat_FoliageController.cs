using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mat_FoliageController : MonoBehaviour
{
    public float timeToAdd;
    public Gradient color1;
    public Gradient color2;

    public Vector3 windBlowDirection;
    public float windBlowArea;
    public float windBlowSpeed;

    private Material material;

    private void Start()
    {
        material = GetComponent<SpriteRenderer>().material;

        if(material != null)
        {
            material.SetFloat("_WindBlowArea", windBlowArea);
            material.SetVector("_WindBlowDirection", windBlowDirection);
            material.SetFloat("_WindBlowSpeed", windBlowSpeed);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            StopAllCoroutines();
            StartCoroutine(ChangeColor(0f, 1f));
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            StopAllCoroutines();
            StartCoroutine(ChangeColor(1f, 0f));
        }
    }

    private IEnumerator ChangeColor(float start, float end)
    {
        if (material == null) yield break;

        float timeCounter;

        if(start < end)
        {
            timeCounter = start;
            while (timeCounter < end)
            {
                material.SetColor("_Color1", color1.Evaluate(timeCounter));
                material.SetColor("_Color2", color2.Evaluate(timeCounter));
                timeCounter += timeToAdd;
                yield return null;
            }

            material.SetColor("_Color1", color1.Evaluate(end));
            material.SetColor("_Color2", color2.Evaluate(end));
        }
        else
        {
            timeCounter = start;
            while (timeCounter > end)
            {
                material.SetColor("_Color1", color1.Evaluate(timeCounter));
                material.SetColor("_Color2", color2.Evaluate(timeCounter));
                timeCounter -= timeToAdd;
                yield return null;
            }

            material.SetColor("_Color1", color1.Evaluate(end));
            material.SetColor("_Color2", color2.Evaluate(end));
        }
    }
    
}
