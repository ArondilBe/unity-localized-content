using System.Collections;
using NUnit.Framework;
using UI;
using UnityEngine;
using UnityEngine.TestTools;

namespace Test
{
    public class LocalizedTextTest
    {
        private LocalizedText localizedText;

        [SetUp]
        public void SetUp()
        {
            GameObject gameObject = new GameObject("localized");
            localizedText = gameObject.AddComponent<LocalizedText>();
        }

        [Test]
        public void SetTextNoTextComponent()
        {
            Assert.Throws<System.Exception>(() =>
            {
                localizedText.SetText("Text");
            });
        }

        [Test]
        public void SetTextWithTextComponent()
        {
            localizedText.TextComponent = new GameObject(
                "text"
            ).AddComponent<TMPro.TextMeshProUGUI>();
            Assert.DoesNotThrow(() =>
            {
                localizedText.SetText("Text");
            });
        }

        [TearDown]
        public void TearDown()
        {
            if (localizedText != null)
            {
                Object.DestroyImmediate(localizedText.gameObject);
                localizedText = null;
            }
        }
    }
}
