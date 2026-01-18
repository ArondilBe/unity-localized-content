using NUnit.Framework;
using UI;
using UnityEngine;

namespace Test
{
    public class LocalizedTextUITest
    {
        private LocalizedTextUI localizedTextUI;

        [SetUp]
        public void SetUp()
        {
            GameObject gameObject = new GameObject("localized");
            localizedTextUI = gameObject.AddComponent<LocalizedTextUI>();
        }

        [Test]
        public void SetTextNoTextComponent()
        {
            Assert.Throws<System.Exception>(() =>
            {
                localizedTextUI.SetText("Text");
            });
        }

        [Test]
        public void SetTextWithTextComponent()
        {
            localizedTextUI.TextComponent = new GameObject(
                "text"
            ).AddComponent<TMPro.TextMeshProUGUI>();
            Assert.DoesNotThrow(() =>
            {
                localizedTextUI.SetText("Text");
            });
        }

        [TearDown]
        public void TearDown()
        {
            if (localizedTextUI != null)
            {
                Object.DestroyImmediate(localizedTextUI.gameObject);
                localizedTextUI = null;
            }
        }
    }
}
