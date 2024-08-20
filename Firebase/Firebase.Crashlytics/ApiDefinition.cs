using System;
using Foundation;
using ObjCRuntime;

namespace Firebase.Crashlytics
{
    // @interface FIRCrashlyticsReport : NSObject
    [BaseType(typeof(NSObject), Name = "FIRCrashlyticsReport")]
    [DisableDefaultCtor]
    interface CrashlyticsReport
    {
        // @property (readonly, nonatomic) NSString * _Nonnull reportID;
        [Export("reportID")]
        string ReportID { get; }

        // @property (readonly, nonatomic) NSDate * _Nonnull dateCreated;
        [Export("dateCreated")]
        NSDate DateCreated { get; }

        // @property (readonly, nonatomic) BOOL hasCrash;
        [Export("hasCrash")]
        bool HasCrash { get; }

        // -(void)log:(NSString * _Nonnull)msg;
        [Export("log:")]
        void Log(string msg);

        // -(void)logWithFormat:(NSString * _Nonnull)format, ... __attribute__((format(NSString, 1, 2)));
        [Internal]
        [Export("logWithFormat:", IsVariadic = true)]
        void LogWithFormat(string format, IntPtr varArgs);

        // -(void)logWithFormat:(NSString * _Nonnull)format arguments:(va_list)args __attribute__((swift_name("log(format:arguments:)")));
        [Export("logWithFormat:arguments:")]
        unsafe void LogWithFormat(string format, sbyte* args);

        // -(void)setCustomValue:(id _Nullable)value forKey:(NSString * _Nonnull)key;
        [Export("setCustomValue:forKey:")]
        void SetCustomValue([NullAllowed] NSObject value, string key);

        // -(void)setCustomKeysAndValues:(NSDictionary * _Nonnull)keysAndValues;
        [Export("setCustomKeysAndValues:")]
        void SetCustomKeysAndValues(NSDictionary keysAndValues);

        // -(void)setUserID:(NSString * _Nullable)userID;
        [Export("setUserID:")]
        void SetUserID([NullAllowed] string userID);
    }

    // @interface FIRStackFrame : NSObject
    [BaseType(typeof(NSObject), Name = "FIRStackFrame")]
    [DisableDefaultCtor]
    interface StackFrame
    {
        // -(instancetype _Nonnull)initWithSymbol:(NSString * _Nonnull)symbol file:(NSString * _Nonnull)file line:(NSInteger)line;
        [Export("initWithSymbol:file:line:")]
        NativeHandle Constructor(string symbol, string file, nint line);

        // +(instancetype _Nonnull)stackFrameWithAddress:(NSUInteger)address;
        [Static]
        [Export("stackFrameWithAddress:")]
        StackFrame StackFrameWithAddress(nuint address);

        // +(instancetype _Nonnull)stackFrameWithSymbol:(NSString * _Nonnull)symbol file:(NSString * _Nonnull)file line:(NSInteger)line __attribute__((availability(swift, unavailable)));
        //[Unavailable(PlatformName.Swift)]
        [Static]
        [Export("stackFrameWithSymbol:file:line:")]
        StackFrame StackFrameWithSymbol(string symbol, string file, nint line);
    }

    // @interface FIRExceptionModel : NSObject
    [BaseType(typeof(NSObject), Name = "FIRExceptionModel")]
    [DisableDefaultCtor]
    interface ExceptionModel
    {
        // -(instancetype _Nonnull)initWithName:(NSString * _Nonnull)name reason:(NSString * _Nonnull)reason;
        [Export("initWithName:reason:")]
        NativeHandle Constructor(string name, string reason);

        // +(instancetype _Nonnull)exceptionModelWithName:(NSString * _Nonnull)name reason:(NSString * _Nonnull)reason __attribute__((availability(swift, unavailable)));
        //[Unavailable(PlatformName.Swift)]
        [Static]
        [Export("exceptionModelWithName:reason:")]
        ExceptionModel ExceptionModelWithName(string name, string reason);

        // @property (copy, nonatomic) NSArray<FIRStackFrame *> * _Nonnull stackTrace;
        [Export("stackTrace", ArgumentSemantic.Copy)]
        StackFrame[] StackTrace { get; set; }
    }

