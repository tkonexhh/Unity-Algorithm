using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutMono : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int i;
        DemoOut(out i);
    }

    private void DemoOut(out int n)
    {
        n = 10;
    }

    // private void DemoOutChange(out int n, out int m)
    // {
    //     int temp = n;
    //     n = m;
    //     m = temp;

    // }
}
