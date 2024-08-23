using System.Net.Http;
using Cirrious.FluentLayouts.Touch;
using Firebase.Analytics;
using Firebase.Crashlytics;

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

        View.AddSubview(buttonEvent);

        buttonEvent.TouchUpInside += Button_TouchUpInside;

        UIButton buttonCrash = new UIButton() { TranslatesAutoresizingMaskIntoConstraints = false };
        buttonCrash.SetTitle("Crash the app", UIControlState.Normal);
        buttonCrash.SetTitleColor(UIColor.Black, UIControlState.Normal);

        View.AddSubview(buttonCrash);

        buttonCrash.TouchUpInside += ButtonCrash_TouchUpInside;

        // Constraints
        View.AddConstraints
        (
            label.WithSameCenterX(View),
            label.WithSameCenterY(View),

            buttonEvent.WithSameCenterX(View),
            buttonEvent.Below(label, 20f),

            buttonCrash.WithSameCenterX(View),
            buttonCrash.Below(buttonEvent, 20f)
        );

        Firebase.Core.App.Configure();

        Analytics.Analytics.SetAnalyticsCollectionEnabled(true);
        Crashlytics.Crashlytics.SharedInstance.SetCrashlyticsCollectionEnabled(true);

        // ANALYTICS
        string test = Analytics.Analytics.AppInstanceId;
        Analytics.Analytics.LogEvent("EventNameTest", null);

        //// CRASHLYTICS
        //NSError nSError = new NSError(new NSString("Domain"), 55);
        //Crashlytics.Crashlytics.SharedInstance.RecordError(nSError);

        //// PERFORMANCE
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
}