    // @interface FIRCrashlytics : NSObject
    [BaseType(typeof(NSObject), Name = "FIRCrashlytics")]
    [DisableDefaultCtor]
    interface Crashlytics
    {
        // +(instancetype _Nonnull)crashlytics __attribute__((swift_name("crashlytics()")));
        [Static]
        [Export("crashlytics")]
        Crashlytics SharedInstance { get; }

        // -(void)log:(NSString * _Nonnull)msg;
        [Export("log:")]
        void Log(string msg);

        // -(void)logWithFormat:(NSString * _Nonnull)format, ... __attribute__((format(NSString, 1, 2)));
        [Internal]
        [Export("logWithFormat:", IsVariadic = true)]
        void LogWithFormat(string format, IntPtr varArgs);

        // -(void)logWithFormat:(NSString * _Nonnull)format arguments:(va_list)args __attribute__((swift_name("log(format:arguments:)")));
        [Export("logWithFormat:arguments:")]
        unsafe void LogWithFormat(string format, sbyte* args);

        // -(void)setCustomValue:(id _Nullable)value forKey:(NSString * _Nonnull)key;
        [Export("setCustomValue:forKey:")]
        void SetCustomValue([NullAllowed] NSObject value, string key);

        // -(void)setCustomKeysAndValues:(NSDictionary * _Nonnull)keysAndValues;
        [Export("setCustomKeysAndValues:")]
        void SetCustomKeysAndValues(NSDictionary keysAndValues);

        // -(void)setUserID:(NSString * _Nullable)userID;
        [Export("setUserID:")]
        void SetUserID([NullAllowed] string userID);

        // -(void)recordError:(NSError * _Nonnull)error __attribute__((swift_name("record(error:)")));
        [Export("recordError:")]
        void RecordError(NSError error);

        // -(void)recordError:(NSError * _Nonnull)error userInfo:(NSDictionary<NSString *,id> * _Nullable)userInfo __attribute__((swift_name("record(error:userInfo:)")));
        [Export("recordError:userInfo:")]
        void RecordError(NSError error, [NullAllowed] NSDictionary<NSString, NSObject> userInfo);

        // -(void)recordExceptionModel:(FIRExceptionModel * _Nonnull)exceptionModel __attribute__((swift_name("record(exceptionModel:)")));
        [Export("recordExceptionModel:")]
        void RecordExceptionModel(ExceptionModel exceptionModel);

        // -(BOOL)didCrashDuringPreviousExecution;
        [Export("didCrashDuringPreviousExecution")]
        //[Verify(MethodToProperty)]
        bool DidCrashDuringPreviousExecution { get; }

        // -(void)setCrashlyticsCollectionEnabled:(BOOL)enabled;
        [Export("setCrashlyticsCollectionEnabled:")]
        void SetCrashlyticsCollectionEnabled(bool enabled);

        // -(BOOL)isCrashlyticsCollectionEnabled;
        [Export("isCrashlyticsCollectionEnabled")]
        //[Verify(MethodToProperty)]
        bool IsCrashlyticsCollectionEnabled { get; }

        // -(void)checkForUnsentReportsWithCompletion:(void (^ _Nonnull)(BOOL))completion __attribute__((swift_name("checkForUnsentReports(completion:)")));
        [Export("checkForUnsentReportsWithCompletion:")]
        void CheckForUnsentReportsWithCompletion(Action<bool> completion);

        // -(void)checkAndUpdateUnsentReportsWithCompletion:(void (^ _Nonnull)(FIRCrashlyticsReport * _Nullable))completion __attribute__((swift_name("checkAndUpdateUnsentReports(completion:)")));
        [Export("checkAndUpdateUnsentReportsWithCompletion:")]
        void CheckAndUpdateUnsentReportsWithCompletion(Action<CrashlyticsReport> completion);

        // -(void)sendUnsentReports;
        [Export("sendUnsentReports")]
        void SendUnsentReports();

        // -(void)deleteUnsentReports;
        [Export("deleteUnsentReports")]
        void DeleteUnsentReports();
    }
}
