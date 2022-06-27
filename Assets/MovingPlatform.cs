using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// A Kinematic platform that moves in between n number of positions over time.
/// </summary>
public class MovingPlatform : MonoBehaviour
{
    public List<Vector2> positions;
    public CycleType cycleType;
    public MovementType movementType;
    public float timeBetweenPoints;

    private List<GameObject> platformClingers;
    private int currPosIndex;
    private float timeStamp;
    private Vector2 lastPos;

    // Start is called before the first frame update
    void Start()
    {
        this.platformClingers = new List<GameObject>();
        this.cycleType = CycleType.None;
        currPosIndex = 0;
        timeStamp = 0;
    }

    // Update is called once per frame
    // TODO : Implement different movement types
    void Update()
    {
        this.timeStamp += Time.deltaTime;
        if (timeStamp > timeBetweenPoints)
        {
            currPosIndex = (currPosIndex + 1) % positions.Count;
            timeStamp -= timeBetweenPoints;
        }
        float t = Mathf.InverseLerp(0, timeBetweenPoints, timeStamp);
        this.lastPos = this.transform.position;
        this.transform.position = Vector2.Lerp(positions[currPosIndex], positions[(currPosIndex + 1) % positions.Count], t);
        foreach(GameObject platformClinger in platformClingers)
        {
            ExecuteEvents.Execute<IMovingPlatformEventHandler>(platformClinger, null, (handler, data) => handler.OnPlatformMove(this));
        }
    }

    public void Initialize(List<Vector2> points, float timeBetweenPoints, CycleType cycleType, MovementType movementType)
    {
        this.transform.position = points[0];
        this.positions = points;
        this.timeBetweenPoints = timeBetweenPoints;
        this.cycleType = cycleType;
        this.movementType = movementType;
    }

    /// <summary>
    /// It is necessary to write a velocity method since the platform is kinematic.
    /// </summary>
    public Vector2 GetVelocity()
    {
        return ((Vector2) this.transform.position - (Vector2) lastPos) / Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(Constants.Tags.Player.ToString()))
        {
            this.platformClingers.Add(collision.gameObject);
            collision.gameObject.transform.parent = this.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(Constants.Tags.Player.ToString()))
        {
            if(collision.transform.parent != null && collision.transform.parent.Equals(this.transform))
            {
                this.platformClingers.Remove(collision.gameObject);
                collision.transform.parent = null;
                ExecuteEvents.Execute<IMovingPlatformEventHandler>(collision.gameObject, null, (handler, data) => handler.OnPlatformLeave(this));
            }
        }
    }

    /// <summary>
    /// Draws the line where the platform will move in the editor
    /// </summary>
    void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        for (int i = 0; i < positions.Count; i++)
        {
            Gizmos.DrawLine(positions[i], positions[(i + 1) % positions.Count]);
        }
    }

    public enum CycleType
    {
        Reverse,
        None,
        Loop
    }

    public enum MovementType
    {
        Linear,
        Smooth,
        Instantaneous
    }
}
