﻿// OCOLOR - Get or set Cursor color in Windows Console
using System;
using System.Collections.Generic;

namespace orez.color {
	class Program {

		// data
		/// <summary>
		/// Color dictionary, which matches names with color codes.
		/// </summary>
		private static Dictionary<string, string> CodeMap = new Dictionary<string, string> {
			["black"] = "0", ["darkgray"] = "8",
			["darkblue"] = "1", ["blue"] = "9",
			["darkgreen"] = "2", ["green"] = "10",
			["darkcyan"] = "3", ["cyan"] = "11",
			["darkred"] = "4", ["red"] = "12",
			["darkmagenta"] = "5", ["magenta"] = "13",
			["darkyellow"] = "6", ["yellow"] = "14",
			["gray"] = "7", ["white"] = "15"
		};

		/// <summary>
		/// Like a sunflower...
		/// </summary>
		/// <param name="args">Input arguments.</param>
		static void Main(string[] args) {
			var clr = ColorDict();
			// process input
			var f = (args.Length >= 1 ? args[0] : "+0").ToLower();
			var b = (args.Length >= 2 ? args[1] : "+0").ToLower();
			f = f.Length > 3 ? (clr.ContainsKey(f) ? clr[f] : "7") : f;
			b = b.Length > 3 ? (clr.ContainsKey(b) ? clr[b] : "0") : b;
			var fv = (int)((f[0] == '+' || f[0] == '-' ? Console.ForegroundColor : 0) + int.Parse(f));
			var bv = (int)((b[0] == '+' || b[0] == '-' ? Console.BackgroundColor : 0) + int.Parse(b));
			// get or set color
			if(args.Length == 0) Console.WriteLine(fv + " " + bv);
			else {
				Console.ForegroundColor = (ConsoleColor)((fv < 0 ? 0 : fv) % 16);
				Console.BackgroundColor = (ConsoleColor)((bv < 0 ? 0 : bv) % 16);
			}
		}

		/// <summary>
		/// Get color code from specified value string.
		/// Value string can start with a '+' or '-' to indicate change.
		/// </summary>
		/// <param name="val">Color value string (ex- "3", "-2" or "green")</param>
		/// <param name="old">Old color code, which is used as origin for change.</param>
		/// <returns>Integer color code.</returns>
		private static int GetCode(string val, int old) {
			int code = 0;
			val = (val.Length == 0 ? "+0" : val).ToLower();
			val = CodeMap.ContainsKey(val) ? CodeMap[val] : val;
			int.TryParse(val, out code);
			if(val[0] == '+' || val[0] == '-') code += old;
			return code;
		}

		private static ConsoleColor Color(int code) {
			return (ConsoleColor)(((uint)code) & 0xF);
		}
	}
}
