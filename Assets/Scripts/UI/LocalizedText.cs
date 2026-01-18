using System;
using TMPro;
using UnityEngine;

namespace UI
{
    public class LocalizedText : MonoBehaviour
    {
        [SerializeField]
        private string identifier;

        public TextMeshProUGUI TextComponent;

        public string Identifier => identifier;

        public void SetText(string textContent)
        {
            try
            {
                TextComponent.text = textContent;
            }
            catch (Exception exception)
            {
                throw new Exception(
                    $"Failed to set text for identifier: {identifier}: {exception.Message}"
                );
            }
        }
    }
}
