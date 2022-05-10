using System;

namespace _2ndSemesterFinalExamen
{
	public static class Program
	{
		[STAThread]
		static void Main()
		{
			using (Game1 game = Game1.Instance)
				game.Run();
		}
	}
}
