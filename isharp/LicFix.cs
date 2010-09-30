using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace isharp
{
	public class LicFix
	{
		List<FileInfo> files;
		string licencetxt = "Test Licence bla bla\r\nbla bla bla...";
		
		string header;
		string[] matchHeaders;
		
		private LicFix (string path, string header, string[] matchHeaders)
		{
			this.files = new List<FileInfo>();
			scanDir(new DirectoryInfo(path), this.files);
			
			this.header = header;
			this.lcheader = header.ToLower();
			
			var lcmatchheaders = new List<string>();
			lcmatchheaders.Add(lcheader);
			foreach (var mh in matchHeaders)
			{
				var lcmh = mh.ToLower();
				if (!lcmatchheaders.Contains(lcmh))
				{
					lcmatchheaders.Add(lcmh);
				}
			}
			this.matchHeaders = lcmatchheaders.ToArray();
		}
		
		static void scanDir(DirectoryInfo dirinfo, List<FileInfo> files)
		{
			if (dirinfo.Name.StartsWith("."))
			{
				// TODO: ignore hidden folders
				return;
			}
			
			foreach (var fileinfo in dirinfo.GetFiles())
			{
				if (fileinfo.Name.StartsWith("."))
				{
					// TODO: ignore hidden files
					continue;
				}
				if (!string.IsNullOrEmpty(fileinfo.Extension))
				{
					switch (fileinfo.Extension.ToLower())
					{
						case "cs":
							files.Add(fileinfo);
							continue;
					}
				}
			}
			
			foreach (var subFolder in dirinfo.GetDirectories())
			{
				scanDir(subFolder, files);
			}
		}
		
		public static int Run(string[] args)
		{
			var path = AppDomain.CurrentDomain.BaseDirectory;
			var header = "LGPL License";
			var matchHeaders = new string[]{"LGPL License"};
			var fixxer = new LicFix(path, header, matchHeaders);
			fixxer.Run();
			return 0;
		}
		
		void Run()
		{
			Console.WriteLine("=== LICENCE ===");
			Console.WriteLine("===============");
			Console.WriteLine("Found {0} files to update.", files.Count);
			Console.Write("Are you sure? (y/n");
			char c;
			try { c = Convert.ToChar(Console.Read()); }
			catch { c = 'n'; }
			
			if (c == 'y')
			{
				foreach (var file in this.files)
				{
					switch (file.Extension.ToLower())
					{
						case "cs":
							fixfile_cs(file);
							continue;
					}
				}
			}
		}
		
		static string cs_readToNextLine(StreamReader reader, string[] matchRegions)
		{
			string line = null;
			while ((line = reader.ReadLine()) == string.Empty 
			       || (line = line.Trim()).Length == 0
			       || line.StartsWith("//")
			       || line.StartsWith("/*")
			       || line.EndsWith("*/")
			       || (line.StartsWith("#") && !line.StartsWith("#region")));
				
			
			return line;
		}
		
		void fixfile_cs(FileInfo file)
		{
			using (var stream = File.Open(file.FullName, FileMode.Open, FileAccess.ReadWrite, FileShare.Read))
			{
				var reader = new StreamReader(stream);
				string line = cs_readToNextLine(reader);
				
				if (string.IsNullOrEmpty(line))
				{
					cs_writeData(stream, 0);
				}
				else if (line.StartsWith(REGIONSTR))
				{
					const string REGIONSTR = "#region ";
	
					var region = line.Substring(REGIONSTR.Length).Trim().ToLower();
					if (Array.IndexOf(matchRegions, region) != -1)
					{
						
					}
				}
				
				if (line.StartsWith("#region");
				{
					
				}
				else
				{
					
				var writer = new StreamWriter(stream);
			}
		}
		
		void cs_writeData(Stream stream, int position)
		{
			
		}
			
	}
}

