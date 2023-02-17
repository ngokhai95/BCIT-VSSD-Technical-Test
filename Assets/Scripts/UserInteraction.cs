using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UserInteraction : MonoBehaviour
{
    public GameObject selectedObject;

    private enum MaterialType { Original, Highlight, XRay, Transparent }

    [SerializeField] private float raycastDistance = 10f;
    [SerializeField] private Material highlightMaterial;
    [SerializeField] private Material originalMaterial;
    [SerializeField] private Material xrayMaterial;
    [SerializeField] private Material transparentMaterial;

    private GameObject currentHover;
    private GameObject lastHover;
    private Text popUpText;
    private Dropdown dropdown;

    private void Start()
    {
        popUpText = GameObject.Find("PopUp").GetComponent<Text>();
        dropdown = GameObject.Find("Dropdown").GetComponent<Dropdown>();
    }

    private void Update()
    {
        // Create a ray from the main camera that passes through the position of the mouse cursor in the screen space
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, raycastDistance))
        {
            GameObject hitObject = hit.collider.gameObject;
            if (hitObject != currentHover)
            {
                // Store the last hover object and update the current hover object
                lastHover = currentHover;
                currentHover = hitObject;
                // Set the highlight material for the current hover object
                SetMaterial(currentHover, MaterialType.Highlight);
                // Display the name of the selected object in the popup text
                if (currentHover == selectedObject)
                {
                    popUpText.text = selectedObject.name;
                }
                else
                {
                    popUpText.text = "";
                }
            }
        }
        // Reset the current hover object if ray not hit
        else
        {
            if (currentHover != null)
            {
                // Reset the material of the current hover object
                SetMaterial(currentHover, GetMaterialType(currentHover.tag));
                currentHover = null;
            }
            else
            {
                popUpText.text = "";
            }
        }
        // Reset the material of the last hover object
        if (lastHover != null && currentHover != lastHover)
        {
            SetMaterial(lastHover, GetMaterialType(lastHover.tag));
            lastHover = null;
        }
        // Select current hover object on mouse click
        if (Input.GetMouseButtonDown(0))
        {
            if (currentHover != null)
            {
                SelectObject(currentHover);
            }
            else
            {
                UnselectObject();
            }
        }

        if (selectedObject != null)
        {
            SetMaterial(selectedObject, MaterialType.Highlight);
            lastHover = selectedObject;
            popUpText.transform.position = Input.mousePosition;
        }
    }

    private MaterialType GetMaterialType(string tag)
    {
        switch (tag)
        {
            case "X-Ray":
                return MaterialType.XRay;
            case "Transparent":
                return MaterialType.Transparent;
            default:
                return MaterialType.Original;
        }
    }

    private void SetMaterial(GameObject obj, MaterialType materialType)
    {
        Renderer renderer = obj.GetComponent<Renderer>();
        if (renderer == null)
        {
            return;
        }
        switch (materialType)
        {
            case MaterialType.Original:
                renderer.material = originalMaterial;
                break;
            case MaterialType.Highlight:
                renderer.material = highlightMaterial;
                break;
            case MaterialType.XRay:
                renderer.material = xrayMaterial;
                break;
            case MaterialType.Transparent:
                renderer.material = transparentMaterial;
                break;
            default:
                break;
        }
    }

    private void SelectObject(GameObject obj)
    {
        selectedObject = obj;
        popUpText.text = selectedObject.name;
        dropdown.captionText.text = selectedObject.name;
    }

    private void UnselectObject()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            selectedObject = null;
            popUpText.text = "";
        }
    }
}