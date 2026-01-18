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

        public string Code => code;

        public string DisplayedName => displayedName;

        public EReadingDirection ReadingDirection => readingDirection;

        public SystemLanguage LinkedUnityLanguage => linkedUnityLanguage;

        public LanguageDefinition(
            string code,
            string displayedName,
            EReadingDirection readingDirection,
            SystemLanguage linkedUnityLanguage
        )
        {
            this.code = code;
            this.displayedName = displayedName;
            this.readingDirection = readingDirection;
            this.linkedUnityLanguage = linkedUnityLanguage;
        }
    }

    public enum EReadingDirection
    {
        LeftToRight,
        RightToLeft,
    }
}
