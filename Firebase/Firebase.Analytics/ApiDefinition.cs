using System;
using System.Collections.Generic;
using Foundation;

namespace Firebase.Analytics
{
    using System;
    using Foundation;

    // @interface FIRAnalytics : NSObject
    [BaseType(typeof(NSObject), Name = "FIRAnalytics")]
    [DisableDefaultCtor]
    interface Analytics
    {
        // +(void)logEventWithName:(NSString * _Nonnull)name parameters:(NSDictionary<NSString *,id> * _Nullable)parameters __attribute__((swift_name("logEvent(_:parameters:)")));
        [Static]
        [Export("logEventWithName:parameters:")]
        void LogEventWithName(string name, [NullAllowed] NSDictionary<NSString, NSObject> parameters);

        // +(void)setUserPropertyString:(NSString * _Nullable)value forName:(NSString * _Nonnull)name __attribute__((swift_name("setUserProperty(_:forName:)")));
        [Static]
        [Export("setUserPropertyString:forName:")]
        void SetUserPropertyString([NullAllowed] string value, string name);

        // +(void)setUserID:(NSString * _Nullable)userID;
        [Static]
        [Export("setUserID:")]
        void SetUserID([NullAllowed] string userID);

        // +(void)setAnalyticsCollectionEnabled:(BOOL)analyticsCollectionEnabled;
        [Static]
        [Export("setAnalyticsCollectionEnabled:")]
        void SetAnalyticsCollectionEnabled(bool analyticsCollectionEnabled);

        // +(void)setSessionTimeoutInterval:(NSTimeInterval)sessionTimeoutInterval;
        [Static]
        [Export("setSessionTimeoutInterval:")]
        void SetSessionTimeoutInterval(double sessionTimeoutInterval);

        // +(void)sessionIDWithCompletion:(void (^ _Nonnull)(int64_t, NSError * _Nullable))completion;
        [Static]
        [Export("sessionIDWithCompletion:")]
        void SessionIDWithCompletion(Action<long, NSError> completion);

        // +(NSString * _Nullable)appInstanceID;
        [Static]
        [NullAllowed, Export("appInstanceID")]
        //[Verify(MethodToProperty)]
        string AppInstanceID { get; }

        // +(void)resetAnalyticsData;
        [Static]
        [Export("resetAnalyticsData")]
        void ResetAnalyticsData();

        // +(void)setDefaultEventParameters:(NSDictionary<NSString *,id> * _Nullable)parameters;
        [Static]
        [Export("setDefaultEventParameters:")]
        void SetDefaultEventParameters([NullAllowed] NSDictionary<NSString, NSObject> parameters);
    }
}


