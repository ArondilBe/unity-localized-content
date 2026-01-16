using System.Collections;
using Localization;
using NUnit.Framework;
using UnityEngine;

namespace Test
{
    public class LocalizedEntryTest
    {
        private LocalizedEntry localizedEntry;

        [SetUp]
        public void SetUp()
        {
            localizedEntry = new("TestEntry");
            localizedEntry.Localizations.Add(new LocalizedString("EN", "Test Content EN"));
            localizedEntry.Localizations.Add(new LocalizedString("FR", "Test Content FR"));
        }

        [TearDown]
        public void TearDown()
        {
            localizedEntry = null;
        }

        [Test]
        public void GetLocalizedContentValueFound()
        {
            Assert.AreEqual("Test Content EN", localizedEntry.GetLocalizedContent("EN"));
        }

        [Test]
        public void GetLocalizedContentValueNotFound()
        {
            Assert.Throws<System.Exception>(() =>
            {
                localizedEntry.GetLocalizedContent("DE");
            });
        }
    }
}
