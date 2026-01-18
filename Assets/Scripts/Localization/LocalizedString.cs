using System;
using UnityEngine;

namespace Localization
{
    [Serializable]
    public class LocalizedString
    {
        [SerializeField]
        private string languageCode;

        [SerializeField]
        [TextArea]
        private string content;

        public string LanguageCode => languageCode;

        public string Content => content;

        public LocalizedString(string languageCode, string content)
        {
            this.languageCode = languageCode;
            this.content = content;
        }
    }
}
