using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstanceMaterial : MonoBehaviour
{
    [SerializeField] private Material materialReference;
    [SerializeField] private bool ImageComponent = false;

    private SpriteRenderer spriteRenderer;
    private Image image;

    void Start()
    {
        Material mat = new Material(materialReference);

        if (!ImageComponent)
        {
            if (gameObject.TryGetComponent<SpriteRenderer>(out SpriteRenderer t))
            {
                spriteRenderer = t;
            }

            if (materialReference != null)
            {
                spriteRenderer.material = mat;
            }
        }
        else
        {
            image = GetComponent<Image>();
            image.material = mat;
        }
        
    }
}
