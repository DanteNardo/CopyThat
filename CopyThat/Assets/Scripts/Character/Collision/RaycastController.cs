using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class RaycastController : MonoBehaviour {
    
    // Attributes 
    public LayerMask collisionMask;

    public const float dstBetweenRays = 0.1f; // initially 0.25f
    [HideInInspector]
    public int horizontalRayCount;
    [HideInInspector]
    public int verticalRayCount;
    public const float skinWidth = 0.01f; // initially 0.015f

    [HideInInspector]
    public float horizontalRaySpacing;
    [HideInInspector]
    public float verticalRaySpacing;

    [HideInInspector]
    public BoxCollider2D collider;
    [HideInInspector]
    public RaycastOrigins raycastOrigins;

    // Raycasting --------------------------------------
    // Use this for initialization
    public virtual void Awake()
    {
        collider = GetComponent<BoxCollider2D>(); 
    }

    public virtual void Start()
    {
        CalculateRaySpacing();
    }
    
    // Sets the rays' starting positions to the correct corners 
    public virtual void UpdateRaycastOrigins()
    {
        //transform.rotation = transform.parent.rotation;
        //collider.transform.localRotation = transform.localRotation; 
        //Bounds bounds = collider.bounds;
        //bounds.Expand(skinWidth * -2);

        //// Original method 
        //raycastOrigins.bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
        //raycastOrigins.bottomRight = new Vector2(bounds.max.x, bounds.min.y);
        //raycastOrigins.topLeft = new Vector2(bounds.min.x, bounds.max.y);
        //raycastOrigins.topRight = new Vector2(bounds.max.x, bounds.max.y);

        Bounds bounds = collider.bounds;
        bounds.Expand(skinWidth * -2);

        // Accounting for world space
        float top = collider.offset.y + (collider.size.y / 2f);
        float btm = collider.offset.y - (collider.size.y / 2f);
        float left = collider.offset.x - (collider.size.x / 2f);
        float right = collider.offset.x + (collider.size.x / 2f);

        Vector3 topLeft = transform.TransformPoint(new Vector3(left, top, 0f));
        Vector3 topRight = transform.TransformPoint(new Vector3(right, top, 0f));
        Vector3 btmLeft = transform.TransformPoint(new Vector3(left, btm, 0f));
        Vector3 btmRight = transform.TransformPoint(new Vector3(right, btm, 0f));

        raycastOrigins.bottomLeft = new Vector2(btmLeft.x, btmLeft.y);
        raycastOrigins.bottomRight = new Vector2(btmRight.x, btmRight.y);
        raycastOrigins.topLeft = new Vector2(topLeft.x, topLeft.y);
        raycastOrigins.topRight = new Vector2(topRight.x, topRight.y);
    }

    // Calculates the space between each ray for vertical and horizontal rays 
    public void CalculateRaySpacing()
    {
        Bounds bounds = collider.bounds;
        bounds.Expand(skinWidth * -2);

        float boundsWidth = bounds.size.x;
        float boundsHeight = bounds.size.y;

        horizontalRayCount = Mathf.RoundToInt(boundsHeight / dstBetweenRays);
        verticalRayCount = Mathf.RoundToInt(boundsWidth / dstBetweenRays);

        horizontalRaySpacing = bounds.size.y / (horizontalRayCount - 1);
        verticalRaySpacing = bounds.size.x / (verticalRayCount - 1);

    }

    /// <summary>
    /// Raycast positions 
    /// </summary>
    public struct RaycastOrigins
    {
        public Vector2 topLeft, topRight;
        public Vector2 bottomLeft, bottomRight;
    }

    // Struct contains data per collision 
    public struct CollisionInfo
    {
        public bool above, below;
        public bool left, right;

        public bool climbingSlope;
        public bool descendingSlope;
        public bool slidingDownMaxSlope; 

        public float slopeAngle, slopeAnglePrevious;
        public Vector2 slopeNormal;
        public Vector2 moveAmountOld; 
        public int faceDir;
        public bool fallingThroughPlatform; 

        public void Reset()
        {
            above = below = false;
            left = right = false;
            climbingSlope = false;
            descendingSlope = false;
            slidingDownMaxSlope = false; 
            slopeAnglePrevious = slopeAngle;
            slopeNormal = Vector2.zero; 
            slopeAngle = 0;
        }
    }
}
