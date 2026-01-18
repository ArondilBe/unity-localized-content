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
        }

        [TearDown]
        public void TearDown()
        {
            Object.DestroyImmediate(languagesList);
        }

        [Test]
        public void GetLanguageValueFound()
        {
            languagesList.Languages.Add(
                new("FR", "Français", EReadingDirection.LeftToRight, SystemLanguage.French)
            );
            Assert.AreSame(languagesList.GetLanguage("FR"), languagesList.Languages[0]);
        }

        [Test]
        public void GetLanguageValueNotFound()
        {
            languagesList.Languages.Add(
                new("FR", "Français", EReadingDirection.LeftToRight, SystemLanguage.French)
            );
            Assert.Throws<System.Exception>(() =>
            {
                languagesList.GetLanguage("EN");
            });
        }

        [Test]
        public void GetLanguageCodesLanguagesFound()
        {
            languagesList.Languages.Add(
                new("FR", "Français", EReadingDirection.LeftToRight, SystemLanguage.French)
            );
            Assert.AreEqual(new string[] { "FR" }, languagesList.GetLanguageCodes());
        }

        [Test]
        public void GetLanguageCodesNoLanguageFound()
        {
            Assert.AreEqual(new string[] { }, languagesList.GetLanguageCodes());
        }

        [Test]
        public void GetLanguageDisplayedNamesLanguagesFound()
        {
            languagesList.Languages.Add(
                new("FR", "Français", EReadingDirection.LeftToRight, SystemLanguage.French)
            );
            Assert.AreEqual(new string[] { "Français" }, languagesList.GetLanguageDisplayedNames());
        }

        [Test]
        public void GetLanguageDisplayedNamesNoLanguageFound()
        {
            Assert.AreEqual(new string[] { }, languagesList.GetLanguageDisplayedNames());
        }
    }
}
