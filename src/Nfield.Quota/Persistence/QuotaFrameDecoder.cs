﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Nfield.Quota.Persistence
{
    public static class QuotaFrameDecoder
    {
        public static QuotaFrame Decode(string json)
        {
            var settings = new JsonSerializerSettings();
            settings.Converters.Add(new GuidJsonConverter());

            return JsonConvert.DeserializeObject<QuotaFrame>(json, settings);
        }
    }
}
