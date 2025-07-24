using System;
using System.Diagnostics;
using System.Net.Http;
using Cirrious.FluentLayouts.Touch;
using Firebase.Analytics;
using Firebase.Crashlytics;
using Firebase.Performance;

namespace Firebase.Test.iOS;

public class MainViewController : UIViewController
{
    public override void ViewDidLoad ()
	{
		base.ViewDidLoad ();
        // Perform any additional setup after loading the view, typically from a nib.

        View.BackgroundColor = UIColor.SystemBackground;

        UILabel label = new UILabel()
        {
            TranslatesAutoresizingMaskIntoConstraints = false,
            //BackgroundColor = UIColor.SystemBackground,
            TextAlignment = UITextAlignment.Center,
            Text = "Hello, iOS!",
            AutoresizingMask = UIViewAutoresizing.All,
        };

        View.AddSubview(label);

        UIButton buttonEvent = new UIButton() { TranslatesAutoresizingMaskIntoConstraints = false };
        buttonEvent.SetTitle("Send click event", UIControlState.Normal);
        buttonEvent.SetTitleColor(UIColor.Black, UIControlState.Normal);
        buttonEvent.SetTitleColor(UIColor.White, UIControlState.Highlighted);

        View.AddSubview(buttonEvent);

        buttonEvent.TouchUpInside += Button_TouchUpInside;

        UIButton buttonCrash = new UIButton() { TranslatesAutoresizingMaskIntoConstraints = false };
        buttonCrash.SetTitle("Crash the app", UIControlState.Normal);
        buttonCrash.SetTitleColor(UIColor.Black, UIControlState.Normal);
        buttonCrash.SetTitleColor(UIColor.White, UIControlState.Highlighted);

        View.AddSubview(buttonCrash);

        buttonCrash.TouchUpInside += ButtonCrash_TouchUpInside;

        UIButton buttonCrashTask = new UIButton() { TranslatesAutoresizingMaskIntoConstraints = false };
        buttonCrashTask.SetTitle("Crash in the Task", UIControlState.Normal);
        buttonCrashTask.SetTitleColor(UIColor.Black, UIControlState.Normal);
        buttonCrashTask.SetTitleColor(UIColor.White, UIControlState.Highlighted);

        View.AddSubview(buttonCrashTask);

        buttonCrashTask.TouchUpInside += ButtonCrashTask_TouchUpInside;

        // Constraints
        View.AddConstraints
        (
            label.WithSameCenterX(View),
            label.WithSameCenterY(View),

            buttonEvent.WithSameCenterX(View),
            buttonEvent.Below(label, 20f),

            buttonCrash.WithSameCenterX(View),
            buttonCrash.Below(buttonEvent, 20f),

            buttonCrashTask.WithSameCenterX(View),
            buttonCrashTask.Below(buttonCrash, 20f)
        );

        Firebase.Core.App.Configure();
        
        Analytics.Analytics.SetAnalyticsCollectionEnabled(true);
        Crashlytics.Crashlytics.SharedInstance.SetCrashlyticsCollectionEnabled(true);

        // ANALYTICS
        string test = Analytics.Analytics.AppInstanceId;
        Analytics.Analytics.LogEvent("EventNameTest", null);

        // PERFORMANCE
         Firebase.Performance.HttpMetric httpMetric = new Firebase.Performance.HttpMetric("https://valdperformance.com/", Firebase.Performance.HttpMethod.Get);
         httpMetric.ResponseCode = 200;
         httpMetric.RequestPayloadSize = 10000;
         httpMetric.ResponsePayloadSize = 50000;
         httpMetric.ResponseContentType = "application/json";
         httpMetric.Start();
         
         Task.Run(async () =>
         {
             await Task.Delay(3000);
             httpMetric.Stop();
         });

        // Based on:
        // https://github.com/xamarin/GooglePlayServicesComponents/issues/423
        // https://github.com/drungrin/Fabric.Sdk.Xamarin/blob/f7e6207e5731cfc10ac0c6e96df3d68b19caf2a0/Sources/CrashlyticsKit.Touch/Crashlytics.cs
        AppDomain.CurrentDomain.UnhandledException += (s, a) => RecordManagedException(a.ExceptionObject);
        TaskScheduler.UnobservedTaskException += (s, a) => RecordManagedException(a.Exception);
    }

    private void ButtonCrashTask_TouchUpInside(object? sender, EventArgs e)
    {
        Task.Run(async () =>
        {
            await Task.Delay(1000);
        
            Firebase.Performance.HttpMetric httpMetric = null;
            var test = httpMetric.ResponseCode;
        }).Observe();
    }

    private void ButtonCrash_TouchUpInside(object? sender, EventArgs e)
    {
        // this will crash the app
        int[] crashTestArray = new int[0];
        var boom = crashTestArray[6];
    }

    private void Button_TouchUpInside(object? sender, EventArgs e)
    {
        Analytics.Analytics.LogEvent("ButtonClickEvent", null);
    }

    private static void RecordManagedException(object exceptionObject)
    {
        var exception = exceptionObject as Exception;
        if (exception == null)
            return;
        
        Console.WriteLine($"IGOR-TESTING:{exception}");
        
          ExceptionModel exceptionModel = new ExceptionModel($"ReleaseIPA:{exception.GetType().FullName}", exception.Message)
          {
              StackTrace = StackTraceParser.Parse(exception).Select((frame, Index) =>
                  new Firebase.Crashlytics.StackFrame(
                      string.IsNullOrEmpty(frame.MethodName) ? frame.ClassName : $"{frame.ClassName}.{frame.MethodName}",
                      frame.FileName,
                      frame.LineNumber)).ToArray()
          };
        
        Crashlytics.Crashlytics.SharedInstance.RecordExceptionModel(exceptionModel);
        
        Environment.Exit(Environment.ExitCode);
    }
}


