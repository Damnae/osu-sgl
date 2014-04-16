using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SGL.Framework;

namespace SampleStoryboard {
	public class Storyboard : BaseStoryboard {

		public override String GetSongFolder() {
			return @"C:\Games\osu!\Songs\testsong\";
		}

		public override String GetOsbPath() {
			return "testsong (Damnae).osb";
		}

		public override bool IsWidescreen() {
			return true;
		}

		public override void DeclareMaps(List<Map> maps) {
			var map1 = new Map("testsong (Damnae) [testdiff1].osu");
			map1.Put("someProperty", 1);
			maps.Add(map1);

			var map2 = new Map("testsong (Damnae) [testdiff2].osu");
			map2.Put("someProperty", 0);
			maps.Add(map2);
		}

		public override void GenerateMapStoryboard(Map map) {
			var someProperty = (Int32)map.Get("someProperty");

			if (someProperty == 1) {
				var sprite = SB.Sprite("dot2.png", SB.Foreground, SB.TopCentre);
				sprite.move(0, 10000, 320, 240, 340, 360);
			}
		}

		public override void GenerateStoryboard() {
			var sprite = SB.Sprite("dot2.png", SB.Foreground, SB.TopCentre);
			sprite.fade(0, 10000, 1, 0);
		}
	}
}
