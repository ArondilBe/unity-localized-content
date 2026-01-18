#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using Localization;
using Language;
using System;

namespace EditorUI
{
    [CustomEditor(typeof(LocalizationManager))]
    public class LocalizationManagerEditor : Editor
    {
        private SerializedProperty localizationsListProperty;
        private SerializedProperty currentLanguageProperty;

        void OnEnable()
        {
            localizationsListProperty = serializedObject.FindProperty("localizationsList");
            currentLanguageProperty = serializedObject.FindProperty("currentLanguage");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            LocalizationsList linkedLocalizationsList = GetLinkedLocalizationsList();

            if (!linkedLocalizationsList)
            {
                DrawNoLocalizationsListWarningMessage();
                return;
            }

            if (!linkedLocalizationsList.LanguagesLists)
            {
                DrawNoLanguagesListInLocalizationsListWarningMessage();
                return;
            }

            LanguagesList languagesList = linkedLocalizationsList.LanguagesLists;

            if (languagesList.Languages.Count == 0)
            {
                DrawnNoLanguagesInLocalizationsListLanguagesListWarningMessage();
                return;
            }

            string[] languageDisplayedNames = languagesList.GetLanguageDisplayedNames();

            if (languageDisplayedNames.Length == 0)
            {
                DrawNoValidLanguageInLocalizationsListLanguagesListWarningMessage();
                return;
            }

            DisplayLanguageSelectionDropdown(languageDisplayedNames, languagesList.Languages);

            serializedObject.ApplyModifiedProperties();
        }

        LocalizationsList GetLinkedLocalizationsList()
        {
            EditorGUILayout.PropertyField(localizationsListProperty);
            return localizationsListProperty.objectReferenceValue as LocalizationsList;
        }

        void DrawNoLocalizationsListWarningMessage()
        {
            EditorGUILayout.HelpBox("Please provide a localizations list.", MessageType.Warning);
            serializedObject.ApplyModifiedProperties();
        }

        void DrawNoLanguagesListInLocalizationsListWarningMessage()
        {
            EditorGUILayout.HelpBox(
                "Please add a languages list to your localizations list.",
                MessageType.Warning
            );
            serializedObject.ApplyModifiedProperties();
        }

        void DrawnNoLanguagesInLocalizationsListLanguagesListWarningMessage()
        {
            EditorGUILayout.HelpBox(
                "Please define at least one language in your localizations list's languages list.",
                MessageType.Warning
            );
            serializedObject.ApplyModifiedProperties();
        }

        void DrawNoValidLanguageInLocalizationsListLanguagesListWarningMessage()
        {
            EditorGUILayout.HelpBox(
                "Please define at least a valid language in your localizations list's languages list.",
                MessageType.Warning
            );
            serializedObject.ApplyModifiedProperties();
        }

        void DisplayLanguageSelectionDropdown(
            string[] languageDisplayedNames,
            System.Collections.Generic.List<LanguageDefinition> languages
        )
        {
            string currentDisplayedName = "";
            SerializedProperty displayedNameProp = currentLanguageProperty.FindPropertyRelative(
                "displayedName"
            );
            if (displayedNameProp != null)
            {
                currentDisplayedName = displayedNameProp.stringValue;
            }

            int currentLanguageIndex = Mathf.Max(
                0,
                Array.IndexOf(languageDisplayedNames, currentDisplayedName)
            );

            int selectedLanguageIndex = EditorGUILayout.Popup(
                "Current Language",
                currentLanguageIndex,
                languageDisplayedNames
            );

            if (selectedLanguageIndex != currentLanguageIndex)
            {
                LanguageDefinition selectedLanguage = languages[selectedLanguageIndex];

                SerializedProperty codeProperty = currentLanguageProperty.FindPropertyRelative(
                    "code"
                );
                SerializedProperty displayedNameProperty =
                    currentLanguageProperty.FindPropertyRelative("displayedName");
                SerializedProperty readingDirectionProperty =
                    currentLanguageProperty.FindPropertyRelative("readingDirection");
                SerializedProperty linkedUnityLanguageProperty =
                    currentLanguageProperty.FindPropertyRelative("linkedUnityLanguage");

                codeProperty.stringValue = selectedLanguage.Code;
                displayedNameProperty.stringValue = selectedLanguage.DisplayedName;
                readingDirectionProperty.enumValueIndex = (int)selectedLanguage.ReadingDirection;
                linkedUnityLanguageProperty.enumValueIndex = (int)
                    selectedLanguage.LinkedUnityLanguage;
            }
        }
    }
}

#endif
