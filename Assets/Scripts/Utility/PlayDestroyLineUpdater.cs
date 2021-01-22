using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayDestroyLineUpdater : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.GetComponent<LineUpdater>());
        Destroy(this.GetComponent<LineRenderer>());
        Destroy(this);
    }
}
