#region Disclaimer / License
// Copyright (C) 2008, Kenneth Skovhede
// http://www.hexad.dk, opensource@hexad.dk
// 
// This library is free software; you can redistribute it and/or
// modify it under the terms of the GNU Lesser General Public
// License as published by the Free Software Foundation; either
// version 2.1 of the License, or (at your option) any later version.
// 
// This library is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
// Lesser General Public License for more details.
// 
// You should have received a copy of the GNU Lesser General Public
// License along with this library; if not, write to the Free Software
// Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301  USA
// 
#endregion
using System;
using System.Collections.Generic;
using System.Text;

namespace OSGeo.MapGuide.Maestro.ResourceEditors
{
    /// <summary>
    /// Represents a ColorBrewer color set
    /// </summary>
    public class ColorBrewer
    {
        private string m_name;
        private string m_type;
        private List<System.Drawing.Color> m_colors;

        /// <summary>
        /// Gets the name of the ColorBrewer set
        /// </summary>
        public string Name { get { return m_name; } }
        /// <summary>
        /// Gets the assigned type for the ColorBrewer set
        /// </summary>
        public string Type { get { return m_type; } }
        /// <summary>
        /// Gets the ordered list of colors to use
        /// </summary>
        public List<System.Drawing.Color> Colors { get { return m_colors; } }

        /// <summary>
        /// Constructs a new ColorBrewer instance
        /// </summary>
        /// <param name="name">The name of the set</param>
        /// <param name="type">The set type</param>
        /// <param name="colors">The colors in the set</param>
        public ColorBrewer(string name, string type, List<System.Drawing.Color> colors)
        {
            m_name = name;
            m_type = type;
            m_colors = colors;
        }

        /// <summary>
        /// Parses a CSV file for ColorBrewer setup, uses double quotes for text delimiter, and comma for record delimiter
        /// </summary>
        /// <param name="filename">The name of the file to parse</param>
        /// <returns>A list of parsed ColorBrewer sets</returns>
        public static List<ColorBrewer> ParseCSV(string filename)
        {
            return ParseCSV(filename, '"', ',');
        }

