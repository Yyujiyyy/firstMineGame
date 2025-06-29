using UnityEngine;

public class Exit : MonoBehaviour
{
    [SerializeField] private GameObject popup;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ExitSetting()
    {
        popup.SetActive(false);
    }
}
