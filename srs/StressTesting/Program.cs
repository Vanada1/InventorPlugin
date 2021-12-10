using Builder;
using Core;
using InventorApi;
using KompasApi;
using Services;
using System;
using System.Diagnostics;
using System.IO;

namespace StressTesting
{
	class Program
	{
		static void Main(string[] args)
		{
			TestInventor();
			//TestKompas3D();
		}

		private static void TestInventor()
		{
			TestApi(new InventorWrapper());
		}

		private static void TestKompas3D()
		{
			TestApi(new KompasWrapper());
		}

		private static void TestApi(IApiService apiService)
		{
			var builder = new FenceBuilder();
			var stopWatch = new Stopwatch();
			stopWatch.Start();
			var fenceParameters = new FenceParameters();
			var streamWriter = new StreamWriter($"log{apiService}.txt", true);
			var count = 0;
			var stopCount = 40;
			while (count < stopCount)
			{
				builder.BuildFence(fenceParameters, apiService);
				streamWriter.WriteLine($"{++count} -- {stopWatch.Elapsed:hh\\:mm\\:ss}");
				streamWriter.Flush();
			}

			stopWatch.Stop();
			streamWriter.Close();
			streamWriter.Dispose();
			Console.Write($"End {stopWatch.Elapsed}");
		}
	}
}
