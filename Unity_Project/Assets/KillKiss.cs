using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillKiss : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(killMe());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private IEnumerator killMe()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);
    }
}
