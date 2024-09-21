﻿//  Copyright 2019 Google Inc. All Rights Reserved.
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

// NOTE: This file is a modified version of NdrParser.cs from OleViewDotNet
// https://github.com/tyranid/oleviewdotnet. It's been relicensed from GPLv3 by
// the original author James Forshaw to be used under the Apache License for this
// project.

using System;
using System.Collections.Generic;
using NtCoreLib.Ndr.Formatter;

namespace NtCoreLib.Ndr.Dce;
#pragma warning disable 1591
[Serializable]
public sealed class NdrUnknownTypeReference : NdrBaseTypeReference
{
    private static readonly HashSet<NdrFormatCharacter> _formats = new();
    internal NdrUnknownTypeReference(NdrFormatCharacter format) : base(format)
    {
        if (_formats.Add(format))
        {
            NdrUtils.WriteLine(format.ToString());
        }
    }

    private protected override string FormatType(INdrFormatterContext context)
    {
        return $"{context.FormatComment("Unhandled")}{base.FormatType(context)}";
    }
}
#pragma warning restore 1591
