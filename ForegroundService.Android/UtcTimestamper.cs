using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace ForegroundService.Droid
{
	public class UtcTimestamper
	{
		DateTime startTime;
		bool wasReset = false;

		public UtcTimestamper()
		{
			startTime = DateTime.UtcNow;
		}

		public string GetFormattedTimestamp()
		{
			TimeSpan duration = DateTime.UtcNow.Subtract(startTime);

			return wasReset ? $"Service restarted at {startTime} ({duration:c} ago)." : $"Service started at {startTime} ({duration:c} ago).";
		}

		public void Restart()
		{
			startTime = DateTime.UtcNow;
			wasReset = true;
		}
	}
}