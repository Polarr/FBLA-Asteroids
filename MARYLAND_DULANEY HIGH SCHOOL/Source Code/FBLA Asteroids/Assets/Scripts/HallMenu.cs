using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class HallMenu : MonoBehaviour
{
    string URL = "https://cascadefalls.000webhostapp.com/getScores.php"; //get score link
    public GameObject name; //the name inputfield

    IEnumerator Start()
    {
        WWWForm form = new WWWForm(); //format form for database

        using (UnityWebRequest www = UnityWebRequest.Post(URL, form))
        {
            yield return www.SendWebRequest(); //wait for it to finish

            if (www.isNetworkError || www.isHttpError) //make sure there's no errors
            {
                Debug.Log(www.error); //log the error if there is one
            }
            else
            {
                string[] ret = www.downloadHandler.text.Split('|'); //we're gonna use '|' to split all of the different names
                foreach (string n in ret.Where(x => x != "" && x != " "))
                {
                    var i = Instantiate(name); //instantiate it
                    i.transform.SetParent(transform, false); //because we're going to set the parent of the object, we don't want the position to scale with the parent
                    i.transform.GetChild(0).GetComponent<Text>().text = "Name: " + n; //set the text = name
                }
            }
        }
    }
}
