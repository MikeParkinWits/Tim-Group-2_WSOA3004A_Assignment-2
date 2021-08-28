﻿using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GrappleHookController : MonoBehaviour
{
    [Header("Sling Variables")]
    public LayerMask slingAttachLayerMask;
    public float maxSlingDistance = 20f;
    [Range(0, 100)]
    public int slingPercentageBounce = 25;
    private LineRenderer slingLineRenderer;
    private GameObject slingAnchorPoint;
    private Rigidbody2D slingHingeAnchorRb;
    private SpriteRenderer slingHingeAnchorSprite;
    private SpringJoint2D slingSpringJoint;
    private bool slingAnchorAttached;
    float aimAngle = -3.1f;


    [Header("Player Variables")]
    public float climbSpeed = 3f;
    [Range(0.0f, 1.0f)]
    public float facingDirDistanceFromPlayer = 0.5f;
    [Range(0.0f, 1.0f)]
    public float gravityScale = 0.75f;
    private Transform dirIndicator;
    private SpriteRenderer dirIndicatorSprite;
    private Vector2 playerPos;

    [Header("Control Options")]
    public bool multiButtonMouseControls;
    public bool leftMouseControl;
    public bool keyboardAndMouseControls;
    public bool keyboardOnlyControls;
    private bool controlTypeDown;
    private bool controlTypeUp;
    private bool keyboardOnly;

    [Header("Other Private")]
    private List<Vector2> slingWrapPositions = new List<Vector2>();
    private bool distanceSet;
    private bool isColliding;
    private Dictionary<Vector2, int> wrapPointsDictionary = new Dictionary<Vector2, int>();

    void Awake ()
    {

        keyboardOnly = false;

        //Variable Declarations
        slingLineRenderer = gameObject.GetComponent<LineRenderer>();

        slingAnchorPoint = GameObject.Find("Sling Anchor Point");
        slingHingeAnchorRb = slingAnchorPoint.GetComponent<Rigidbody2D>();
        slingHingeAnchorSprite = slingAnchorPoint.GetComponent<SpriteRenderer>();

        slingSpringJoint = gameObject.GetComponent<SpringJoint2D>();

        dirIndicator = GameObject.Find("Direction Indicator").GetComponent<Transform>();
        dirIndicatorSprite = dirIndicator.GetComponent<SpriteRenderer>();

        //Other Declarations
        slingSpringJoint.enabled = false;
	    playerPos = transform.position;

        this.gameObject.GetComponent<Rigidbody2D>().gravityScale = gravityScale;

        Debug.Log(controlTypeDown + "" + controlTypeUp);

    }

    /// <summary>
    /// Figures out the closest Polygon collider vertex to a specified Raycast2D hit point in order to assist in 'rope wrapping'
    /// </summary>
    /// <param name="hit">The raycast2d hit</param>
    /// <param name="polyCollider">the reference polygon collider 2D</param>
    /// <returns></returns>
    private Vector2 GetClosestColliderPointFromRaycastHit(RaycastHit2D hit, PolygonCollider2D polyCollider)
    {
        // Transform polygoncolliderpoints to world space (default is local)
        Dictionary<float, Vector2> distanceDictionary = polyCollider.points.ToDictionary<Vector2, float, Vector2>(
            position => Vector2.Distance(hit.point, polyCollider.transform.TransformPoint(position)), 
            position => polyCollider.transform.TransformPoint(position));

        IEnumerable<KeyValuePair<float, Vector2>> orderedDictionary = distanceDictionary.OrderBy(e => e.Key);

        if (orderedDictionary.Any())
        {
            return orderedDictionary.First().Value;
        }
        else
        {
            return Vector2.zero;
        }
    }

    // Update is called once per frame
    void Update ()
	{
        Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f));

        if (multiButtonMouseControls)
        {
            controlTypeDown = Input.GetMouseButton(0);
            controlTypeUp = Input.GetMouseButton(1);
        }

        if (leftMouseControl)
        {
            controlTypeDown = Input.GetMouseButtonDown(0);
            controlTypeUp = Input.GetMouseButtonUp(0);
        }

        if (keyboardAndMouseControls)
        {
            controlTypeDown = Input.GetKeyDown(KeyCode.Space);
            controlTypeUp = Input.GetKeyUp(KeyCode.Space);
        }

        if (keyboardOnlyControls)
        {
            controlTypeDown = Input.GetKeyDown(KeyCode.Space);
            controlTypeUp = Input.GetKeyUp(KeyCode.Space);

            keyboardOnly = true;
        }

        if (!keyboardOnly)
        {
            Vector3 facingDir = worldMousePosition - transform.position;
            aimAngle = Mathf.Atan2(facingDir.y, facingDir.x);

        }
        else if (keyboardOnly && !slingAnchorAttached)
        {
            Vector3 facingDir = worldMousePosition - transform.position;

            if (aimAngle > 6.25f)
            {
                aimAngle = 0f;
            }
            else
            {
                aimAngle += Time.deltaTime * 3f;
            }

            Debug.Log(aimAngle);
        }

        this.gameObject.GetComponent<Rigidbody2D>().gravityScale = gravityScale;

        if (aimAngle < 0f)
        {
            aimAngle = Mathf.PI * 2 + aimAngle;
        }

        Debug.Log(aimAngle);

        Vector3 aimDir = Quaternion.Euler(0, 0, aimAngle * Mathf.Rad2Deg) * Vector2.right;

        playerPos = transform.position;

        if (!slingAnchorAttached)
        {
            SetFacingDirSpritePosition(aimAngle);
	    }
	    else
        {
            dirIndicatorSprite.enabled = false;

            // Wrap rope around points of colliders if there are raycast collisions between player position and their closest current wrap around collider / angle point.
	        if (slingWrapPositions.Count > 0)
	        {
	            Vector2 lastSlingPoint = slingWrapPositions.Last();
                RaycastHit2D playerToLastHit = Physics2D.Raycast(playerPos, (lastSlingPoint - playerPos).normalized, Vector2.Distance(playerPos, lastSlingPoint) - 0.1f, slingAttachLayerMask);
                if (playerToLastHit)
                {
                    PolygonCollider2D colliderWithVertices = playerToLastHit.collider as PolygonCollider2D;
                    if (colliderWithVertices != null)
                    {
                        Vector2 closestHitPoint = GetClosestColliderPointFromRaycastHit(playerToLastHit, colliderWithVertices);
                        if (wrapPointsDictionary.ContainsKey(closestHitPoint))
                        {
                            // Reset the rope if it wraps around an 'already wrapped' position.
                            ResetSling();
                            return;
                        }

                        slingWrapPositions.Add(closestHitPoint);
                        wrapPointsDictionary.Add(closestHitPoint, 0);
                        distanceSet = false;
                    }
                }
            }
        }

        UpdateSlingPositions();
        HandleSlingLength();
        HandleInput(aimDir, controlTypeDown, controlTypeUp);
        HandleSlingUnwrap();
    }

    /// <summary>
    /// Handles input within the RopeSystem component
    /// </summary>
    /// <param name="aimDirection">The current direction for aiming based on mouse position</param>
    private void HandleInput(Vector2 aimDirection, bool inputDown, bool inputUp)
    {

        bool name = Input.GetMouseButton(0);

        if (inputDown)
        {
            if (slingAnchorAttached)
            {
                return;
            }

            slingLineRenderer.enabled = true;

            RaycastHit2D hit = Physics2D.Raycast(playerPos, aimDirection, maxSlingDistance, slingAttachLayerMask);
            if (hit.collider != null)
            {
                slingAnchorAttached = true;
                if (!slingWrapPositions.Contains(hit.point))
                {
                    // Jump slightly to distance the player a little from the ground after grappling to something.


                    if (slingWrapPositions.Count == 0)
                    {
                        slingSpringJoint.distance = (Vector2.Distance(playerPos, hit.point) - (Vector2.Distance(playerPos, hit.point) * (slingPercentageBounce + gameObject.GetComponent<Rigidbody2D>().velocity.magnitude)/100));
                        slingSpringJoint.frequency = 1;
                    }


                    //transform.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 200f), ForceMode2D.Force);
                    slingWrapPositions.Add(hit.point);
                    wrapPointsDictionary.Add(hit.point, 0);
                    //slingSpringJoint.distance = Vector2.Distance(playerPos, hit.point);
                    slingSpringJoint.enabled = true;
                    slingHingeAnchorSprite.enabled = true;
                }
            }
            else
            {
                slingLineRenderer.enabled = false;
                slingAnchorAttached = false;
                slingSpringJoint.enabled = false;
            }
        }

        if (inputUp)
        {
            ResetSling();
        }
    }

    /// <summary>
    /// Resets the rope in terms of gameplay, visual, and supporting variable values.
    /// </summary>
    private void ResetSling()
    {
        slingSpringJoint.enabled = false;
        slingAnchorAttached = false;
        slingLineRenderer.positionCount = 2;
        slingLineRenderer.SetPosition(0, transform.position);
        slingLineRenderer.SetPosition(1, transform.position);
        slingWrapPositions.Clear();
        wrapPointsDictionary.Clear();
        slingHingeAnchorSprite.enabled = false;
    }

    /// <summary>
    /// Move the aiming crosshair based on aim angle
    /// </summary>
    /// <param name="aimAngle">The mouse aiming angle</param>
    private void SetFacingDirSpritePosition(float aimAngle)
    {
        if (!dirIndicatorSprite.enabled)
        {
            dirIndicatorSprite.enabled = true;
        }

        float facingSpriteX = transform.position.x + facingDirDistanceFromPlayer * Mathf.Cos(aimAngle);
        float facingSpriteY = transform.position.y + facingDirDistanceFromPlayer * Mathf.Sin(aimAngle);

        Vector3 facingDirSprite = new Vector3(facingSpriteX, facingSpriteY, 0);
        dirIndicator.transform.position = facingDirSprite;
    }

    /// <summary>
    /// Retracts or extends the 'rope'
    /// </summary>
    private void HandleSlingLength()
    {
        if (Input.GetAxis("Vertical") >= 1f && slingAnchorAttached && !isColliding)
        {
            slingSpringJoint.distance -= Time.deltaTime * climbSpeed;
        }
        else if (Input.GetAxis("Vertical") < 0f && slingAnchorAttached)
        {
            slingSpringJoint.distance += Time.deltaTime * climbSpeed;
        }
    }

    /// <summary>
    /// Handles updating of the rope hinge and anchor points based on objects the rope can wrap around. These must be PolygonCollider2D physics objects.
    /// </summary>
    private void UpdateSlingPositions()
    {
        if (slingAnchorAttached)
        {
            slingLineRenderer.positionCount = slingWrapPositions.Count + 1;

            for (int i = slingLineRenderer.positionCount - 1; i >= 0; i--)
            {
                if (i != slingLineRenderer.positionCount - 1) // if not the Last point of line renderer
                {
                    slingLineRenderer.SetPosition(i, slingWrapPositions[i]);
                    
                    // Set the rope anchor to the 2nd to last rope position (where the current hinge/anchor should be) or if only 1 rope position then set that one to anchor point
                    if (i == slingWrapPositions.Count - 1 || slingWrapPositions.Count == 1)
                    {
                        if (slingWrapPositions.Count == 1)
                        {
                            Vector2 slingPos = slingWrapPositions[slingWrapPositions.Count - 1];
                            slingHingeAnchorRb.transform.position = slingPos;
                            if (!distanceSet)
                            {
                                //slingSpringJoint.distance = Vector2.Distance(transform.position, ropePosition);
                                distanceSet = true;
                            }
                        }
                        else
                        {
                            Vector2 slingPos = slingWrapPositions[slingWrapPositions.Count - 1];
                            slingHingeAnchorRb.transform.position = slingPos;
                            if (!distanceSet)
                            {
                                slingSpringJoint.distance = Vector2.Distance(transform.position, slingPos);
                                distanceSet = true;
                            }
                        }
                    }
                    else if (i - 1 == slingWrapPositions.IndexOf(slingWrapPositions.Last()))
                    {
                        // if the line renderer position we're on is meant for the current anchor/hinge point...
                        Vector2 slingPos = slingWrapPositions.Last();
                        slingHingeAnchorRb.transform.position = slingPos;
                        if (!distanceSet)
                        {
                            slingSpringJoint.distance = Vector2.Distance(transform.position, slingPos);
                            distanceSet = true;
                        }
                    }
                }
                else
                {
                    // Player position
                    slingLineRenderer.SetPosition(i, transform.position);
                }
            }
        }
    }

    private void HandleSlingUnwrap()
    {
        if (slingWrapPositions.Count <= 1)
        {
            return;
        }

        // Hinge = next point up from the player position
        // Anchor = next point up from the Hinge
        // Hinge Angle = Angle between anchor and hinge
        // Player Angle = Angle between anchor and player

        int anchorIndex = slingWrapPositions.Count - 2;
        int hingeIndex = slingWrapPositions.Count - 1;
        Vector2 anchorPosition = slingWrapPositions[anchorIndex];
        Vector2 hingePosition = slingWrapPositions[hingeIndex];
        Vector2 hingeDir = hingePosition - anchorPosition;
        float hingeAngle = Vector2.Angle(anchorPosition, hingeDir);
        Vector2 playerDir = playerPos - anchorPosition;
        float playerAngle = Vector2.Angle(anchorPosition, playerDir);

        if (!wrapPointsDictionary.ContainsKey(hingePosition))
        {
            return;
        }

        if (playerAngle < hingeAngle)
        {
            if (wrapPointsDictionary[hingePosition] == 1)
            {
                UnwrapRopePosition(anchorIndex, hingeIndex);
                return;
            }

            wrapPointsDictionary[hingePosition] = -1;
        }
        else
        {
            if (wrapPointsDictionary[hingePosition] == -1)
            {
                UnwrapRopePosition(anchorIndex, hingeIndex);
                return;
            }
            wrapPointsDictionary[hingePosition] = 1;
        }
    }

    private void UnwrapRopePosition(int anchorIndex, int hingeIndex)
    {
        Vector2 newAnchorPosition = slingWrapPositions[anchorIndex];
        wrapPointsDictionary.Remove(slingWrapPositions[hingeIndex]);
        slingWrapPositions.RemoveAt(hingeIndex);

        slingHingeAnchorRb.transform.position = newAnchorPosition;
        distanceSet = false;

        // Set new sling distance joint distance for anchor position if not yet set.
        if (distanceSet)
        {
            return;
        }
        slingSpringJoint.distance = Vector2.Distance(transform.position, newAnchorPosition);
        distanceSet = true;
    }

    void OnTriggerStay2D(Collider2D colliderStay)
    {
        isColliding = true;
    }

    private void OnTriggerExit2D(Collider2D colliderOnExit)
    {
        isColliding = false;
    }
}
