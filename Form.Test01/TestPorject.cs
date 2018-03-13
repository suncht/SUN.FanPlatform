using SF.Infrastructure;
using SF.Infrastructure.DataExchange;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Practices.Unity;
using System.IO;

namespace Form.Test01
{
    public class TestPorject
    {
        private string treeSvaeName = "tree.xml";
        private string prooertyName = "property.xml";
        private string logName = "log.txt";
        private string data = "data.xml";
        IInvokeMethod service = ServiceContainer.CreateInstance().Resolve<IInvokeMethod>("InvokeMethodService");
        public void Open(object o)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if(fbd.ShowDialog()==DialogResult.OK)
            {
                IDataExchange gvService = ServiceContainer.CreateInstance().Resolve<IDataExchange>("DataExchangeService");
                string selectPath = fbd.SelectedPath;
                string treepath = Path.Combine(selectPath, treeSvaeName);
                string propPath = Path.Combine(selectPath, prooertyName);
                string dataPath = Path.Combine(selectPath, data);
                if (File.Exists(treepath))
                {
                    service.Invoke("panel11", "LoadFromFile", new object[1] { treepath });
                }
                if (File.Exists(propPath))
                {
                    service.Invoke("panel12", "LoadFromFile", new object[1] { propPath });
                }
                if (File.Exists(dataPath))
                {
                    gvService.LoadFromFile(dataPath);
                }
                gvService["ProjectPath"] = fbd.SelectedPath;
            }
        }

        public void Save(object o)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                IDataExchange gvService = ServiceContainer.CreateInstance().Resolve<IDataExchange>("DataExchangeService");
                gvService["ProjectPath"] = fbd.SelectedPath;
                string treepath = Path.Combine(gvService["ProjectPath"], treeSvaeName);
                string propPath = Path.Combine(gvService["ProjectPath"], prooertyName);
                string dataPath = Path.Combine(gvService["ProjectPath"], data);
                
                    service.Invoke("panel11_SaveToFile", new object[1] { treepath });
                
                
                    service.Invoke("panel12_SaveToFile", new object[1] { propPath });
                
                    gvService.SaveToFile(dataPath);
                
                
            }
        }

        public void Exit(object o)
        {
            Application.Exit();
        }
    }
}
