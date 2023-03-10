using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ParallaxScrolling : MonoBehaviour
{
    public float speed = 10f;
    public Vector2 direction = new Vector2(-1f, 0f);
    public bool moveCamera = false;
    public bool isLoop = false;

    private List<SpriteRenderer> backgroundPart;
    private Vector2 repeatableSize;

    public GameObject vCam;

    void Start()
    {
        // For infinite background only
        if (isLoop)
        {
            //---------------------------------------------------------------------------------
            // 1 - Retrieve background objects
            // -- We need to know what this background is made of
            // -- Store a reference of each object
            // -- Order those items in the order of the scrolling, so we know the item that will be the first to be recycled
            // -- Compute the relative position between each part before they start moving
            //---------------------------------------------------------------------------------

            // Get all part of the layer
            backgroundPart = new List<SpriteRenderer>();

            for (int i = 0; i < transform.childCount; i++)
            {
                Transform child = transform.GetChild(i);
                SpriteRenderer r = child.GetComponent<SpriteRenderer>();

                // Only visible children
                if (r != null)
                {
                    backgroundPart.Add(r);
                }
            }

            if (backgroundPart.Count == 0)
            {
                Debug.LogError("Nothing to scroll!");
            }

            // Sort by position 
            // -- Depends on the scrolling direction
            backgroundPart = backgroundPart.OrderBy(t => t.transform.position.x * (-1 * direction.x)).ThenBy(t => t.transform.position.y * (-1 * direction.y)).ToList();

            // Get the size of the repeatable parts
            var first = backgroundPart.First();
            var last = backgroundPart.Last();

            repeatableSize = new Vector2(
              Mathf.Abs(last.transform.position.x - first.transform.position.x),
              Mathf.Abs(last.transform.position.y - first.transform.position.y)
              );
        }
    }

    void Update()
    {
        // Movement
        Vector3 movement = new Vector3(
          speed * direction.x,
          speed * direction.y,
          0);

        movement *= Time.deltaTime;
        transform.Translate(movement);

        // Move the camera
        if (moveCamera)
        {
            vCam.transform.Translate(movement);
        }

        // Loop
        if (isLoop)
        {
            //---------------------------------------------------------------------------------
            // 2 - Check if the object is before, in or after the camera bounds
            //---------------------------------------------------------------------------------

            // Camera borders
            var dist = (transform.position - Camera.main.transform.position).z;
            float leftBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).x;
            float rightBorder = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, dist)).x;

            var topBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).y;
            var bottomBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, dist)).y;

            // Determine entry and exit border using direction
            Vector3 exitBorder = Vector3.zero;
            Vector3 entryBorder = Vector3.zero;

            if (direction.x < 0)
            {
                exitBorder.x = leftBorder;
                entryBorder.x = rightBorder;
            }
            else if (direction.x > 0)
            {
                exitBorder.x = rightBorder;
                entryBorder.x = leftBorder;
            }

            if (direction.y < 0)
            {
                exitBorder.y = bottomBorder;
                entryBorder.y = topBorder;
            }
            else if (direction.y > 0)
            {
                exitBorder.y = topBorder;
                entryBorder.y = bottomBorder;
            }

            // Get the first object
            SpriteRenderer firstChild = backgroundPart.FirstOrDefault();

            if (firstChild != null)
            {
                bool checkVisible = false;

                // Check if we are after the camera
                // The check is on the position first as IsVisibleFrom is a heavy method
                // Here again, we check the border depending on the direction
                if (direction.x != 0)
                {
                    if ((direction.x < 0 && (firstChild.transform.position.x < exitBorder.x))
                    || (direction.x > 0 && (firstChild.transform.position.x > exitBorder.x)))
                    {
                        checkVisible = true;
                    }
                }
                if (direction.y != 0)
                {
                    if ((direction.y < 0 && (firstChild.transform.position.y < exitBorder.y))
                    || (direction.y > 0 && (firstChild.transform.position.y > exitBorder.y)))
                    {
                        checkVisible = true;
                    }
                }

                // Check if the sprite is really visible on the camera or not
                if (checkVisible)
                {
                    //---------------------------------------------------------------------------------
                    // 3 - The object was in the camera bounds but isn't anymore.
                    // -- We need to recycle it
                    // -- That means he was the first, he's now the last
                    // -- And we physically moves him to the further position possible
                    //---------------------------------------------------------------------------------

                    if (firstChild.IsVisibleFrom(Camera.main) == false)
                    {
                        // Set position in the end
                        firstChild.transform.position = new Vector3(
                          firstChild.transform.position.x + ((repeatableSize.x + firstChild.bounds.size.x) * -1 * direction.x),
                          firstChild.transform.position.y + ((repeatableSize.y + firstChild.bounds.size.y) * -1 * direction.y),
                          firstChild.transform.position.z
                          );

                        // The first part become the last one
                        backgroundPart.Remove(firstChild);
                        backgroundPart.Add(firstChild);
                    }
                }
            }

        }
    }
}
