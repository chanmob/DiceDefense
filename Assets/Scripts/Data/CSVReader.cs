/*
  ============================================
	Author	: ChanMob
	Time 	: 2019-02-19 오후 2:51:56
  ============================================
*/

using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class CSVReader
{
	/* [PUBLIC VARIABLE]					*/


	/* [PROTECTED && PRIVATE VARIABLE]		*/

	private static string SPLIT_LINE = @"\r\n|\n\r|\n|\r";

	private static char[] TRIMS = { '\"' };

	/*----------------[PUBLIC METHOD]------------------------------*/

	public static List<Dictionary<string, object>> Read(string csv)
	{
		List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();

		string[] lines = Regex.Split(csv, SPLIT_LINE);
		if (lines.Length <= 1) return list;

		string[] headers = lines[0].Split(',');

		int linesLen = lines.Length;
		for (var i = 1; i < linesLen; i++)
		{
			string[] values = lines[i].Split(',');
			if (values.Length == 0 || values[0] == "") continue;

			Dictionary<string, object> dict = new Dictionary<string, object>();

			int headerLen = headers.Length;
			for (int j = 0; j < headerLen; j++)
			{
				string header = headers[j];
				string value = values[j];

				value = value.Replace("\\n", "\n");

				int n; float f; bool b; object obj = value;

				if (int.TryParse(value, out n))
					obj = n;
				else if (float.TryParse(value, out f))
					obj = f;
				else if (bool.TryParse(value, out b))
					obj = b;

				dict[header] = obj;
			}

			list.Add(dict);
		}

		return list;
	}

	/*----------------[PROTECTED && PRIVATE METHOD]----------------*/


}