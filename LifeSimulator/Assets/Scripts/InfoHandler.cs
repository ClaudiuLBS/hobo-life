using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class InfoHandler : MonoBehaviour
{
    // Start is called before the first frame update
    private static TextMeshProUGUI infoText;
    public static InfoHandler instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    void Start()
    {
        infoText = GetComponent<TextMeshProUGUI>();
        infoText.text = "";
        infoText.faceColor = new Color32(250, 215, 74, 0);
    }

    IEnumerator FadeOut()
    {
        for (int alpha = 255; alpha >= 0; alpha--)
        {
            infoText.faceColor = new Color32(250, 215, 74, (byte)alpha);
            yield return new WaitForSeconds(0.005f);
        }
    }

    public void setInfo(string message) {
        infoText.text = message;
        StopAllCoroutines();
        StartCoroutine(FadeOut());
    }
}
