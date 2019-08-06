using UnityEngine;

public class Building : MonoBehaviour
{
    public bool IsFirst = true;

    [SerializeField] private GameObject template;
    [SerializeField] private LayerMask layer;

    private GameObject prefab;
    private Preview preview;
    private MeshRenderer mesh;

    void Awake()
    {
        CreatePreview();
    }

    void Update()
    {
        MovePreview();

        if (Input.GetMouseButtonDown(0) && (preview.IsSnapped || IsFirst))
        {
            Build();
        }

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

    private void MovePreview()
    {
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, 30f, layer))
        {
            prefab.transform.position = new Vector3(
                        Mathf.Round(hit.point.x),
                        Mathf.Round(hit.point.y) + mesh.bounds.size.y * .5f,
                        Mathf.Round(hit.point.z)
                    );
        }
    }

    private void Build()
    {
        preview.Build();

        CreatePreview();

        IsFirst = false;
    }

    private void CreatePreview()
    {
        prefab = null;
        prefab = Instantiate(template);

        preview = prefab.GetComponent<Preview>();
        mesh = prefab.GetComponent<MeshRenderer>();
    }
}
