﻿//  Copyright 2021 Google Inc. All Rights Reserved.
//
//  Licensed under the Apache License, Version 2.0 (the "License");
//  you may not use this file except in compliance with the License.
//  You may obtain a copy of the License at
//
//  http://www.apache.org/licenses/LICENSE-2.0
//
//  Unless required by applicable law or agreed to in writing, software
//  distributed under the License is distributed on an "AS IS" BASIS,
//  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//  See the License for the specific language governing permissions and
//  limitations under the License.

namespace NtCoreLib.Win32.Security.Authentication;

/// <summary>
/// Placeholder interface for a server authentication context.
/// </summary>
public interface IServerAuthenticationContext : IAuthenticationContext
{
    /// <summary>
    /// Current request attributes.
    /// </summary>
    AcceptContextReqFlags RequestAttributes { get; set; }

    /// <summary>
    /// Current return attributes.
    /// </summary>
    AcceptContextRetFlags ReturnAttributes { get; }
}