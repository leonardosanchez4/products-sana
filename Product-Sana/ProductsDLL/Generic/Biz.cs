using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsDLL
{
    public static class Biz
    {
        private const string AppSettingKeyDefault = "DBConnection";
        public static ProductRepository Product()
        {
            return new ProductRepository(AppSettingKeyDefault);
        }
    }
}
