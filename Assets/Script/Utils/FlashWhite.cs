using UnityEngine;
using System.Collections;

public class FlashWhite : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    private Material defaultMaterial;
    private Material flashMaterial;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultMaterial = spriteRenderer.material;
        flashMaterial = Resources.Load<Material>("Materials/mWhite");
    }

    public void Flash()
    {
        spriteRenderer.material = flashMaterial;
        StartCoroutine(ResetMaterial());
    }
    IEnumerator ResetMaterial()
    {
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.material = defaultMaterial;
    }

    public void Reset()
    {
        spriteRenderer.material = defaultMaterial;
    }
}
