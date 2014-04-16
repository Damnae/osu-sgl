using System;
using System.Collections.Generic;
using System.Text;

namespace SGL.Framework {

	public class Map {
		private string path;
		private Dictionary<string, Object> properties = new Dictionary<string, Object>();

		/// <summary>
		/// Path to the map .osu, relative to the song's folder.
		/// ex: 
		///		Nekomata Master - Far east nightbird (kors k Remix) (jonathanlfj) [Another].osu
		/// </summary>
		public string Path { get { return path; } }

		/// <summary>
		/// Path to the map's background, relative to the song's folder.. This parsed from the .osu and will be null if the map doesn't have a background. The path must be 
		/// ex: 
		///		bg.jpg
		/// </summary>
		public string BackgroundPath { get; set; }

		/// <summary>
		/// Creates a new Map. The path must be relative to the song's folder.
		/// ex: 
		///		Nekomata Master - Far east nightbird (kors k Remix) (jonathanlfj) [Another].osu
		/// </summary>
		public Map(String path) {
			this.path = path;
		}

		/// <summary>
		/// Used to set diff specific values that can be used to make variants of the storyboard for different diffs.
		/// To be used at the DeclareMaps step.
		/// </summary>
		public void Put(string key, Object value) {
			properties.Add(key, value);
		}

		/// <summary>
		/// Used to retrieve diff specific values that can be used to make variants of the storyboard for different diffs.
		/// To be used at the GenerateMapStoryboard step.
		/// </summary>
		public Object Get(string key) {
			return properties[key];
		}
	}
}
