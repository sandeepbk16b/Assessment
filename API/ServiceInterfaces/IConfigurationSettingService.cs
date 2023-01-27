using System.Threading.Tasks;
using System;
using API.Entities;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace API.Services
{
    public interface IConfigurationSettingService
    {
        Task<ConfigurationSetting> GetConfigurationSettingValue(string key);
    }
}
