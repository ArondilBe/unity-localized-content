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

            LoadLocalizationContent();
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
    }
}
