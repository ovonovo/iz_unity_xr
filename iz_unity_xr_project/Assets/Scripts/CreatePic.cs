using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CreatePic : MonoBehaviour
{
    public GameObject picture; // The cube prefab to be spawned
    public Camera photoCamera;
    public RenderTexture photoTexture;
    Texture2D myTexture;
    
    // [Header("Save Images")]
    // public bool savePhoto = false;
    // public string fileName = "myNewPhoto";
    
   
    // public AudioSource audioSource;

    void Start(){      
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.J)){
            MakePic();
        }
    }

    public void MakePic()
    {
        myTexture = toTexture2D(photoTexture);

        GameObject spawnedPic = Instantiate(picture, transform.position, transform.rotation);
        spawnedPic.transform.parent = Photowall.photos.transform;
        spawnedPic.GetComponent<Rigidbody>();
        // Textur für Vorderseite erstellen
        spawnedPic.transform.GetChild(0).GetComponent<MeshRenderer>().material.SetTexture("_MainTex", myTexture);
        // Textur für Rückseite erstellen
        spawnedPic.transform.GetChild(1).GetComponent<MeshRenderer>().material.SetTexture("_MainTex", myTexture);
        
        // audioSource.Play();
    }

    Texture2D toTexture2D(RenderTexture renTex)
    {
        Texture2D tex = new Texture2D(renTex.width, renTex.height, TextureFormat.RGB24, false);

        // ReadPixels looks at the active RenderTexture.
        RenderTexture.active = renTex;
        tex.ReadPixels(new Rect(0, 0, renTex.width, renTex.height), 0, 0);
        tex.Apply();
        // if(savePhoto) SaveImage(tex);
        return tex;
    }

    // void SaveImage(Texture2D tex){
    //     var path = "Assets/RenderedTextures/" + fileName + ".png";
    //     File.WriteAllBytes(path, tex.EncodeToPNG());
    //     Debug.Log("Saved file to: " + path);
    // }
}
