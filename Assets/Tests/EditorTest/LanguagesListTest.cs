using System.Collections;
using Language;
using NUnit.Framework;
using UnityEngine;

namespace Test
{
    public class LanguageTest
    {
        private LanguagesList languagesList;

        [SetUp]
        public void SetUp()
        {
            languagesList = ScriptableObject.CreateInstance<LanguagesList>();
            languagesList.Languages.Add(
                new("FR", "Français", EReadingDirection.LeftToRight, SystemLanguage.French)
            );
        }

        [TearDown]
        public void TearDown()
        {
            Object.DestroyImmediate(languagesList);
        }

        [Test]
        public void GetLanguageValueFound()
        {
            Assert.AreSame(languagesList.GetLanguage("FR"), languagesList.Languages[0]);
        }

        [Test]
        public void GetLanguageValueNotFound()
        {
            Assert.Throws<System.Exception>(() =>
            {
                languagesList.GetLanguage("EN");
            });
        }
    }
}
