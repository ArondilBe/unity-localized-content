using System;
using Language;
using UI;
using UnityEngine;

namespace Localization
{
    public class LocalizationManager : MonoBehaviour
    {
        private static LocalizationManager instance;

        [SerializeField]
        private LocalizationsList localizationsList;

        [SerializeField]
        private LanguageDefinition currentLanguage;

        public static LocalizationManager Instance => instance;

        [SerializeField]
        private LanguagesDropdown languagesDropdown;

        [SerializeField]
        private LanguageDefinition fallbackLanguage;

        void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else if (instance != this)
            {
                Destroy(gameObject);
            }

            if (languagesDropdown)
            {
                LoadLanguagesOptionsInDropdown();
            }

            try
            {
                currentLanguage =
                    localizationsList.LanguagesLists.GetLanguageBasedOnLinkedUnityLanguage(
                        Application.systemLanguage
                    );
            }
            catch
            {
                Debug.LogWarning(
                    $"Default language for system language {Application.systemLanguage} not found in languages list. Falling back to {fallbackLanguage.DisplayedName}."
                );
                currentLanguage = fallbackLanguage;
                if (languagesDropdown)
                {
                    languagesDropdown.SetSelectedLanguage(currentLanguage.DisplayedName);
                }
            }

            LoadLocalizationContent();
        }

        void Update()
        {
            if (
                languagesDropdown
                && currentLanguage.DisplayedName != languagesDropdown.SelectedLanguage
            )
            {
                SetCurrentLanguageBasedOnDisplayedName(languagesDropdown.SelectedLanguage);
                LoadLocalizationContent();
            }
        }

        private void LoadLocalizationContent()
        {
            LocalizedTextUI[] localizedTextUIs = FindObjectsByType<LocalizedTextUI>(
                FindObjectsSortMode.None
            );

            LocalizedText[] localizedTexts = FindObjectsByType<LocalizedText>(
                FindObjectsSortMode.None
            );

            foreach (LocalizedTextUI localizedTextUI in localizedTextUIs)
            {
                try
                {
                    localizedTextUI.SetText(
                        localizationsList
                            .GetLocalizedEntry(localizedTextUI.Identifier)
                            .GetLocalizedContent(currentLanguage.Code)
                    );
                }
                catch (Exception exception)
                {
                    Debug.LogWarning(
                        $"Error loading localization for text ui element: {exception.Message}"
                    );
                }
            }

            foreach (LocalizedText localizedText in localizedTexts)
            {
                try
                {
                    localizedText.SetText(
                        localizationsList
                            .GetLocalizedEntry(localizedText.Identifier)
                            .GetLocalizedContent(currentLanguage.Code)
                    );
                }
                catch (Exception exception)
                {
                    Debug.LogWarning(
                        $"Error loading localization for text element: {exception.Message}"
                    );
                }
            }
        }

        public void LoadLanguagesOptionsInDropdown()
        {
            languagesDropdown.SetLanguageOptions(
                localizationsList.LanguagesLists.GetLanguageDisplayedNames()
            );
        }

        public void SetCurrentLanguageBasedOnDisplayedName(string displayedName)
        {
            LanguageDefinition languageDefinition =
                localizationsList.LanguagesLists.GetLanguageBasedOnDisplayedName(displayedName);
            currentLanguage = languageDefinition;
            LoadLocalizationContent();
        }
    }
}
