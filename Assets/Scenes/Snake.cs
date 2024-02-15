using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    private Vector2 direction = Vector2.right;
    private List<Transform> segments;
    public Transform segmentPrefab;
    public List<GameObject> wall;
    private void Start()
    {
        segments= new List<Transform>();
        segments.Add(this.transform);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.W))
            { direction = Vector2.up; }
        else if(Input.GetKeyDown(KeyCode.S))
            { direction = Vector2.down; }
        else if( Input.GetKeyDown(KeyCode.D))
            { direction = Vector2.right; }
        else if(Input.GetKeyDown(KeyCode.A))
            { direction = Vector2.left; }
    }

    private void FixedUpdate()
    {
        for( int i = segments.Count -1; i > 0; i--)
        {
            segments[i].position= segments[i-1].position;
        }

        this.transform.position = (new Vector2(Mathf.Round(transform.position.x) + direction.x, Mathf.Round(transform.position.y) + direction.y) ) ;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Food")
        {
            Grow();
        }
        else if(other.tag == "Obstacle")
        {
            ResetState();
        }
    }
    void Grow()
    {
        Transform segment = Instantiate(segmentPrefab);
        segments.Add (segment);
    }
    void ResetState()
    {
        for (int i = 1; i < segments.Count; i++)
        {
            Destroy(segments[i].gameObject);
        }
        segments.Clear();
        segments.Add(this.transform);
        this.transform.position = Vector3.zero;
    }
}
 