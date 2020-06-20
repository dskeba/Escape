
using UnityEngine;
using UnityEngine.UI;

public class FPSCounter : MonoBehaviour
{
    private Text fpsCounterText;
    private void Start()
    {
        fpsCounterText = GetComponent<Text>();
    }
    void Update()
    {
        fpsCounterText.text = "" + (int)(1f / Time.unscaledDeltaTime);
    }
}
