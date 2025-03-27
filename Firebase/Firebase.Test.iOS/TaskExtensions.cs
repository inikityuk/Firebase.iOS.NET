using System;
namespace Firebase.Test.iOS
{
	public static class TaskExtensions
	{
        public static async void Observe(this Task task, bool continueOnCapturedContext = true)
        {
            await task.ConfigureAwait(continueOnCapturedContext);
        }
    }
}

