using System;
using System.Collections.Generic;
using System.Linq;

namespace Wenkaigu.PlantCritterSettingsCN
{
	/// <summary>
	/// English → Simplified Chinese map for Plant Settings Manager and Critter Settings
	/// Manager UI chrome plus plant/critter display names.
	/// Exact match first; then longest-first substring replace for concatenated help text.
	/// Plant names and base species names follow official 官中 from
	/// strings_preinstalled_zh_klei.po (STRINGS.CREATURES.SPECIES.*.NAME / STRINGS.ITEMS.*).
	/// </summary>
	internal static class ZhStrings
	{
		private static readonly Dictionary<string, string> Exact;
		private static readonly List<KeyValuePair<string, string>> Partials;

		static ZhStrings()
		{
			Exact = new Dictionary<string, string>(StringComparer.Ordinal)
			{
				// Mods list / chrome (shared with Plant Settings Manager)
				{ "Plant Settings Manager", "植物设置管理器" },
				{ "PLANT SETTINGS MANAGER", "植物设置管理器" },
				{ "Critter Settings Manager", "动物设置管理器" },
				{ "CRITTER SETTINGS MANAGER", "动物设置管理器" },
				{ "Config", "配置" },
				{ "SAVE & CLOSE", "保存并关闭" },
				{ "RESET ALL TO DEFAULT", "全部重置为默认" },
				{ "RESET TO DEFAULT", "重置为默认" },
				{ "Settings saved.", "设置已保存。" },
				{ "Settings saved.\n\nA game restart is required for changes to take effect.", "设置已保存。\n\n需要重启游戏后更改才会生效。" },
				{ "A <b>game restart</b> is required for changes to take effect.", "需要<b>重启游戏</b>后更改才会生效。" },
				{ "RESTART", "重启" },
				{ "CONTINUE", "继续" },
				{ "CLOSE", "关闭" },

				// Sections (Plant)
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

				// Sections / chrome (Critter) — confident labels; Task 5 will verify and extend.
				{ "CRITTERS", "动物" },
				{ "SELECT A CRITTER", "选择一只动物" },
				{ "SELECT A PLANT", "选择一株植物" },

				// Plant field labels
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

				// Critter field labels — confident subset; Task 5 will verify against DLL.
				{ "Lifespan (cycles)", "寿命（周期）" },
				{ "Fertility (cycles)", "繁育周期（周期）" },
				{ "Egg chance (%)", "产蛋概率（%）" },
				{ "Incubation (cycles)", "孵化（周期）" },
				{ "Calories per cycle (kcal)", "每周期热量（千卡）" },
				{ "Diet (kg/cycle)", "食量（千克/周期）" },
			{ "Space requirement", "空间需求" },
			{ "Population multiplier", "种群倍率" },

			// Critter section headers (decompiled from CritterSettings.SettingsScreen.BuildFields)
			{ "LIFECYCLE", "生命周期" },
			{ "HUNGER", "饥饿" },
			{ "DIET CONVERSION", "饮食转化" },
			{ "PRODUCTION", "产出" },
			{ "MILKING", "挤奶" },

			// Critter field labels (BuildFields)
			{ "Egg lay time — groomed (cycles)", "产蛋时间 — 已照料（周期）" },
			{ "Incubation time (cycles)", "孵化时间（周期）" },
			{ "Space before crowded (tiles)", "拥挤前空间（格）" },
			{ "Space before cramped (tiles)", "狭窄前空间（格）" },
			{ "Comfortable min (°C)", "舒适最低（°C）" },
			{ "Comfortable max (°C)", "舒适最高（°C）" },
			{ "Lethal min (°C)", "致死最低（°C）" },
			{ "Lethal max (°C)", "致死最高（°C）" },
			{ "Food calories multiplier", "食物热量倍率" },
			{ "Output amount multiplier", "产出数量倍率" },
			{ "Food consumption multiplier", "食物消耗倍率" },
			{ "Scale regrowth time", "鳞片再生时间" },
			{ "Shell regrowth time", "甲壳再生时间" },
			{ "Feather regrowth time", "羽毛再生时间" },
			{ "Antler regrowth time", "鹿角再生时间" },
			{ "Reed fiber output", "芦苇纤维产出" },
			{ "Plastic output", "塑料产出" },
			{ "Regal Bammoth Crest output", "皇犸兔冠产出" },
			{ "Wood output", "木材产出" },
			{ "Glass output", "玻璃产出" },
			{ "Feather output", "羽毛产出" },
			{ "Iron Ore output", "铁矿产出" },
			{ "Output per cycle", "每周期产出" },
			{ "Ovolene (FishMilk) output", "鱼奶烯产出" },
			{ "Squid Ink output", "鱿鱼墨产出" },
			{ "Brackene output", "卤水烯产出" },
			{ "Biodiesel output", "生物柴油产出" },
			{ "Milking output", "挤奶产出" },
			{ "Requires Hydrogen atmosphere", "需要氢气环境" },

			// Critter confirm dialog buttons (Save flow)
			{ "SAVE ANYWAY", "仍然保存" },
			{ "GO BACK", "返回" },

			// Critter help-text descriptions (stable prefixes before "Vanilla: …").
			// These are concatenated with "  |  default: <val>" at runtime, so they
			// only ever match via the Partials substring pass — hence length >= 12.
			{ "Absolute value in cycles. How long before the critter dies of old age. Enter the exact number of cycles you want — not a multiplier. ", "绝对值（周期）。动物老死前的存活时长。输入你想要的精确周期数 — 不是倍率。" },
			{ "Absolute value in cycles (groomed rate). Enter the exact number of cycles you want between each egg when the critter is groomed — not a multiplier. Wild rate is always 10× this automatically. ", "绝对值（周期，已照料速率）。输入已照料时每次产蛋之间你想要的精确周期数 — 不是倍率。野生速率自动为该值的10倍。" },
			{ "Absolute value in cycles. How long until the egg hatches into a baby. Enter the exact number of cycles you want — not a multiplier. Note: this patches the egg prefab, so changes take effect for newly laid eggs only (existing eggs in your base keep their original rate). ", "绝对值（周期）。蛋孵化为幼体所需的时间。输入你想要的精确周期数 — 不是倍率。注意：此修改作用于蛋的预制件，因此仅对新产的蛋生效（基地中已有的蛋保留原始速率）。" },
			{ "Absolute value in tiles. How much space each critter needs before the Crowded debuff triggers. Lower = can pack them tighter. Enter the exact tile count you want, not a multiplier. ", "绝对值（格）。每只动物在触发“拥挤”减益前所需的空间。越低 = 可以养得更密集。输入你想要的精确格数，不是倍率。" },
			{ "Separate from Crowded above — doesn't need to match it. Think of it as: tiles ONE critter or ONE egg takes up, for this check only. Cramped triggers when (critters + eggs) × this number exceeds your room's tile size. Example: set to 1, a 96-tile room tolerates 8 critters + 88 eggs fine, but 8 critters + 89 eggs (97 total) triggers Cramped. Lower = more tolerance before Cramped. Set to 0 to fall back to vanilla: eggs then cost the SAME space as adults (the Crowded value), so if your critters already fill the room, even ONE egg triggers Cramped right away. Use a low number like 1 here to give eggs their own breathing room instead.", "与上方的“拥挤”相互独立 — 无需保持一致。可理解为：仅在此项检查中，一只动物或一枚蛋所占的格数。当（动物 + 蛋）× 此数值超过你房间的总格数时触发“狭窄”。例如：设为1，一个96格的房间可以容纳8只动物 + 88枚蛋，但8只动物 + 89枚蛋（共97）会触发“狭窄”。越低 = 对“狭窄”越宽容。设为0则回退到原版：蛋占用与成年动物相同的空间（即“拥挤”值），因此如果动物已占满房间，哪怕一枚蛋也会立即触发“狭窄”。建议设为1之类的低值，让蛋有自己的空间。" },
			{ "Absolute value in kcal/cycle. How fast the critter gets hungry — lower means stays full longer. Enter the exact amount you want, not a multiplier. ", "绝对值（千卡/周期）。动物饥饿的速度 — 越低 = 保持饱腹越久。输入你想要的精确数值，不是倍率。" },
			{ "Critter is stressed below this temperature. ", "低于此温度时动物会受压。" },
			{ "Critter is stressed above this temperature. ", "高于此温度时动物会受压。" },
			{ "Critter dies if temperature drops below this. ", "低于此温度时动物会死亡。" },
			{ "Critter dies if temperature rises above this. ", "高于此温度时动物会死亡。" },
			{ "MULTIPLIER. Scales calories gained per kg of food eaten. Higher = critter gets more calories per bite (needs less food). 1.0 = vanilla.", "倍率。缩放每千克食物获得的热量。越高 = 每口获得更多热量（需要更少食物）。1.0 = 原版。" },
			{ "MULTIPLIER. Scales how much resource is produced per kg of food eaten. 2.0 = twice as much coal/oil/etc per bite. Can offset a MODERATE reduction in \"Food consumption multiplier\" below (e.g. consumption 0.5 + this at 2.0 ≈ same output, half the food) — but cannot compensate for consumption near 0, since output depends on food mass actually eaten. 1.0 = vanilla.", "倍率。缩放每千克食物吃下后产出的资源量。2.0 = 每口产出两倍的煤/油等。可抵消下方“食物消耗倍率”的中等降低（例如消耗0.5 + 此项2.0 ≈ 产出不变、食物减半）— 但无法抵消接近0的消耗，因为产出取决于实际吃下的食物质量。1.0 = 原版。" },
			{ "MULTIPLIER. Scales how many kg are eaten per meal. Higher = eats more per bite (finishes food faster). Output also scales with food mass eaten, not just calories gained, so lowering this also lowers output — they move together. Moderate values work well together: e.g. consumption 0.5 + Output amount multiplier 2.0 roughly preserves output per cycle while halving food eaten. 0.0 = eats almost nothing AND produces almost nothing — there is no output multiplier high enough to fully compensate for near-zero consumption, since output is mass × rate and mass is near zero. For low food cost with preserved output, use a moderate reduction (0.3–0.7) rather than 0.0. 1.0 = vanilla.", "倍率。缩放每餐吃下的千克数。越高 = 每口吃得越多（更快吃完食物）。产出也与实际吃下的食物质量成正比，而非仅与热量相关，因此降低此项也会降低产出 — 两者联动。中等数值搭配效果较好：例如消耗0.5 + 产出倍率2.0 可大致保持每周期产出不变同时食物减半。0.0 = 几乎不吃也几乎不产 — 没有任何产出倍率能完全抵消接近零的消耗，因为产出 = 质量 × 速率，而质量接近零。要以较低食物消耗保持产出，请使用中等降低（0.3–0.7）而非0.0。1.0 = 原版。" },
			{ "MULTIPLIER on vanilla regrowth time — not an absolute value in cycles. 1.0 = vanilla speed. 0.5 = half as long (twice as fast). 2.0 = twice as long. Example: Drecko vanilla is 8 cycles. Setting 0.5 gives 4 cycles, setting 0.01 gives 0.08 cycles (48 seconds) — NOT 0.01 cycles.", "倍率（基于原版再生时间）— 不是绝对周期值。1.0 = 原版速度。0.5 = 一半时间（快一倍）。2.0 = 两倍时间。例如：壁虎原版为8周期。设为0.5得到4周期，设为0.01得到0.08周期（48秒）— 不是0.01周期。" },
			{ "MULTIPLIER on vanilla output amount. 1.0 = vanilla. 2.0 = twice as much per shear. 0.5 = half as much.", "倍率（基于原版产出量）。1.0 = 原版。2.0 = 每次剪切两倍。0.5 = 一半。" },
			{ "When unchecked, scales grow in any atmosphere.", "未勾选时，鳞片在任何环境中都会生长。" },
			{ "Multiplies the amount produced when milked at the Aquatic Milking Station. 2.0 = twice as much output per milking. 1.0 = vanilla", "缩放在水生挤奶站挤奶时的产出量。2.0 = 每次挤奶两倍产出。1.0 = 原版" },
			{ ". NOTE: Gassy Moo and Husky Moo share the same underlying milking field — for predictable results, set the SAME value here for both critters. Different values will compound.", "。注意：释气海牛和魁梧海牛共享同一个底层挤奶字段 — 为获得可预测结果，请为两种动物设置相同的值。不同值会产生叠加效应。" },
			{ "WARNING: Gassy Moo and Husky Moo have different Milking Output multiplier values.\n\nThese two critters share the same underlying game field. When both are set, the patches compound — the second critter processed multiplies the value already changed by the first, producing unpredictable results.\n\nSet both to the SAME value for predictable output.\n\nSave anyway?", "警告：释气海牛与魁梧海牛的挤奶产出倍率不同。\n\n这两种动物共享同一个底层游戏字段。两者都被设置时，修改会叠加 — 后处理的动物会乘上前者已修改的值，产生不可预测的结果。\n\n请将两者设为相同的值以获得可预测产出。\n\n仍然保存？" },

				// Help / descriptions (Plant)
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

				// Plant display names — aligned to strings_preinstalled_zh_klei.po SPECIES/ITEM NAME
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
				{ "Snactus", "菌斑根" },
				{ "Megafrond", "巨蕨" },
				{ "Seakomb", "海梳蕨" },
				{ "Starnacle", "藤壶星" },
				{ "Clampum", "巨钳蚌" },
				{ "Tublia", "管虫" },
				{ "Pinpoket", "针胆团" },
				{ "Sodicane", "咸蔗" },
				{ "Bulbloom", "灯泡花" },

				// Critter display names — official 官中 from STRINGS.CREATURES.SPECIES.*.NAME
				// (base species + variants). Base IDs (Bee, Moo, …) map to the species' displayed
				// Chinese name so any internal-style DisplayName is still translated correctly.
				{ "Hatch", "好吃哈奇" },
				{ "Sage Hatch", "草质哈奇" },
				{ "Stone Hatch", "石壳哈奇" },
				{ "Smooth Hatch", "光滑哈奇" },
				{ "Puft", "喷浮飞鱼" },
				{ "Dense Puft", "厚壳飞鱼" },
				{ "Squeaky Puft", "洁净飞鱼" },
				{ "Puft Prince", "贵族飞鱼" },
				{ "Shine Bug", "发光虫" },
				{ "Sun Bug", "阳光虫" },
				{ "Royal Bug", "皇家虫" },
				{ "Azure Bug", "天蓝虫" },
				{ "Abyss Bug", "深渊虫" },
				{ "Coral Bug", "珊瑚虫" },
				{ "Radiant Bug", "光耀虫" },
				{ "Slickster", "浮油生物" },
				{ "Molten Slickster", "熔岩浮油生物" },
				{ "Longhair Slickster", "长毛浮油生物" },
				{ "Drecko", "毛鳞壁虎" },
				{ "Glossy Drecko", "滑鳞壁虎" },
				{ "Pip", "树鼠" },
				{ "Cuddle Pip", "毛绒树鼠" },
				{ "Shove Vole", "锹环田鼠" },
				{ "Delecta Vole", "珍馐田鼠" },
				{ "Gassy Moo", "释气海牛" },
				{ "Husky Moo", "魁梧海牛" },
				{ "Pokeshell", "抛壳蟹" },
				{ "Oakshell", "木壳蟹" },
				{ "Sanishell", "沙泥蟹" },
				{ "Pacu", "帕库鱼" },
				{ "Tropical Pacu", "热带帕库鱼" },
				{ "Gulp Fish", "大嘴鱼" },
				{ "Sweetle", "甜素甲虫" },
				{ "Grubgrub", "虫果果虫" },
				{ "Plug Slug", "电弧蛞蝓" },
				{ "Smog Slug", "烟雾蛞蝓" },
				{ "Sponge Slug", "海绵蛞蝓" },
				{ "Beeta", "辐射蜂" },
				{ "Blowter", "鼓气鱼" },
				{ "Bammoth", "绒犸兔" },
				{ "Regal Bammoth", "皇犸兔" },
				{ "Flox", "狐鹿" },
				{ "Shatter Flox", "碎晶狐鹿" },
				{ "Dartle", "逸蜥" },
				{ "Morb", "疫病章鱼" },
				{ "Beakon", "灯喙鱼" },
				{ "Gildgo", "金螺" },
				{ "Gnit", "蚋虱" },
				{ "Jawbo", "颚鱼" },
				{ "Kelpole", "藻蝌蚪" },
				{ "Seaquine", "海马" },
				{ "Spigot Seal", "栓角海豹" },
				{ "Orehull", "矿甲龟" },
				{ "Lumb", "尖块兽" },
				{ "Blum Lumb", "叶块兽" },
				{ "Mimika", "拟蛾" },
				{ "Rhex", "霸王鹦" },
				{ "Slogo", "缓螺" },
				{ "Glo Squid", "彩斑鱿" },

				// Base species IDs that may appear as internal-style DisplayNames; map to the
				// species' official 官中 displayed name.
				{ "Bee", "辐射蜂" },
				{ "Moo", "释气海牛" },
				{ "Mosquito", "蚋虱" },
				{ "Oilfloater", "浮油生物" },
				{ "Stego", "尖块兽" },
				{ "Staterpillar", "电弧蛞蝓" },
				{ "Seal", "栓角海豹" },
				{ "Snail", "缓螺" },
				{ "Glom", "疫病章鱼" },
				{ "Mole", "锹环田鼠" },
				{ "Raptor", "霸王鹦" },
				{ "Squirrel", "树鼠" },
				{ "Chameleon", "逸蜥" },
				{ "Butterfly", "拟蛾" },
				{ "Crab", "抛壳蟹" },
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
