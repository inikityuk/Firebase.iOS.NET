using System;

namespace Firebase.Core
{
    using System;
    using Firebase.Core;
    using Foundation;
    using ObjCRuntime;

    // typedef void (^FIRAppVoidBoolCallback)(BOOL);
    delegate void AppVoidBoolHandler(bool success);

    // @interface FIRApp : NSObject
    [BaseType(typeof(NSObject), Name = "FIRApp")]
    [DisableDefaultCtor]
    interface App : INativeObject
    {
        // +(void)configure;
        [Static]
        [Export("configure")]
        void Configure();

        // +(void)configureWithOptions:(FIROptions * _Nonnull)options __attribute__((swift_name("configure(options:)")));
        [Static]
        [Export("configureWithOptions:")]
        void ConfigureWithOptions(Options options);

        // +(void)configureWithName:(NSString * _Nonnull)name options:(FIROptions * _Nonnull)options __attribute__((swift_name("configure(name:options:)")));
        [Static]
        [Export("configureWithName:options:")]
        void ConfigureWithName(string name, Options options);

        // +(FIRApp * _Nullable)defaultApp __attribute__((swift_name("app()")));
        [Static]
        [NullAllowed, Export("defaultApp")]
        //[Verify(MethodToProperty)]
        App DefaultApp { get; }

        // +(FIRApp * _Nullable)appNamed:(NSString * _Nonnull)name __attribute__((swift_name("app(name:)")));
        [Static]
        [Export("appNamed:")]
        [return: NullAllowed]
        App AppNamed(string name);

        // @property (readonly, class) NSDictionary<NSString *,FIRApp *> * _Nullable allApps;
        [Static]
        [NullAllowed, Export("allApps")]
        NSDictionary<NSString, App> AllApps { get; }

        // -(void)deleteApp:(void (^ _Nonnull)(BOOL))completion;
        [Export("deleteApp:")]
        void DeleteApp(AppVoidBoolHandler completion);

        // @property (readonly, copy, nonatomic) NSString * _Nonnull name;
        [Export("name")]
        string Name { get; }

        // @property (readonly, copy, nonatomic) FIROptions * _Nonnull options;
        [Export("options", ArgumentSemantic.Copy)]
        Options Options { get; }

        // @property (getter = isDataCollectionDefaultEnabled, readwrite, nonatomic) BOOL dataCollectionDefaultEnabled;
        [Export("dataCollectionDefaultEnabled")]
        bool DataCollectionDefaultEnabled { [Bind("isDataCollectionDefaultEnabled")] get; set; }
    }

    // @interface FIRConfiguration : NSObject
    [BaseType(typeof(NSObject), Name = "FIRConfiguration")]
    [DisableDefaultCtor]
    interface Configuration
    {
        // @property (readonly, nonatomic, class) NS_SWIFT_NAME(shared) FIRConfiguration * sharedInstance __attribute__((swift_name("shared")));
        [Static]
        [Export("sharedInstance")]
        Configuration SharedInstance { get; }

        // -(void)setLoggerLevel:(FIRLoggerLevel)loggerLevel;
        [Export("setLoggerLevel:")]
        void SetLoggerLevel(LoggerLevel loggerLevel);
    }

    // @interface FIROptions : NSObject <NSCopying>
    [BaseType(typeof(NSObject), Name = "FIROptions")]
    [DisableDefaultCtor]
    interface Options : INSCopying
    {
        // +(FIROptions * _Nullable)defaultOptions __attribute__((swift_name("defaultOptions()")));
        [Static]
        [NullAllowed, Export("defaultOptions")]
        //[Verify(MethodToProperty)]
        Options DefaultOptions { get; }

        // @property (copy, nonatomic) NS_SWIFT_NAME(apiKey) NSString * APIKey __attribute__((swift_name("apiKey")));
        [Export("APIKey")]
        string APIKey { get; set; }

        // @property (copy, nonatomic) NSString * _Nonnull bundleID;
        [Export("bundleID")]
        string BundleID { get; set; }

        // @property (copy, nonatomic) NSString * _Nullable clientID;
        [NullAllowed, Export("clientID")]
        string ClientID { get; set; }

        // @property (copy, nonatomic) DEPRECATED_ATTRIBUTE NSString * trackingID __attribute__((deprecated("")));
        [Export("trackingID")]
        string TrackingID { get; set; }

        // @property (copy, nonatomic) NS_SWIFT_NAME(gcmSenderID) NSString * GCMSenderID __attribute__((swift_name("gcmSenderID")));
        [Export("GCMSenderID")]
        string GCMSenderID { get; set; }

        // @property (copy, nonatomic) NSString * _Nullable projectID;
        [NullAllowed, Export("projectID")]
        string ProjectID { get; set; }

        // @property (copy, nonatomic) DEPRECATED_ATTRIBUTE NSString * androidClientID __attribute__((deprecated("")));
        [Export("androidClientID")]
        string AndroidClientID { get; set; }

        // @property (copy, nonatomic) NSString * _Nonnull googleAppID;
        [Export("googleAppID")]
        string GoogleAppID { get; set; }

        // @property (copy, nonatomic) NSString * _Nullable databaseURL;
        [NullAllowed, Export("databaseURL")]
        string DatabaseURL { get; set; }

        // @property (copy, nonatomic) NSString * _Nullable deepLinkURLScheme;
        [NullAllowed, Export("deepLinkURLScheme")]
        string DeepLinkURLScheme { get; set; }

        // @property (copy, nonatomic) NSString * _Nullable storageBucket;
        [NullAllowed, Export("storageBucket")]
        string StorageBucket { get; set; }

        // @property (copy, nonatomic) NSString * _Nullable appGroupID;
        [NullAllowed, Export("appGroupID")]
        string AppGroupID { get; set; }

        // -(instancetype _Nullable)initWithContentsOfFile:(NSString * _Nonnull)plistPath __attribute__((objc_designated_initializer));
        [Export("initWithContentsOfFile:")]
        //[DesignatedInitializer]
        NativeHandle Constructor(string plistPath);

        // -(instancetype _Nonnull)initWithGoogleAppID:(NSString * _Nonnull)googleAppID GCMSenderID:(NSString * _Nonnull)GCMSenderID __attribute__((swift_name("init(googleAppID:gcmSenderID:)"))) __attribute__((objc_designated_initializer));
        [Export("initWithGoogleAppID:GCMSenderID:")]
        //[DesignatedInitializer]
        NativeHandle Constructor(string googleAppID, string GCMSenderID);
    }
}


