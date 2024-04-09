using Cinemachine;
using UnityEngine;

public class AreaCamera : MonoBehaviour
{
    //Area-section's camera
    private CinemachineVirtualCamera FollowCamera;
    
    //Camera confiner component
    private CinemachineConfiner2D Confiner;

    //Bounding collider
    private PolygonCollider2D BoundingShape;

    private void Awake()
    {
        if (gameObject.GetComponentInChildren<CinemachineVirtualCamera>() != null)
        {
            FollowCamera = gameObject.GetComponentInChildren<CinemachineVirtualCamera>();
            Confiner = FollowCamera.GetComponent<CinemachineConfiner2D>();

            FollowCamera.Follow = GameObject.Find("Player").gameObject.transform;

            FollowCamera.gameObject.SetActive(false);
        }

        BoundingShape = GetComponentInChildren<PolygonCollider2D>();
    }
    private void ActivateAreaCamera()
    {
        FollowCamera.gameObject.SetActive(true);
        Confiner.m_BoundingShape2D = BoundingShape;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ActivateAreaCamera();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        FollowCamera.gameObject.SetActive(false);        
    }
}
