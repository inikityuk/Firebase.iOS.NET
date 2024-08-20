using ObjCRuntime;

namespace Firebase.Performance
{
    [Native]
    public enum HttpMethod : long
    {
        Get,
        Put,
        Post,
        Delete,
        Head,
        Patch,
        Options,
        Trace,
        Connect
    }
}


