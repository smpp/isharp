using System;

namespace isharp
{
	class MainClass
	{
		public static int Main (string[] args)
		{
			if (args != null && args.Length > 0)
			{
				switch (args[0].ToLower())
				{
					case "licfix":
						return LicFix.Run(args);
				}
			}
			Console.WriteLine ("I don`t know what to do?!");
			return 10022; // An invalid argument was supplied. 
		}
	}
}

