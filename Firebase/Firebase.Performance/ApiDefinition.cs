namespace Firebase.Performance
{
    using System;
    using Foundation;
    using ObjCRuntime;

    [Protocol (Name = "FIRPerformanceAttributable")]
    [BaseType(typeof(NSObject))]
    interface PerformanceAttributable
    {
        // @required @property (readonly, nonatomic) NSDictionary<NSString *,NSString *> * _Nonnull attributes;
        [Abstract]
        [Export("attributes")]
        NSDictionary<NSString, NSString> Attributes { get; }

        // @required -(void)setValue:(NSString * _Nonnull)value forAttribute:(NSString * _Nonnull)attribute;
        [Abstract]
        [Export("setValue:forAttribute:")]
        void SetValue(string value, string attribute);

        // @required -(NSString * _Nullable)valueForAttribute:(NSString * _Nonnull)attribute;
        [Abstract]
        [Export("valueForAttribute:")]
        [return: NullAllowed]
        string ValueForAttribute(string attribute);

        // @required -(void)removeAttribute:(NSString * _Nonnull)attribute;
        [Abstract]
        [Export("removeAttribute:")]
        void RemoveAttribute(string attribute);
    }

    // @interface FIRHTTPMetric : NSObject <FIRPerformanceAttributable>
    //[Unavailable(PlatformName.MacOSXAppExtension)]
    //[Unavailable(PlatformName.iOSAppExtension)]
    [BaseType(typeof(NSObject), Name = "FIRHTTPMetric")]
    [DisableDefaultCtor]
    interface HttpMetric : PerformanceAttributable
    {
        // -(instancetype _Nullable)initWithURL:(NSURL * _Nonnull)URL HTTPMethod:(FIRHTTPMethod)httpMethod __attribute__((swift_name("init(url:httpMethod:)")));
        [Export("initWithURL:HTTPMethod:")]
        NativeHandle Constructor(NSUrl URL, HttpMethod httpMethod);

        // @property (assign, nonatomic) NSInteger responseCode;
        [Export("responseCode")]
        nint ResponseCode { get; set; }

        // @property (assign, nonatomic) long requestPayloadSize;
        [Export("requestPayloadSize")]
        nint RequestPayloadSize { get; set; }

        // @property (assign, nonatomic) long responsePayloadSize;
        [Export("responsePayloadSize")]
        nint ResponsePayloadSize { get; set; }

        // @property (copy, nonatomic) NSString * _Nullable responseContentType;
        [NullAllowed, Export("responseContentType")]
        string ResponseContentType { get; set; }

        // -(void)start;
        [Export("start")]
        void Start();

        // -(void)stop;
        [Export("stop")]
        void Stop();
    }

    // @interface FIRTrace : NSObject <FIRPerformanceAttributable>
    //[Unavailable(PlatformName.MacOSXAppExtension)]
    //[Unavailable(PlatformName.iOSAppExtension)]
    [BaseType(typeof(NSObject), Name = "FIRTrace")]
    [DisableDefaultCtor]
    interface Trace : PerformanceAttributable
    {
        // @property (readonly, copy, nonatomic) NSString * _Nonnull name;
        [Export("name")]
        string Name { get; }

        // -(void)start;
        [Export("start")]
        void Start();

        // -(void)stop;
        [Export("stop")]
        void Stop();

        // -(void)incrementMetric:(NSString * _Nonnull)metricName byInt:(int64_t)incrementValue __attribute__((swift_name("incrementMetric(_:by:)")));
        [Export("incrementMetric:byInt:")]
        void IncrementMetric(string metricName, long incrementValue);

        // -(int64_t)valueForIntMetric:(NSString * _Nonnull)metricName __attribute__((swift_name("valueForMetric(_:)")));
        [Export("valueForIntMetric:")]
        long ValueForIntMetric(string metricName);

        // -(void)setIntValue:(int64_t)value forMetric:(NSString * _Nonnull)metricName __attribute__((swift_name("setValue(_:forMetric:)")));
        [Export("setIntValue:forMetric:")]
        void SetIntValue(long value, string metricName);
    }

    // @interface FIRPerformance : NSObject
    //[Unavailable(PlatformName.MacOSXAppExtension)]
    //[Unavailable(PlatformName.iOSAppExtension)]
    [BaseType(typeof(NSObject), Name = "FIRPerformance")]
    interface Performance
    {
        // @property (getter = isDataCollectionEnabled, assign, nonatomic) BOOL dataCollectionEnabled;
        [Export("dataCollectionEnabled")]
        bool DataCollectionEnabled { [Bind("isDataCollectionEnabled")] get; set; }

        // @property (getter = isInstrumentationEnabled, assign, nonatomic) BOOL instrumentationEnabled;
        [Export("instrumentationEnabled")]
        bool InstrumentationEnabled { [Bind("isInstrumentationEnabled")] get; set; }

        // +(instancetype _Nonnull)sharedInstance __attribute__((swift_name("sharedInstance()")));
        [Static]
        [Export("sharedInstance")]
        Performance SharedInstance();

        // +(FIRTrace * _Nullable)startTraceWithName:(NSString * _Nonnull)name __attribute__((swift_name("startTrace(name:)")));
        [Static]
        [Export("startTraceWithName:")]
        [return: NullAllowed]
        Trace StartTraceWithName(string name);

        // -(FIRTrace * _Nullable)traceWithName:(NSString * _Nonnull)name __attribute__((swift_name("trace(name:)")));
        [Export("traceWithName:")]
        [return: NullAllowed]
        Trace TraceWithName(string name);
    }

}


