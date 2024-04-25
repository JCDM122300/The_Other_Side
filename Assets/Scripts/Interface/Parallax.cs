
using UnityEngine;
using UnityEngine.UI;

public class Parallax : MonoBehaviour
{

    Transform cam; //Main camera
    Vector3 cameraStartPos;
    float distance;

    GameObject[] background;
    Material[] mat;
    float[] backSpeed;
    float[] farthestBack;

    [Range(0.01f, 0.05f)]
    public float parallaxSpeed;

   
    private void Start()
    {
        cam = Camera.main.transform;
        cameraStartPos = cam.position;
        int backCount = transform.childCount; 
        mat = new Material[backCount];  
        backSpeed = new float[backCount];   
        background = new GameObject[backCount];
        for (int i = 0; i < backCount; i++)
        {
            background[i] = transform.GetChild(i).gameObject;
            mat[i] = background[i].GetComponent<Renderer>().material;

        }
    }
    
}
