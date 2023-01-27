using API.Data;
using API.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services
{
    public class ConfigurationSettingService : IConfigurationSettingService
    {
        private readonly StoreContext _context;
        public ConfigurationSettingService(StoreContext context) 
        {
            _context = context;
        }
        public async Task<ConfigurationSetting> GetConfigurationSettingValue(string key)
        {
            try
            {
                return await _context.ConfigurationSettings.FirstOrDefaultAsync(m => m.Key == key);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
