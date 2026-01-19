#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using Localization;
using Language;
using System;
using UI;

namespace EditorUI
{
    [CustomEditor(typeof(LocalizationManager))]
    public class LocalizationManagerEditor : Editor
    {
        private SerializedProperty localizationsListProperty;
        private SerializedProperty languagesDropdownProperty;
        private SerializedProperty fallbackLanguageProperty;

        void OnEnable()
        {
            localizationsListProperty = serializedObject.FindProperty("localizationsList");
            languagesDropdownProperty = serializedObject.FindProperty("languagesDropdown");
            fallbackLanguageProperty = serializedObject.FindProperty("fallbackLanguage");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            LocalizationsList linkedLocalizationsList = GetLinkedLocalizationsList();
            GetLinkedLanguagesDropdown();

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

        LanguagesDropdown GetLinkedLanguagesDropdown()
        {
            EditorGUILayout.PropertyField(languagesDropdownProperty);
            return languagesDropdownProperty.objectReferenceValue as LanguagesDropdown;
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
            string fallbackDisplayedName = "";
            SerializedProperty displayedNameProp = fallbackLanguageProperty.FindPropertyRelative(
                "displayedName"
            );
            if (displayedNameProp != null)
            {
                fallbackDisplayedName = displayedNameProp.stringValue;
            }

            int fallbackLanguageIndex = Mathf.Max(
                0,
                Array.IndexOf(languageDisplayedNames, fallbackDisplayedName)
            );

            int selectedLanguageIndex = EditorGUILayout.Popup(
                "Fallback Language",
                fallbackLanguageIndex,
                languageDisplayedNames
            );

            if (selectedLanguageIndex != fallbackLanguageIndex)
            {
                LanguageDefinition selectedLanguage = languages[selectedLanguageIndex];

                SerializedProperty codeProperty = fallbackLanguageProperty.FindPropertyRelative(
                    "code"
                );
                SerializedProperty displayedNameProperty =
                    fallbackLanguageProperty.FindPropertyRelative("displayedName");
                SerializedProperty readingDirectionProperty =
                    fallbackLanguageProperty.FindPropertyRelative("readingDirection");
                SerializedProperty linkedUnityLanguageProperty =
                    fallbackLanguageProperty.FindPropertyRelative("linkedUnityLanguage");

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
