using System;
using Metal;
using Microsoft.VisualBasic;
using System.Text.RegularExpressions;

namespace Firebase.Test.iOS
{
	//NOTE: Some resources used to implement and modify stacktrace conversion to Firebase supported format
    // https://github.com/xamarin/GooglePlayServicesComponents/issues/423
    // https://gist.github.com/Cardanis/c4316f931ad0de4c9d6c01eebf61884b
    // https://github.com/drungrin/Fabric.Sdk.Xamarin/blob/f7e6207e5731cfc10ac0c6e96df3d68b19caf2a0/Sources/CrashlyticsKit.Droid/Crashlytics.cs#L108

    /// <summary>
    /// This class converting .NET exception stack trace to temporary "StackFrame" class,
    /// which mimic "Firebase.Crashlytics.StackFrame" class on iOS and "StackTraceElement" on Android.
    /// This trace convertion allow us to have properly grouped and readable error/crashes reports in Firebase console
    /// </summary>
    public class StackTraceParser
    {
        private static readonly Regex _regex = new Regex(@"^\s*at (?<className>\S+)\.(?<methodName>\S+\(.*\))(.* in \/(.+\/)*(?<fileName>(.+\..+)):line(?<lineNumber> \d+))?$", RegexOptions.Multiline | RegexOptions.ExplicitCapture);

        public static IEnumerable<StackFrame> Parse(Exception exception)
        {
            var stackFrames = new List<StackFrame>();

            try
            {
                ParseLocal(exception, stackFrames);

                return stackFrames;
            }
            catch (Exception)
            {
                return stackFrames;
            }
        }

        private static void ParseLocal(Exception exception, List<StackFrame> stackFrames)
        {
            if (exception == null)
                return;

            stackFrames.AddRange(Parse(exception.StackTrace));

            if (exception is AggregateException aggregateException)
            {
                var number = 0;
                foreach (var innerException in aggregateException.InnerExceptions)
                {
                    var (namespaceName, className) = GetNamespaceNameAndClassName(innerException.GetType());

                    var methodName = string.Empty;

                    if (!string.IsNullOrEmpty(namespaceName))
                    {
                        methodName = $"{className}: {innerException.Message}";
                        className = $"(Inner Exception #{number++}) {namespaceName}";
                    }
                    else
                    {
                        className = $"(Inner Exception #{number++}) {className}: {innerException.Message}";
                    }

                    stackFrames.Add(new StackFrame(className, methodName, "", 0));

                    ParseLocal(innerException, stackFrames);
                }
            }
            else if (exception.InnerException != null)
            {
                var (namespaceName, className) = GetNamespaceNameAndClassName(exception.InnerException.GetType());

                var methodName = string.Empty;

                if (!string.IsNullOrEmpty(namespaceName))
                {
                    methodName = $"{className}: {exception.InnerException.Message}";
                    className = $"(Inner Exception) {namespaceName}";
                }
                else
                {
                    className = $"(Inner Exception) {className}: {exception.InnerException.Message}";
                }

                stackFrames.Add(new StackFrame(className, methodName, "", 0));

                ParseLocal(exception.InnerException, stackFrames);
            }
        }

        private static (string? NamespaceName, string ClassName) GetNamespaceNameAndClassName(Type type)
        {
            var className = type.ToString();
            var namespaceName = type.Namespace;

            if (!string.IsNullOrEmpty(namespaceName))
            {
                className = className.Substring(namespaceName.Length + 1);
            }

            return (namespaceName, className);
        }

        public static IEnumerable<StackFrame> Parse(string stackTrace)
        {
            if (string.IsNullOrEmpty(stackTrace))
                yield break;

            foreach (Match match in _regex.Matches(stackTrace))
            {
                var className = match.Groups["className"].Value;
                var methodName = match.Groups["methodName"].Value;

                var lineNumberGroup = match.Groups["lineNumber"];
                var lineNumber = lineNumberGroup.Success ? int.Parse(lineNumberGroup.Value) : 0;

                var fileNameGroup = match.Groups["fileName"];
                var fileName = fileNameGroup.Success && lineNumber > 0 ? fileNameGroup.Value : $"{className}.{methodName}";

                yield return new StackFrame(className, methodName, fileName, lineNumber);
            }
        }
    }
}

