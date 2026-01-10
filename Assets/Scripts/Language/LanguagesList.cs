using System;
using System.Collections.Generic;
using UnityEngine;

namespace Language
{
    [CreateAssetMenu(
        fileName = "LanguagesList",
        menuName = "Scriptable Objects/Language/Languages List"
    )]
    public class LanguagesList : ScriptableObject
    {
        [SerializeField]
        private List<Language> languages;

        public List<Language> Languages
        {
            get { return languages; }
        }
    }

    [Serializable]
    public struct Language
    {
        public string Code;
        public string DisplayedName;
    }
}
