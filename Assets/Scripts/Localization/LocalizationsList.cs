using System.Collections.Generic;
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

        public List<LocalizedEntry> LocalizedEntries
        {
            get { return localizedEntries; }
        }

        public LanguagesList LanguagesLists
        {
            get { return languagesList; }
        }
    }
}