        /// <summary>
        /// Parses a CSV file for ColorBrewer setup
        /// </summary>
        /// <param name="filename">The name of the file to parse</param>
        /// <param name="recordDelimiter">The character used to delimit the records</param>
        /// <param name="textDelimiter">The character used to enclose strings</param>
        /// <returns>A list of parsed ColorBrewer sets</returns>
        public static List<ColorBrewer> ParseCSV(string filename, char textDelimiter, char recordDelimiter)
        {
            using (System.IO.StreamReader sr = new System.IO.StreamReader(filename, System.Text.Encoding.UTF8, true))
            {
                List<ColorBrewer> result = new List<ColorBrewer>();
                Dictionary<string, int> columns = new Dictionary<string, int>();
                List<string> colnames = TokenizeLine(sr.ReadLine(), recordDelimiter, textDelimiter);

                for (int i = 0; i < colnames.Count; i++)
                    colnames[i] = colnames[i].ToLower();

                columns.Add("ColorName", colnames.IndexOf("colorname"));
                columns.Add("NumOfColors", colnames.IndexOf("numofcolors"));
                columns.Add("Type", colnames.IndexOf("type"));
                columns.Add("CritVal", colnames.IndexOf("critval"));
                columns.Add("ColorNum", colnames.IndexOf("colornum"));
                columns.Add("ColorLetter", colnames.IndexOf("colorletter"));
                columns.Add("R", colnames.IndexOf("r"));
                columns.Add("G", colnames.IndexOf("g"));
                columns.Add("B", colnames.IndexOf("b"));
                columns.Add("SchemeType", colnames.IndexOf("schemetype"));

                if (columns["ColorName"] < 0)
                    throw new Exception("Missing column \"ColorName\"");
                if (columns["Type"] < 0)
                    throw new Exception("Missing column \"Type\"");
                if (columns["NumOfColors"] < 0)
                    throw new Exception("Missing column \"NumOfColors\"");
                if (columns["R"] < 0)
                    throw new Exception("Missing column \"R\"");
                if (columns["G"] < 0)
                    throw new Exception("Missing column \"G\"");
                if (columns["B"] < 0)
                    throw new Exception("Missing column \"B\"");

                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    List<string> values = TokenizeLine(line, recordDelimiter, textDelimiter);
                    
                    if (values.Count != columns.Count)
                        throw new Exception(string.Format("Invalid field count in line: ", line));

                    string colorName = values[columns["ColorName"]];
                    string type = values[columns["Type"]];
                    
                    if (string.IsNullOrEmpty(colorName) || string.IsNullOrEmpty(type))
                        continue; //Assume comment

                    int colorCount;
                    if (!int.TryParse(values[columns["NumOfColors"]], out colorCount))
                        continue; //Assume comment

                    List<System.Drawing.Color> colors = new List<System.Drawing.Color>();

                    while (!sr.EndOfStream && colorCount > 0)
                    {
                        if (!string.IsNullOrEmpty(line))
                        {
                            values = TokenizeLine(line, recordDelimiter, textDelimiter);
                            if (values.Count != colnames.Count)
                                throw new Exception(string.Format("Invalid record count in line: {0}", line));
                            
                            byte r, g, b;
                            if (!byte.TryParse(values[columns["R"]], out r))
                                throw new Exception(string.Format("Invalid R color component {0} in line {1}", values[columns["R"]], line));
                            if (!byte.TryParse(values[columns["G"]], out g))
                                throw new Exception(string.Format("Invalid G color component {0} in line {1}", values[columns["G"]], line));
                            if (!byte.TryParse(values[columns["B"]], out b))
                                throw new Exception(string.Format("Invalid B color component {0} in line {1}", values[columns["B"]], line));

                            colors.Add(System.Drawing.Color.FromArgb(r, g, b));
                            colorCount--;
                        }

                        if (colorCount > 0)
                            line = sr.ReadLine();
                    }

                    if (colorCount != 0)
                        throw new Exception(string.Format("Failed to read {0} color(s) for line: {1}", colorCount, line));

                    result.Add(new ColorBrewer(colorName, type, colors));
                }

                return result;
            }
        }

        /// <summary>
        /// Splits a line into record fields, and removes text delimiters
        /// </summary>
        /// <param name="line">The line to tokenize</param>
        /// <param name="recordDelimiter">The character used to delimit the records</param>
        /// <param name="textDelimiter">The character used to enclose strings</param>
        /// <returns>A list of records</returns>
        private static List<string> TokenizeLine(string line, char recordDelimiter, char textDelimiter)
        {
            if (string.IsNullOrEmpty(line))
                return new List<string>();

            bool inQuotes = false;
            int startIndex = 0;
            List<string> records = new List<string>();

            for (int i = 0; i < line.Length; i++)
                if (line[i] == textDelimiter)
                    inQuotes = !inQuotes;
                else if (!inQuotes && line[i] == recordDelimiter)
                {
                    string rec = line.Substring(startIndex, i - startIndex);
                    if (rec.StartsWith(textDelimiter.ToString()) && rec.EndsWith(textDelimiter.ToString()))
                        rec = rec.Substring(1, rec.Length - 2);
                    records.Add(rec);
                    startIndex = i+1;
                }

            if (startIndex <= line.Length)
            {
                string rec = line.Substring(startIndex);
                if (rec.StartsWith(textDelimiter.ToString()) && rec.EndsWith(textDelimiter.ToString()))
                    rec = rec.Substring(1, rec.Length - 2);

                records.Add(rec);
            }
            
            return records;
        }

        /// <summary>
        /// Returns the objects string representation
        /// </summary>
        /// <returns>The string the represents the object</returns>
        public override string ToString()
        {
            string type = "";
            switch (m_type)
            {
                case "qual":
                    type = "Qualitative";
                    break;
                case "seq":
                    type = "Sequential";
                    break;
                case "div":
                    type = "Diverging";
                    break;
            }

            return m_name + " - " + type;
        }
    }
}
