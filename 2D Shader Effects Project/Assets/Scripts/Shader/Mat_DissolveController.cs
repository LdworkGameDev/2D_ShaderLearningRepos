using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mat_DissolveController : MonoBehaviour
{
    public float dissolveArea;
    public float dissolveDensity;

    [ColorUsage(true, true)]
    public Color dissolveColor;
    public float dissolveTime;

    private Material material;

    private void Start()
    {
        material = GetComponent<SpriteRenderer>().material;

        if (material != null)
        {
            material.SetColor("_DissolveColor", dissolveColor);
            material.SetFloat("_DissolveArea", dissolveArea);
            material.SetFloat("_Density", dissolveDensity);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            StopAllCoroutines();
            StartCoroutine(DissolveIn());
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            StopAllCoroutines();
            StartCoroutine(DissolveOut());
        }
    }

    private IEnumerator DissolveIn()
    {
        if (material == null) yield break;
        float dissolveTimeCounter = dissolveTime;

        while(dissolveTimeCounter > 0)
        {
            dissolveTimeCounter -= Time.deltaTime;
            material.SetFloat("_DissolveValue", dissolveTimeCounter / dissolveTime);            
            yield return null;
        }

        material.SetFloat("_DissolveValue", 0);
    }

    private IEnumerator DissolveOut()
    {
        if (material == null) yield break;
        float dissolveTimeCounter = 0;

        while(dissolveTimeCounter < dissolveTime)
        {
            dissolveTimeCounter += Time.deltaTime;
            material.SetFloat("_DissolveValue", dissolveTimeCounter / dissolveTime);
            yield return null;
        }

        material.SetFloat("_DissolveValue", 1);
    }
}
