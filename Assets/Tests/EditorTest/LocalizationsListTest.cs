using Localization;
using NUnit.Framework;
using UnityEngine;

namespace Test
{
    public class LocalizationsListTest
    {
        private LocalizationsList localizationsList;

        [SetUp]
        public void SetUp()
        {
            localizationsList = ScriptableObject.CreateInstance<LocalizationsList>();
            localizationsList.LocalizedEntries.Add(new LocalizedEntry("TestEntry"));
        }

        [TearDown]
        public void TearDown()
        {
            Object.DestroyImmediate(localizationsList);
        }

        [Test]
        public void GetLocalizedEntryValueFound()
        {
            Assert.AreSame(
                localizationsList.GetLocalizedEntry("TestEntry"),
                localizationsList.LocalizedEntries[0]
            );
        }

        [Test]
        public void GetLocalizedEntryValueNotFound()
        {
            Assert.Throws<System.Exception>(() =>
            {
                localizationsList.GetLocalizedEntry("NonExistentEntry");
            });
        }
    }
}
