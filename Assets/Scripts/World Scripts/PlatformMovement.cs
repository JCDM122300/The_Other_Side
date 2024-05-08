
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    [SerializeField] GameObject[] waypoints;
    int currentWaypointIndex = 0;

    [SerializeField] float speed = 1f;

    // Reference to the player
    GameObject player;

    // Flag to track if the player is on the platform
    bool isPlayerOnPlatform = false;

    //GameObject scale;
    // Store the platform's position in the previous frame
    Vector3 previousPlatformPosition;

    // Start is called before the first frame update
    void Start()
    {
        // Find the player object by tag
        player = GameObject.FindGameObjectWithTag("Player");
        //scale = GameObject.Find("Scale");

        // Initialize the previous platform position
        previousPlatformPosition = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        // Move the platform
        transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypointIndex].transform.position, speed * Time.deltaTime);

        // Check if the platform has reached the waypoint
        if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex].transform.position) < 0.1f)
        {
            // Move to the next waypoint
            currentWaypointIndex++;
            if (currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = 0;
            }
        }
    }

    void LateUpdate()
    {
        // Update the player's position relative to the platform if player is on platform
        if (isPlayerOnPlatform && player != null)
        {
            // Calculate the difference in platform's position between frames
            Vector3 platformMovement = transform.position - previousPlatformPosition;

            // Apply the difference to the player's position in X direction only
            player.transform.position += new Vector3(platformMovement.x, 0, 0);

            // Reset player's scale
            player.transform.localScale = Vector3.one;
        }

        // Update the previous platform position for the next frame
        previousPlatformPosition = transform.position;
    }

    // When the player collides with the platform
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Set the player as on the platform
            isPlayerOnPlatform = true;
        }
    }

    // When the player leaves the platform
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Unset the player as on the platform
            isPlayerOnPlatform = false;
        }
    }

}
#region Old Code
//public class PlatformMovement : MonoBehaviour
//{
//    [SerializeField] GameObject[] waypoints;
//    int currentWaypointIndex = 0;

//    [SerializeField] float speed = 1f;

//    // Reference to the player
//    GameObject player;

//    GameObject scale;

//    // Start is called before the first frame update
//    void Start()
//    {
//        // Find the player object by tag
//        player = GameObject.FindGameObjectWithTag("Player");
//        scale = GameObject.Find("Scale");

//    }

//    // Update is called once per frame
//    void Update()
//    {
//        // Move the platform
//        transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypointIndex].transform.position, speed * Time.deltaTime);

//        // Check if the platform has reached the waypoint
//        if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex].transform.position) < 0.1f)
//        {
//            // Move to the next waypoint
//            currentWaypointIndex++;
//            if (currentWaypointIndex >= waypoints.Length)
//            {
//                currentWaypointIndex = 0;
//            }
//        }
//    }

//    //LateUpdate is called after all Update functions have been called
//    void LateUpdate()
//    {
//        // Update the player's position relative to the platform
//        if (player != null && player.transform.parent == transform)
//        {
//            Vector3 offset = player.transform.position - transform.position;
//            player.transform.position = transform.position + offset;

//            // Reset the player's local scale
//            player.transform.localScale = Vector3.one;
//        }
//    }

//    // When the player collides with the platform
//    private void OnCollisionEnter2D(Collision2D collision)
//    {
//        if (collision.gameObject.CompareTag("Player"))
//        {
//            // Parent the player to the platform
//            collision.transform.SetParent(transform, true);
//            collision.transform.localScale = Vector3.one;

//            //Set parent object as scale empty game object
//            //collision.transform.parent = scale.transform;
//        }
//    }



//    //When the player leaves the platform
//    private void OnCollisionExit2D(Collision2D collision)
//    {
//        if (collision.gameObject.CompareTag("Player"))
//        {
//            // Unparent the player from the platform
//            collision.transform.SetParent(null);

//            //unparent scale 
//                //collision.transform.parent = null;

//        }
//    }
//}
#endregion