using Builder;
using Core;
using InventorApi;
using KompasApi;
using Microsoft.VisualBasic.Devices;
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
			//TestInventor();
			TestKompas3D();
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
			Process currentProcess = System.Diagnostics.Process.GetCurrentProcess();
			var count = 0;
			while (true)
			{
				builder.BuildFence(fenceParameters, apiService);
				var computerInfo = new ComputerInfo();
				var usedMemory = (computerInfo.TotalPhysicalMemory - computerInfo.AvailablePhysicalMemory) *
				                 0.000000000931322574615478515625;
				streamWriter.WriteLine(
					$"{++count}\t{stopWatch.Elapsed:hh\\:mm\\:ss}\t{usedMemory}");
				streamWriter.Flush();
			}

			stopWatch.Stop();
			streamWriter.Close();
			streamWriter.Dispose();
			Console.Write($"End {new ComputerInfo().TotalPhysicalMemory}");
		}
	}
}
