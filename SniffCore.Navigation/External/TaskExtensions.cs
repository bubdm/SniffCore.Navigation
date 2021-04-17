using System;
using System.Threading.Tasks;

namespace SniffCore.Navigation.External
{
    public static class TaskExtensions
    {
        public static async void FireAndForget(this Task task)
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