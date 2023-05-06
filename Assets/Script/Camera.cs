using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] private Transform Target;
    private Vector3 pos;
    private void Awake()
    {
        if (!Target)
            Target = FindObjectOfType<Player>().transform;
    }

    private void Update()
    {
        pos = Target.position;
        pos.z = -10f;
        //pos.y += 1f;
        transform.position = Vector3.Lerp(transform.position, pos, Time.deltaTime*2);
    }
}
