using TMPro;
using UnityEngine;

public class Alive : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txtAliveUI;
    private void Awake()
    {       
        this.RegisterListener(EventID.UpdateAlive, (param) => ChangeAlive());
        this.RegisterListener(EventID.OnPlay, (param) => ChangeAlive());
    }
    public void ChangeAlive()
    {
        txtAliveUI.text = "Alive : " + LevelManager.Instance.GetCountAlive();
    }
}
