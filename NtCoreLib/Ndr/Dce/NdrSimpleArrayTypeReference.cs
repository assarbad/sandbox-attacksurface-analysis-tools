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
using System.IO;
using NtCoreLib.Ndr.Parser;

namespace NtCoreLib.Ndr.Dce;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
[Serializable]
public sealed class NdrSimpleArrayTypeReference : NdrBaseArrayTypeReference
{
    public int TotalSize { get; }

    internal NdrSimpleArrayTypeReference(NdrParseContext context, NdrFormatCharacter format, BinaryReader reader) : base(context, format, reader)
    {
        if (format == NdrFormatCharacter.FC_SMFARRAY)
        {
            TotalSize = reader.ReadUInt16();
        }
        else
        {
            TotalSize = reader.ReadInt32();
        }

        ReadElementType(context, reader);
    }

    protected override int GetElementCount()
    {
        return ElementSize > 0 ? TotalSize / ElementSize : 0;
    }

    public override int GetSize()
    {
        return TotalSize;
    }
}
#pragma warning restore 1591
