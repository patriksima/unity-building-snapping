using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public bool IsFirst = true;

    [SerializeField]
    private GameObject template;
    private GameObject prefab;
    private Preview preview;

    [SerializeField]
    private LayerMask layer;

    private float stickTolerance = .5f;

    void Start()
    {
        prefab = Instantiate(template);
        preview = prefab.GetComponent<Preview>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            print("Unsnap");
            prefab.transform.position = new Vector3(100f, 100f, 100f);
        }

        MovePreview();

        if (Input.GetMouseButtonDown(0) && (preview.IsSnapped || IsFirst))
        {
            Build();
        }

        if (Input.GetKey("escape"))
        {
            print("Bye bye!");
            Application.Quit();
        }
    }

    private void MovePreview()
    {
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, 30f, layer))
        {
            if (preview.IsSnapped)
            {

                float dist = Vector3.Distance(hit.point, prefab.transform.position);

                Debug.DrawLine(prefab.transform.position, hit.point, Color.yellow);

                if (dist >= stickTolerance)
                {
                    /*
                    Vector3 s = prefab.GetComponent<MeshRenderer>().bounds.size;
                    Vector3 n = hit.normal;
                    Vector3 h = Vector3.Scale(s, n) * .5f;
                    //print("Size:" + s.ToString("F4") + ", normal:" + n + ", scale:" + h.ToString("F4") + ", hit:" + hit.point.ToString("F4") + ", result:" + (hit.point + h).ToString("F4"));
                    prefab.transform.position = (hit.point + h);
                    */

                    prefab.transform.position = new Vector3(
                    Mathf.Round(hit.point.x),
                    Mathf.Round(hit.point.y) + .5f,
                    Mathf.Round(hit.point.z)
                    );
                }

                //print("Hit:" + hit.collider.name + " (" + hit.point + "), Dist:" + Vector3.Distance(hit.point, prefab.transform.position));
            }
            else
            {
                /*
                float y = hit.point.y + (prefab.transform.localScale.y / 2f);
                Vector3 pos = new Vector3(hit.point.x, y, hit.point.z);
                prefab.transform.position = pos;*/

                /*
                Vector3 s = prefab.GetComponent<MeshRenderer>().bounds.size;
                Vector3 n = hit.normal;
                Vector3 h = Vector3.Scale(s, n) * .5f;
                //print("Size:" + s.ToString("F4") + ", normal:" + n + ", scale:" + h.ToString("F4") + ", hit:" + hit.point.ToString("F4") + ", result:" + (hit.point + h).ToString("F4"));
                prefab.transform.position = (hit.point + h);
                */

                prefab.transform.position = new Vector3(
                    Mathf.Round(hit.point.x),
                    Mathf.Round(hit.point.y) + .5f,
                    Mathf.Round(hit.point.z)
                    );
            }
        }
    }

    private void Build()
    {
        // try create new prefab outside any others
        Vector3 pos = Camera.main.ScreenToWorldPoint(
                 new Vector3(Input.mousePosition.x,
                 Input.mousePosition.y,
                 Camera.main.nearClipPlane));
        Quaternion rot = prefab.transform.rotation;

        pos = prefab.transform.position + Vector3.forward;
        preview.Build();
        /*
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, 30f, layer))
        {
            //print("Hit col:" + hit.collider.name + ", tag:" + hit.collider.tag);
            if (hit.collider.CompareTag("Block"))
                pos = hit.collider.bounds.center + hit.normal;
            else
            {
                Vector3 dir = prefab.transform.position - hit.point;
                //print("Dir:" + dir);
                pos = prefab.transform.position + dir;
            }
        }

        pos = prefab.transform.position + Vector3.forward;
        */
        prefab = null;
        prefab = Instantiate(template, pos, rot);
        preview = prefab.GetComponent<Preview>();
        IsFirst = false;
    }
}
