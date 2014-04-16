using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace SGL.Framework {
	public abstract class BaseStoryboard {
		
		/// <summary>
		/// Absolute path to the song's folder.
		/// ex: 
		///		C:\Games\osu!\Songs\144171 Nekomata Master - Far east nightbird (kors k Remix)
		/// </summary>
		public abstract String GetSongFolder();
		
		/// <summary>
		/// Path to the osb, relative to the song's folder.
		/// ex: 
		///		Nekomata Master - Far east nightbird (kors k Remix) (jonathanlfj).osb
		/// </summary>
		public abstract String GetOsbPath();
		
		/// <summary>
		/// Whether the storyboard is widescreen or not.
		/// All declared map will have their WidescreenStoryboard property rewritten to match this.
		/// </summary>
		public abstract bool IsWidescreen();
		
		/// <summary>
		/// Add Map objects to the list to handle diff-specific storyboards.
		/// </summary>
		public abstract void DeclareMaps(List<Map> maps);
		
		/// <summary>
		/// Write the main storyboard code here (common to all diffs).
		/// </summary>
		public abstract void GenerateStoryboard();
		
		/// <summary>
		/// Write the diff specific storyboard code here.
		/// This gets called once for each declared Map.
		/// </summary>
		public abstract void GenerateMapStoryboard(Map map);

		public void Generate() {
			var folder = GetSongFolder();
			var maps = new List<Map>();

			DeclareMaps(maps);

			foreach (var map in maps) {
				ParseMapDetails(folder, map);
				GenerateMapStoryboard(map);
				WriteMapStoryboard(folder, map);
				SB.Clear();
			}

			GenerateStoryboard();
			WriteStoryboard(folder);
		}

		private static void ParseMapDetails(string folder, Map map) {
			String bgPath = null;
			using (var streamReader = new StreamReader(Path.Combine(folder, map.Path))) {
				while (!streamReader.EndOfStream) {
					var line = streamReader.ReadLine().Trim();
					var matcher = Regex.Match(line, @"^\[(\w+)\]$");

					if (matcher.Success) {
						var sectionName = matcher.Result("$1");

						if (sectionName.Equals("Events")) {
							while (!streamReader.EndOfStream) {
								var sectionLine = streamReader.ReadLine().Trim();

								if (sectionLine == "")
									break;

								if (sectionLine.StartsWith("//"))
									continue;

								var values = sectionLine.Split(',');

								if (values[0] != "0")
									continue;

								bgPath = values[2].Substring(1, values[2].Length - 2);
							}
						}
					}
				}
			}
			map.BackgroundPath = bgPath;
		}

		private void WriteMapStoryboard(string folder, Map map) {
			var code = SB.GenerateCode();
			Trace.Write(code);

			var diffContents = "";
			using (var streamReader = new StreamReader(Path.Combine(folder, map.Path))) {
				diffContents = streamReader.ReadToEnd();
			}

			var beginning = "//Storyboard Layer 0 (Background)";
			var end = "//Storyboard Sound Samples";

			var contentsBeginning = diffContents.IndexOf(beginning);
			var contentsEnd = diffContents.IndexOf(end);

			var codeBeginning = code.IndexOf(beginning);
			var codeEnd = code.IndexOf(end);

			var updatedDiffContents = diffContents.Substring(0, contentsBeginning);
			updatedDiffContents += code.Substring(codeBeginning, codeEnd - codeBeginning);
			updatedDiffContents += diffContents.Substring(contentsEnd, diffContents.Length - contentsEnd);
			if (IsWidescreen())
				updatedDiffContents = updatedDiffContents.Replace("WidescreenStoryboard: 0", "WidescreenStoryboard: 1");
			else
				updatedDiffContents = updatedDiffContents.Replace("WidescreenStoryboard: 1", "WidescreenStoryboard: 0");

			using (TextWriter tw = new StreamWriter(folder + map.Path)) {
				tw.Write(updatedDiffContents);
			}
		}

		private void WriteStoryboard(String folder) {
			using (TextWriter tw = new StreamWriter(Path.Combine(folder, GetOsbPath()))) {
				var code = SB.GenerateCode();
				Trace.Write(code);
				tw.Write(code);
			}
		}
	}
}
