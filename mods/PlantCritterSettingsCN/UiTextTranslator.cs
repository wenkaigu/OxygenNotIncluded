using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Wenkaigu.PlantCritterSettingsCN
{
	/// <summary>
	/// Walks a UI Transform subtree and replaces English text on LocText, TMP_Text and
	/// UnityEngine.UI.Text components with the Simplified Chinese mapping from ZhStrings.
	/// Inactive children are included so pre-hidden panels are translated before reveal.
	/// </summary>
	internal static class UiTextTranslator
	{
		public static void TranslateHierarchy(Transform root)
		{
			if (root == null) return;

			foreach (var loc in root.GetComponentsInChildren<LocText>(true))
			{
				if (loc == null) continue;
				var tmp = (TMP_Text)loc;
				var next = ZhStrings.Translate(tmp.text);
				if (!string.Equals(next, tmp.text, System.StringComparison.Ordinal))
					tmp.text = next;
			}

			foreach (var tmp in root.GetComponentsInChildren<TMP_Text>(true))
			{
				if (tmp == null || tmp is LocText) continue;
				var next = ZhStrings.Translate(tmp.text);
				if (!string.Equals(next, tmp.text, System.StringComparison.Ordinal))
					tmp.text = next;
			}

			foreach (var ui in root.GetComponentsInChildren<Text>(true))
			{
				if (ui == null) continue;
				var next = ZhStrings.Translate(ui.text);
				if (!string.Equals(next, ui.text, System.StringComparison.Ordinal))
					ui.text = next;
			}
		}
	}
}
