using System;
using TMPro;
using UnityEngine;

namespace UI
{
    public class LanguagesDropdown : MonoBehaviour
    {
        [SerializeField]
        private TMP_Dropdown languageDropdown;

        private string selectedLanguage;

        public string SelectedLanguage => selectedLanguage;

        public void SetLanguageOptions(string[] languages)
        {
            languageDropdown.ClearOptions();
            languageDropdown.AddOptions(new System.Collections.Generic.List<string>(languages));
            selectedLanguage = languages[0];
        }

        public void OnLanguageChanged()
        {
            selectedLanguage = languageDropdown.options[languageDropdown.value].text;
        }

        public void SetSelectedLanguage(string language)
        {
            int index = languageDropdown.options.FindIndex(option => option.text == language);
            if (index > -1)
            {
                languageDropdown.value = index;
                selectedLanguage = language;
            }
            else
            {
                throw new Exception($"Language {language} not found in dropdown options.");
            }
        }
    }
}
