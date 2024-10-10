using UnityEngine;

public class HUDElement : MonoBehaviour
{
    public Camera mainCamera;
    public Vector3 offset;
    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = GameObject.FindGameObjectsWithTag("MainCamera")[0].GetComponent<Camera>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Aggiorna la posizione del GameObject in modo che segua la telecamera con l'offset specificato
        transform.position = mainCamera.transform.position;

        // Allinea la rotazione del GameObject con quella della telecamera
        transform.rotation = mainCamera.transform.rotation;
    }
}
