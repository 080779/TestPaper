using Chat.IService.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chat.DTO.DTO;

namespace Chat.Service.Service
{
    public class SettingService : ISettingService
    {
        public SettingDTO[] GetAll()
        {
            throw new NotImplementedException();
        }

        public bool? GetBoolValue(string name)
        {
            throw new NotImplementedException();
        }

        public int? GetIntValue(string name)
        {
            throw new NotImplementedException();
        }

        public string GetValue(string name)
        {
            throw new NotImplementedException();
        }

        public void SetBoolValue(string name, bool value)
        {
            throw new NotImplementedException();
        }

        public void SetIntValue(string name, int value)
        {
            throw new NotImplementedException();
        }

        public void SetValue(string name, string value)
        {
            throw new NotImplementedException();
        }
    }
}
