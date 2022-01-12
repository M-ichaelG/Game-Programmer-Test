using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    private static UIManager s_Instance = null;
    public static UIManager instance
    {
        get
        {
            if (s_Instance == null)
            {
                s_Instance = FindObjectOfType(typeof(UIManager)) as UIManager;
                if (s_Instance == null)
                    Debug.LogWarning("Could not locate a UIManager object. \n You only can have exactly one UIManager object in the scene.");
            }
            return s_Instance;
        }
    }

    public GameObject objCanvasTopDown;
    public GameObject objCanvasThirdPerson;
    public Button btnCamera;
    public GameObject objCameraTopDown;
    public GameObject objCameraThirdPerson;

    private void Start()
    {
        btnCamera.onClick.AddListener(SwitchCamera);
    }

    private void SwitchCamera()
    {
        objCanvasThirdPerson.SetActive(!objCanvasThirdPerson.activeSelf);
        objCanvasTopDown.SetActive(!objCanvasTopDown.activeSelf);
        objCameraTopDown.SetActive(!objCameraTopDown.activeSelf);
        objCameraThirdPerson.SetActive(!objCameraThirdPerson.activeSelf);
    }
}
