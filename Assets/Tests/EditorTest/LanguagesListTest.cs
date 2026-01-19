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
        public void GetLanguageBasedOnLanguageCodeValueFound()
        {
            languagesList.Languages.Add(
                new("FR", "Français", EReadingDirection.LeftToRight, SystemLanguage.French)
            );
            Assert.AreSame(
                languagesList.GetLanguageBasedOnLanguageCode("FR"),
                languagesList.Languages[0]
            );
        }

        [Test]
        public void GetLanguageBasedOnLanguageCodeValueNotFound()
        {
            languagesList.Languages.Add(
                new("FR", "Français", EReadingDirection.LeftToRight, SystemLanguage.French)
            );
            Assert.Throws<System.Exception>(() =>
            {
                languagesList.GetLanguageBasedOnLanguageCode("EN");
            });
        }

        [Test]
        public void GetLanguageBasedOnDisplayedNameValueFound()
        {
            languagesList.Languages.Add(
                new("FR", "Français", EReadingDirection.LeftToRight, SystemLanguage.French)
            );
            Assert.AreSame(
                languagesList.GetLanguageBasedOnDisplayedName("Français"),
                languagesList.Languages[0]
            );
        }

        [Test]
        public void GetLanguageBasedOnDisplayedNameValueNotFound()
        {
            languagesList.Languages.Add(
                new("FR", "Français", EReadingDirection.LeftToRight, SystemLanguage.French)
            );
            Assert.Throws<System.Exception>(() =>
            {
                languagesList.GetLanguageBasedOnDisplayedName("English");
            });
        }

        [Test]
        public void GetLanguageBasedOnLinkedUnityLanguageValueFound()
        {
            languagesList.Languages.Add(
                new("FR", "Français", EReadingDirection.LeftToRight, SystemLanguage.French)
            );
            Assert.AreSame(
                languagesList.GetLanguageBasedOnLinkedUnityLanguage(SystemLanguage.French),
                languagesList.Languages[0]
            );
        }

        [Test]
        public void GetLanguageBasedOnLinkedUnityLanguageValueNotFound()
        {
            languagesList.Languages.Add(
                new("FR", "Français", EReadingDirection.LeftToRight, SystemLanguage.French)
            );
            Assert.Throws<System.Exception>(() =>
            {
                languagesList.GetLanguageBasedOnDisplayedName("English");
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
