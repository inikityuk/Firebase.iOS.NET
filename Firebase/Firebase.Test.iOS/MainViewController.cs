using System.Net.Http;
using Firebase.Analytics;

namespace Firebase.Test.iOS;

public class MainViewController : UIViewController
{
    public override void ViewDidLoad ()
	{
		base.ViewDidLoad ();
        // Perform any additional setup after loading the view, typically from a nib.

        // create a UIViewController with a single UILabel
        View.AddSubview(new UILabel(UIScreen.MainScreen.Bounds)
        {
            BackgroundColor = UIColor.SystemBackground,
            TextAlignment = UITextAlignment.Center,
            Text = "Hello, iOS!",
            AutoresizingMask = UIViewAutoresizing.All,
        });

        Firebase.Core.App.Configure();

        // ANALYTICS
        string test = Analytics.Analytics.AppInstanceId;
        Analytics.Analytics.LogEventWithName("EventNameTest", null);

        // CRASHLYTICS
        NSError nSError = new NSError(new NSString("Domain"), 55);
        Crashlytics.Crashlytics.SharedInstance.RecordError(nSError);

        //int[] crashTestArray = new int[0];
        //var boom = crashTestArray[6];

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
    }
}

