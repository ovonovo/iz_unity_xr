using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HotkeyManager : MonoBehaviour
{
    [Header("DT/XR Player")]
    public GameObject xrPlayer;
    public GameObject dtPlayer;

    [Header("Menus")]
    public GameObject menuBanner;
    public GameObject helpBanner;
    public GameObject impressumBanner;
    public GameObject quitBanner;

    Transform playerStartPosition;
    GameObject activePlayer; 

    [Header("PlayerPositions")]
    public Transform playerPosition0; 
    public Transform playerPosition1; 
    public Transform playerPosition2; 
    public Transform playerPosition3; 

    enum States {nothingActive, exitActive, helpActive, menuActive, impressumActive};
    States state;

    void Start(){
        playerStartPosition = new GameObject().transform;
        SetPlayerStartPosition();
    }

    // Update is called once per frame
    void Update()
    {
        // ESC
        if(state != States.exitActive && Input.GetKeyDown(KeyCode.Escape)){
            // Exit: Abfragen, ob wirklich Programm beenden
            ShowQuitBanner();
            state = States.exitActive;
        } else if(state == States.exitActive) CloseProgramm();
       
        // F-Tasten
        if(Input.GetKeyDown(KeyCode.F1)){
            //  Hilfe: Hinweise zur Interaktion und Navigation 
            ToggleHelp();
        }
        if(Input.GetKeyDown(KeyCode.F2)){
            // Impressum, Informationen: Autor, Semester, Studiengang, HS 
            ToggleImpressum();
        }
        if(Input.GetKeyDown(KeyCode.F3)){
            // Startansicht (Kamerapos. (A)): zurück zur Startansicht (damit ist kein Neustart gemeint)i.d.R. Kameraposition A
            PlayerToPosition(playerStartPosition);
        }
        if(Input.GetKeyDown(KeyCode.F4)){
            // Objekt-Info: Fokus bzw. Aufruf der Informationen zum Objekt (z.B. Beschreibung, Techn. Zeichnung, Diashow historischer Abb., etc.)
            ShowObjectInfo();
        }
        if(Input.GetKeyDown(KeyCode.F5)){
            // Kamerapos./Funktion X: Kameraposition (B) (besondere Ansicht für die Präsentation) ggf. + auslösen Funktion X (z.B. mit Fokus auf wichtige Funktion des Gegenstandes)
            PlayerToPosition(playerPosition0);
        }
        if(Input.GetKeyDown(KeyCode.F6)){
            // weitere ausgewählte Kamerapositionen (C) und/oder Funktionen
            PlayerToPosition(playerPosition1);
        }
        if(Input.GetKeyDown(KeyCode.F7)){
            // weitere ausgewählte Kamerapositionen (D) und/oder Funktionen
            PlayerToPosition(playerPosition2);
        }
        if(Input.GetKeyDown(KeyCode.F8)){
            // weitere ausgewählte Kamerapositionen (E) und/oder Funktionen
            PlayerToPosition(playerPosition3);
        }
        if(Input.GetKeyDown(KeyCode.F9)){
            // Sonderfunktion 1
        }
        if(Input.GetKeyDown(KeyCode.F10)){
            // Sonderfunktion 2
        }
        // if(Input.GetKeyDown(KeyCode.F11)){}
        if(Input.GetKeyDown(KeyCode.F12)){
            // Neustart der Anwendung / Unity Szene neu laden
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    void CloseProgramm(){
        // ESC: Abbrechen
        if(Input.GetKeyDown(KeyCode.Escape)){
            state = States.nothingActive;
            CloseQuitBanner();
        // ENTER: Beenden
        } else if(Input.GetKeyDown(KeyCode.Return)){
            CloseApplication();
        }
    }

    void ShowQuitBanner(){
        // enable a canvas
        quitBanner.SetActive(true);
    }
    public void CloseQuitBanner(){
        // disable a canvas
        quitBanner.SetActive(false);
    }

    void ToggleHelp(){
        helpBanner.SetActive(!helpBanner.activeSelf);
    }

    void ToggleImpressum(){
        impressumBanner.SetActive(!impressumBanner.activeSelf);
    }

    void SetPlayerStartPosition(){
        if(xrPlayer.gameObject.activeInHierarchy == true){
            activePlayer = xrPlayer;
        } 
        else if(dtPlayer.gameObject.activeInHierarchy == true){
            activePlayer = dtPlayer;
        } 
        
        if(activePlayer != null){
            print("not null");
            playerStartPosition.position = activePlayer.transform.position;
            playerStartPosition.rotation = activePlayer.transform.rotation;
        } 
    }

    public void PlayerToPosition(Transform pos){
        // setze Player zu bestimmter position
        activePlayer.transform.position = pos.position;
        activePlayer.transform.rotation = pos.rotation;
    }

    void  ShowObjectInfo(){
        // Zeige eine Bestimmte Infotafel in der Anwendung
    }

    public void CloseApplication() {
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
