
using UnityEngine;
using UnityEngine.UI;

public class Parallax : MonoBehaviour
{
    public GameObject bg;
    private Camera mainCam;
    private Vector2 screenbounds; 

   
    private void Start()
    {
        mainCam = gameObject.GetComponent<Camera>();
        screenbounds = mainCam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCam.transform.position.z));
    }
    void Update()
    {
       
    }
}
