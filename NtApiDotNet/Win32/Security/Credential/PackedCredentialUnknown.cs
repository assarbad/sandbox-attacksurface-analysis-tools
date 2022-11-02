﻿//  Copyright 2022 Google LLC. All Rights Reserved.
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

using System;
using System.Collections.Generic;

namespace NtApiDotNet.Win32.Security.Credential
{
    /// <summary>
    /// Class to represent an unknown packed credentials structure.
    /// </summary>
    public sealed class PackedCredentialUnknown : PackedCredential
    {
        /// <summary>
        /// The credentials data.
        /// </summary>
        /// <remarks>Changing the data </remarks>
        public byte[] Credentials => (byte[])_credentials.Clone();

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="cred_type">The type of packed credentials.</param>
        /// <param name="credentials">The packed credentials structure.</param>
        /// <param name="package_list">The list of supported security packages.</param>
        public PackedCredentialUnknown(Guid cred_type, byte[] credentials, IEnumerable<uint> package_list = null)
            : base(cred_type, credentials, package_list)
        {
        }
    }
}
