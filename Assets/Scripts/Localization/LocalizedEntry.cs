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

        public List<LocalizedString> Localizations
        {
            get { return localizations; }
        }

        public LocalizedEntry(string identifier, List<LocalizedString> localizations)
        {
            this.identifier = identifier;
            this.localizations = localizations;
        }

        public LocalizedEntry(string identifier)
        {
            this.identifier = identifier;
            this.localizations = new();
        }

        public string GetLocalizedContent(string languageCode)
        {
            try
            {
                LocalizedString localizedString = localizations.First(localization =>
                    localization.LanguageCode == languageCode
                );
                return localizedString.Content;
            }
            catch
            {
                throw new Exception(
                    $"No localized content found for the element {identifier} for the language code {languageCode}."
                );
            }
        }
    }
}
