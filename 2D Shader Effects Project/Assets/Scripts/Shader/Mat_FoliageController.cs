using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mat_FoliageController : MonoBehaviour
{
    public Material mat_ChangeColor;
    public Material mat_SnowCover;

    [Range(0f, 0.05f)] public float changeColorTime;
    [Range(0f, 0.05f)] public float snowCoverTime;
    public Gradient color1;
    public Gradient color2;

    private Material currentMat;

    private void Start()
    {
        GetComponent<SpriteRenderer>().material = mat_ChangeColor;
        currentMat = GetComponent<SpriteRenderer>().material;
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

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            StopAllCoroutines();
            StartCoroutine(SnowCoverIn());
        }

        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            StopAllCoroutines();
            StartCoroutine(SnowCoverOut());
        }
    }

    private IEnumerator ChangeColor(float start, float end)
    {
        GetComponent<SpriteRenderer>().material = mat_ChangeColor;
        currentMat = GetComponent<SpriteRenderer>().material;
        float timeCounter;

        if(start < end)
        {
            timeCounter = start;
            while (timeCounter < end)
            {
                currentMat.SetColor("_Color1", color1.Evaluate(timeCounter));
                currentMat.SetColor("_Color2", color2.Evaluate(timeCounter));
                timeCounter += changeColorTime;
                yield return null;
            }

            currentMat.SetColor("_Color1", color1.Evaluate(end));
            currentMat.SetColor("_Color2", color2.Evaluate(end));
        }
        else
        {
            timeCounter = start;
            while (timeCounter > end)
            {
                currentMat.SetColor("_Color1", color1.Evaluate(timeCounter));
                currentMat.SetColor("_Color2", color2.Evaluate(timeCounter));
                timeCounter -= changeColorTime;
                yield return null;
            }

            currentMat.SetColor("_Color1", color1.Evaluate(end));
            currentMat.SetColor("_Color2", color2.Evaluate(end));
        }
    }
    
    private IEnumerator SnowCoverIn()
    {
        GetComponent<SpriteRenderer>().material = mat_SnowCover;
        currentMat = GetComponent<SpriteRenderer>().material;

        float snowCoverAmount = 0.8f;
        while(snowCoverAmount >= 0.3f)
        {
            currentMat.SetFloat("_SnowAmount", snowCoverAmount);
            snowCoverAmount -= snowCoverTime;
            yield return null;
        }
    }

    private IEnumerator SnowCoverOut()
    {
        GetComponent<SpriteRenderer>().material = mat_SnowCover;
        currentMat = GetComponent<SpriteRenderer>().material;

        float snowCoverAmount = 0.3f;
        while (snowCoverAmount <= 0.8f)
        {
            currentMat.SetFloat("_SnowAmount", snowCoverAmount);
            snowCoverAmount += snowCoverTime;
            yield return null;
        }
    }
}
