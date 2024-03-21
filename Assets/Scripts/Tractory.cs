using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tractory : MonoBehaviour
{
    [SerializeField] private GameObject Hero;
    [SerializeField] private GameObject Target;

    [SerializeField] private LineRenderer line;

    private void Update()
    {
        DrawLine();
    }


    private void DrawLine()
    {
        if (Target == null || Hero == null) { return; }

        line.SetPosition(0, Hero.transform.position);
        line.SetPosition(1, Target.transform.position);
    }
}
