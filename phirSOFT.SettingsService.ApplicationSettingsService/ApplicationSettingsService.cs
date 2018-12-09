using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace phirSOFT.SettingsService.Settings
{
    public class ApplicationSettingsService : ISettingsService
    {
        private readonly ApplicationSettingsBase _applicationSettings;

        public ApplicationSettingsService(ApplicationSettingsBase applicationSettings)
        {
            _applicationSettings = applicationSettings;
        }

        public Task<object> GetSettingAsync(string key, Type type)
        {
            Debug.Assert(type.IsInstanceOfType(_applicationSettings[key]));
            return Task.FromResult(_applicationSettings[key]);
        }

        public Task<bool> IsRegisterdAsync(string key)
        {
            return Task.FromResult(_applicationSettings.Context.ContainsKey(key));
        }

        public Task SetSettingAsync(string key, object value, Type type)
        {
            _applicationSettings[key] = value;
            return Task.CompletedTask;
        }

        public Task RegisterSettingAsync(string key, object defaultValue, object initialValue, Type type)
        {
            return Task.FromException(new NotSupportedException());
        }

        public Task UnregisterSettingAsync(string key)
        {
            return Task.FromException(new NotSupportedException());
        }

        public Task StoreAsync()
        {
            _applicationSettings.Save();
            _applicationSettings.Reload();
            return Task.CompletedTask;
        }

        public Task DiscardAsync()
        {
            _applicationSettings.Reload();
            return Task.CompletedTask;
        }
    }
}
