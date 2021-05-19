//
// Copyright (c) David Wendland. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
//

namespace SniffCore.PleaseWaits
{
    public interface IPleaseWaitProvider
    {
        void Show();
        void Close();
        void HandleProgress(ProgressData progressData);
        void HandleCanceled(LoadingCanceled canceledData);
    }
}