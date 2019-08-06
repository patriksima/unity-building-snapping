using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Preview : MonoBehaviour
{
    public bool IsSnapped { get; private set; } = false;

    [SerializeField] private GameObject prefab;
    [SerializeField] private Material goodMaterial;
    [SerializeField] private Material badMaterial;

    private List<Collider> blocks = new List<Collider>();
    private List<Collider> snaps = new List<Collider>();
    private MeshRenderer mesh;

    private void Awake()
    {
        mesh = GetComponent<MeshRenderer>();
        ChangeMaterial();
    }

    private void Snap()
    {
        IsSnapped = (blocks.Count == 0) && (snaps.Count > 0);

        if (IsSnapped)
        {
            transform.position = snaps[snaps.Count - 1].transform.position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Block"))
            blocks.Add(other);

        if (other.CompareTag("Snap"))
            snaps.Add(other);

        Snap();
        ChangeMaterial();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Block"))
            blocks.Remove(other);

        if (other.CompareTag("Snap"))
            snaps.Remove(other);

        Snap();
        ChangeMaterial();
    }

    private void ChangeMaterial()
    {
        if (IsSnapped)
        {
            mesh.material = goodMaterial;
        }
        else
        {
            mesh.material = badMaterial;
        }
    }

    public void Build()
    {
        Instantiate(prefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
