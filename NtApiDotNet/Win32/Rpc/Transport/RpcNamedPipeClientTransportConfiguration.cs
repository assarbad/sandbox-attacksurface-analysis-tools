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

using NtApiDotNet.Win32.Security.Authentication;
using NtApiDotNet.Win32.Security.Authentication.Kerberos.Client;
using NtApiDotNet.Win32.Security.Authentication.Ntlm.Client;
using System;

namespace NtApiDotNet.Win32.Rpc.Transport
{
    /// <summary>
    /// Class to configure the RPC named pipe transport.
    /// </summary>
    /// <remarks>If you configure this, even with no options, </remarks>
    public sealed class RpcNamedPipeClientTransportConfiguration : RpcClientTransportConfiguration
    {
        /// <summary>
        /// The authentication package name for the SMB authentication. Defaults to Negotiate.
        /// </summary>
        public string PackageName { get; set; }

        /// <summary>
        /// Specify to use a NULL session authentication.
        /// </summary>
        public bool NullSession { get; set; }

        /// <summary>
        /// Enable delegation for the authentication.
        /// </summary>
        public bool Delegation { get; set; }

        /// <summary>
        /// Disable SMB2 signing.
        /// </summary>
        public bool DisableSigning { get; set; }

        /// <summary>
        /// Enable mutual authentication if available.
        /// </summary>
        public bool MutualAuthentication { get; set; }

        /// <summary>
        /// Authentication credentials.
        /// </summary>
        public AuthenticationCredentials Credentials { get; set; }

        /// <summary>
        /// The SPN for the authentication.
        /// </summary>
        public string ServicePrincipalName { get; set; }

        /// <summary>
        /// Specify a kerberos ticket cache.
        /// </summary>
        public KerberosLocalTicketCache TicketCache { get; set; }

        private InitializeContextReqFlags GetContextRequestFlags()
        {
            InitializeContextReqFlags ret = DisableSigning ? InitializeContextReqFlags.NoIntegrity : InitializeContextReqFlags.Integrity;
            if (NullSession)
                ret |= InitializeContextReqFlags.NullSession;
            if (MutualAuthentication)
                ret |= InitializeContextReqFlags.MutualAuth;
            if (Delegation)
                ret |= InitializeContextReqFlags.Delegate | InitializeContextReqFlags.MutualAuth;
            return ret;
        }

        internal IClientAuthenticationContext CreateAuthenticationContext(string hostname)
        {
            string package = PackageName ?? AuthenticationPackage.NEGOSSP_NAME;
            string spn = ServicePrincipalName ?? $"cifs/{hostname}";

            var request_flags = GetContextRequestFlags();

            if (AuthenticationPackage.CheckKerberos(package) && TicketCache != null)
            {
                return TicketCache.CreateClientContext(spn, request_flags);
            }

            if (!NtObjectUtils.IsWindows || Credentials is NtHashAuthenticationCredentials)
            {
                if (!AuthenticationPackage.CheckNtlm(package))
                {
                    throw new NotImplementedException($"Authentication package {package} not supported.");
                }
                return new NtlmClientAuthenticationContext(Credentials, request_flags, spn);
            }

            bool initialize = !package.Equals(AuthenticationPackage.NEGOSSP_NAME, StringComparison.OrdinalIgnoreCase);
            return new ClientAuthenticationContext(CredentialHandle.Create(package,
                SecPkgCredFlags.Outbound, Credentials), request_flags,
                    spn, null, SecDataRep.Network, initialize)
            { OwnsCredentials = true };
        }
    }
}
