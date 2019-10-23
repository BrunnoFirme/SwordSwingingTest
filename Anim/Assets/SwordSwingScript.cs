using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSwingScript : MonoBehaviour
{
    [SerializeField]
    int range;
    [SerializeField]
    Transform lineStart;
    [SerializeField]
    GameObject sparkPrefab;

    public void SwingSword()
    {
        if (lineStart == null)
        {
            Debug.LogError("Did not assign linestart to SwordSwingScript on |" + this.gameObject.name + "| GameObject");
            return;
        }
        RaycastHit hit;
        if (Physics.Raycast(lineStart.position, lineStart.TransformDirection(Vector3.forward), out hit, range))
        {
            Debug.DrawRay(lineStart.position, lineStart.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            Debug.Log("Did Hit");
            Debug.Log(hit.transform.gameObject.name);
            Instantiate(sparkPrefab, hit.point, sparkPrefab.transform.rotation);
        }
        else
        {
            Debug.Log("Did not hit.");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            SwingSword();
            Debug.Log("Sword was Swung");
        }
    }
}