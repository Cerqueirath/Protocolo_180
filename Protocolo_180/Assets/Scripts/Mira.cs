using UnityEngine;

public class Mira : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Input.mousePosition;
    }

    void OnApplicationQuit()
    {
        Cursor.visible = true;
    }
}
