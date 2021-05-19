//
// Copyright (c) David Wendland. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
//

using System.Threading.Tasks;
using SniffCore.PleaseWaits;

namespace SniffCore.Navigation
{
    public interface IDelayedAsyncLoader
    {
        Task LoadAsync(LoadingProgress progress);
    }
}