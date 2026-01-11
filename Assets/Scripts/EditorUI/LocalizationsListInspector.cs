#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System.Linq;
using Localization;
using Language;
using System.Collections.Generic;

namespace EditorUI
{
    [CustomEditor(typeof(LocalizationsList))]
    public class LocalizationsListInspector : Editor
    {
        private SerializedProperty languagesListProperty;
        private SerializedProperty localizedEntriesProperty;

        void OnEnable()
        {
            languagesListProperty = serializedObject.FindProperty("languagesList");
            localizedEntriesProperty = serializedObject.FindProperty("localizedEntries");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            LanguagesList languagesList = GetLinkedLanguagesList();

            if (
                languagesList == null
                || languagesList.Languages == null
                || languagesList.Languages.Count() == 0
            )
            {
                DisplayEmptyLanguagesListPropertyWarningMessage(languagesList);

                serializedObject.ApplyModifiedProperties();
                return;
            }

            string[] languageCodes = GetLanguageCodes(languagesList.Languages);

            if (languageCodes.Length == 0)
            {
                EditorGUILayout.HelpBox(
                    "No valid language found in the languages list.",
                    MessageType.Warning
                );
                serializedObject.ApplyModifiedProperties();
                return;
            }

            DisplayLocalizationEntryList(languageCodes);
            serializedObject.ApplyModifiedProperties();
        }

        public LanguagesList GetLinkedLanguagesList()
        {
            EditorGUILayout.PropertyField(languagesListProperty);
            return languagesListProperty.objectReferenceValue as LanguagesList;
        }

        public void DisplayEmptyLanguagesListPropertyWarningMessage(LanguagesList languagesList)
        {
            if (languagesList == null)
            {
                EditorGUILayout.HelpBox(
                    "Assign a languages list to select languages.",
                    MessageType.Warning
                );
                return;
            }

            EditorGUILayout.HelpBox(
                "Define languages in your languages list.",
                MessageType.Warning
            );
        }

        public string[] GetLanguageCodes(List<LanguageDefinition> languages)
        {
            return languages
                    .Where(language => !string.IsNullOrEmpty(language.Code))
                    .Select(language => language.Code)
                    .Distinct()
                    .ToArray()
                ?? new string[0];
        }

        void DisplayLocalizationEntryList(string[] languageCodes)
        {
            EditorGUILayout.Space(10);
            EditorGUILayout.LabelField("Localization Entries", EditorStyles.boldLabel);
            for (
                int localizedEntryIndex = 0;
                localizedEntryIndex < localizedEntriesProperty.arraySize;
                localizedEntryIndex++
            )
            {
                SerializedProperty localizedEntryProperty =
                    localizedEntriesProperty.GetArrayElementAtIndex(localizedEntryIndex);
                SerializedProperty identifierProperty = localizedEntryProperty.FindPropertyRelative(
                    "identifier"
                );
                SerializedProperty localizationsProperty =
                    localizedEntryProperty.FindPropertyRelative("localizations");

                EditorGUILayout.BeginVertical("box");

                EditorGUILayout.PropertyField(identifierProperty);
                EditorGUILayout.LabelField("Localizations", EditorStyles.miniBoldLabel);

                for (
                    int localizationIndex = 0;
                    localizationIndex < localizationsProperty.arraySize;
                    localizationIndex++
                )
                {
                    DisplayLocalization(languageCodes, localizationsProperty, localizationIndex);
                }
                DisplayLocalizationButtons(localizationsProperty);

                EditorGUILayout.EndVertical();
            }

            DisplayLocalizedEntryButtons();
        }

        void DisplayLocalization(
            string[] languageCodes,
            SerializedProperty localizationsProperty,
            int localizationIndex
        )
        {
            SerializedProperty localizationProperty = localizationsProperty.GetArrayElementAtIndex(
                localizationIndex
            );

            SerializedProperty languageCodeProperty = localizationProperty.FindPropertyRelative(
                "languageCode"
            );
            SerializedProperty contentProperty = localizationProperty.FindPropertyRelative(
                "content"
            );

            EditorGUILayout.BeginVertical("helpbox");

            int currentLanguageCodeIndex = Mathf.Max(
                0,
                System.Array.IndexOf(languageCodes, languageCodeProperty.stringValue)
            );

            int newLanguageCodeIndex = EditorGUILayout.Popup(
                "Language",
                currentLanguageCodeIndex,
                languageCodes
            );

            languageCodeProperty.stringValue = languageCodes[newLanguageCodeIndex];
            EditorGUILayout.PropertyField(contentProperty);

            EditorGUILayout.EndVertical();
        }

        void DisplayLocalizationButtons(SerializedProperty localizationsProperty)
        {
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Add Localization"))
            {
                localizationsProperty.InsertArrayElementAtIndex(localizationsProperty.arraySize);
            }
            if (GUILayout.Button("Remove Localization") && localizationsProperty.arraySize > 0)
            {
                localizationsProperty.DeleteArrayElementAtIndex(
                    localizationsProperty.arraySize - 1
                );
            }
            EditorGUILayout.EndHorizontal();
        }

        void DisplayLocalizedEntryButtons()
        {
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Add Localized Entry"))
            {
                localizedEntriesProperty.InsertArrayElementAtIndex(
                    localizedEntriesProperty.arraySize
                );
            }

            if (
                GUILayout.Button("Remove Localized Entry")
                && localizedEntriesProperty.arraySize > 0
            )
            {
                localizedEntriesProperty.DeleteArrayElementAtIndex(
                    localizedEntriesProperty.arraySize - 1
                );
            }

            EditorGUILayout.EndHorizontal();
        }
    }
}

#endif
