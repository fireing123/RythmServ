using TMPro;
using UnityEngine;

public class ScoreText : MonoBehaviour
{
    TMP_Text text;

    void Start()
    {
        text = GetComponent<TMP_Text>();
    }

    void Update()
    {
        text.text = PointCountor.GetPoint().ToString();
    }
}
