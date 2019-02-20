using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CompleteScreen : MonoBehaviour
{
    string URL = "https://cascadefalls.000webhostapp.com/submitScore.php"; //registering + all the inputs
    public InputField name;

    public void SubmitForm()
    {
        if (name.text.Length > 0)
            StartCoroutine(Submit());
    }

    public IEnumerator Submit()
    {
        WWWForm form = new WWWForm(); //format form for database
        form.AddField("namePost", name.text);

        using (UnityWebRequest www = UnityWebRequest.Post(URL, form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }

            SceneManager.LoadScene(0);
        }
    }
}
