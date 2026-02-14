using UnityEngine;

public class PopupManager : MonoBehaviour
{
    [SerializeField] private PopupBoxVisuals popup;

    private PopupData currentData;

    public void Show(PopupData data)
    {
        currentData = data;

        popup.Bind(data);
        popup.OnButtonClicked += HandleButtonClicked;
        
        popup.Show();
    }

    private void HandleButtonClicked(int index)
    {
        currentData.buttons[index].Callback?.Invoke();
        Hide();
    }

    public void Hide()
    {
        popup.OnButtonClicked -= HandleButtonClicked;
        popup.Hide();
    }
}
