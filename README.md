# unity-localized-content

- [unity-localized-content](#unity-localized-content)
  - [About this package](#about-this-package)
  - [The languages list](#the-languages-list)
  - [The localizations list](#the-localizations-list)
  - [The localization manager](#the-localization-manager)
    - [LocalizedTextUI](#localizedtextui)
    - [LocalizedText](#localizedtext)
    - [LanguagesDropdown](#languagesdropdown)
  - [Example objects](#example-objects)
  - [Prefabs](#prefabs)

## About this package

Unity package to handle localized content made by **[ArondilBe(arondilbe@gmail.com)](https://github.com/ArondilBe)**. The content of this package allows to create a dynamic languages list which then can be used to create localized entries list.

## The languages list

The first step is to define a languages list. To create a languages list object you can **right click** and then go to
`Create -> Scriptable Objects/Localized Content/Languages List`.

It will create a new scriptable object to handle your **languages list**. To define a new language you can add an element to the **languages** property.

Each language is composed of **4** properties:

- **Code**: The identifier of the language. While it's recommended to use **[iso code](https://en.wikipedia.org/wiki/List_of_ISO_639_language_codes)**, there is no restriction to what you can set as value for this property.
- **Displayed Name**: Which is the readable value used to be displayed in text.
- **Reading Direction**: Which is used to determined the reding direction of the language _(Not used at the moment)_.
- **Linked Unity Language**: Which is a reference to a **UnityEngine.SystemLanguage** value that can be linked to the language _(Not used at the moment)_.

The languages list contains a function named **GetLanguage** which takes a **language code** (of type **string**) and which returns a **language definition** if found or throws an error if not.

## The localizations list

When you have all your languages defined, you can then define your localized entries. To create a languages list object you can **right click** and then go to
`Create -> Scriptable Objects/Localized Content/Localizations List`.

The **localizations list** will first need a linked **languages list** object with at lest a valid language defined (a language which at least has is **Code** defined). Once the localizations list has this property defined, you can start to add new **localized entry**.

A **Localized Entry** is composed of a **Identifier** used to find it and of a list of **localized string**. For each of them you'll have to define the **language code** (restricted to the ones defined in the linked **languages list**) and its content.

The localizations list contains a function named **GetLocalizedEntry** which takes a **identifier** (of type **string**) and which returns a **localized entry** if found or throws an error if not.

Each localized entry contains a function named **GetLocalizedContent** which takes a **language code** (of type **string**) and which returns the **localized content** (of type **string**) if found or throws an error if not.

## The localization manager

Once you'll have your localizations list define, you can add a **localization manager** to your scene. This gameobject will persist between scenes.

The **localization manager** is composed of a **localizations list** and of a **current language**. The latest is based on the values defined in the **languages list** linked to its **localizations list**.

When **awaken** the localization manager will call it's function **LoadLocalizationContent** which will find all instance of **LocalizedTextUI** and **LocalizedText** to set the localized content.

If a **LanguagesDropdown** is defined the localization manager will load the languages **displayed name** into it. When the value of dropdown changes, the localized content is reloaded.

The localization manager is also configured to set the app's language as the default one. A fallback language can also be configured to be applied if the app's language is not defined in the languages list.

### LocalizedTextUI

The **LocalizedTextUI** component is a script which can be attached to any **UI** element to load it's localized text (working with **TextMeshProUGUI**). It's composed of a **Identifier**, used to find the right localized entry and of a **TextMeshProUGUI** which will receive the localized text.

### LocalizedText

The **LocalizedTextUI** component is a script which can be attached to any **non UI** element to load it's localized text. It's composed of a **Identifier**, used to find the right localized entry and of a **string** which will receive the localized text.

### LanguagesDropdown

The **LanguagesDropdown** component is a script which will handle the language's change. It's composed of a **TMP_Dropdown** which will contain the languages displayed named (loaded by the **localization manager**) and of a **selected language** which is the current value of the dropdown.

## Example objects

In the **ConfigObjects** of this package you'll find an example object for the **languages list** and for the **localizations list**. You can also find an examples for a **localization loading** on scene in the folder **Scenes**.

## Prefabs

You can find prefabs for all defined elements in the **Prefabs** folder.
