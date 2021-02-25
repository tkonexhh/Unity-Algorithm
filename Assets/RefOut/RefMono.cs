using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefMono : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int i = 0;
        DemoRef(ref i);

        int n = 1, m = 2;
        DemoRefChange(ref n, ref m);
        Debug.LogError(n + "--" + m);
    }

    private void DemoRef(ref int n)
    {
        Debug.Log(n);
    }

    private void DemoRefChange(ref int n, ref int m)
    {
        int temp = n;
        n = m;
        m = temp;

    }
}
