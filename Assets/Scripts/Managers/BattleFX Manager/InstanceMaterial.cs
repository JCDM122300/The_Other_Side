using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstanceMaterial : MonoBehaviour
{
    [SerializeField] private Material materialReference;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        if (gameObject.TryGetComponent<SpriteRenderer>(out SpriteRenderer t))
        {
            spriteRenderer = t;
        }

        if (materialReference != null)
        {
            Material mat = new Material(materialReference);
            spriteRenderer.material = mat;
        }
    }
}
