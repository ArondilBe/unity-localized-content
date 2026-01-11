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

        public string LanguageCode
        {
            get { return languageCode; }
        }

        public string Content
        {
            get { return content; }
        }
    }
}
