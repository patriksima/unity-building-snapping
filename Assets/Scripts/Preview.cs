using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Preview : MonoBehaviour
{
    public bool IsSnapped { get; private set; } = false;

    [SerializeField]
    private GameObject prefab;
    [SerializeField]
    private List<string> allowedTags = new List<string>();
    [SerializeField]
    private Material goodMaterial;
    [SerializeField]
    private Material badMaterial;

    private List<Collider> blocks = new List<Collider>();
    private List<Collider> snaps = new List<Collider>();
    private MeshRenderer meshRenderer;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
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
        //print("Enter " + other.tag + ": Blocks = " + blocks.Count + ", Snaps = " + snaps.Count);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Block"))
            blocks.Remove(other);

        if (other.CompareTag("Snap"))
            snaps.Remove(other);

        Snap();
        ChangeMaterial();
        //print("Exit " + other.tag + ": Blocks = " + blocks.Count + ", Snaps = " + snaps.Count);
    }

    private void ChangeMaterial()
    {
        if (IsSnapped)
        {
            meshRenderer.material = goodMaterial;
        }
        else
        {
            meshRenderer.material = badMaterial;
        }
    }

    public void Build()
    {
        print("Build!");
        Instantiate(prefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
