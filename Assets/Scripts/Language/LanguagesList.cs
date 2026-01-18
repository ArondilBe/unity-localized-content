using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Language
{
    [CreateAssetMenu(
        fileName = "LanguagesList",
        menuName = "Scriptable Objects/Localized Content/Languages List"
    )]
    public class LanguagesList : ScriptableObject
    {
        [SerializeField]
        private List<LanguageDefinition> languages = new();

        public List<LanguageDefinition> Languages => languages;

        public string[] GetLanguageCodes()
        {
            return languages
                    .Where(language => !string.IsNullOrEmpty(language.Code))
                    .Select(language => language.Code)
                    .Distinct()
                    .ToArray()
                ?? new string[0];
        }

        public string[] GetLanguageDisplayedNames()
        {
            return languages
                    .Where(language => !string.IsNullOrEmpty(language.DisplayedName))
                    .Select(language => language.DisplayedName)
                    .Distinct()
                    .ToArray()
                ?? new string[0];
        }

        public LanguageDefinition GetLanguage(string languageCode)
        {
            try
            {
                return languages.First(language => language.Code == languageCode);
            }
            catch
            {
                throw new Exception($"No language found for language code {languageCode}.");
            }
        }
    }
}
