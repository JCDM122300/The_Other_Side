
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

    GameObject scale;

    // Start is called before the first frame update
    void Start()
    {
        // Find the player object by tag
        player = GameObject.FindGameObjectWithTag("Player");
        scale = GameObject.Find("Scale");
        
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

    //LateUpdate is called after all Update functions have been called
    void LateUpdate()
    {
        // Update the player's position relative to the platform
        if (player != null && player.transform.parent == transform)
        {
            Vector3 offset = player.transform.position - transform.position;
            player.transform.position = transform.position + offset;

            // Reset the player's local scale
            player.transform.localScale = Vector3.one;
        }
    }


    // When the player collides with the platform
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Parent the player to the platform
            //collision.transform.SetParent(this.transform);

            //Set parent object as scale empty game object
            collision.transform.parent = scale.transform;
        }
    }

    // When the player leaves the platform
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Unparent the player from the platform
            collision.transform.SetParent(null);

            //unparent scale 
            collision.transform.parent = null;

        }
    }
}