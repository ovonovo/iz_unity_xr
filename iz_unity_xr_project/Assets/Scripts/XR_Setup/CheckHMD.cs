using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CheckHMD : MonoBehaviour
{
    private static bool created = false;

    private static bool passedVrCheck = false;

    public bool isVR;

    GameObject cam;

    int foundDevices;

    void Awake()
    {
        if (!created)
        {
            if (!passedVrCheck)
            {
                cam = Camera.main.gameObject;
            }
            DontDestroyOnLoad(this.gameObject);
            created = true;
        }
    }

    void Update()
    {
        if (!passedVrCheck)
        {
            checkDevices();
            checkDevicesByNode();
            print(cam.transform.position);
            if (cam.transform.position != Vector3.zero || foundDevices > 0)
            {
                cam = null;
                passedVrCheck = true;

                isVR = true;
                LoadNextScene();
            }
            else
            {
                StartCoroutine(RunDesktop());
            }
        }
    }

    private IEnumerator RunDesktop()
    {
        yield return new WaitForSeconds(.5f);
        if (!passedVrCheck)
        {
            cam = null;
            passedVrCheck = true;

            isVR = false;
            LoadNextScene();
        }
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // Alternative zum finden von Devices
    public void checkDevices()
    {
        var inputDevices = new List<UnityEngine.XR.InputDevice>();
        UnityEngine.XR.InputDevices.GetDevices (inputDevices);
        if (inputDevices.Count > 0)
        {
            print("HumanDescription FOUND");
            foundDevices = inputDevices.Count;
        }
        foreach (var device in inputDevices)
        {
            Debug
                .Log(string
                    .Format("Device found with name '{0}' and role '{1}'",
                    device.name,
                    device.characteristics.ToString()));
        }
    }

    void checkDevicesByNode()
    {
        var HeadDevices = new List<UnityEngine.XR.InputDevice>();
        UnityEngine
            .XR
            .InputDevices
            .GetDevicesAtXRNode(UnityEngine.XR.XRNode.Head,
            HeadDevices);
  
      if (HeadDevices.Count > 0)
        {
          foundDevices = HeadDevices.Count;
            Debug.Log("Found more than one left hand!");
        }
    }
}
