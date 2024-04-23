using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class HotkeyManager : MonoBehaviour
{
    [Header("DT/XR Player")]
    public GameObject xrPlayer;
    public GameObject dtPlayer;

    [Header("Menus")]
    public InputActionReference menuButtonXR;
    public GameObject menuBanner;
    public GameObject helpBanner;
    public GameObject impressumBanner;

    Transform playerStartPosition;
    GameObject activePlayer;

    [Header("PlayerPositions")]
    public Transform playerPosition0;
    public Transform playerPosition1;
    public Transform playerPosition2;
    public Transform playerPosition3;

    [Header("Move XR-Player Up and Down")]
    public float upStep = 0.01f;

    enum States { nothingActive, exitActive, helpActive, menuActive, impressumActive };
    States state;

    void Start()
    {
        playerStartPosition = new GameObject().transform;
        SetPlayerStartPosition();
        // Option 1 um eine XR Action auszuführen
        // menuButtonXR.action.performed += menuPerformed;
    }
    // gehört zu Option 1 um eine XR Action auszuführen
    // private void menuPerformed(InputAction.CallbackContext obj){
    //     print("MaNUUa");
    //     ToggleMenu();
    // }

    // Update is called once per frame
    void Update()
    {
        // Option 2 um eine XR Action auszuführen
        if (menuButtonXR)
        {
            if (menuButtonXR.action.triggered)
            {
                ToggleMenu();
            }
        }


        // ESC
        if (state != States.exitActive && Input.GetKeyDown(KeyCode.Escape))
        {
            // Exit: Abfragen, ob wirklich Programm beenden
            ShowMenuBanner();
            state = States.exitActive;
        }
        else if (state == States.exitActive) CloseProgramm();

        // F-Tasten
        if (Input.GetKeyDown(KeyCode.F1))
        {
            //  Hilfe: Hinweise zur Interaktion und Navigation 
            ToggleHelp();
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            // Impressum, Informationen: Autor, Semester, Studiengang, HS 
            ToggleImpressum();
        }
        if (Input.GetKeyDown(KeyCode.F3))
        {
            // Startansicht (Kamerapos. (A)): zurück zur Startansicht (damit ist kein Neustart gemeint) i.d.R. Kameraposition (A)
            PlayerToPosition(playerStartPosition);
        }
        if (Input.GetKeyDown(KeyCode.F4))
        {
            // Objekt-Info: Fokus bzw. Aufruf der Informationen zum Objekt (z.B. Beschreibung, Techn. Zeichnung, Diashow historischer Abb., etc.)
            ShowObjectInfo();
        }
        if (Input.GetKeyDown(KeyCode.F5))
        {
            // Kamerapos./Funktion X: Kameraposition (B) (besondere Ansicht für die Präsentation) ggf. + auslösen Funktion X (z.B. mit Fokus auf wichtige Funktion des Gegenstandes)
            PlayerToPosition(playerPosition0);
        }
        if (Input.GetKeyDown(KeyCode.F6))
        {
            // weitere ausgewählte Kamerapositionen (C) und/oder Funktionen
            PlayerToPosition(playerPosition1);
        }
        if (Input.GetKeyDown(KeyCode.F7))
        {
            // weitere ausgewählte Kamerapositionen (D) und/oder Funktionen
            PlayerToPosition(playerPosition2);
        }
        if (Input.GetKeyDown(KeyCode.F8))
        {
            // weitere ausgewählte Kamerapositionen (E) und/oder Funktionen
            PlayerToPosition(playerPosition3);
        }
        if (Input.GetKeyDown(KeyCode.F9))
        {
            // Sonderfunktion 1
        }
        if (Input.GetKeyDown(KeyCode.F10))
        {
            // Sonderfunktion 2
        }
        // if(Input.GetKeyDown(KeyCode.F11)){}
        if (Input.GetKeyDown(KeyCode.F12))
        {
            // Neustart der Anwendung / Unity Szene neu laden
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        // move Scene up in VR with arrow keys
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (Input.GetKey(KeyCode.LeftShift)) xrPlayer.transform.position += new Vector3(0, upStep * 10, 0);
            else xrPlayer.transform.position += new Vector3(0, upStep, 0);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (Input.GetKey(KeyCode.LeftShift)) xrPlayer.transform.position += new Vector3(0, -upStep * 10, 0);
            else xrPlayer.transform.position += new Vector3(0, -upStep, 0);
        }
    }

    void CloseProgramm()
    {
        // ESC: Abbrechen
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            state = States.nothingActive;
            CloseAllMenus();
            // ENTER: Beenden
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            CloseApplication();
        }
    }

    public void ShowMenuBanner()
    {
        // enable a canvas
        if (!menuBanner) return;
        menuBanner.SetActive(true);
        helpBanner.SetActive(false);
        impressumBanner.SetActive(false);
    }

    public void ToggleMenu()
    {
        if (!menuBanner) return;
        helpBanner.SetActive(false);
        impressumBanner.SetActive(false);
        menuBanner.SetActive(!menuBanner.activeSelf);
    }

    public void ToggleHelp()
    {
        if (!helpBanner) return;
        helpBanner.SetActive(!helpBanner.activeSelf);
        impressumBanner.SetActive(false);
        menuBanner.SetActive(false);
    }

    public void ToggleImpressum()
    {
        if (!impressumBanner) return;
        helpBanner.SetActive(false);
        impressumBanner.SetActive(!impressumBanner.activeSelf);
        menuBanner.SetActive(false);
    }

    public void CloseAllMenus()
    {
        if (helpBanner) helpBanner.SetActive(false);
        if (impressumBanner) impressumBanner.SetActive(false);
        if (menuBanner) menuBanner.SetActive(false);
    }

    void SetPlayerStartPosition()
    {
        if (xrPlayer.gameObject.activeInHierarchy == true)
        {
            activePlayer = xrPlayer;
        }
        else if (dtPlayer.gameObject.activeInHierarchy == true)
        {
            activePlayer = dtPlayer;
        }

        if (activePlayer != null)
        {
            print("not null");
            playerStartPosition.position = activePlayer.transform.position;
            playerStartPosition.rotation = activePlayer.transform.rotation;
        }
    }


    public void PlayerToPosition(Transform _pos)
    {
        CharacterController charContr = activePlayer.GetComponent<CharacterController>();
        bool hasCharContr = charContr != null;

        if (hasCharContr) activePlayer.GetComponent<CharacterController>().enabled = false;

        activePlayer.transform.position = _pos.position;
        activePlayer.transform.rotation = _pos.rotation;

        if (hasCharContr) activePlayer.GetComponent<CharacterController>().enabled = true;

    }

    void ShowObjectInfo()
    {
        // Zeige eine Bestimmte Infotafel in der Anwendung
    }

    public void CloseApplication()
    {
        // If we are running in a standalone build of the game
#if UNITY_STANDALONE
        // Quit the application
        Application.Quit();
#endif

        // If we are running in the editor
#if UNITY_EDITOR
        // Stop playing the scene
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
