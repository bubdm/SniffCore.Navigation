using System;
using System.Threading.Tasks;

namespace SniffCore.Navigation.External
{
    internal static class TaskExtensions
    {
        internal static async void FireAndForget(this Task task)
        {
            try
            {
                await task;
            }
            catch (Exception)
            {
            }
        }
    }
}