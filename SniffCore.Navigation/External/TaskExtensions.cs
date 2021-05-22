//
// Copyright (c) David Wendland. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
//

using System.Threading.Tasks;

namespace SniffCore.Navigation.External
{
    internal static class TaskExtensions
    {
        internal static async void FireAndForget(this Task task)
        {
            await task;
        }
    }
}