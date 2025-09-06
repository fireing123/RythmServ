using UnityEngine;

public class SkillCoolTimeViewer : MonoBehaviour
{
    public GameObject shockWave;
    public GameObject dash;
    public GameObject barrier;
    public GameObject missile;

    public void Show(bool shock, bool dashe, bool barrie, bool missil)
    {
        shockWave.SetActive(shock);
        dash.SetActive(dashe);
        barrier.SetActive(barrie);
        missile.SetActive(missil);
    }
}
