#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using Localization;
using Language;

namespace EditorUI
{
    [CustomEditor(typeof(LocalizationsList))]
    public class LocalizationsListEditor : Editor
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
            LanguagesList linkedLanguagesList = GetLinkedLanguagesList();

            if (!linkedLanguagesList)
            {
                DrawNoLanguagesListWarningMessage();
                return;
            }

            if (linkedLanguagesList.Languages.Count == 0)
            {
                DrawEmptyLanguagesListWarningMessage();
                return;
            }

            string[] languageCodes = linkedLanguagesList.GetLanguageCodes();

            if (languageCodes.Length == 0)
            {
                DrawNoValidLanguageWarningMessage();
                return;
            }

            DrawLocalizedEntriesList(languageCodes);

            serializedObject.ApplyModifiedProperties();
        }

        LanguagesList GetLinkedLanguagesList()
        {
            EditorGUILayout.PropertyField(languagesListProperty);
            return languagesListProperty.objectReferenceValue as LanguagesList;
        }

        void DrawNoLanguagesListWarningMessage()
        {
            EditorGUILayout.HelpBox("Please provide a languages list.", MessageType.Warning);
            serializedObject.ApplyModifiedProperties();
        }

        void DrawEmptyLanguagesListWarningMessage()
        {
            EditorGUILayout.HelpBox(
                "Please define some languages in your languages list.",
                MessageType.Warning
            );
            serializedObject.ApplyModifiedProperties();
        }

        void DrawNoValidLanguageWarningMessage()
        {
            EditorGUILayout.HelpBox(
                "Please define at least a valid language.",
                MessageType.Warning
            );
            serializedObject.ApplyModifiedProperties();
        }

        void DrawLocalizedEntriesList(string[] languageCodes)
        {
            EditorGUILayout.Space(10);
            EditorGUILayout.LabelField("Localized Entries", EditorStyles.boldLabel);
            EditorGUILayout.BeginVertical("box");
            for (
                int localizedEntryIndex = 0;
                localizedEntryIndex < localizedEntriesProperty.arraySize;
                localizedEntryIndex++
            )
            {
                DrawLocalizedEntry(localizedEntryIndex, languageCodes);
            }
            DrawAddLocalizedEntryButton();
            EditorGUILayout.EndVertical();
        }

        void DrawLocalizedEntry(int localizedEntryIndex, string[] languageCodes)
        {
            SerializedProperty localizedEntryProperty =
                localizedEntriesProperty.GetArrayElementAtIndex(localizedEntryIndex);
            SerializedProperty identifierProperty = localizedEntryProperty.FindPropertyRelative(
                "identifier"
            );
            SerializedProperty localizationsProperty = localizedEntryProperty.FindPropertyRelative(
                "localizations"
            );

            EditorGUILayout.BeginVertical("box");

            EditorGUILayout.BeginHorizontal();
            GUIContent deleteIcon = EditorGUIUtility.IconContent("TreeEditor.Trash");

            if (GUILayout.Button(deleteIcon, GUIStyle.none))
            {
                localizedEntriesProperty.DeleteArrayElementAtIndex(localizedEntryIndex);
                return;
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.PropertyField(identifierProperty);
            EditorGUILayout.LabelField("Localizations", EditorStyles.miniBoldLabel);
            for (
                int localizationIndex = 0;
                localizationIndex < localizationsProperty.arraySize;
                localizationIndex++
            )
            {
                DrawLocalization(languageCodes, localizationsProperty, localizationIndex);
            }
            DrawAddLocalizationButton(localizationsProperty);
            EditorGUILayout.EndVertical();
        }

        void DrawAddLocalizedEntryButton()
        {
            if (GUILayout.Button("Add new localized entry"))
            {
                localizedEntriesProperty.InsertArrayElementAtIndex(
                    localizedEntriesProperty.arraySize
                );
            }
        }

        void DrawLocalization(
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

            EditorGUILayout.BeginHorizontal();
            GUIContent deleteIcon = EditorGUIUtility.IconContent("TreeEditor.Trash");

            if (GUILayout.Button(deleteIcon, GUIStyle.none))
            {
                localizationsProperty.DeleteArrayElementAtIndex(localizationIndex);
                return;
            }
            EditorGUILayout.EndHorizontal();

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

        void DrawAddLocalizationButton(SerializedProperty localizationsProperty)
        {
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Add Localization"))
            {
                localizationsProperty.InsertArrayElementAtIndex(localizationsProperty.arraySize);
            }
            EditorGUILayout.EndHorizontal();
        }
    }
}

#endif
