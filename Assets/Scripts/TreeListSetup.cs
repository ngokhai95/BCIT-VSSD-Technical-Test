using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TreeListSetup : MonoBehaviour
{
    [SerializeField] private GameObject model;
    [SerializeField] private Dropdown dropdown;

    private UserInteraction userInteraction;

    void Start()
    {
        // Populate the dropdown with the names of all descendants of the input game object
        List<string> descendantNames = GetDescendantNames(model.transform);
        PopulateDropdownOptions(descendantNames, dropdown);
    }

    // Get the names of all descendants of the input game object
    List<string> GetDescendantNames(Transform obj)
    {
        List<string> names = new List<string>();
        GetDescendantNamesRecursive(obj, names);
        return names;
    }

    void GetDescendantNamesRecursive(Transform obj, List<string> names)
    {
        foreach (Transform child in obj)
        {
            string childName = child.gameObject.name;
            if (child.childCount == 0)
            {
                childName = " - " + childName;
            }
            else
            {
                childName = childName + ":";
            }
            names.Add(childName);
            GetDescendantNamesRecursive(child, names);
        }
    }

    // Populate the dropdown with the given options
    void PopulateDropdownOptions(List<string> options, Dropdown dropdown)
    {
        dropdown.ClearOptions();
        List<Dropdown.OptionData> dropdownOptions = new List<Dropdown.OptionData>();
        foreach (string option in options)
        {
            Dropdown.OptionData dropdownOption = new Dropdown.OptionData(option);
            dropdownOptions.Add(dropdownOption);
        }
        dropdown.AddOptions(dropdownOptions);
    }

    //Update the selected part on chosen option
    public void OnDropdownValueChanged()
    {
        dropdown.captionText.text = dropdown.captionText.text.Trim(' ', '-');
        dropdown.captionText.text = dropdown.captionText.text.TrimEnd(':');
        GameObject part = GameObject.Find(dropdown.captionText.text);
        if (part != null)
        {
            userInteraction = FindObjectOfType<UserInteraction>();
            userInteraction.selectedObject = part;
        }
    }
}