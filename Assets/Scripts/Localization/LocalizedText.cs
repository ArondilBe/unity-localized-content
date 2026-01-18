using UnityEngine;

public class LocalizedText : MonoBehaviour
{
    [SerializeField]
    private string identifier;

    [SerializeField]
    private string text;

    public string Identifier => identifier;

    public string Text => text;

    public void SetText(string textContent)
    {
        text = textContent;
    }
}
