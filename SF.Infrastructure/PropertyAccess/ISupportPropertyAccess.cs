using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SF.Infrastructure.PropertyAccess
{
    /// <summary>
    /// 数据访问对象必须实现此接口,并向服务注册
    /// </summary>
    public interface ISupportPropertyAccess
    {
        /// <summary>
        /// 当前对象的字符串名描述，作为全局访问属性的前缀
        /// </summary>
        string AccessName { get; }

        
        /// <summary>
        /// key:属性访问键，与[Name]用_相连作为完整访问键
        /// value:真实要访问的属性名
        /// </summary>
        Dictionary<string,string> AllowAccessPropertiesDic{get;}


    }
}
