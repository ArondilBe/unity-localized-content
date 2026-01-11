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

        public List<LanguageDefinition> Languages
        {
            get { return languages; }
        }

        public LanguageDefinition GetLanguage(string languageCode)
        {
            LanguageDefinition language = languages.First(language =>
                language.Code == languageCode
            );
            return language
                ?? throw new Exception($"No language found for language code {languageCode}.");
        }
    }
}
