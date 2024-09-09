using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class START : MonoBehaviour
{
    
  
    // Start is called before the first frame update
    void Start()
    {
    Scene scene = SceneManager.GetActiveScene();
    }

    // Update is called once per frame
   public void OnClick()
    {
        SceneManager.LoadScene(2);
}
}