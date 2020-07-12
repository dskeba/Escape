
using UnityEngine;
using UnityEngine.UI;

public class FPSCounter : MonoBehaviour
{
    private Text _fpsCounterText;

    private void Start()
    {
        _fpsCounterText = GetComponent<Text>();
    }

    private void Update()
    {
        _fpsCounterText.text = "" + (int)(1f / Time.unscaledDeltaTime);
    }
}
