using System;

namespace BS.Utilities
{
	public class Validation
	{
		private Validation()	{	}
		
		public static bool IsFlagged(int flaggedEnum, int flaggedValue)
		{
			if ((flaggedEnum & flaggedValue) != 0)
				return true;
			else
				return false;
		}
	}
}
