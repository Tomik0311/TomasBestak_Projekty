using System;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class EnemyAi : MonoBehaviour{
    public float speed = 10f;
    private Transform Target;
    private int wavePointIndex = 0;
    void Start()
    {
        Target = Waypoints.points[0];

    }
    private void Update()
    {
        Vector3 dir = Target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
        if (Vector3.Distance(transform.position, Target.position) <= 0.2f)
        {
            GetNextWaypoint();
        }

    }

     void GetNextWaypoint()
    {
        if(wavePointIndex >= Waypoints.points.Length - 1)
        {
            Destroy(gameObject);
            return;
        } 
        wavePointIndex++;
        Target = Waypoints.points[wavePointIndex];
    }
}
