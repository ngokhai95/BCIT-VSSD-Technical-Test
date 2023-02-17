using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour
{
    [SerializeField] private GameObject resetButton;
    [SerializeField] private GameObject partListButton;
    [SerializeField] private GameObject xrayButton;
    [SerializeField] private GameObject transparentButton;
    [SerializeField] private Dropdown dropdown;
    [SerializeField] private Material originalMaterial;
    [SerializeField] private Material xrayMaterial;
    [SerializeField] private Material transparentMaterial;

    private Spawner spawner;
    private UserInteraction userInteraction;

    private void Start()
    {
        spawner = FindObjectOfType<Spawner>();

        resetButton.GetComponent<Button>().onClick.AddListener(ResetModel);
        partListButton.GetComponent<Button>().onClick.AddListener(ShowHidePartList);
        xrayButton.GetComponent<Button>().onClick.AddListener(XRay);
        transparentButton.GetComponent<Button>().onClick.AddListener(Transparent);
    }

    private void ResetModel()
    {
        var model = GameObject.FindGameObjectWithTag("Model");
        Destroy(model);
        spawner.SpawnModel();
        dropdown.value = 0;
    }

    private void ShowHidePartList()
    {
        dropdown.gameObject.SetActive(!dropdown.gameObject.activeSelf);
    }

    private void XRay()
    {
        userInteraction = FindObjectOfType<UserInteraction>();
        var selectedPart = userInteraction.selectedObject;
        var model = GameObject.FindGameObjectWithTag("Model");
        var parts = ModelUtils.GetAllDescendants(model);

        foreach (var part in parts)
        {
            if (part.tag == "X-Ray")
            {
                if (part != selectedPart && part.TryGetComponent(out Renderer renderer))
                {
                    renderer.material = originalMaterial;
                }
                part.tag = "Untagged";
            }
            else
            {
                if (part != selectedPart && part.TryGetComponent(out Renderer renderer))
                {
                    renderer.material = xrayMaterial;
                }
                part.tag = "X-Ray";
            }
        }
    }

    private void Transparent()
    {
        userInteraction = FindObjectOfType<UserInteraction>();
        var selectedPart = userInteraction.selectedObject;
        var model = GameObject.FindGameObjectWithTag("Model");
        var parts = ModelUtils.GetAllDescendants(model);

        foreach (var part in parts)
        {
            if (part.tag == "Transparent")
            {
                if (part != selectedPart && part.TryGetComponent(out Renderer renderer))
                {
                    renderer.material = originalMaterial;
                }
                part.tag = "Untagged";
            }
            else
            {
                if (part != selectedPart && part.TryGetComponent(out Renderer renderer))
                {
                    renderer.material = transparentMaterial;
                    // Enable depth writing
                    renderer.material.SetInt("_ZWrite", 1);
                    // Set the source blend mode to source alpha
                    renderer.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                    // Set the destination blend mode to one minus source alpha
                    renderer.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                    // Set the culling mode to back
                    renderer.material.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Back);
                }
                part.tag = "Transparent";
            }
        }
    }
}