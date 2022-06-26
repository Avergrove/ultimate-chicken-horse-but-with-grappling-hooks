using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A Kinematic platform that moves in between n number of positions over time.
/// </summary>
public class MovingPlatform : MonoBehaviour
{
    public List<Vector2> positions;
    public CycleType cycleType;
    public MovementType movementType;
    public float timeBetweenPoints;
    
    private int currPosIndex;
    private float timeStamp;


    // Start is called before the first frame update
    void Start()
    {
        this.cycleType = CycleType.None;
        currPosIndex = 0;
        timeStamp = 0;
    }

    // Update is called once per frame
    // TODO : Implement different movement types
    void Update()
    {
        this.timeStamp += Time.deltaTime;
        if(timeStamp > timeBetweenPoints)
        {
            currPosIndex = (currPosIndex + 1) % positions.Count;
            timeStamp -= timeBetweenPoints;
        }
        float t = Mathf.InverseLerp(0, timeBetweenPoints, timeStamp);
        this.transform.position = Vector2.Lerp(positions[currPosIndex], positions[(currPosIndex + 1) % positions.Count], t);
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
    /// Draws the line where the platform will move in the editor
    /// </summary>
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        for(int i = 0; i < positions.Count; i++)
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
