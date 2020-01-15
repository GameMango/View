using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScale : MonoBehaviour
{
    public Transform cam;
    public Vector3 start;
    public Vector3 startScale;
    public float scaleJump = 0.1f;
    public List<Vector3> hits = new List<Vector3>();
    public List<Vector3> closest = new List<Vector3>();

    private Rigidbody _rigidbody;
    public Collider collider;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        foreach (var hit in hits)
        {
            Gizmos.DrawSphere(hit, 0.1f);
        }
        
        Gizmos.color = Color.blue;
        foreach (var hit in closest)
        {
            Gizmos.DrawSphere(hit, 0.1f);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            closest.Clear();
            hits.Clear();
            start = transform.position;
            startScale = transform.localScale;

            var headPos = cam.position;
            var oldPos = start;
            var dir = (oldPos - headPos).normalized;
            var ray = new Ray(oldPos, dir);

            if (Physics.Raycast(ray, out var hitInfo))
            {
                
                Vector3 pos = start;
                Vector3 startSize = collider.bounds.size;

                float stop = 0.2f;

                while (_rigidbody.SweepTest(dir, out var sweepHit))
                {
                    Bounds bounds = collider.bounds;
                    bounds.size = startSize * Vector3.Distance(headPos, pos)
                                  / Vector3.Distance(headPos, oldPos);
                    var clos = bounds.ClosestPoint(sweepHit.point);
                    closest.Add(clos);
                    float dis = Vector3.Distance(clos, sweepHit.point);
                    hits.Add(sweepHit.point);
                    if (dis <= stop)
                        break;
                
                    pos += dir * scaleJump;
                    _rigidbody.position = pos;
                    transform.localScale = startScale * Vector3.Distance(headPos, pos)
                                           / Vector3.Distance(headPos, oldPos);
                    
                }
                

                collider.enabled = false;
            }
        }
    }
}