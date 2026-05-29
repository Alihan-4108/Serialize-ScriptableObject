# Serialize ScriptableObject

Working with ScriptableObjects often requires constantly opening assets just to tweak values. This tool removes that friction.

Perfect for any data-driven architecture using ScriptableObjects

---

## Video

Watch the tool in action on YouTube:

[![Watch on YouTube](https://img.shields.io/badge/YouTube-Watch%20Demo-red?style=for-the-badge&logo=youtube)](https://www.youtube.com/@mehmetalihanaydn4872)

# Installation

## Install via Unity Package Manager

Open:

`Window > Package Manager`

Click the `+` button → `Add package from git URL...`

Paste:

```txt
https://github.com/Alihan-4108/Serialize-ScriptableObject.git
```

# Usage

> **Note:** Don't forget to add `using Alihan4108.SerializeScriptableObject;` at the top of your script.

Simply add the `[SerializeSO]` attribute to your serialized ScriptableObject field.

## Basic Example

```csharp
using UnityEngine;
using Alihan4108.SerializeScriptableObject;

public class Example : MonoBehaviour
{
    [SerializeSO]
    [SerializeField] private PlayerDataExampleSO itemData;
}
```

---

# Customization

The attribute contains 3 optional parameters:

```csharp
[SerializeSO(
    label: "",
    titleAlignment: TextAnchor.MiddleLeft,
    color: "#FFFFFF"
)]
```

---

# Attribute Parameters

## `label`

Changes the displayed header text.

```csharp
label: "Weapon Data"
```

If `label` is left empty, no custom header will be displayed.

---

## `titleAlignment`

Changes the header text alignment.

> **Default:** `TextAnchor.MiddleLeft`

---

## `color`

Changes the header text color using HEX format.

Example:

```csharp
color: "#FFAA00" // Orange
```

---

# Full Example

```csharp
using UnityEngine;
using Alihan4108.SerializeScriptableObject;

public class Example : MonoBehaviour
{
    //Example 1
    [SerializeSO]
    [SerializeField] private PlayerData playerData;

    //Example 2
    [SerializeSO( label: "Enemy Settings", titleAlignment: TextAnchor.MiddleCenter, color: "#FF5555")]
    [SerializeField] private EnemyData enemyData;
}
```

---

## Supported Types

This attribute is primarily designed for `ScriptableObject` references, as its main purpose is to improve workflow in data-driven Unity architectures.

Technically, it can also be used with `Component` references without any issues, since the underlying implementation supports Unity object references in general.

However, `GameObject` and `Transform` types are intentionally excluded to avoid unnecessary or ambiguous usage scenarios.

By Design:

* **Primary use case:** `ScriptableObject`
* **Optional support:** `Component`
* **Not supported:** `GameObject`, `Transform`

---
