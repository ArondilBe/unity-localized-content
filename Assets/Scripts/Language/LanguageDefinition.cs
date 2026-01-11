using System;
using UnityEngine;

namespace Language
{
    [Serializable]
    public class LanguageDefinition
    {
        [SerializeField]
        private string code;

        [SerializeField]
        private string displayedName;

        [SerializeField]
        private EReadingDirection readingDirection;

        [SerializeField]
        private SystemLanguage linkedUnityLanguage;

        public string Code
        {
            get { return code; }
        }

        public string DisplayedName
        {
            get { return displayedName; }
        }

        public EReadingDirection ReadingDirection
        {
            get { return readingDirection; }
        }

        public SystemLanguage LinkedUnityLanguage
        {
            get { return linkedUnityLanguage; }
        }
    }

    public enum EReadingDirection
    {
        LeftToRight,
        RightToLeft,
    }
}
