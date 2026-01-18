using System;
using System.Collections.Generic;
using System.Linq;
using Language;
using UnityEngine;

namespace Localization
{
    [CreateAssetMenu(
        fileName = "LocalizationsList",
        menuName = "Scriptable Objects/Localized Content/Localizations List"
    )]
    public class LocalizationsList : ScriptableObject
    {
        [SerializeField]
        private LanguagesList languagesList;

        [SerializeField]
        private List<LocalizedEntry> localizedEntries = new();

        public List<LocalizedEntry> LocalizedEntries => localizedEntries;

        public LanguagesList LanguagesLists => languagesList;

        public LocalizedEntry GetLocalizedEntry(string identifier)
        {
            try
            {
                return LocalizedEntries.First(localizedEntry =>
                    localizedEntry.Identifier == identifier
                );
            }
            catch
            {
                throw new Exception($"No localized entry found with identifier {identifier}.");
            }
        }
    }
}
