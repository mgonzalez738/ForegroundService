using System;
using Android.Content;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.OS;
using Java.Lang;
using ForegroundService.Droid;
using Xamarin.Forms;

namespace ForegroundService.Droid
{
    [Activity(Label = "ForegroundService", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        static readonly string TAG = typeof(MainActivity).FullName;

        Intent startServiceIntent;
        Intent stopServiceIntent;
        bool isStarted = false;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());


            OnNewIntent(this.Intent);

            if (savedInstanceState != null)
            {
                isStarted = savedInstanceState.GetBoolean(Constants.SERVICE_STARTED_KEY, false);
            }

            startServiceIntent = new Intent(this, typeof(TimestampService));
            startServiceIntent.SetAction(Constants.ACTION_START_SERVICE);

            stopServiceIntent = new Intent(this, typeof(TimestampService));
            stopServiceIntent.SetAction(Constants.ACTION_STOP_SERVICE);

            if (isStarted)
            {
                StopServiceExecute();
            }
            else
            {
                MainPage.NotificationEvent += NotificationServiceExecution;

            }
        }

        protected override void OnNewIntent(Intent intent)
        {
            if (intent == null)
            {
                return;
            }

            var bundle = intent.Extras;
            if (bundle != null)
            {
                if (bundle.ContainsKey(Constants.SERVICE_STARTED_KEY))
                {
                    isStarted = true;
                }
            }
        }

        protected override void OnSaveInstanceState(Bundle outState)
        {
            outState.PutBoolean(Constants.SERVICE_STARTED_KEY, isStarted);
            base.OnSaveInstanceState(outState);
        }

        protected override void OnDestroy()
        {
            StopServiceExecute();
            StopService(startServiceIntent);
            base.OnDestroy();
        }


        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        void StopServiceExecute()
        {
            Log.Info(TAG, "User requested that the service be stopped.");
            StopService(stopServiceIntent);
            isStarted = false;
        }

        public void StartServiceExecute()
        {
            StartService(startServiceIntent);
            Log.Info(TAG, "User requested that the service be started.");

            isStarted = true;
        }

        public void NotificationServiceExecution()
        {
            StartServiceExecute();
        }

    }
}