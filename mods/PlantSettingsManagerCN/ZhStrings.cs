using System;
using System.Collections.Generic;
using System.Linq;

namespace Wenkaigu.PlantSettingsManagerCN
{
	/// <summary>
	/// English → Simplified Chinese map for Plant Settings Manager UI chrome and plant labels.
	/// Exact match first; then longest-first substring replace for concatenated help text.
	/// </summary>
	internal static class ZhStrings
	{
		private static readonly Dictionary<string, string> Exact;
		private static readonly List<KeyValuePair<string, string>> Partials;

		static ZhStrings()
		{
			Exact = new Dictionary<string, string>(StringComparer.Ordinal)
			{
				// Mods list / chrome
				{ "Plant Settings Manager", "植物设置管理器" },
				{ "Config", "配置" },
				{ "SAVE & CLOSE", "保存并关闭" },
				{ "RESET ALL TO DEFAULT", "全部重置为默认" },
				{ "RESET TO DEFAULT", "重置当前植物" },
				{ "Settings saved.", "设置已保存。" },
				{ "A <b>game restart</b> is required for changes to take effect.", "需要<b>重启游戏</b>后更改才会生效。" },
				{ "RESTART", "重启" },
				{ "CONTINUE", "继续" },
				{ "CLOSE", "关闭" },

				// Sections
				{ "PLANTS", "植物" },
				{ "GROWTH", "生长" },
				{ "TEMPERATURE", "温度" },
				{ "FERTILIZER", "肥料" },
				{ "IRRIGATION", "灌溉" },
				{ "SPECIAL", "特殊" },
				{ "LIGHT", "光照" },
				{ "TREE", "树木" },
				// "MUTATIONS" = section title in Plant Settings Manager; "Spaced Out!" = official DLC name
				{ "MUTATIONS  (Spaced Out!)", "突变（《眼冒金星！》）" },
				{ "Spaced Out!", "《眼冒金星！》" },

				// Field labels
				{ "Growth time (cycles)", "生长时间（周期）" },
				{ "Yield multiplier ", "产量倍率 " },
				{ "Yield multiplier", "产量倍率" },
				{ "Min temperature (", "最低温度（" },
				{ "Max temperature (", "最高温度（" },
				{ "Requires fertilizer", "需要肥料" },
				{ "Requires irrigation", "需要灌溉" },
				{ "Requires pollination", "需要授粉" },
				{ "Amount (kg/cycle)", "用量（千克/周期）" },
				{ "Amount (multiplier)", "用量（倍率）" },
				{ "Has alternative liquid", "备选液体" },
				{ "Alternative element", "备选元素" },
				{ "Alternative amount (multiplier)", "备选用量（倍率）" },
				{ "Min light (lux)", "最低光照（勒克斯）" },
				{ "Oxygen output (multiplier)", "氧气产出（倍率）" },
				{ "Requires light", "需要光照" },
				{ "Prefers darkness", "偏好黑暗" },
				{ "Secondary grow time (multiplier)", "二次生长时间（倍率）" },
				{ "Max branch count", "最大分支数" },
				{ "Branch grow time (multiplier)", "分支生长时间（倍率）" },
				{ "Branch grow time (cycles)", "分支生长时间（周期）" },
				{ "Nectar output (kg/cycle)", "花蜜产出（千克/周期）" },
				{ "Optimal branch count", "最佳分支数" },
				{ "Branch min light (lux)", "分支最低光照（勒克斯）" },
				{ "Branch optimal light (lux)", "分支最佳光照（勒克斯）" },
				{ "items (count)", "物品（数量）" },
				{ "Grow time override (maturity multiplier)", "生长时间覆盖（成熟倍率）" },
				{ "Harvest amount override", "收获量覆盖" },
				{ "Fertilizer use override", "肥料用量覆盖" },
				{ "Temperature range override", "温度范围覆盖" },
				{ "Min light override (lux)", "最低光照覆盖（勒克斯）" },
				{ "Radiation threshold override", "辐射阈值覆盖" },

				// Help / descriptions
				{ "1.0 = vanilla speed. 2.0 = takes twice as long. 0.5 = grows twice as fast.", "1.0 = 原版速度。2.0 = 慢一倍。0.5 = 快一倍。" },
				{ "1.0 = vanilla yield. 2.0 = twice as much. 0.5 = half as much.", "1.0 = 原版产量。2.0 = 两倍。0.5 = 一半。" },
				{ "The plant stops growing if it gets colder than this.", "低于此温度时植物停止生长。" },
				{ "The plant stops growing if it gets hotter than this.", "高于此温度时植物停止生长。" },
				{ "Turn off to make the plant grow without needing any solid resource.", "关闭后植物生长不再需要固体肥料。" },
				{ "Which solid resource the plant eats to grow.", "植物生长所需的固体资源。" },
				{ "1.0 = vanilla amount. 2.0 = needs twice as much. 0.5 = needs half as much.", "1.0 = 原版用量。2.0 = 两倍。0.5 = 一半。" },
				{ "Turn off to make the plant grow without needing any liquid.", "关闭后植物生长不再需要液体灌溉。" },
				{ "Which liquid the plant drinks to grow.", "植物生长所需的液体。" },
				{ "1.0 = vanilla amount. 2.0 = drinks twice as much. 0.5 = drinks half as much.", "1.0 = 原版用量。2.0 = 两倍。0.5 = 一半。" },
				{ "This plant accepts a second liquid instead of the primary ", "该植物可使用第二种液体代替主要液体" },
				{ " it only needs one of the two, not both.", "，只需提供其中一种即可。" },
				{ "The plant will drink this liquid if the primary liquid is not available.", "主要液体不可用时，植物会使用该备选液体。" },
				{ "Turn off to make the plant grow without needing a critter to pollinate it.", "关闭后植物生长不再需要动物授粉。" },
				{ "How many lux the coral needs to produce oxygen.", "珊瑚产氧所需的最低勒克斯。" },
				{ "1.0 = vanilla O2 output. 2.0 = produces twice as much oxygen.", "1.0 = 原版氧气产出。2.0 = 两倍。" },
				{ "Turn on to make the plant need a minimum light level to grow.", "开启后植物需要最低光照才能生长。" },
				{ "How many lux the plant needs. 200 = dim light, 10000 = strong light.", "植物所需勒克斯。200 = 弱光，10000 = 强光。" },
				{ "Turn on to make the plant wilt in light instead of darkness.", "开启后植物在光照下枯萎（而非黑暗）。" },
				{ "1.0 = vanilla speed. 2.0 = secondary fruit cycle takes twice as long. 0.5 = twice as fast.", "1.0 = 原版速度。2.0 = 二次果实周期慢一倍。0.5 = 快一倍。" },
				{ "Maximum number of stalks the Tower Kelp can grow.", "塔藻可生长的最大茎数。" },
				{ "1.0 = vanilla speed. 2.0 = branches take twice as long to grow. 0.5 = twice as fast.", "1.0 = 原版速度。2.0 = 分支慢一倍。0.5 = 快一倍。" },
				{ "Maximum number of branches the tree can grow at once.", "树木可同时生长的最大分支数。" },
				{ "How many cycles it takes for each branch to grow and be ready to harvest.", "每个分支生长并可供收获所需的周期数。" },
				{ "1.0 = vanilla speed. 2.0 = branches take twice as long. 0.5 = twice as fast.", "1.0 = 原版速度。2.0 = 分支慢一倍。0.5 = 快一倍。" },
				{ "How many kg of nectar the tree produces per cycle.", "树木每周期产出的花蜜千克数。" },
				{ "How many branches are needed for the tree to produce nectar at full rate.", "树木满速产花蜜所需的分支数。" },
				{ "Minimum light the branches need to grow.", "分支生长所需的最低光照。" },
				{ "Light level at which branches grow at full speed.", "分支满速生长所需的光照。" },

				// Plant display names — aligned to Ref Docs/strings_preinstalled_zh_klei.po SPECIES/ITEM NAME
				{ "Bristle Blossom", "毛刺花" },
				{ "Dusk Cap", "夜幕菇" },
				{ "Sleet Wheat", "冰霜小麦" },
				{ "Pincha Pepperplant", "火椒藤" },
				{ "Nosh Sprout", "小吃芽" },
				{ "Grubfruit Plant", "虫果芽" },
				{ "Spindly Grubfruit Plant", "贫瘠虫果芽" },
				{ "Bog Bucket", "沼浆笼" },
				{ "Pikeapple Bush", "刺壳果灌木" },
				{ "Plume Squash", "羽叶果薯" },
				{ "Gas Grass", "释气草" },
				{ "Ovagro Node", "漫殖藤主干" },
				{ "Dew Dripper", "露珠藤" },
				{ "Saturn Critter Trap", "土星动物捕草" },
				{ "Sweatcorn Stalk", "汗甜玉米秆" },
				{ "Arbor Tree", "乔木树" },
				{ "Arbor Tree Branch", "乔木树枝杈" },
				{ "Bonbon Tree", "糖心树" },
				{ "Bonbon Tree Branch", "糖心树枝杈" },
				{ "Balm Lily", "芳香百合" },
				{ "Thimble Reed", "顶针芦苇" },
				{ "Lura Plant", "露饵花" },
				{ "Buried Muckroot", "掩埋的淤泥根" },
				{ "Sherberry Plant", "雪莓藤" },
				{ "Swamp Chard", "沼泽甜菜" },
				{ "Dasha Saltvine", "沙盐藤" },
				{ "Alveo Vera", "气囊芦荟" },
				{ "Idylla Flower", "恬静花" },
				{ "Mimika Bud", "拟芽" },
				{ "Mirth Leaf", "欢乐叶" },
				{ "Jumping Joya", "雀跃掌" },
				{ "Bluff Briar", "诱人荆棘" },
				{ "Buddy Bud", "同伴芽" },
				{ "Bliss Burst", "极乐刺" },
				{ "Ring Rosebush", "环玫花" },
				{ "Tranquil Toes", "安宁芷" },
				{ "Mellow Mallow", "锦醇菇" },
				{ "Flue Coral", "烟囱珊瑚" },
				{ "Gum Palm", "粘胶棕榈" },
				{ "Petta Pouf", "蓬茸柳" },
				{ "Husha Cups", "静杯花" },
				{ "Mussel Sprout", "贻贝芽" },
				{ "Tower Kelp", "塔藻" },
				{ "Tower Kelp Stalk", "塔藻" },
				{ "Ovagro Vine", "漫殖藤" },
				{ "Mealwood", "米虱木" },
				{ "Waterweed", "水草" },
				{ "Wheezewort", "冰息萝卜" },
				{ "Sporechid", "孢子兰" },
				{ "Oxyfern", "氧齿蕨" },
				{ "Hexalent", "六角根" },
			};

			// Longer keys first for substring replacement of concatenated strings.
			Partials = Exact
				.Where(kv => kv.Key.Length >= 12)
				.OrderByDescending(kv => kv.Key.Length)
				.ToList();
		}

		public static string Translate(string text)
		{
			if (string.IsNullOrEmpty(text))
				return text;

			if (Exact.TryGetValue(text, out var exact))
				return exact;

			var result = text;
			foreach (var pair in Partials)
			{
				if (result.IndexOf(pair.Key, StringComparison.Ordinal) >= 0)
					result = result.Replace(pair.Key, pair.Value);
			}

			return result;
		}
	}
}
