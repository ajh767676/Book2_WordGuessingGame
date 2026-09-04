using UnityEngine;
using TMPro;

public class DisplayLastWordGuessed : MonoBehaviour
{
    void Start()
    {
        GetComponent<TextMeshProUGUI>().text =
            PlayerPrefs.GetString("lastWordGuessed");
    }
}