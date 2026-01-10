using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TranslatedContent
{
    [CreateAssetMenu(
        fileName = "LanguagesList",
        menuName = "Scriptable Objects/Language/Languages List"
    )]
    public class LanguagesList : ScriptableObject
    {
        [SerializeField]
        private List<Language> languages = new();

        public List<Language> Languages
        {
            get { return languages; }
        }

        public Language GetLanguage(string languageCode)
        {
            Language language = languages.First(language => language.Code == languageCode);
            return language
                ?? throw new Exception($"No language found for language code {languageCode}.");
        }
    }
}
