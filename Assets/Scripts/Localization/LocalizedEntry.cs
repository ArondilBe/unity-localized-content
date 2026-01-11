using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Localization
{
    [Serializable]
    public class LocalizedEntry
    {
        [SerializeField]
        private string identifier;

        [SerializeField]
        private List<LocalizedString> localizations = new();

        public string Identifier
        {
            get { return identifier; }
        }

        public string GetLocalizedContent(string languageCode)
        {
            LocalizedString localizedString = localizations.First(localization =>
                localization.LanguageCode == languageCode
            );
            return localizedString.Content
                ?? throw new Exception(
                    $"No localized content found for the element {identifier} for the language code {languageCode}."
                );
        }
    }
}
