using UnityEngine;

public class Exitgame : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Application.Quit();
        }
    }
}
