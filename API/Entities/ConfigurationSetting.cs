using System.Collections.Generic;

namespace API.Entities
{
    public class ConfigurationSetting
    {
        public int ConfigurationSettingId { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
    }
}