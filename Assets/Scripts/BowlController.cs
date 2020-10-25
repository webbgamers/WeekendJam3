using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlController : MonoBehaviour
{
    GameObject carrier = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (carrier != null) {
            transform.position = carrier.transform.position;
        }
    }

    public bool Carry(GameObject newCarrier) {
        if (carrier == null) {
            return false;
        }
        else {
            carrier = newCarrier;
            return true;
        }
    }
}
