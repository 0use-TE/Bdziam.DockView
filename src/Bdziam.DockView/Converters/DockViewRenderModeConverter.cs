// Copyright (c) Bdziam. All rights reserved.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Bdziam.DockView.Components;

namespace Bdziam.DockView.Converters
{
    /// <summary>
    /// Custom JSON converter for DockViewContentType that ensures values are serialized as lowercase strings
    /// </summary>
    public class DockViewRenderModeConverter : JsonConverter<DockViewRenderMode>
    {
        /// <summary>
        /// Reads and converts JSON to DockViewContentType
        /// </summary>
        public override DockViewRenderMode Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.String)
            {
                throw new JsonException($"Expected string value for {nameof(DockViewRenderMode)}");
            }

            var value = reader.GetString();
            if (string.IsNullOrEmpty(value))
            {
                return DockViewRenderMode.Always; // Default value
            }

            // Parse the enum value ignoring case
            return Enum.Parse<DockViewRenderMode>(value, true);
        }

        /// <summary>
        /// Converts DockViewContentType to JSON as lowercase string
        /// </summary>
        public override void Write(Utf8JsonWriter writer, DockViewRenderMode value, JsonSerializerOptions options)
        {
            // Write the enum value as lowercase string
            writer.WriteStringValue(value.ToString().ToLowerInvariant());
        }
    }
}
